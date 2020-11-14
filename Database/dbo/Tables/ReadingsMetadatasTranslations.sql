CREATE TABLE [dbo].[ReadingsMetadatasTranslations] (
    [ReadingId]           INT             NOT NULL,
    [LanguageId]          INT             NOT NULL,
    [ReadingsMetadatasId] INT             NOT NULL,
    [Text]                NVARCHAR (1000) NOT NULL,
    CONSTRAINT [PK__Readings__BDB5F89AC3DD09AB] PRIMARY KEY CLUSTERED ([ReadingId] ASC, [ReadingsMetadatasId] ASC, [LanguageId] ASC),
    CONSTRAINT [FK__ReadingsS__Langu__65370702] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id]),
    CONSTRAINT [FK__ReadingsS__Readi__6442E2C9] FOREIGN KEY ([ReadingId]) REFERENCES [dbo].[Readings] ([Id]),
    CONSTRAINT [FK__ReadingsS__Readi__662B2B3B] FOREIGN KEY ([ReadingsMetadatasId]) REFERENCES [dbo].[ReadingsMetadatas] ([Id])
);



