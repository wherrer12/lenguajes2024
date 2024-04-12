select * from libros
go

-- sintaxis para almacenar libro
insert into Libros (Titulo, Editorial, PrecioVenta,Foto,FechaPublicacion,Estado)
values ('Estructura datos III', 'Imprenta la violeta',42500,'ND',getdate(),'A')
go
