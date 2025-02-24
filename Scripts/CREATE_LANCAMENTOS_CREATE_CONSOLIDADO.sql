-- Tabela Lancamentos
CREATE TABLE Lancamentos (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Tipo VARCHAR(10) NOT NULL CHECK (Tipo IN ('Crédito', 'Débito')),
    Valor DECIMAL(18, 2) NOT NULL,
    Descricao VARCHAR(255),
    DataLancamento DATETIME NOT NULL,
    DataCriacao DATETIME NOT NULL DEFAULT GETDATE()
);

-- Tabela Consolidado
CREATE TABLE Consolidado (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Data DATE NOT NULL,
    Saldo DECIMAL(18, 2) NOT NULL,
    DataCriacao DATETIME NOT NULL DEFAULT GETDATE()
);