CREATE TABLE [dbo].[BooksTranslations] (
    [BookId]     INT           NOT NULL,
    [LanguageId] INT           NOT NULL,
    [Text]       NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK__BooksTra__9673475DECC7AAEC] PRIMARY KEY CLUSTERED ([BookId] ASC, [LanguageId] ASC),
    CONSTRAINT [FK__BooksTrad__BookI__0E6E26BF] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] ([Id]),
    CONSTRAINT [FK__BooksTrad__Langu__0F624AF8] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id])
);



