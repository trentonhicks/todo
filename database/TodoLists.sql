USE [Todo]

GO

CREATE TABLE [TodoLists](
    [ID] UNIQUEIDENTIFIER PRIMARY KEY,
    [ListTitle] VARCHAR(50) NOT NULL,
    [Completed] BIT NOT NULL DEFAULT(0),
    [Contributors] VARCHAR(max) NOT NULL
)

GO