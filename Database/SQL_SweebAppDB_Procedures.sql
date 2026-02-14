USE SweebDataBase;
GO;

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

CREATE PROCEDURE addRuleHit
	@RuleId INT,
	@ThreatEventId INT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.RuleHits (RuleId , ThreatEventId) VALUES (@RuleId , @ThreatEventId)
END
GO;

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

