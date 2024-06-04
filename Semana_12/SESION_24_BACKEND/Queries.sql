-- CREACION BASE DE DATOS

USE master
GO

-- SE CONSULTA SI EXISTE UNA BD CREADA CON EL NOMBRE PUNTOVENTASG1
IF EXISTS (SELECT NAME FROM DBO.SYSDATABASES WHERE NAME = 'PUNTOVENTASG1')
DROP DATABASE [PUNTOVENTASG1] -- SIEXISTE SE BORRA
GO
CREATE DATABASE [PUNTOVENTASG1] -- SE CREA LA BD
GO

-- SE DEBE SELECCIONAR LA BD
USE [PUNTOVENTASG1]
GO

-- SE CREA LA TABLA PARA ALMACENAR LOS DATOS DE LOS USURIOS
IF EXISTS (SELECT NAME FROM DBO.SYSOBJECTS WHERE NAME = 'Usuarios')
DROP TABLE [Usuarios]
GO
CREATE TABLE [Usuarios](
	Email varchar (150) not null primary key,
	NombreCompleto varchar (150) not null,
	Password varchar (150) not null,
	FechaRegistro datetime not null default getdate(),
	Estado char not null default 'a')
go

-- Procedimiento almacenado encargado de consultar un usuario
if exists (select name from dbo.sysobjects where name = '[Sp_Cns_Usuario]')
drop procedure [Sp_Cns_Usuario]
go

create procedure [Sp_Cns_Usuario](
	@pEmail varchar (150),
	@pPassword varchar (150))
as
select u.Email, u.NombreCompleto, u.Password, u.FechaRegistro, u.Estado
	from Usuarios u
	where rtrim(ltrim(u.Email)) = @pEmail and rtrim(ltrim(u.Password)) = @pPassword
go

-- Se almacenan las credenciales del admin
insert into Usuarios (Email,NombreCompleto,Password)
values ('admin@gmail.com','Administrador','admin')
go
insert into Usuarios (Email,NombreCompleto,Password)
values ('jcruz@gmail.com','Administrador',123456)
go

-- 
select * from Usuarios