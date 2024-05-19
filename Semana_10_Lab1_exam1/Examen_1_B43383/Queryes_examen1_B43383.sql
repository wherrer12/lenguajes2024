Create database examen_I


use [examen_I]
go

create table users(
	Nombre varchar(50) not null,
	Correo varchar(50) not null,
	Clave varchar(25) not null,
	Rol varchar(15) not null
);

insert into users (Nombre,Correo,Clave,Rol) values ('Jorge Cruz Cruz','jcruz@gmail.com',41963,'Admin');
insert into users (Nombre,Correo,Clave,Rol) values ('Jinnet Vega Marin','jvega@gmail.com',74934,'Admin');

drop table users;

select * from users;