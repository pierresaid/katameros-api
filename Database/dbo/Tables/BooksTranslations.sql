CREATE TABLE [dbo].[BooksTranslations] (
    [BookId]     INT           NOT NULL,
    [LanguageId] INT           NOT NULL,
    [Text]       NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([BookId] ASC, [LanguageId] ASC),
    FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] ([Id]),
    FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id])
);

