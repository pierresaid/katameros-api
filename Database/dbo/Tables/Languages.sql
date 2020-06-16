CREATE TABLE [dbo].[Languages] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Code] NVARCHAR (50)  NOT NULL,
    [Name] NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

