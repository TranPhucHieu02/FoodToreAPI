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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE TABLE [FoodParts] (
        [idFoodPart] int NOT NULL IDENTITY,
        [nameFoodPart] nvarchar(max) NOT NULL,
        [fat] real NOT NULL,
        [calories] real NOT NULL,
        [cacbonhydrat] real NOT NULL,
        [protein] real NOT NULL,
        [desciption] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_FoodParts] PRIMARY KEY ([idFoodPart])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE TABLE [FoodTypes] (
        [idFoodType] int NOT NULL IDENTITY,
        [name] nvarchar(max) NOT NULL,
        [description] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_FoodTypes] PRIMARY KEY ([idFoodType])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE TABLE [Nutributions] (
        [idNutribution] int NOT NULL IDENTITY,
        [name] nvarchar(max) NOT NULL,
        [description] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Nutributions] PRIMARY KEY ([idNutribution])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE TABLE [Purposes] (
        [idPurpose] int NOT NULL IDENTITY,
        [name] nvarchar(max) NOT NULL,
        [desciption] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Purposes] PRIMARY KEY ([idPurpose])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE TABLE [Roles] (
        [idRole] int NOT NULL IDENTITY,
        [name] nvarchar(max) NOT NULL,
        [description] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([idRole])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE TABLE [Statebody] (
        [idStatebody] int NOT NULL IDENTITY,
        [name] nvarchar(max) NOT NULL,
        [description] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Statebody] PRIMARY KEY ([idStatebody])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE TABLE [Foods] (
        [idFood] int NOT NULL IDENTITY,
        [nameFoodPart] nvarchar(max) NOT NULL,
        [fat] real NOT NULL,
        [calories] real NOT NULL,
        [cacbonhydrat] real NOT NULL,
        [protein] real NOT NULL,
        [desciption] nvarchar(max) NOT NULL,
        [recipePath] nvarchar(max) NOT NULL,
        [ratecount] int NOT NULL,
        [idFoodTypes] int NOT NULL,
        CONSTRAINT [PK_Foods] PRIMARY KEY ([idFood]),
        CONSTRAINT [FK_Foods_FoodTypes_idFoodTypes] FOREIGN KEY ([idFoodTypes]) REFERENCES [FoodTypes] ([idFoodType]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE TABLE [Users] (
        [idUser] int NOT NULL IDENTITY,
        [emailAddress] nvarchar(max) NOT NULL,
        [password] nvarchar(max) NOT NULL,
        [weight] int NOT NULL,
        [heigh] int NOT NULL,
        [gender] int NOT NULL,
        [age] int NOT NULL,
        [avatar] nvarchar(max) NOT NULL,
        [idStatebody] int NOT NULL,
        [idRole] int NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([idUser]),
        CONSTRAINT [FK_Users_Roles_idRole] FOREIGN KEY ([idRole]) REFERENCES [Roles] ([idRole]) ON DELETE CASCADE,
        CONSTRAINT [FK_Users_Statebody_idStatebody] FOREIGN KEY ([idStatebody]) REFERENCES [Statebody] ([idStatebody]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE TABLE [DetailNutributionFoods] (
        [idNutributionFood] int NOT NULL IDENTITY,
        [idFoods] int NOT NULL,
        [idNutribution] int NOT NULL,
        [level] int NOT NULL,
        CONSTRAINT [PK_DetailNutributionFoods] PRIMARY KEY ([idNutributionFood]),
        CONSTRAINT [FK_DetailNutributionFoods_Foods_idFoods] FOREIGN KEY ([idFoods]) REFERENCES [Foods] ([idFood]) ON DELETE CASCADE,
        CONSTRAINT [FK_DetailNutributionFoods_Nutributions_idNutribution] FOREIGN KEY ([idNutribution]) REFERENCES [Nutributions] ([idNutribution]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE TABLE [Rates] (
        [idRate] int NOT NULL IDENTITY,
        [idFood] int NOT NULL,
        [idFoodpart] int NOT NULL,
        [rate] int NOT NULL,
        CONSTRAINT [PK_Rates] PRIMARY KEY ([idRate]),
        CONSTRAINT [FK_Rates_FoodParts_idFoodpart] FOREIGN KEY ([idFoodpart]) REFERENCES [FoodParts] ([idFoodPart]) ON DELETE CASCADE,
        CONSTRAINT [FK_Rates_Foods_idFood] FOREIGN KEY ([idFood]) REFERENCES [Foods] ([idFood]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE TABLE [DetailPurposes] (
        [idPurposedetail] int NOT NULL IDENTITY,
        [idUser] int NOT NULL,
        [idPurpose] int NOT NULL,
        [level] int NOT NULL,
        CONSTRAINT [PK_DetailPurposes] PRIMARY KEY ([idPurposedetail]),
        CONSTRAINT [FK_DetailPurposes_Purposes_idPurpose] FOREIGN KEY ([idPurpose]) REFERENCES [Purposes] ([idPurpose]) ON DELETE CASCADE,
        CONSTRAINT [FK_DetailPurposes_Users_idUser] FOREIGN KEY ([idUser]) REFERENCES [Users] ([idUser]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE TABLE [Diets] (
        [idDiet] int NOT NULL IDENTITY,
        [dateStart] datetime2 NOT NULL,
        [isActive] int NOT NULL,
        [idUser] int NOT NULL,
        CONSTRAINT [PK_Diets] PRIMARY KEY ([idDiet]),
        CONSTRAINT [FK_Diets_Users_idUser] FOREIGN KEY ([idUser]) REFERENCES [Users] ([idUser]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE TABLE [DetailDietFoods] (
        [idDetailDietFood] int NOT NULL IDENTITY,
        [idFood] int NOT NULL,
        [idDiet] int NOT NULL,
        [date] datetime2 NOT NULL,
        [bref] int NOT NULL,
        CONSTRAINT [PK_DetailDietFoods] PRIMARY KEY ([idDetailDietFood]),
        CONSTRAINT [FK_DetailDietFoods_Diets_idDiet] FOREIGN KEY ([idDiet]) REFERENCES [Diets] ([idDiet]) ON DELETE CASCADE,
        CONSTRAINT [FK_DetailDietFoods_Foods_idFood] FOREIGN KEY ([idFood]) REFERENCES [Foods] ([idFood]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE INDEX [IX_DetailDietFoods_idDiet] ON [DetailDietFoods] ([idDiet]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE INDEX [IX_DetailDietFoods_idFood] ON [DetailDietFoods] ([idFood]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE INDEX [IX_DetailNutributionFoods_idFoods] ON [DetailNutributionFoods] ([idFoods]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE INDEX [IX_DetailNutributionFoods_idNutribution] ON [DetailNutributionFoods] ([idNutribution]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE INDEX [IX_DetailPurposes_idPurpose] ON [DetailPurposes] ([idPurpose]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE INDEX [IX_DetailPurposes_idUser] ON [DetailPurposes] ([idUser]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE INDEX [IX_Diets_idUser] ON [Diets] ([idUser]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE INDEX [IX_Foods_idFoodTypes] ON [Foods] ([idFoodTypes]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE INDEX [IX_Rates_idFood] ON [Rates] ([idFood]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE INDEX [IX_Rates_idFoodpart] ON [Rates] ([idFoodpart]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE INDEX [IX_Users_idRole] ON [Users] ([idRole]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    CREATE INDEX [IX_Users_idStatebody] ON [Users] ([idStatebody]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230910073922_createdb')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230910073922_createdb', N'6.0.21');
END;
GO

COMMIT;
GO

