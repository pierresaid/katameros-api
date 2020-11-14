CREATE TABLE [dbo].[SentencesTranslations] (
    [SentenceId] INT             NOT NULL,
    [LanguageId] INT             NOT NULL,
    [Text]       NVARCHAR (1000) NOT NULL,
    PRIMARY KEY CLUSTERED ([SentenceId] ASC, [LanguageId] ASC),
    FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id]),
    FOREIGN KEY ([SentenceId]) REFERENCES [dbo].[Sentences] ([Id])
);

