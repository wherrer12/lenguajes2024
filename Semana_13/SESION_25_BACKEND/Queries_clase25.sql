-- SE DEBE SELECCIONAR LA BD
USE [PUNTOVENTASG1]
GO

-- Procedimiento almacenado para buscar usuarios por nombre
create procedure [Sp_Buscar_Usuarios](
@Nombre varchar(100))
as
select * 
from Usuarios
where NombreCompleto like '%' + RTRIM(ltrim(@Nombre)) + '%'
order by NombreCompleto
go

--Like lo que hace es compara dentro de la cadena de caracreres los valoeres que se estan enviando como parametro

-- Testing procedimiento almacenado
exec [Sp_Buscar_Usuarios] 'min'

select * from usuarios;

--Registro de mas usuarios
insert into Usuarios (Email,NombreCompleto,Password) values ('marco@gmail.com','Usuario','user')
insert into Usuarios (Email,NombreCompleto,Password) values ('antonio@gmail.com','Usuario','user')
insert into Usuarios (Email,NombreCompleto,Password) values ('rocio@gmail.com','Usuario','user')
go

-- Procedimiento almacenado encargado de eliminar el usuario
create or alter procedure [Sp_Del_Usuarios](
@Email varchar (100))
as 
delete from Usuarios
where rtrim(ltrim(Email)) = RTRIM(ltrim(@Email))
go
