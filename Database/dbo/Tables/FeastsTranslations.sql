CREATE TABLE [dbo].[FeastsTranslations] (
    [FeastId]    INT           NOT NULL,
    [LanguageId] INT           NOT NULL,
    [Text]       NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([FeastId] ASC, [LanguageId] ASC),
    FOREIGN KEY ([FeastId]) REFERENCES [dbo].[Feasts] ([Id]),
    FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id])
);

