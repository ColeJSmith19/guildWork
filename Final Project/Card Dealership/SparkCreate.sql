USE master
GO
if exists (select * from sysdatabases where name='Spark')
		drop database Spark
go

CREATE DATABASE Spark
GO