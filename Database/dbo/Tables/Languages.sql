CREATE TABLE [dbo].[Languages] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Code] NVARCHAR (50)  NOT NULL,
    [Name] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK__Language__3214EC072AD4690C] PRIMARY KEY CLUSTERED ([Id] ASC)
);



