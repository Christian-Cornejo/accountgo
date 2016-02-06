﻿CREATE TABLE [dbo].[Log] (
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[TimeStamp] DATETIME2 NULL,
	[Level] NVARCHAR(100) NULL,
	[Logger] NVARCHAR(MAX) NULL, 
    [Message] NVARCHAR(MAX) NULL, 
    [Username] NVARCHAR(MAX) NULL, 
    [CallSite] NVARCHAR(MAX) NULL, 
    [Thread] NVARCHAR(MAX) NULL,
    [Exception] NVARCHAR(MAX) NULL, 
    [StackTrace] NVARCHAR(MAX) NULL
	CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED ([Id] ASC)
)