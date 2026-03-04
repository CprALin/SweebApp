USE SweebDataBase;
GO;

/*
================================================
  USERS
================================================
*/
DROP PROCEDURE registerUser;
CREATE PROCEDURE registerUser
    @Username NVARCHAR(255),
	@Email NVARCHAR(255),
	@PasswordHash NVARCHAR(255),
	@Success INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

	BEGIN TRY
	    BEGIN TRANSACTION;
		
		INSERT INTO dbo.User_Info (Username , Email , PasswordHash ) 
		VALUES (@Username , @Email , @PasswordHash);
	
		DECLARE @UserId INT = CAST(SCOPE_IDENTITY() AS INT);
		
		INSERT INTO dbo.UserSettings (UserId) VALUES (@UserId);

		COMMIT;
		SET @Success = 1;
	END TRY
	BEGIN CATCH
	    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        SET @Success = 0;
	END CATCH
END
GO;


DROP PROCEDURE loginUser;
CREATE PROCEDURE loginUser
    @Username NVARCHAR(255)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT TOP 1 IdUser , PasswordHash 
	FROM dbo.User_Info
	WHERE Username = @Username OR Email = @Username;
END
GO;


DROP PROCEDURE getUserById;
CREATE PROCEDURE getUserById
	@UserId INT
AS 
BEGIN 
	SET NOCOUNT ON;
    
	IF NOT EXISTS (
	 SELECT 1 FROM User_Info WHERE IdUser = @UserId
    )
	BEGIN 
		RAISERROR('This user can`t be found !' , 16, 1);
		RETURN;
	END

	SELECT * FROM User_Info WHERE IdUser = @UserId;
END
GO;


CREATE PROCEDURE updateUserEmailById
	@UserId INT,
	@NewEmail NVARCHAR(255),
	@Success INT OUTPUT 
AS
BEGIN 
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM dbo.User_Info WHERE Email = @NewEmail)
	BEGIN 
		SET @Success = 0;
		RETURN;
	END

	BEGIN TRY 
		UPDATE dbo.User_Info 
		SET Email = @NewEmail 
		WHERE IdUser = @UserId;

		SET @Success = 1;
	END TRY
	BEGIN CATCH
		SET @Success = 0;
	END CATCH 
END
GO;



/*
================================================
  USERS SETTINGS
================================================
*/

CREATE PROCEDURE getSettings
   @UserId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM UserSettings
	WHERE UserId = @UserId
END
GO;


CREATE PROCEDURE updateAllwaysOnTop
	@IdSettings INT,
	@AllwaysOnTop BIT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.UserSettings 
	SET AllwaysOnTop = @AllwaysOnTop
	WHERE IdSettings = @IdSettings
    
END
GO;

CREATE PROCEDURE updateAllowNotifications
	@IdSettings INT,
	@AllowNotifications BIT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.UserSettings 
	SET AllowNotifications = @AllowNotifications
	WHERE IdSettings = @IdSettings
    
END
GO;


CREATE PROCEDURE updateTheme
	@IdSettings INT,
	@Theme NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.UserSettings 
	SET Theme = @Theme
	WHERE IdSettings = @IdSettings
    
END
GO;

CREATE PROCEDURE updateRunAtStartup
	@IdSettings INT,
	@RunAtStartup BIT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.UserSettings 
	SET RunAtStartup = @RunAtStartup
	WHERE IdSettings = @IdSettings
    
END
GO;

/*
================================================
  DEVICES
================================================
*/

CREATE PROCEDURE addDevice
	@Name NVARCHAR(255),
	@OS NVARCHAR(255),
	@UserId INT
AS
BEGIN 
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.Devices WHERE Name = @Name AND UserId = @UserId)
	BEGIN
	   INSERT INTO dbo.Devices (Name , OS, UserId) VALUES (@Name , @OS, @UserId)
	END
END
GO;

CREATE PROCEDURE getDevicesForUser
	@UserId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM dbo.Devices 
	WHERE UserId = @UserId
END
GO;

CREATE PROCEDURE deleteDeviceById
	@IdDevice INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM dbo.Devices 
	WHERE IdDevice = @IdDevice
END
GO;
	

/*
================================================
  RULES
================================================
*/

CREATE PROCEDURE addRule
	@UserId INT,
	@RuleName NVARCHAR(255),
	@IsEnabled BIT,
	@Priority INT,
	@Action NVARCHAR(255),
	@MatchType NVARCHAR(50),
	@Pattern NVARCHAR(255)
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT 1 FROM dbo.Rules WHERE Name = @RuleName AND UserId = @UserId)
	BEGIN
		INSERT INTO dbo.Rules (UserId , Name , IsEnabled , Priority , Action , MatchType , Pattern ) 
		VALUES (@UserId , @RuleName , @IsEnabled , @Priority , @Action , @MatchType , @Pattern)
	END
END
GO;

CREATE PROCEDURE getUserRules 
   @UserId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM dbo.Rules 
	WHERE UserId = @UserId
END
GO;

CREATE PROCEDURE deleteRuleById
	@IdRule INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM dbo.Rules 
	WHERE IdRule = @IdRule
END
GO;

