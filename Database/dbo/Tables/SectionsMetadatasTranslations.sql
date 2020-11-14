CREATE TABLE [dbo].[SectionsMetadatasTranslations] (
    [SectionsId]          INT             NOT NULL,
    [LanguageId]          INT             NOT NULL,
    [SectionsMetadatasId] INT             NOT NULL,
    [Text]                NVARCHAR (1000) NOT NULL,
    CONSTRAINT [PK__Sections__F99AAFAF3ABEA180] PRIMARY KEY CLUSTERED ([SectionsId] ASC, [SectionsMetadatasId] ASC, [LanguageId] ASC),
    CONSTRAINT [FK__SectionsM__Langu__0C50D423] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id]),
    CONSTRAINT [FK__SectionsM__Secti__0D44F85C] FOREIGN KEY ([SectionsId]) REFERENCES [dbo].[Sections] ([Id]),
    CONSTRAINT [FK__SectionsM__Secti__0E391C95] FOREIGN KEY ([SectionsMetadatasId]) REFERENCES [dbo].[SectionsMetadatas] ([Id])
);



