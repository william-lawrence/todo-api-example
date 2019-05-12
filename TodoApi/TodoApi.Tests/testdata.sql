
DELETE FROM TodoItems;

SET IDENTITY_INSERT TodoItems ON;

INSERT INTO TodoItems (Id, TodoText, IsCompleted, IsDeleted)
VALUES (1, 'test1', 0, 0);

SET IDENTITY_INSERT TodoItems OFF;
