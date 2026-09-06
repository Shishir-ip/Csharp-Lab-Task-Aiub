-- CreateCustomerDB.sql
-- Script to create the CustomerDB database and the Customers table
-- Run this script in SQL Server (e.g. using SQL Server Management Studio) on the machine where the app connects (\.\SQLEXPRESS for the default connection string in the project).

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'CustomerDB')
BEGIN
    ALTER DATABASE [CustomerDB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [CustomerDB];
END
GO

CREATE DATABASE [CustomerDB];
GO

USE [CustomerDB];
GO

IF OBJECT_ID('dbo.Customers', 'U') IS NOT NULL
    DROP TABLE dbo.Customers;
GO

CREATE TABLE dbo.Customers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    [Password] NVARCHAR(256) NULL,
    [Gender] NVARCHAR(50) NULL,
    [Category] NVARCHAR(50) NULL
);
GO

-- Ensure Name is unique since the application checks for existence by Name
CREATE UNIQUE INDEX IX_Customers_Name ON dbo.Customers([Name]);
GO

-- Sample/test data (optional)
INSERT INTO dbo.Customers([Name],[Password],[Gender],[Category]) VALUES
    (N'Alice', N'pass123', N'Female', N'Regular'),
    (N'Bob', N'password', N'Male', N'Irregular');
GO

-- Helpful query examples the application uses:
-- Check existence: SELECT COUNT(1) FROM Customers WHERE [Name] = @name
-- Insert: INSERT INTO Customers([Name],[Password],[Gender],[Category]) VALUES(@name,@password,@gender,@category)
-- Update: UPDATE Customers SET [Password]=@password, [Gender]=@gender, [Category]=@category WHERE [Name]=@name
-- Delete: DELETE FROM Customers WHERE [Name] = @name
-- Search: SELECT [Name],[Password],[Gender],[Category] FROM Customers WHERE [Name] = @name
