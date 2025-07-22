IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Art] (
    [Id] int NOT NULL IDENTITY,
    [Title] varchar(200) NULL,
    [Description] varchar(max) NULL,
    [path] varchar(500) NULL,
    [CreatedAt] datetime NOT NULL DEFAULT ((getdate())),
    [UploadedBy] varchar(100) NULL,
    [UploaderName] nvarchar(255) NULL,
    CONSTRAINT [PK__Art__3214EC07067B08CB] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [compitition] (
    [id] int NOT NULL IDENTITY,
    [Title] varchar(255) NULL,
    [Theme] varchar(255) NULL,
    [Description] varchar(255) NULL,
    [SubmitDate] varchar(255) NULL,
    CONSTRAINT [PK__compitit__3213E83F9979B6BF] PRIMARY KEY ([id])
);
GO

CREATE TABLE [Register] (
    [id] int NOT NULL IDENTITY,
    [name] varchar(255) NULL,
    [email] varchar(255) NULL,
    [password] varchar(255) NULL,
    [c_password] varchar(255) NULL,
    [phone] varchar(255) NULL,
    [artist] varchar(255) NULL,
    CONSTRAINT [PK__Register__3213E83F1B5B9CD6] PRIMARY KEY ([id])
);
GO

CREATE TABLE [Remarks] (
    [Id] int NOT NULL IDENTITY,
    [ArtId] int NOT NULL,
    [TeacherName] nvarchar(100) NULL,
    [Comment] nvarchar(max) NULL,
    [CreatedAt] datetime NOT NULL DEFAULT ((getdate())),
    CONSTRAINT [PK__Remarks__3214EC07C68988BE] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__Remarks__ArtId__07C12930] FOREIGN KEY ([ArtId]) REFERENCES [Art] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Remarks_ArtId] ON [Remarks] ([ArtId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250717102148_InitialCreate', N'8.0.18');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[compitition]') AND [c].[name] = N'Title');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [compitition] DROP CONSTRAINT [' + @var0 + '];');
UPDATE [compitition] SET [Title] = '' WHERE [Title] IS NULL;
ALTER TABLE [compitition] ALTER COLUMN [Title] varchar(255) NOT NULL;
ALTER TABLE [compitition] ADD DEFAULT '' FOR [Title];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[compitition]') AND [c].[name] = N'Theme');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [compitition] DROP CONSTRAINT [' + @var1 + '];');
UPDATE [compitition] SET [Theme] = '' WHERE [Theme] IS NULL;
ALTER TABLE [compitition] ALTER COLUMN [Theme] varchar(255) NOT NULL;
ALTER TABLE [compitition] ADD DEFAULT '' FOR [Theme];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[compitition]') AND [c].[name] = N'SubmitDate');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [compitition] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [compitition] ALTER COLUMN [SubmitDate] datetime2 NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[compitition]') AND [c].[name] = N'Description');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [compitition] DROP CONSTRAINT [' + @var3 + '];');
UPDATE [compitition] SET [Description] = '' WHERE [Description] IS NULL;
ALTER TABLE [compitition] ALTER COLUMN [Description] varchar(255) NOT NULL;
ALTER TABLE [compitition] ADD DEFAULT '' FOR [Description];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250717104944_Compitition', N'8.0.18');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [C_Art] (
    [id] int NOT NULL IDENTITY,
    [title] nvarchar(255) NOT NULL,
    [description] nvarchar(255) NOT NULL,
    [uploadname] nvarchar(255) NOT NULL,
    [uploademal] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_C_Art] PRIMARY KEY ([id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250717132808_AddCArtTable', N'8.0.18');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[C_Art].[uploadname]', N'UploadName', N'COLUMN';
GO

EXEC sp_rename N'[C_Art].[uploademal]', N'UploadEmal', N'COLUMN';
GO

EXEC sp_rename N'[C_Art].[title]', N'Title', N'COLUMN';
GO

EXEC sp_rename N'[C_Art].[description]', N'Description', N'COLUMN';
GO

EXEC sp_rename N'[C_Art].[id]', N'Id', N'COLUMN';
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[C_Art]') AND [c].[name] = N'UploadName');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [C_Art] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [C_Art] ALTER COLUMN [UploadName] nvarchar(max) NOT NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[C_Art]') AND [c].[name] = N'UploadEmal');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [C_Art] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [C_Art] ALTER COLUMN [UploadEmal] nvarchar(max) NOT NULL;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[C_Art]') AND [c].[name] = N'Title');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [C_Art] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [C_Art] ALTER COLUMN [Title] nvarchar(max) NOT NULL;
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[C_Art]') AND [c].[name] = N'Description');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [C_Art] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [C_Art] ALTER COLUMN [Description] nvarchar(max) NOT NULL;
GO

ALTER TABLE [C_Art] ADD [CompetitionId] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [C_Art] ADD [ImagePath] nvarchar(max) NOT NULL DEFAULT N'';
GO

CREATE TABLE [Awards] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [StudentName] nvarchar(max) NOT NULL,
    [Position] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Awards] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250717160134_CreateAwardTable', N'8.0.18');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[C_Art]') AND [c].[name] = N'CompetitionId');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [C_Art] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [C_Art] DROP COLUMN [CompetitionId];
GO

CREATE TABLE [Teachers] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [Password] nvarchar(max) NULL,
    [CPassword] nvarchar(max) NULL,
    [Phone] nvarchar(max) NULL,
    [Role] nvarchar(max) NULL,
    CONSTRAINT [PK_Teachers] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250719222315_AddTeacherTable', N'8.0.18');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Managers] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [Password] nvarchar(max) NULL,
    [CPassword] nvarchar(max) NULL,
    [Phone] nvarchar(max) NULL,
    [Role] nvarchar(max) NULL,
    CONSTRAINT [PK_Managers] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250719222953_AddManagerTable', N'8.0.18');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Admins] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [Password] nvarchar(max) NULL,
    [CPassword] nvarchar(max) NULL,
    [Phone] nvarchar(max) NULL,
    [Role] nvarchar(max) NULL,
    CONSTRAINT [PK_Admins] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250719223129_AddAdminTable', N'8.0.18');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Register] ADD [CreatedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250721143744_AddCreatedDateToRegister', N'8.0.18');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Admins] ADD [CreatedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250721143958_AddCreatedDateToAdmins', N'8.0.18');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Teachers] ADD [CreatedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250721144124_AddCreatedDateToTeachers', N'8.0.18');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Managers] ADD [CreatedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250721144253_AddCreatedDateToManagers', N'8.0.18');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Exhibitions] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(255) NOT NULL,
    [Pic] nvarchar(255) NOT NULL,
    [Starts] datetime2 NOT NULL,
    [Ends] datetime2 NOT NULL,
    CONSTRAINT [PK_Exhibitions] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250721161109_CreateExhibitionTable', N'8.0.18');
GO

COMMIT;
GO

