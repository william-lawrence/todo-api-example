
DELETE FROM TodoItems;

SET IDENTITY_INSERT TodoItems ON;

INSERT INTO TodoItems (Id, TodoText, IsCompleted, IsDeleted)
VALUES (1, 'test1', 0, 0);

INSERT INTO TodoItems (Id, TodoText, IsCompleted, IsDeleted)
VALUES (2, 'test2', 0, 1);

INSERT INTO TodoItems (Id, TodoText, IsCompleted, IsDeleted)
VALUES (3, 'test3', 1, 0);

INSERT INTO TodoItems (Id, TodoText, IsCompleted, IsDeleted)
VALUES (4, 'test4', 1, 1);

SET IDENTITY_INSERT TodoItems OFF;
