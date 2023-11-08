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
    FechaRegistro DATETIME,
    TipoUsuario NVARCHAR(50) -- Puede ser 'Invitado', 'Registrado', 'Administrador'
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