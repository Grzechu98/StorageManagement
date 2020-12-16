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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201125105234_InitCreate')
BEGIN
    CREATE TABLE [Contractors] (
        [Id] int NOT NULL IDENTITY,
        [NIP] nvarchar(max) NULL,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_Contractors] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201125105234_InitCreate')
BEGIN
    CREATE TABLE [Products] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Value] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201125105234_InitCreate')
BEGIN
    CREATE TABLE [Warehouses] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [City] nvarchar(max) NOT NULL,
        [Street] nvarchar(max) NOT NULL,
        [UnitNumber] nvarchar(max) NOT NULL,
        [PostCode] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Warehouses] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201125105234_InitCreate')
BEGIN
    CREATE TABLE [StorageRacks] (
        [Id] int NOT NULL IDENTITY,
        [RackNumber] nvarchar(max) NULL,
        [IsTaken] bit NOT NULL,
        [ContractorId] int NULL,
        [WarehouseId] int NOT NULL,
        CONSTRAINT [PK_StorageRacks] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_StorageRacks_Contractors_ContractorId] FOREIGN KEY ([ContractorId]) REFERENCES [Contractors] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_StorageRacks_Warehouses_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [Warehouses] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201125105234_InitCreate')
BEGIN
    CREATE TABLE [Shelves] (
        [Id] int NOT NULL IDENTITY,
        [ShelfNumber] nvarchar(max) NULL,
        [Quantity] int NOT NULL,
        [RackId] int NOT NULL,
        [ProductId] int NOT NULL,
        CONSTRAINT [PK_Shelves] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Shelves_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Shelves_StorageRacks_RackId] FOREIGN KEY ([RackId]) REFERENCES [StorageRacks] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201125105234_InitCreate')
BEGIN
    CREATE UNIQUE INDEX [IX_Shelves_ProductId] ON [Shelves] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201125105234_InitCreate')
