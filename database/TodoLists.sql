USE [ToDo]

GO

CREATE TABLE [TodoLists](
    [ID] int PRiMARY KEY IDENTITY(1,1) NOT NULL,
    [ListTitle] VARCHAR(50) NOT NULL,
    [AccountID] int not null,
    [Completed] BIT NOT NULL DEFAULT(0),
	[NextListItemPosition] INT NOT NULL DEFAULT(0)

    FOREIGN KEY ([AccountID]) REFERENCES [Accounts](ID) ON DELETE CASCADE
   
)

GO