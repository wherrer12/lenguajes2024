-- Selección de base de datos grupo 1
use [BilbiotecaWebG1]
go

-- sintaxis para crear el catalogo de usuarios
create table [Usuarios] (
	Email varchar (150) not null primary key,
	NombreCompleto varchar (250) not null,
	Password varchar (200) not null,
	Restablecer char  not null default 'S',
	FechaRegistro datetime not null default getdate(),
	Estado char not null default 'A' )
go

-- Se inserta el usuario administrador
insert into Usuarios (Email, NombreCompleto,Password)
values ('admin@gmail.com', 'Administrador','admin')
go

-- Se consultan los datos para los usuarios
select * from Usuarios
go