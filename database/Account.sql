USE [ToDo]

GO

Create Table [Accounts](
    [ID] UNIQUEIDENTIFIER PRIMARY KEY,
    [FullName] VARCHAR(50),
    [PictureUrl] VARCHAR(255),
	[Email] VARCHAR(50) NOT NULL,
    [Contributors] VARCHAR(max) NOT NULL
)

GO