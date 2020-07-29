CREATE DATABASE MvcCRUDDB

CREATE TABLE Employee(
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Position] [varchar](50) NULL,
	[Office] [varchar](50) NULL,
	[Age] [int] NULL,
	[Salary] [int] NULL,
	PRIMARY KEY(EmployeeID)
);