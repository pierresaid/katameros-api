CREATE TABLE [dbo].[Verses] (
    [Id]      INT             IDENTITY (1, 1) NOT NULL,
    [BibleId] INT             NOT NULL,
    [BookId]  INT             NOT NULL,
    [Chapter] INT             NOT NULL,
    [Number]  INT             NOT NULL,
    [Text]    NVARCHAR (1000) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([BibleId]) REFERENCES [dbo].[Bibles] ([Id]),
    CONSTRAINT [VersesB_BookId_FK] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] ([Id])
);

