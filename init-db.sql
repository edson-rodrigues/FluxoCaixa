-- Cria a base de dados fluxo-caixa
CREATE DATABASE fluxo_caixa;
GO

-- Seleciona a base de dados
USE fluxo_caixa;
GO

-- Criação da tabela Lancamentos
CREATE TABLE Lancamentos (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Tipo VARCHAR(10) NOT NULL CHECK (Tipo IN ('Crédito', 'Débito')),
    Valor DECIMAL(18, 2) NOT NULL,
    Descricao VARCHAR(255),
    DataLancamento DATETIME NOT NULL,
    DataCriacao DATETIME NOT NULL DEFAULT GETDATE()
);
GO
