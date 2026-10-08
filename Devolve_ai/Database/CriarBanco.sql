-- =============================================================
-- Script de criação do banco do sistema "Cadê Meu?"
-- Pode ser executado quantas vezes quiser: só cria o que não existe.
-- O próprio sistema executa este script ao iniciar (BancoInicializador).
-- =============================================================

IF DB_ID(N'CadeMeu') IS NULL
    CREATE DATABASE CadeMeu;
GO

USE CadeMeu;
GO

-- Amigos fica em uma tabela própria para não repetir nome e contato
-- em cada empréstimo (um amigo pode pegar várias coisas).
IF OBJECT_ID(N'dbo.Amigos', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Amigos
    (
        Id       INT IDENTITY(1, 1) NOT NULL CONSTRAINT PK_Amigos PRIMARY KEY,
        Nome     NVARCHAR(100)      NOT NULL,
        Telefone VARCHAR(11)        NOT NULL, -- somente dígitos, ex.: 11987654321
        Email    NVARCHAR(150)      NULL
    );
END
GO

IF OBJECT_ID(N'dbo.Emprestimos', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Emprestimos
    (
        Id                    INT IDENTITY(1, 1) NOT NULL CONSTRAINT PK_Emprestimos PRIMARY KEY,
        Item                  NVARCHAR(100)      NOT NULL,
        AmigoId               INT                NOT NULL
            CONSTRAINT FK_Emprestimos_Amigos REFERENCES dbo.Amigos (Id),
        DataEmprestimo        DATE               NOT NULL,
        DataDevolucaoPrevista DATE               NULL, -- opcional: nem todo empréstimo tem data combinada
        DataDevolucao         DATE               NULL, -- preenchida quando o item volta

        -- Última linha de defesa: mesmo que alguém pule a validação do sistema,
        -- o banco recusa uma data combinada anterior ao empréstimo.
        CONSTRAINT CK_Emprestimos_DataPrevista
            CHECK (DataDevolucaoPrevista IS NULL OR DataDevolucaoPrevista >= DataEmprestimo)
    );
END
GO
