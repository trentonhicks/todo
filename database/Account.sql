USE [ToDo]

GO

Create Table [Accounts](
    [ID] INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
    [FullName] VARCHAR(50),
    [PictureUrl] VARCHAR(255),
	[Email] VARCHAR(50) NOT NULL
)

GO