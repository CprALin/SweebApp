# Stored Procedures

This section documents the stored procedures used by the application.
Stored procedures are responsible for **data creation**, **traceability**, and
**audit support**, while higher-level business logic is handled in the backend.

The procedures follow these principles:
- keep database logic **simple and predictable**
- rely on database constraints for data integrity
- delegate error handling and flow control to the backend
- support auditability and explainability

---

## 1) `registerUser`

### Purpose
Creates a new user account and initializes default user settings.
The operation is atomic and executed inside a transaction.

### Parameters
- **`@Username`** *(NVARCHAR(255))*  
  Unique username.
- **`@Email`** *(NVARCHAR(255))*  
  Unique email address.
- **`@PasswordHash`** *(NVARCHAR(255))*  
  Secure hash of the user password.
- **`@Success`** *(INT, OUTPUT)*  
  `1` = success, `0` = failure.

### Behavior
1. Inserts a new record into `User_Info`
2. Retrieves the generated `IdUser` using `SCOPE_IDENTITY()`
3. Inserts default preferences into `UserSettings`
4. Commits the transaction on success
5. Rolls back and returns failure on error

### Side Effects
- Writes to `User_Info`
- Writes to `UserSettings`

---

## 2) `loginUser`

### Purpose
Validates user credentials and returns the associated user ID.

### Parameters
- **`@Username`** *(NVARCHAR(255))*  
  Username or email.
- **`@PasswordHash`** *(NVARCHAR(255))*  
  Password hash.
- **`@Success`** *(INT, OUTPUT)*  
  `1` = valid credentials, `0` = invalid.
- **`@UserId`** *(INT, OUTPUT)*  
  User identifier if authentication succeeds.

### Behavior
- Searches for a matching user using `(Username OR Email) + PasswordHash`
- Returns success flag and user ID if found

### Side Effects
- None (read-only)

---

## 3) `addDevice`

### Purpose
Registers a device associated with a user for **traceability purposes**.
Used to identify the origin of detected traffic and alerts.

### Parameters
- **`@Name`** *(NVARCHAR(255))*  
  Device name or hostname.
- **`@OS`** *(NVARCHAR(255))*  
  Operating system information.
- **`@UserId`** *(INT)*  
  Reference to the owning user.

### Behavior
- Inserts a device record only if a device with the same `(Name, UserId)` does not already exist

### Side Effects
- Writes to `Devices`

### Notes
This procedure is intended for **lightweight device tracing**, not trusted-device security.

---

## 4) `addRule`

### Purpose
Adds a user-defined security rule used by the policy engine.

### Parameters
- **`@UserId`** *(INT)*  
  Rule owner.
- **`@RuleName`** *(NVARCHAR(255))*  
  Rule name.
- **`@IsEnabled`** *(BIT)*  
  Rule enabled state.
- **`@Priority`** *(INT)*  
  Evaluation priority.
- **`@Action`** *(NVARCHAR(255))*  
  Action to apply (`Allow`, `Warn`, `Block`).
- **`@MatchType`** *(NVARCHAR(50))*  
  Type of match (Domain, URL, Regex, etc.).
- **`@Pattern`** *(NVARCHAR(255))*  
  Matching pattern.

### Behavior
- Inserts a new rule only if a rule with the same `(Name, UserId)` does not already exist

### Side Effects
- Writes to `Rules`

---

## 5) `addThreatEvent`

### Purpose
Stores a detected **suspicious or malicious network request**.
This is the core data source for detection, alerting, and auditing.

### Parameters
- **`@URL`** *(NVARCHAR(2048))*  
  Full request URL.
- **`@Protocol`** *(NVARCHAR(10))*  
  Network protocol.
- **`@Host`** *(NVARCHAR(255))*  
  Destination host.
- **`@Path`** *(NVARCHAR(255))*  
  Request path.
- **`@Status`** *(NVARCHAR(50))*  
  Detection status.
- **`@Verdict`** *(NVARCHAR(50))*  
  Classification verdict.
- **`@ActionTaken`** *(NVARCHAR(255))*  
  Enforcement action.
- **`@Score`** *(INT)*  
  Risk score.
- **`@Category`** *(NVARCHAR(255))*  
  Threat category.
- **`@DeviceId`** *(INT)*  
  Source device.

### Behavior
- Inserts a new threat event only if a similar event does not already exist for the same device

### Side Effects
- Writes to `ThreatEvents`

---

## 6) `addRuleHit`

### Purpose
Records an **audit entry** indicating that a rule was matched for a threat event.

### Parameters
- **`@RuleId`** *(INT)*  
  Applied rule.
- **`@ThreatEventId`** *(INT)*  
  Affected threat event.

### Behavior
- Inserts a record into `RuleHits`

### Side Effects
- Writes to `RuleHits`

---

## 7) `addDetectionReason`

### Purpose
Stores explainability signals describing *why* a threat event was detected.

### Parameters
- **`@ReasonCode`** *(NVARCHAR(50))*  
  Detection signal identifier.
- **`@Weight`** *(INT)*  
  Contribution weight.
- **`@Details`** *(NVARCHAR(255))*  
  Structured explanation.
- **`@ThreatEventId`** *(INT)*  
  Associated threat event.

### Behavior
- Inserts a detection reason linked to a threat event

### Side Effects
- Writes to `DetectionReasons`

---

## 8) `addAlert`

### Purpose
Creates a user-facing alert generated from a threat event.

### Parameters
- **`@UserId`** *(INT)*  
  Target user.
- **`@DeviceId`** *(INT)*  
  Device where the threat occurred.
- **`@ThreatEventId`** *(INT)*  
  Source threat event.
- **`@Severity`** *(NVARCHAR(50))*  
  Alert severity.
- **`@IsRead`** *(BIT)*  
  Read status.

### Behavior
- Inserts a new alert record

### Side Effects
- Writes to `Alerts`

---

## Design Notes

- Stored procedures are intentionally **simple and deterministic**
- Validation, retries, and error handling are performed in the backend
- Database constraints enforce consistency
- Auditability is achieved through `ThreatEvents`, `RuleHits`, and `DetectionReasons`
