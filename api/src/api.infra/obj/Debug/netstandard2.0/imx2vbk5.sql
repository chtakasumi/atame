IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [grupo] (
    [Id] int NOT NULL IDENTITY,
    [descricao] nvarchar(40) NOT NULL,
    CONSTRAINT [PK_grupo] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [perfil] (
    [Id] int NOT NULL IDENTITY,
    [descricao] int NOT NULL,
    CONSTRAINT [PK_perfil] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Usuario] (
    [Id] int NOT NULL IDENTITY,
    [login] varchar(40) NOT NULL,
    [senha] nvarchar(40) NOT NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [PerfilGrupo] (
    [Id] int NOT NULL IDENTITY,
    [PerfilId] int NOT NULL,
    [GrupoId] int NOT NULL,
    CONSTRAINT [PK_PerfilGrupo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PerfilGrupo_grupo_GrupoId] FOREIGN KEY ([GrupoId]) REFERENCES [grupo] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PerfilGrupo_perfil_PerfilId] FOREIGN KEY ([PerfilId]) REFERENCES [perfil] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [GrupoUsuario] (
    [Id] int NOT NULL IDENTITY,
    [GrupoId] int NOT NULL,
    [UsuarioId] int NOT NULL,
    CONSTRAINT [PK_GrupoUsuario] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_GrupoUsuario_grupo_GrupoId] FOREIGN KEY ([GrupoId]) REFERENCES [grupo] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_GrupoUsuario_Usuario_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuario] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_GrupoUsuario_GrupoId] ON [GrupoUsuario] ([GrupoId]);

GO

CREATE INDEX [IX_GrupoUsuario_UsuarioId] ON [GrupoUsuario] ([UsuarioId]);

GO

CREATE INDEX [IX_PerfilGrupo_GrupoId] ON [PerfilGrupo] ([GrupoId]);

GO

CREATE INDEX [IX_PerfilGrupo_PerfilId] ON [PerfilGrupo] ([PerfilId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190108001322_InitialCreate', N'2.1.4-rtm-31024');

GO

ALTER TABLE [PerfilGrupo] DROP CONSTRAINT [FK_PerfilGrupo_perfil_PerfilId];

GO

ALTER TABLE [PerfilGrupo] ADD CONSTRAINT [FK_PerfilGrupo_perfil_PerfilId] FOREIGN KEY ([PerfilId]) REFERENCES [perfil] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190108004242_restricao_deletar_perfil', N'2.1.4-rtm-31024');

GO