/*
================================================
  THREAT EVENTS
================================================
*/
CREATE PROCEDURE addThreatEvent 
	@URL NVARCHAR(2048),
	@Protocol NVARCHAR(10),
	@Host NVARCHAR(255),
	@Path NVARCHAR(255),
	@Status NVARCHAR(50),
	@Verdict NVARCHAR(50),
	@ActionTaken NVARCHAR(255),
	@Score INT,
	@Category NVARCHAR(255),
	@DeviceId INT
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT 1 FROM dbo.ThreatEvents WHERE URL = @URL AND DeviceId = @DeviceId AND Status = @Status AND Verdict = @Verdict)
	BEGIN
		INSERT INTO dbo.ThreatEvents (URL , Protocol , Host , Path , Status , Verdict , ActionTaken , Score , Category , DeviceId) 
		VALUES (@URL , @Protocol , @Host , @Path , @Status , @Verdict , @ActionTaken , @Score , @Category, @DeviceId)
	END
END
GO;

CREATE PROCEDURE getThreatEventsForDevice
	@DeviceId INT
AS
BEGIN 
	SET NOCOUNT ON;

	SELECT * FROM dbo.ThreatEvents
	WHERE DeviceID = @DeviceId
END
GO;



/*
================================================
  DETECTION REASONS
================================================
*/

CREATE PROCEDURE addDetectionReason
	@ReasonCode NVARCHAR(50),
	@Weight INT,
	@Details NVARCHAR(255),
	@ThreatEventId INT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO  dbo.DetectionReasons (ReasonCode , Weight , Details , ThreatEventId) 
	VALUES (@ReasonCode , @Weight , @Details , @ThreatEventId)
END
GO;

CREATE PROCEDURE getDetectionReason
	@ThreatEventId INT 
AS
BEGIN 
	SET NOCOUNT ON;

	SELECT * FROM dbo.DetectionReasons
	WHERE ThreatEventId = @ThreatEventId
END
GO;

/*
================================================
  ALERTS
================================================
*/

CREATE PROCEDURE addAlert
	@UserId INT,
	@DeviceId INT,
	@ThreatEventId INT,
	@Severity NVARCHAR(50),
    @IsRead BIT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.Alerts (UserId , DeviceId , ThreatEventId , Severity , IsRead)
	VALUES (@UserId , @DeviceId , @ThreatEventId , @Severity , @IsRead)
END
GO;

CREATE PROCEDURE getHotAlert 
	@UserId INT,
	@DeviceId INT,
	@ThreatEventId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 1 FROM dbo.Alerts
	WHERE UserId = @UserId AND DeviceId = @DeviceId AND ThreatEventId = @ThreatEventId
END
GO;

CREATE PROCEDURE getAllAlertsForDevice
	@UserId INT,
	@DeviceId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM dbo.Alerts
	WHERE UserId = @UserId AND DeviceId = @DeviceId
END
GO;



/*
================================================
  RULE HITS
================================================
*/

CREATE PROCEDURE addRuleHit
	@RuleId INT,
	@ThreatEventId INT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.RuleHits (RuleId , ThreatEventId) VALUES (@RuleId , @ThreatEventId)
END
GO;


CREATE PROCEDURE getRulesHit
	@ThreatEventId INT 
AS
BEGIN 
	SET NOCOUNT ON;

	SELECT * FROM dbo.RuleHits
	WHERE ThreatEventId = @ThreatEventId
END
GO;


/*
================================================
  USEFUL VIEWS - END USER
================================================
*/

/*
   Alerts details used for : User alerts history , 
   Device alerts history , Last 20 alerts for user DASHBOARD
*/
CREATE OR ALTER VIEW dbo.vw_AlertsFeed
AS
SELECT 
	a.IdAlert,
	a.UserId,
	a.DeviceId,
	d.Name as DeviceName,
	a.ThreatEventId,
	te.URL,
	te.Host,
	te.Path,
	te.Status,
	te.Verdict,
	a.Severity,
	a.IsRead,
	a.CreatedAt
FROM dbo.Alerts a
JOIN dbo.Devices d ON d.IdDevice = a.DeviceId
JOIN dbo.ThreatEvents te ON te.IdThreatEvent = a.ThreatEventId;
GO;


/*
   Threat events for : User History , Threat Events for Device , Last 20 threats for user DASHBOARD
*/
CREATE OR ALTER VIEW dbo.vw_ThreatEventsWithDevice
AS
SELECT 
	te.IdThreatEvent,
	d.UserId,
	te.DeviceID as DeviceId,
	d.Name as DeviceName,
	te.URL,
	te.Protocol,
	te.Host,
	te.Path,
	te.Status,
	te.Verdict,
	te.ActionTaken,
	te.Score,
	te.Category,
	te.Timestamp
FROM dbo.ThreatEvents te 
JOIN dbo.Devices d ON d.IdDevice = te.DeviceID;
GO;

/*
   Rules activity for : User , Just one Rule , Device
*/
CREATE OR ALTER VIEW dbo.vw_RuleHitsActivity
AS
SELECT 
	rh.IdRuleHit,
	rh.Timestamp AS RuleHitTimestamp,
	r.UserId,
	rh.RuleId,
	r.Name AS RuleName,
	r.Action,
	rh.ThreatEventId,
	te.URL,
	te.Host,
	te.Status,
	d.IdDevice AS DeviceId,
	d.Name AS DeviceName,
	te.Timestamp AS ThreatTimestamp
FROM dbo.RuleHits rh 
JOIN dbo.Rules r ON r.IdRule = rh.RuleId 
JOIN dbo.ThreatEvents te ON te.IdThreatEvent = rh.ThreatEventId
JOIN dbo.Devices d ON d.IdDevice = te.DeviceID;
GO;



