CREATE TABLE [dbo].[GreatLentReadings] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [Week]            INT           NOT NULL,
    [DayOfWeek]       INT           NOT NULL,
    [DayName]         VARCHAR (300) CONSTRAINT [DF__LentReadi__DayNa__1E6F845E] DEFAULT (NULL) NULL,
    [Seasonal_Tune]   VARCHAR (300) NULL,
    [Weather_Prayers] VARCHAR (300) NULL,
    [V_Psalm_Ref]     VARCHAR (300) NULL,
    [V_Gospel_Ref]    VARCHAR (300) NULL,
    [M_Psalm_Ref]     VARCHAR (300) NOT NULL,
    [M_Gospel_Ref]    VARCHAR (300) NOT NULL,
    [P_Gospel_Ref]    VARCHAR (300) NOT NULL,
    [C_Gospel_Ref]    VARCHAR (300) NOT NULL,
    [X_Gospel_Ref]    VARCHAR (300) NOT NULL,
    [L_Psalm_Ref]     VARCHAR (300) NOT NULL,
    [L_Gospel_Ref]    VARCHAR (300) NOT NULL,
    [Prophecy]        VARCHAR (500) NULL,
    CONSTRAINT [PK__LentRead__3214EC076E35E349] PRIMARY KEY CLUSTERED ([Id] ASC)
);



