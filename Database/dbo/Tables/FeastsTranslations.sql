CREATE TABLE [dbo].[FeastsTranslations] (
    [FeastId]    INT           NOT NULL,
    [LanguageId] INT           NOT NULL,
    [Text]       NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK__FeastsTr__C2DDDE856CDC25C2] PRIMARY KEY CLUSTERED ([FeastId] ASC, [LanguageId] ASC),
    CONSTRAINT [FK__FeastsTra__Feast__56B3DD81] FOREIGN KEY ([FeastId]) REFERENCES [dbo].[Feasts] ([Id]),
    CONSTRAINT [FK__FeastsTra__Langu__57A801BA] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id])
);



