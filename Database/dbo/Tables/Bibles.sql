CREATE TABLE [dbo].[Bibles] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (200) NOT NULL,
    [LanguageId] INT            NOT NULL,
    CONSTRAINT [PK__Bibles__3214EC0783BD15F9] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__Bibles__Language__09A971A2] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id])
);



