CREATE TABLE [dbo].[Bibles] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (200) NOT NULL,
    [LanguageId] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id])
);

