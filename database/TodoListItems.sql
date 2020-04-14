USE [ToDo]

GO

CREATE TABLE [TodoListItems](
    [ID] INT PRIMARY KEY IDENTITY(1,1),
    [Notes] VARCHAR(200),
    [Completed] BIT NOT NULL DEFAULT(0),
    [ToDoName] VARCHAR(50),
	[DueDate] DATETIME,
    [ListID] INT,
	[AccountID] INT NOT NULL

    FOREIGN KEY (AccountID) REFERENCES Accounts (ID),
    FOREIGN KEY (ListID) REFERENCES TodoLists (ID) ON DELETE CASCADE
)

GO