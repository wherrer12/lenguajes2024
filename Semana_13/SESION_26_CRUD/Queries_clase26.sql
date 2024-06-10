-- SE DEBE SELECCIONAR LA BD
USE [PUNTOVENTASG1]
GO

-- Sintaxis para crear un procedimiento almacenado
create or alter procedure [Sp_Cns_Usuario](
@pEmail varchar(150),
@pPassword varchar(100))
as
select u.Email,U.NombreCompleto,U.Password,U.FechaRegistro,U.Estado
from Usuarios u 
where rtrim(ltrim(u.Email)) = @pEmail 
and rtrim(ltrim(u.Password)) =@pPassword
go

--procedimiento para registrar un usuario
create or alter procedure [Sp_Ins_Usuarios](
@Email varchar(150),
@NombreComp varchar(200),
@Passwd varchar(200))
as
insert into Usuarios(Email,NombreCompleto,Password)
values (@Email,@NombreComp,@Passwd)
go

-- procedimiento para modificar datos a un usuario
create or alter procedure [Sp_Upd_Usuarios](
@Email varchar(150),
@NombreComp varchar(200),
@Passwd varchar(200))
as
update USUARIOS set
NOMBRECOMPLETO = @NombreComp,
PASSWORD = @Passwd
where EMAIL = @Email
go

