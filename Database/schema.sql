-- Switch to the system (aka master) database.
USE master;
GO

-- Delete the todo database (IF EXISTS)
IF EXISTS(SELECT * FROM sys.databases WHERE name='Todo')
DROP DATABASE Todo;
GO

-- Create a new todo database.
CREATE DATABASE Todo;
GO

-- Switch to the todo database.
USE Todo
GO

BEGIN TRANSACTION

CREATE TABLE TodoItems
(
	Id         int identity (1,1),
	Todo       varchar(256) not null,
	IsComplete bit          not null

	CONSTRAINT pk_TodoItems PRIMARY KEY (id)
);

COMMIT TRANSACTION
