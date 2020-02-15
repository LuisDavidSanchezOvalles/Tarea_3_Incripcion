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
Balance decimal,
);

Create Table Inscripciones
(
InscripcionId int primary key identity,
Fecha dateTime default GetDate(),
PersonaId int,
Comentarios varchar(max),
Monto decimal,
Deposito decimal,
Balance decimal,
);

select * from Personas
select * from Inscripciones

drop Table Personas
drop Table Inscripciones