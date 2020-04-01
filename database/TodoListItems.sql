USE [ToDo]

GO

CREATE TABLE [TodoListItems](
    [ID] INT PRIMARY KEY IDENTITY(1,1),
    [ParentID] INT NULL,
    [Notes] VARCHAR(200),
    [Completed] BIT NOT NULL DEFAULT(0),
    [ToDoName] VARCHAR(50),
    [ListID] INT NOT NULL,
	[AccountID] INT NOT NULL,
	[Position] INT NOT NULL

    FOREIGN KEY ([ListID]) REFERENCES [TodoLists](ID),
    FOREIGN KEY ([AccountID]) REFERENCES [Accounts](ID) ON DELETE CASCADE
)

GO