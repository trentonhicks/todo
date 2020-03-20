USE [ToDo]

GO

CREATE TABLE [ToDos](
    [ID] INT PRIMARY KEY IDENTITY(1,1),
    [ParentID] INT,
    [Notes] VARCHAR(200),
    [Completed] BIT NOT NULL,
    [ToDoName] VARCHAR(50),
    [ListID] INT NOT NULL,
    FOREIGN KEY ([ListID]) REFERENCES [TodoLists](ID)
)

GO