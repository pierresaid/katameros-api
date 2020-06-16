CREATE TABLE [dbo].[SubSectionsMetadatasTranslations] (
    [SubSectionsId]          INT             NOT NULL,
    [LanguageId]             INT             NOT NULL,
    [SubSectionsMetadatasId] INT             NOT NULL,
    [Text]                   NVARCHAR (1000) NOT NULL,
    PRIMARY KEY CLUSTERED ([SubSectionsId] ASC, [SubSectionsMetadatasId] ASC, [LanguageId] ASC),
    FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id]),
    FOREIGN KEY ([SubSectionsId]) REFERENCES [dbo].[SubSections] ([Id]),
    FOREIGN KEY ([SubSectionsMetadatasId]) REFERENCES [dbo].[SubSectionsMetadatas] ([Id])
);

