-- Creacion de DB
Create database examen_I

-- Ir a DB creada
use [examen_I]
go

-- Borrado de tabla por error
drop table [Usuarios]

-- sintaxis para crear los usuarios administradores
create table [Usuarios](
	Email varchar(50) not null primary key,
	NombreCompleto varchar(50) not null,
	Password varchar(25) not null)
go

-- Se inserta el usuario administrador
insert into Usuarios (Email,NombreCompleto,Password) values ('jcruz@gmail.com','Jorge Cruz Cruz',41963);
insert into Usuarios (Email,NombreCompleto,Password) values ('jvega@gmail.com','Jinnet Vega Marin',74934);
go

-- Se consultan los datos para los usuarios
select * from Usuarios
go
