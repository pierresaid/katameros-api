CREATE TABLE [dbo].[SectionsMetadatasTranslations] (
    [SectionsId]          INT             NOT NULL,
    [LanguageId]          INT             NOT NULL,
    [SectionsMetadatasId] INT             NOT NULL,
    [Text]                NVARCHAR (1000) NOT NULL,
    PRIMARY KEY CLUSTERED ([SectionsId] ASC, [SectionsMetadatasId] ASC, [LanguageId] ASC),
    FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id]),
    FOREIGN KEY ([SectionsId]) REFERENCES [dbo].[Sections] ([Id]),
    FOREIGN KEY ([SectionsMetadatasId]) REFERENCES [dbo].[SectionsMetadatas] ([Id])
);

