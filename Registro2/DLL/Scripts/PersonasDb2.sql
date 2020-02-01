Create DataBase PersonasDb2
Go

Use PersonasDb2
Go

Create Table Personas
(
PersonaId int primary key identity,
Nombres varchar(30),
Cedula varchar(13),
Telefono varchar(13),
Direccion varchar(max),
FechaNacimiento Date default GetDate(),
Balance int,
);

Create Table Inscripcion
(
InscripcionId int primary key identity,
Fecha dateTime default GetDate(),
PersonaId int,
Comentarios varchar(max),
Monto int,
Balance int,
);

select * from Personas

delete Personas where PersonaId = 3

drop Table Personas
drop Table Inscripcion