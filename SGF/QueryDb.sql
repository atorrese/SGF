CREATE DATABASE DBPRUEBAS
CREATE TABLE Contacto(
	IdContacto INT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(100),
	Apellido VARCHAR(100),
	Telefono VARCHAR(100),
	Correo VARCHAR(100)
)
SELECT * FROM Contacto
--truncate table Contacto