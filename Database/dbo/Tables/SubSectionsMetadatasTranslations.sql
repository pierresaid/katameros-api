CREATE TABLE [dbo].[SubSectionsMetadatasTranslations] (
    [SubSectionsId]          INT             NOT NULL,
    [LanguageId]             INT             NOT NULL,
    [SubSectionsMetadatasId] INT             NOT NULL,
    [Text]                   NVARCHAR (1000) NOT NULL,
    CONSTRAINT [PK__SubSecti__31E27BF391E1576C] PRIMARY KEY CLUSTERED ([SubSectionsId] ASC, [SubSectionsMetadatasId] ASC, [LanguageId] ASC),
    CONSTRAINT [FK__SubSectio__Langu__7C1A6C5A] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id]),
    CONSTRAINT [FK__SubSectio__SubSe__7D0E9093] FOREIGN KEY ([SubSectionsId]) REFERENCES [dbo].[SubSections] ([Id]),
    CONSTRAINT [FK__SubSectio__SubSe__7E02B4CC] FOREIGN KEY ([SubSectionsMetadatasId]) REFERENCES [dbo].[SubSectionsMetadatas] ([Id])
);



