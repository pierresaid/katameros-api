CREATE TABLE [dbo].[AnnualReadings] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Month_Number] INT           NOT NULL,
    [Month_Name]   VARCHAR (300) NOT NULL,
    [Day]          INT           NOT NULL,
    [Other]        VARCHAR (300) DEFAULT (NULL) NULL,
    [Day_Tune]     VARCHAR (300) DEFAULT (NULL) NULL,
    [DayName]      VARCHAR (300) DEFAULT (NULL) NULL,
    [Season]       VARCHAR (300) DEFAULT (NULL) NULL,
    [V_Psalm_Ref]  VARCHAR (300) DEFAULT (NULL) NOT NULL,
    [V_Gospel_Ref] VARCHAR (300) DEFAULT (NULL) NOT NULL,
    [M_Psalm_Ref]  VARCHAR (300) DEFAULT (NULL) NOT NULL,
    [M_Gospel_Ref] VARCHAR (300) DEFAULT (NULL) NOT NULL,
    [P_Gospel_Ref] VARCHAR (300) DEFAULT (NULL) NOT NULL,
    [C_Gospel_Ref] VARCHAR (300) DEFAULT (NULL) NOT NULL,
    [X_Gospel_Ref] VARCHAR (300) DEFAULT (NULL) NOT NULL,
    [L_Psalm_Ref]  VARCHAR (300) DEFAULT (NULL) NOT NULL,
    [L_Gospel_Ref] VARCHAR (300) DEFAULT (NULL) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

