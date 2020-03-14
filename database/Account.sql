USE [ToDo]

GO

Create Table [Accounts](
    [ID] INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
    [FullName] VARCHAR,
    [UserName] VARCHAR UNIQUE NOT NULL,
    [Picture] VARBINARY(max) NOT NULL,
    [Password] VARCHAR NOT NULL
)

GO