USE [ToDo]

GO

Create Table [AccountLists](
    AccountID UNIQUEIDENTIFIER,
    ListID UNIQUEIDENTIFIER,
    PRIMARY KEY (AccountID, ListID),
    FOREIGN KEY (AccountID) REFERENCES Accounts (ID),
)