-- Migrate EF tables to .net core

ALTER TABLE	dbo.AspNetUsers DROP COLUMN LockoutEndDateUtc
GO
SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL Serializable
GO
BEGIN TRANSACTION
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Dropping foreign keys from [dbo].[AspNetUserClaims]'
GO
ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Dropping foreign keys from [dbo].[AspNetUserLogins]'
GO
ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Dropping foreign keys from [dbo].[AspNetUserRoles]'
GO
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Dropping constraints from [dbo].[AspNetRoles]'
GO
ALTER TABLE [dbo].[AspNetRoles] DROP CONSTRAINT [PK_dbo.AspNetRoles]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Dropping constraints from [dbo].[AspNetUserClaims]'
GO
ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [PK_dbo.AspNetUserClaims]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Dropping constraints from [dbo].[AspNetUserLogins]'
GO
ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [PK_dbo.AspNetUserLogins]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Dropping constraints from [dbo].[AspNetUserRoles]'
GO
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [PK_dbo.AspNetUserRoles]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Dropping constraints from [dbo].[AspNetUsers]'
GO
ALTER TABLE [dbo].[AspNetUsers] DROP CONSTRAINT [PK_dbo.AspNetUsers]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Altering [dbo].[AspNetRoles]'
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
ALTER TABLE [dbo].[AspNetRoles] ADD
[NormalizedName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ConcurrencyStamp] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
ALTER TABLE [dbo].[AspNetRoles] ALTER COLUMN [Id] [nvarchar] (450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
ALTER TABLE [dbo].[AspNetRoles] ALTER COLUMN [Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_AspNetRoles] on [dbo].[AspNetRoles]'
GO
ALTER TABLE [dbo].[AspNetRoles] ADD CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [RoleNameIndex] on [dbo].[AspNetRoles]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles] ([NormalizedName]) WHERE ([NormalizedName] IS NOT NULL)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Altering [dbo].[AspNetUsers]'
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
ALTER TABLE [dbo].[AspNetUsers] ADD
[NormalizedUserName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[NormalizedEmail] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ConcurrencyStamp] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LockoutEnd] [datetimeoffset] NULL
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
ALTER TABLE [dbo].[AspNetUsers] DROP
COLUMN [nickname]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
ALTER TABLE [dbo].[AspNetUsers] ALTER COLUMN [Id] [nvarchar] (450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
ALTER TABLE [dbo].[AspNetUsers] ALTER COLUMN [UserName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
ALTER TABLE [dbo].[AspNetUsers] ALTER COLUMN [FirstName] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
ALTER TABLE [dbo].[AspNetUsers] ALTER COLUMN [LastName] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
ALTER TABLE [dbo].[AspNetUsers] ALTER COLUMN [Address] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
ALTER TABLE [dbo].[AspNetUsers] ADD [LockoutEndDateUtc] [datetime2] NOT NULL DEFAULT '01/01/2010'
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
ALTER TABLE [dbo].[AspNetUsers] ALTER COLUMN [City] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
ALTER TABLE [dbo].[AspNetUsers] ALTER COLUMN [State] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
ALTER TABLE [dbo].[AspNetUsers] ALTER COLUMN [PostalCode] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_AspNetUsers] on [dbo].[AspNetUsers]'
GO
ALTER TABLE [dbo].[AspNetUsers] ADD CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [EmailIndex] on [dbo].[AspNetUsers]'
GO
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers] ([NormalizedEmail])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [UserNameIndex] on [dbo].[AspNetUsers]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers] ([NormalizedUserName]) WHERE ([NormalizedUserName] IS NOT NULL)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Altering [dbo].[AspNetUserClaims]'
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
ALTER TABLE [dbo].[AspNetUserClaims] ALTER COLUMN [UserId] [nvarchar] (450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_AspNetUserClaims] on [dbo].[AspNetUserClaims]'
GO
ALTER TABLE [dbo].[AspNetUserClaims] ADD CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_AspNetUserClaims_UserId] on [dbo].[AspNetUserClaims]'
GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims] ([UserId])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Altering [dbo].[AspNetUserLogins]'
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
ALTER TABLE [dbo].[AspNetUserLogins] ADD
[ProviderDisplayName] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
ALTER TABLE [dbo].[AspNetUserLogins] ALTER COLUMN [UserId] [nvarchar] (450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_AspNetUserLogins] on [dbo].[AspNetUserLogins]'
GO
ALTER TABLE [dbo].[AspNetUserLogins] ADD CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED  ([LoginProvider], [ProviderKey])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_AspNetUserLogins_UserId] on [dbo].[AspNetUserLogins]'
GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins] ([UserId])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Altering [dbo].[AspNetUserRoles]'
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
ALTER TABLE [dbo].[AspNetUserRoles] ALTER COLUMN [UserId] [nvarchar] (450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
ALTER TABLE [dbo].[AspNetUserRoles] ALTER COLUMN [RoleId] [nvarchar] (450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_AspNetUserRoles] on [dbo].[AspNetUserRoles]'
GO
ALTER TABLE [dbo].[AspNetUserRoles] ADD CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED  ([UserId], [RoleId])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_AspNetUserRoles_RoleId] on [dbo].[AspNetUserRoles]'
GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles] ([RoleId])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[AspNetUserRoles]'
GO
ALTER TABLE [dbo].[AspNetUserRoles] ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[AspNetUserClaims]'
GO
ALTER TABLE [dbo].[AspNetUserClaims] ADD CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[AspNetUserLogins]'
GO
ALTER TABLE [dbo].[AspNetUserLogins] ADD CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
COMMIT TRANSACTION
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
-- This statement writes to the SQL Server Log so SQL Monitor can show this deployment.
IF HAS_PERMS_BY_NAME(N'sys.xp_logevent', N'OBJECT', N'EXECUTE') = 1
BEGIN
    DECLARE @databaseName AS nvarchar(2048), @eventMessage AS nvarchar(2048)
    SET @databaseName = REPLACE(REPLACE(DB_NAME(), N'\', N'\\'), N'"', N'\"')
    SET @eventMessage = N'Redgate SQL Compare: { "deployment": { "description": "Redgate SQL Compare deployed to ' + @databaseName + N'", "database": "' + @databaseName + N'" }}'
    EXECUTE sys.xp_logevent 55000, @eventMessage
END
GO
DECLARE @Success AS BIT
SET @Success = 1
SET NOEXEC OFF
IF (@Success = 1) PRINT 'The database update succeeded'
ELSE BEGIN
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
	PRINT 'The database update failed'
END
GO

UPDATE dbo.AspNetUsers SET NormalizedEmail = UPPER(Email);
UPDATE dbo.AspNetUsers SET NormalizedUserName = UPPER(Email);
GO

UPDATE dbo.AspNetRoles SET	NormalizedName = UPPER(Name)
GO