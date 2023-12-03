CREATE DATABASE AgenciaViajes;
GO

USE AgenciaViajes;
GO

-- Crear tabla de Usuarios
CREATE TABLE Usuarios (
    UsuarioID INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(50),
    Email NVARCHAR(100),
    Contrasena NVARCHAR(100),
    FechaRegistro DATETIME DEFAULT GETDATE(),
    TipoUsuario NVARCHAR(50)
);
GO

-- Crear tabla de Destinos
CREATE TABLE Destinos (
    DestinoID INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100),
    Pais NVARCHAR(100),
    ZonaGeografica NVARCHAR(100),
    Descripcion NVARCHAR(MAX),
    ImagenURL NVARCHAR(MAX)
);
GO

-- Crear tabla de Actividades/Excursiones
CREATE TABLE Actividades (
    ActividadID INT PRIMARY KEY IDENTITY,
    DestinoID INT FOREIGN KEY REFERENCES Destinos(DestinoID),
    Nombre NVARCHAR(100),
    Descripcion NVARCHAR(MAX),
    Precio DECIMAL(10, 2),
    Duracion NVARCHAR(50) -- Ejemplo: '3 horas', '1 día'
);
GO

-- Crear tabla de Búsquedas
CREATE TABLE Busquedas (
    BusquedaID INT PRIMARY KEY IDENTITY,
    UsuarioID INT FOREIGN KEY REFERENCES Usuarios(UsuarioID),
    FechaBusqueda DATETIME,
    ParametrosBusqueda NVARCHAR(MAX) -- JSON con los filtros aplicados
);
GO

-- Crear tabla de Búsquedas_Destinos (Relación muchos a muchos)
CREATE TABLE Busquedas_Destinos (
    BusquedaID INT FOREIGN KEY REFERENCES Busquedas(BusquedaID),
    DestinoID INT FOREIGN KEY REFERENCES Destinos(DestinoID),
    PRIMARY KEY (BusquedaID, DestinoID)
);
GO

-- Crear tabla de Destinos_Aleatorios
CREATE TABLE Destinos_Aleatorios (
    DestinoAleatorioID INT PRIMARY KEY IDENTITY,
    DestinoID INT FOREIGN KEY REFERENCES Destinos(DestinoID),
    FechaGeneracion DATETIME
);
GO

SELECT * FROM Usuarios;
SELECT * FROM Destinos;
SELECT * FROM Destinos_Aleatorios;
SELECT * FROM Actividades;
SELECT * FROM Busquedas;
SELECT * FROM Busquedas_Destinos;
GO

ALTER TABLE Usuarios
ALTER COLUMN FechaRegistro DATETIME NULL;
GO

ALTER TABLE Usuarios
ADD CONSTRAINT DF_FechaRegistro DEFAULT GETDATE() FOR FechaRegistro;
GO

INSERT INTO Destinos (Nombre, Pais, ZonaGeografica, Descripcion, ImagenURL)
VALUES
    ('Ciudad de Nueva York', 'Estados Unidos', 'América del Norte', 'Una ciudad icónica con rascacielos famosos.', 'imagen1.jpg'),
    ('París', 'Francia', 'Europa', 'La Ciudad de la Luz con la Torre Eiffel.', 'imagen2.jpg'),
    ('Kioto', 'Japón', 'Asia', 'Una ciudad histórica con templos antiguos.', 'imagen3.jpg');
GO


-- Insertar actividades para el destino Ciudad de Nueva York
INSERT INTO Actividades (DestinoID, Nombre, Descripcion, Precio, Duracion)
VALUES
    (1, 'Recorrido por Times Square', 'Explora la animada Times Square en un recorrido guiado.', 50.00, '3 horas'),
    (1, 'Tour por el Museo Metropolitano de Arte', 'Visita uno de los museos más grandes del mundo.', 40.00, '4 horas');

-- Insertar actividades para el destino París
INSERT INTO Actividades (DestinoID, Nombre, Descripcion, Precio, Duracion)
VALUES
    (2, 'Visita a la Torre Eiffel', 'Contempla las vistas panorámicas desde la icónica Torre Eiffel.', 60.00, '2 horas'),
    (2, 'Tour por el Louvre', 'Explora el famoso Museo del Louvre y su colección de arte.', 45.00, '3 horas');

-- Insertar actividades para el destino Kioto
INSERT INTO Actividades (DestinoID, Nombre, Descripcion, Precio, Duracion)
VALUES
    (3, 'Visita al Templo Kinkaku-ji', 'Descubre el Templo del Pabellón Dorado en Kioto.', 35.00, '2 horas'),
    (3, 'Tour por el Bosque de Bambú de Arashiyama', 'Pasea por el hermoso bosque de bambú de Arashiyama.', 30.00, '3 horas');
GO

-- Insertar una búsqueda para un usuario (reemplaza 'X' con el ID de usuario real)
INSERT INTO Busquedas (UsuarioID, FechaBusqueda, ParametrosBusqueda)
VALUES
    (4, GETDATE(), '{"Destino": "Ciudad de Nueva York", "PrecioMax": 100.00, "DuracionMin": "2 horas"}');
GO

SELECT * FROM Destinos;
SELECT * FROM Actividades;
SELECT * FROM Usuarios;
SELECT * FROM Busquedas;


INSERT INTO Usuarios(Nombre, Email, Contrasena, TipoUsuario) VALUES('Administrador', 'admin@example.com', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 'Administrador');