BEGIN
    CREATE INDEX [IX_Shelves_RackId] ON [Shelves] ([RackId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201125105234_InitCreate')
BEGIN
    CREATE INDEX [IX_StorageRacks_ContractorId] ON [StorageRacks] ([ContractorId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201125105234_InitCreate')
BEGIN
    CREATE INDEX [IX_StorageRacks_WarehouseId] ON [StorageRacks] ([WarehouseId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201125105234_InitCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201125105234_InitCreate', N'5.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202150457_testmig')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201202150457_testmig', N'5.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202153433_testmig1')
BEGIN
    ALTER TABLE [StorageRacks] DROP CONSTRAINT [FK_StorageRacks_Warehouses_WarehouseId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202153433_testmig1')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[StorageRacks]') AND [c].[name] = N'WarehouseId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [StorageRacks] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [StorageRacks] ALTER COLUMN [WarehouseId] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202153433_testmig1')
BEGIN
    ALTER TABLE [StorageRacks] ADD CONSTRAINT [FK_StorageRacks_Warehouses_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [Warehouses] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202153433_testmig1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201202153433_testmig1', N'5.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202153747_testmig2')
BEGIN
    ALTER TABLE [StorageRacks] DROP CONSTRAINT [FK_StorageRacks_Warehouses_WarehouseId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202153747_testmig2')
BEGIN
    DROP INDEX [IX_StorageRacks_WarehouseId] ON [StorageRacks];
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[StorageRacks]') AND [c].[name] = N'WarehouseId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [StorageRacks] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [StorageRacks] ALTER COLUMN [WarehouseId] int NOT NULL;
    ALTER TABLE [StorageRacks] ADD DEFAULT 0 FOR [WarehouseId];
    CREATE INDEX [IX_StorageRacks_WarehouseId] ON [StorageRacks] ([WarehouseId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202153747_testmig2')
BEGIN
    ALTER TABLE [StorageRacks] ADD CONSTRAINT [FK_StorageRacks_Warehouses_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [Warehouses] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202153747_testmig2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201202153747_testmig2', N'5.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202161133_testmig3')
BEGIN
    ALTER TABLE [StorageRacks] DROP CONSTRAINT [FK_StorageRacks_Warehouses_WarehouseId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202161133_testmig3')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[StorageRacks]') AND [c].[name] = N'WarehouseId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [StorageRacks] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [StorageRacks] ALTER COLUMN [WarehouseId] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202161133_testmig3')
BEGIN
    ALTER TABLE [StorageRacks] ADD CONSTRAINT [FK_StorageRacks_Warehouses_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [Warehouses] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202161133_testmig3')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201202161133_testmig3', N'5.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202163533_testmig4')
BEGIN
    ALTER TABLE [StorageRacks] DROP CONSTRAINT [FK_StorageRacks_Warehouses_WarehouseId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202163533_testmig4')
BEGIN
    DROP INDEX [IX_StorageRacks_WarehouseId] ON [StorageRacks];
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[StorageRacks]') AND [c].[name] = N'WarehouseId');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [StorageRacks] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [StorageRacks] ALTER COLUMN [WarehouseId] int NOT NULL;
    ALTER TABLE [StorageRacks] ADD DEFAULT 0 FOR [WarehouseId];
    CREATE INDEX [IX_StorageRacks_WarehouseId] ON [StorageRacks] ([WarehouseId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202163533_testmig4')
BEGIN
    ALTER TABLE [StorageRacks] ADD CONSTRAINT [FK_StorageRacks_Warehouses_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [Warehouses] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202163533_testmig4')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201202163533_testmig4', N'5.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202164041_modelfix')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201202164041_modelfix', N'5.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201203081414_test1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201203081414_test1', N'5.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201203081935_test2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201203081935_test2', N'5.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201203082355_test3')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201203082355_test3', N'5.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201203102732_test4')
BEGIN
    ALTER TABLE [Shelves] DROP CONSTRAINT [FK_Shelves_Products_ProductId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201203102732_test4')
BEGIN
    DROP INDEX [IX_Shelves_ProductId] ON [Shelves];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201203102732_test4')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Shelves]') AND [c].[name] = N'ProductId');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Shelves] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Shelves] ALTER COLUMN [ProductId] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201203102732_test4')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_Shelves_ProductId] ON [Shelves] ([ProductId]) WHERE [ProductId] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201203102732_test4')
BEGIN
    ALTER TABLE [Shelves] ADD CONSTRAINT [FK_Shelves_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201203102732_test4')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201203102732_test4', N'5.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201207164036_seed')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'City', N'Name', N'PostCode', N'Street', N'UnitNumber') AND [object_id] = OBJECT_ID(N'[Warehouses]'))
        SET IDENTITY_INSERT [Warehouses] ON;
    EXEC(N'INSERT INTO [Warehouses] ([Id], [City], [Name], [PostCode], [Street], [UnitNumber])
    VALUES (1, N''Rzeszów'', N''Magazyn 1'', N''35-311'', N''Eugeniusza Kwiatkowskiego'', N''45'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'City', N'Name', N'PostCode', N'Street', N'UnitNumber') AND [object_id] = OBJECT_ID(N'[Warehouses]'))
        SET IDENTITY_INSERT [Warehouses] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201207164036_seed')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ContractorId', N'IsTaken', N'RackNumber', N'WarehouseId') AND [object_id] = OBJECT_ID(N'[StorageRacks]'))
        SET IDENTITY_INSERT [StorageRacks] ON;
    EXEC(N'INSERT INTO [StorageRacks] ([Id], [ContractorId], [IsTaken], [RackNumber], [WarehouseId])
    VALUES (1, NULL, CAST(0 AS bit), N''A1'', 1),
    (2, NULL, CAST(0 AS bit), N''A2'', 1),
    (3, NULL, CAST(0 AS bit), N''B1'', 1),
    (4, NULL, CAST(0 AS bit), N''B2'', 1),
    (5, NULL, CAST(0 AS bit), N''C1'', 1)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ContractorId', N'IsTaken', N'RackNumber', N'WarehouseId') AND [object_id] = OBJECT_ID(N'[StorageRacks]'))
        SET IDENTITY_INSERT [StorageRacks] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201207164036_seed')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ProductId', N'Quantity', N'RackId', N'ShelfNumber') AND [object_id] = OBJECT_ID(N'[Shelves]'))
        SET IDENTITY_INSERT [Shelves] ON;
    EXEC(N'INSERT INTO [Shelves] ([Id], [ProductId], [Quantity], [RackId], [ShelfNumber])
    VALUES (1, NULL, 0, 1, N''1''),
    (18, NULL, 0, 5, N''2''),
    (17, NULL, 0, 5, N''1''),
    (16, NULL, 0, 4, N''4''),
    (15, NULL, 0, 4, N''3''),
    (14, NULL, 0, 4, N''2''),
    (13, NULL, 0, 4, N''1''),
    (12, NULL, 0, 3, N''4''),
    (11, NULL, 0, 3, N''3''),
    (10, NULL, 0, 3, N''2''),
    (9, NULL, 0, 3, N''1''),
    (8, NULL, 0, 2, N''4''),
    (7, NULL, 0, 2, N''3''),
    (6, NULL, 0, 2, N''2''),
    (5, NULL, 0, 2, N''1''),
    (4, NULL, 0, 1, N''4''),
    (3, NULL, 0, 1, N''3''),
    (2, NULL, 0, 1, N''2''),
    (19, NULL, 0, 5, N''3''),
    (20, NULL, 0, 5, N''4'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ProductId', N'Quantity', N'RackId', N'ShelfNumber') AND [object_id] = OBJECT_ID(N'[Shelves]'))
        SET IDENTITY_INSERT [Shelves] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201207164036_seed')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201207164036_seed', N'5.0.0');
END;
GO

COMMIT;
GO

