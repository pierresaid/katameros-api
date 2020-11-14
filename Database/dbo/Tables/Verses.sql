CREATE TABLE [dbo].[Verses] (
    [Id]      INT             IDENTITY (1, 1) NOT NULL,
    [BibleId] INT             NOT NULL,
    [BookId]  INT             NOT NULL,
    [Chapter] INT             NOT NULL,
    [Number]  INT             NOT NULL,
    [Text]    NVARCHAR (1000) NOT NULL,
    CONSTRAINT [PK__VersesB__3214EC07650E3528] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__VersesB__BibleId__160F4887] FOREIGN KEY ([BibleId]) REFERENCES [dbo].[Bibles] ([Id]),
    CONSTRAINT [VersesB_BookId_FK] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] ([Id])
);



