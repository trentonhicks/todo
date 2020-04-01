USE [ToDo]

GO
--ACCOUNTS
INSERT INTO Accounts(FullName,UserName,[Password],Email)
VALUES ('Madison Dodson','MadDod','password','Maddod@gmail.com');
INSERT INTO Accounts(FullName,UserName,[Password],Email)
VALUES ('Ashlee Santana','Santa','password','Ashe@hotmail.com');
INSERT INTO Accounts(FullName,UserName,[Password],Email)
VALUES ('Barney Vazquez','Vaz','password','ILuvU_ULuvMe@gmail.com');

--TODO LISTS
INSERT INTO TodoLists(ListTitle, AccountID)
VALUES ('Cleaning',1);
INSERT INTO TodoLists(ListTitle, AccountID)
VALUES ('Errands',1);
INSERT INTO TodoLists(ListTitle, AccountID)
VALUES ('Groceries',2);
INSERT INTO TodoLists(ListTitle, AccountID)
VALUES ('Work',2);
INSERT INTO TodoLists(ListTitle, AccountID)
VALUES ('Church',3);
INSERT INTO TodoLists(ListTitle, AccountID)
VALUES ('Homework',3);

--TODO LIST ITEMS

--LIST CLEANING
INSERT INTO TodoListItems(ToDoName, Notes, ListID, AccountID)
VALUES ('Room','Clean under bed',1,1);
INSERT INTO TodoListItems(ToDoName, Notes, ListID, AccountID)
VALUES ('Car','Clean seat',1,1);
--LIST ERRANDS 
INSERT INTO TodoListItems(ToDoName, Notes, ListID, AccountID)
VALUES ('Drop off mail','Postoffice closes at 4:30PM',2,1);
INSERT INTO TodoListItems(ToDoName, Notes, ListID, AccountID)
VALUES ('Make House Payment','DeadLine 5PM',2,1);
--LIST GROCERIES 
INSERT INTO TodoListItems(ToDoName, Notes, ListID, AccountID)
VALUES ('Butter','Land O Lakes Salted Butter',3,2);
INSERT INTO TodoListItems(ToDoName, Notes, ListID, AccountID)
VALUES ('Milk','Horizon Organic',3,2);
--LIST WORK 
INSERT INTO TodoListItems(ToDoName, Notes, ListID, AccountID)
VALUES ('Inventory','Rotate old and new',4,2);
INSERT INTO TodoListItems(ToDoName, Notes, ListID, AccountID)
VALUES ('Put in order for next week','Must be done before monday',4,2);
--LIST CHURCH 
INSERT INTO TodoListItems(ToDoName, Notes, ListID, AccountID)
VALUES ('Print flyers','Event on 4/1/20',5,3);
INSERT INTO TodoListItems(ToDoName, Notes, ListID, AccountID)
VALUES ('Count tithe and offering','Must be done before Friday',5,3);
--LIST HOMEWORK 
INSERT INTO TodoListItems(ToDoName, Notes, ListID, AccountID)
VALUES ('Math','Work on fractions',6,3);
INSERT INTO TodoListItems(ToDoName, Notes, ListID, AccountID)
VALUES ('Spanish','Dónde está el baño',6,3);