CREATE TABLE [dbo].[SundayReadings] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Month_Number] INT           NOT NULL,
    [Month_Name]   VARCHAR (300) NOT NULL,
    [Day]          INT           NOT NULL,
    [Other]        VARCHAR (300) NULL,
    [Day_Tune]     VARCHAR (300) NULL,
    [DayName]      VARCHAR (300) NULL,
    [Season]       VARCHAR (300) NULL,
    [V_Psalm_Ref]  VARCHAR (300) NOT NULL,
    [V_Gospel_Ref] VARCHAR (300) NOT NULL,
    [M_Psalm_Ref]  VARCHAR (300) NOT NULL,
    [M_Gospel_Ref] VARCHAR (300) NOT NULL,
    [P_Gospel_Ref] VARCHAR (300) NOT NULL,
    [C_Gospel_Ref] VARCHAR (300) NOT NULL,
    [X_Gospel_Ref] VARCHAR (300) NOT NULL,
    [L_Psalm_Ref]  VARCHAR (300) NOT NULL,
    [L_Gospel_Ref] VARCHAR (300) NOT NULL,
    CONSTRAINT [PK__SundayRe__3214EC079C45F405] PRIMARY KEY CLUSTERED ([Id] ASC)
);



