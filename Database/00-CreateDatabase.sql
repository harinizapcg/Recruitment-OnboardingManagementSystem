-- Create Database Script
-- Replace 'YourDatabaseName' with your actual database name

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'YourDatabaseName')
BEGIN
    CREATE DATABASE [YourDatabaseName];
    PRINT 'Database created successfully!';
END
ELSE
BEGIN
    PRINT 'Database already exists.';
END
GO

USE [YourDatabaseName];
GO
