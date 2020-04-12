USE ToDo

GO

Create TABLE [SubItemLayouts](
    ID int PRIMARY KEY IDENTITY(1,1) NOT NULL,
    ItemId INT NOT NULL,
    Layout VARCHAR(max) NOT NULL,
    FOREIGN KEY (ItemId) REFERENCES TodoListItems (ID) ON DELETE CASCADE,
     UNIQUE (ItemId)
)

GO