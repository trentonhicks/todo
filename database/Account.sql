USE [ToDo]

GO

Create Table [Accounts](
    [ID] INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
    [FullName] VARCHAR(50),
    [UserName] VARCHAR(50) UNIQUE NOT NULL,
    [Picture] VARBINARY(max),
    [Password] VARCHAR(50) NOT NULL
)

GO