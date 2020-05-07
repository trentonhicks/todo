USE [ToDo]

GO

Create Table [AccountLists](
    AccountID UNIQUEIDENTIFIER,
    ListID UNIQUEIDENTIFIER,
    Role TINYINT NOT NULL,
    PRIMARY KEY (AccountID, ListID),
    FOREIGN KEY (AccountID) REFERENCES Accounts (ID),
)