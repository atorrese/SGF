CREATE DATABASE DBPRUEBAS
USE DBPRUEBAS
CREATE TABLE CONTACTO(
IdContacto int primary key identity,
Nombre varchar(40),
Apellido varchar(40),
Telefono varchar(40),
Correo varchar(40)
)

select * from CONTACTO