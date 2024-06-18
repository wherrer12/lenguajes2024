-- Queries clase 27, semana 14 (11 de junio del 2024)
-- Seleccion de base de datos
use [PUNTOVENTASG1]
go

-- Vista a utilizar para generar el reporte
create or alter view [Vp_Catalogo_Usuarios]
as
select u.Email, u.NombreCompleto, u.Password, u.FechaRegistro, u.Estado
from Usuarios u
go

-- Se utiliza la vista
select * from [Vp_Catalogo_Usuarios] order by Email;