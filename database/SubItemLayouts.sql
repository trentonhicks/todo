USE ToDO

GO

Create TABLE [SubItemLayouts](
    ID int PRIMARY KEY IDENTITY(1,1) NOT NULL,
    SubItemId INT NOT NULL,
    Layout VARCHAR(max) NOT NULL,
    FOREIGN KEY (SubItemId) REFERENCES SubItems (ID) ON DELETE CASCADE,
     UNIQUE (SubItemId)
)

GO