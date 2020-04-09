USE [ToDo]

GO
--ACCOUNTS
INSERT INTO Accounts(FullName,UserName,[Password],Email)
VALUES 
('Madison Dodson','MadDod','password','Maddod@gmail.com'),
('Ashlee Santana','Santa','password','Ashe@hotmail.com'),
('Barney Vazquez','Vaz','password','ILuvU_ULuvMe@gmail.com');

--TODO LISTS
INSERT INTO TodoLists(ListTitle, AccountID)
VALUES 
('Cleaning',1),
('Errands',1),
('Groceries',2),
('Work',2),
('Church',3),
('Homework',3);

--TODO LIST ITEMS
INSERT INTO TodoListItems(ToDoName, Notes, ListID, AccountID)
VALUES 
--LIST CLEANING
('Room','Clean under bed',1,1),
('Car','Clean seat',1,1),

--LIST ERRANDS 
('Drop off mail','Postoffice closes at 4:30PM',2,1),
('Make House Payment','DeadLine 5PM',2,1),

--LIST GROCERIES 
('Butter','Land O Lakes Salted Butter',3,2),
('Milk','Horizon Organic',3,2),

--LIST WORK 
('Inventory','Rotate old and new',4,2),
('Put in order for next week','Must be done before monday',4,2),

--LIST CHURCH  
('Print flyers','Event on 4/1/20',5,3),
('Count tithe and offering','Must be done before Friday',5,3),

--LIST HOMEWORK 
('Math','Work on fractions',6,3),
('Spanish','D�nde est� el ba�o',6,3);