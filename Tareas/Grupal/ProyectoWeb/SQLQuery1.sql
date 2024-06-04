--se crea la base de datos
create DataBase BdHotelBeachV2
go

--procedemos a utilizarla
use BdHotelBeachV2

--creamos la tabla cliente
create table Cliente(
Id int Primary key identity,
TipoCedula varchar(50),
Cedula varchar(50),
Nombre varchar(50),
Telefono int,
Direccion varchar(500),
Email varchar(50),
Clave varchar(50),
RolSistema varchar(50),
)

go

--en caso de que falle la creacion en FrameWork
create table Paquete(
Id int primary key identity,
nombrePaquete varchar(50),
coste decimal,
cantidadPersonas int,
FechaReserva DateTime,
cantidadNoches int,
formadePago varchar(50),
numeroCheque int,
montoTotal decimal,
Prima decimal,
cuotas int,
descuento decimal,
EmailUsuario varchar(50)
)
go

--creamos la tabla de usuarios
create table Usuario(
Email varchar(50) primary key,
NombreCompleto varchar(100),
Password varchar(50)
)
go

select * from Usuario
go

select * from Cliente
go

select * from Paquete
go