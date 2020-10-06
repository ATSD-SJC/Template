USE [master];

CREATE DATABASE [ProjetoTemplate3];

USE [ProjetoTemplate3];

CREATE TABLE [dbo].[Classes](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Texto] [varchar](max) NULL,
	[Data] [datetime] NULL,
	[Valor] [float] NULL,
	[Booleano] [bit] NULL
);

CREATE TABLE [dbo].[InnerJoins](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[IdClasse] [int] NULL,
	[Cnpj] [varchar](30) NULL,
	[Cpf] [varchar](15) NULL,
	[Data] [datetime] NULL,
	[Telefone] [varchar](15) NULL,
	[Tipo] [varchar](20) NULL
);

ALTER TABLE [dbo].[InnerJoins]  WITH CHECK ADD FOREIGN KEY([IdClasse])
REFERENCES [dbo].[Classes] ([Id]);

