CREATE TABLE [dbo].[ReadingsMetadatasTranslations] (
    [ReadingId]           INT             NOT NULL,
    [LanguageId]          INT             NOT NULL,
    [ReadingsMetadatasId] INT             NOT NULL,
    [Text]                NVARCHAR (1000) NOT NULL,
    PRIMARY KEY CLUSTERED ([ReadingId] ASC, [ReadingsMetadatasId] ASC, [LanguageId] ASC),
    FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id]),
    FOREIGN KEY ([ReadingId]) REFERENCES [dbo].[Readings] ([Id]),
    FOREIGN KEY ([ReadingsMetadatasId]) REFERENCES [dbo].[ReadingsMetadatas] ([Id])
);

