USE [Todo]

GO

CREATE TABLE [AccountsPlans]
(
    AccountID UNIQUEIDENTIFIER NOT NULL,
    PlanID INT NOT NULL,
    ListCount INT NOT NULL,
    ContributorCount INT NOT NULL,
    PRIMARY KEY (AccountID, PlanID),
    FOREIGN KEY (AccountID) REFERENCES Accounts (ID),
)

GO