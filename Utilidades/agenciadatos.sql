-- Crear la base de datos
CREATE DATABASE [AgenciaViajes];

-- Cambiar al contexto de la base de datos
USE [AgenciaViajes];


-- Crear la tabla Destinos
CREATE TABLE [dbo].[Destinos](
    [DestinoID] [int] IDENTITY(1,1) NOT NULL,
    [Nombre] [nvarchar](100) NULL,
    [Pais] [nvarchar](100) NULL,
    [ZonaGeografica] [nvarchar](100) NULL,
    [Descripcion] [nvarchar](max) NULL,
    [ImagenURL] [nvarchar](max) NULL,
    PRIMARY KEY CLUSTERED ([DestinoID] ASC)
);

-- Crear la tabla Actividades
CREATE TABLE [dbo].[Actividades](
    [ActividadID] [int] IDENTITY(1,1) NOT NULL,
    [DestinoID] [int] NULL,
    [Nombre] [nvarchar](100) NULL,
    [Descripcion] [nvarchar](max) NULL,
    [Precio] [decimal](10, 2) NULL,
    [Duracion] [nvarchar](50) NULL,
    PRIMARY KEY CLUSTERED ([ActividadID] ASC),
    FOREIGN KEY([DestinoID]) REFERENCES [dbo].[Destinos]([DestinoID])
);


-- Crear la tabla Usuarios
CREATE TABLE [dbo].[Usuarios](
    [UsuarioID] [int] IDENTITY(1,1) NOT NULL,
    [Nombre] [nvarchar](50) NULL,
    [Email] [nvarchar](100) NULL,
    [Contrasena] [nvarchar](100) NULL,
    [FechaRegistro] [datetime] DEFAULT (getdate()),
    [TipoUsuario] [nvarchar](50) NULL,
    PRIMARY KEY CLUSTERED ([UsuarioID] ASC)
);



-- Crear la tabla Busquedas
CREATE TABLE [dbo].[Busquedas](
    [BusquedaID] [int] IDENTITY(1,1) NOT NULL,
    [UsuarioID] [int] NULL,
    [FechaBusqueda] [datetime] NULL,
    [ParametrosBusqueda] [nvarchar](max) NULL,
    PRIMARY KEY CLUSTERED ([BusquedaID] ASC),
    FOREIGN KEY([UsuarioID]) REFERENCES [dbo].[Usuarios]([UsuarioID])
);

-- Crear la tabla Busquedas_Destinos
CREATE TABLE [dbo].[Busquedas_Destinos](
    [BusquedaID] [int] NOT NULL,
    [DestinoID] [int] NOT NULL,
    PRIMARY KEY CLUSTERED ([BusquedaID] ASC, [DestinoID] ASC),
    FOREIGN KEY([BusquedaID]) REFERENCES [dbo].[Busquedas]([BusquedaID]),
    FOREIGN KEY([DestinoID]) REFERENCES [dbo].[Destinos]([DestinoID])
);


-- Crear la tabla Destinos_Aleatorios
CREATE TABLE [dbo].[Destinos_Aleatorios](
    [DestinoAleatorioID] [int] IDENTITY(1,1) NOT NULL,
    [DestinoID] [int] NULL,
    [FechaGeneracion] [datetime] NULL,
    PRIMARY KEY CLUSTERED ([DestinoAleatorioID] ASC),
    FOREIGN KEY([DestinoID]) REFERENCES [dbo].[Destinos]([DestinoID])
);

SET IDENTITY_INSERT [dbo].[Destinos] ON;

INSERT [dbo].[Destinos] ([DestinoID], [Nombre], [Pais], [ZonaGeografica], [Descripcion], [ImagenURL]) VALUES (1, N'Ciudad de Nueva York', N'Estados Unidos', N'América del Norte', N'Nueva York incluye 5 distritos que se ubican donde el río Hudson desemboca en el océano Atlántico. En su centro se encuentra Manhattan, un distrito densamente poblado que se encuentra entre los principales centros comerciales, financieros y culturales del mundo.', N'/uploads/imagen1.jpg')
INSERT [dbo].[Destinos] ([DestinoID], [Nombre], [Pais], [ZonaGeografica], [Descripcion], [ImagenURL]) VALUES (2, N'París', N'Francia', N'Europa', N'París, la capital de Francia, es una importante ciudad europea y un centro mundial del arte, la moda, la gastronomía y la cultura. Su paisaje urbano del siglo XIX está entrecruzado por amplios bulevares y el río Sena. Aparte de estos hitos, como la Torre Eiffel y la catedral gótica de Notre Dame del siglo XII, la ciudad es famosa por su cultura del café y las tiendas de moda de diseñador a lo largo de la calle Rue du Faubourg Saint-Honoré.', N'/uploads/francia.jpg')
INSERT [dbo].[Destinos] ([DestinoID], [Nombre], [Pais], [ZonaGeografica], [Descripcion], [ImagenURL]) VALUES (3, N'Kioto', N'Japón', N'Asia', N'Kioto, que alguna vez fue la capital de Japón, es una ciudad de la isla de Honshu. Es famosa por sus numerosos templos budistas clásicos y sus jardines, palacios imperiales, santuarios Shinto y casas de madera tradicionales. También es conocida por tradiciones formales, como las comidas kaiseki, que constan de varios platos de preparaciones distintivas, y las geishas, artistas femeninas que se encuentran comúnmente en el distrito Gion. ', N'/uploads/kiotojpg.jpg')
INSERT [dbo].[Destinos] ([DestinoID], [Nombre], [Pais], [ZonaGeografica], [Descripcion], [ImagenURL]) VALUES (4, N'cancun', N'Mexico', N'America', N'Cancún es una ciudad de México ubicada en la península de Yucatán que limita con el mar Caribe y que es conocida por sus playas, los numerosos centros turísticos y la vida nocturna. Se compone de 2 áreas distintas: el área del centro más tradicional y la Zona Hotelera, la franja costera con hoteles altos, clubes nocturnos, tiendas y restaurantes.', N'/uploads/cancun.jpg')
INSERT [dbo].[Destinos] ([DestinoID], [Nombre], [Pais], [ZonaGeografica], [Descripcion], [ImagenURL]) VALUES (5, N'roma', N'Italia', N'Europa', N'Roma, la capital de Italia, es una extensa ciudad cosmopolita que tiene a la vista casi 3,000 años de arte, arquitectura y cultura de influencia mundial. Las ruinas antiguas como las del Foro y el Coliseo evocan el poder del antiguo Imperio Romano. La ciudad del Vaticano, sede central de la Iglesia católica romana, cuenta con la Basílica de San Pedro y los Museos del Vaticano, que albergan obras maestras como los frescos de la Capilla Sixtina de Miguel Ángel. ', N'/uploads/roma.jpeg')
INSERT [dbo].[Destinos] ([DestinoID], [Nombre], [Pais], [ZonaGeografica], [Descripcion], [ImagenURL]) VALUES (6, N'Cartagena', N'Colombia', N'America del sur', N'Cartagena es una ciudad portuaria en la costa caribeña de Colombia. Junto al mar, se encuentra la Ciudad Vieja amurallada, que se fundó en el siglo XVI, con plazas, calles de adoquines y edificios coloniales coloridos. Con un clima tropical, la ciudad también es un destino popular por sus playas. Se puede llegar en bote a la Isla de Barú, con playas de arena blanca y palmeras, y a las Islas del Rosario, famosas por sus arrecifes de coral. ', N'/uploads/cartagena.jpg')
INSERT [dbo].[Destinos] ([DestinoID], [Nombre], [Pais], [ZonaGeografica], [Descripcion], [ImagenURL]) VALUES (7, N'Mashupishu', N'Peru', N'America del sur', N'achu Picchu es una ciudadela inca ubicada en las alturas de las montañas de los Andes en Perú, sobre el valle del río Urubamba. Se construyó en el siglo XV y luego fue abandonada, y es famosa por sus sofisticadas paredes de piedra seca que combinan enormes bloques sin el uso de un mortero, los edificios fascinantes que se relacionan con las alineaciones astronómicas y sus vistas panorámicas. El uso exacto que tuvo sigue siendo un misterio.', N'/uploads/perujpg.jpg')
INSERT [dbo].[Destinos] ([DestinoID], [Nombre], [Pais], [ZonaGeografica], [Descripcion], [ImagenURL]) VALUES (8, N'Hollywood ', N'Estados unidos', N'Norte America', N'Hollywood, un símbolo de la industria del entretenimiento desde tiempos inmemorables, atrae a los turistas con sitios icónicos como el TCL Chinese Theatre y el Paseo de la Fama. Entre otras atracciones se encuentran Paramount Pictures, recintos musicales históricos como el Hollywood Bowl y el Dolby Theatre, donde se realiza la ceremonia de los Premios Óscar', N'/uploads/hollywod.jpg')
INSERT [dbo].[Destinos] ([DestinoID], [Nombre], [Pais], [ZonaGeografica], [Descripcion], [ImagenURL]) VALUES (9, N'Roatan', N'Honduras', N'Centro America', N'Roatán es una de las Islas de la Bahía de Honduras en el Caribe. Forma parte del enorme Sistema Arrecifal Mesoamericano y es conocida por las playas, los sitios de buceo y la fauna marina, incluido el tiburón ballena. En el suroeste está la concurrida playa de West Bay, con un arrecife de coral cerca de la costa. ', N'/uploads/roatan.jpg')
INSERT [dbo].[Destinos] ([DestinoID], [Nombre], [Pais], [ZonaGeografica], [Descripcion], [ImagenURL]) VALUES (10, N'Semuc Champey', N'Guatemala', N'Centro America', N'Semuc Champey qué significa, es un enclave natural localizado en el municipio guatemalteco de Lanquín, en el departamento de Alta Verapaz, Guatemala.', N'/uploads/semuc.jpg')
INSERT [dbo].[Destinos] ([DestinoID], [Nombre], [Pais], [ZonaGeografica], [Descripcion], [ImagenURL]) VALUES (11, N'Rio Dulce', N'Guatemala', N'Centro', N'El río Dulce se encuentra en el departamento de Izabal, Guatemala, entre el lago de Izabal y la bahía de Amatique de alrededor de 43 km de largo. Desde 1955 ha sido protegido mediante el establecimiento del parque nacional Río Dulce, que protege unos 130 km² junto a las orillas del río y El Golfete', N'/uploads/riodulce.jpg')
INSERT [dbo].[Destinos] ([DestinoID], [Nombre], [Pais], [ZonaGeografica], [Descripcion], [ImagenURL]) VALUES (12, N'Madrid', N'España ', N'Europa', N'Madrid es un municipio y una ciudad de España. La localidad, con categoría histórica de villa, ​ es la capital del Estado​ y de la Comunidad de Madrid.', N'/uploads/madrid.jpg')
 
SET IDENTITY_INSERT [dbo].[Destinos] OFF;

SET IDENTITY_INSERT [dbo].[Actividades] ON;

INSERT [dbo].[Actividades] ([ActividadID], [DestinoID], [Nombre], [Descripcion], [Precio], [Duracion]) VALUES (1, 1, N'Recorrido por Times Square', N'Explora la animada Times Square en un recorrido guiado.', CAST(50.00 AS Decimal(10, 2)), N'3 horas')
INSERT [dbo].[Actividades] ([ActividadID], [DestinoID], [Nombre], [Descripcion], [Precio], [Duracion]) VALUES (3, 2, N'Visita a la Torre Eiffel', N'Contempla las vistas panorámicas desde la icónica Torre Eiffel.', CAST(60.00 AS Decimal(10, 2)), N'2 horas')
INSERT [dbo].[Actividades] ([ActividadID], [DestinoID], [Nombre], [Descripcion], [Precio], [Duracion]) VALUES (6, 3, N'Tour por el Bosque de Bambú de Arashiyama', N'Pasea por el hermoso bosque de bambú de Arashiyama.', CAST(30.00 AS Decimal(10, 2)), N'3 horas')
INSERT [dbo].[Actividades] ([ActividadID], [DestinoID], [Nombre], [Descripcion], [Precio], [Duracion]) VALUES (7, 4, N'Snorket', N'Snorkel aventura exatrema', CAST(25.00 AS Decimal(10, 2)), N'2 horas')
INSERT [dbo].[Actividades] ([ActividadID], [DestinoID], [Nombre], [Descripcion], [Precio], [Duracion]) VALUES (8, 11, N'LancheXtreme', N'paseo en lancha extrema', CAST(16.00 AS Decimal(10, 2)), N'2 horas')
INSERT [dbo].[Actividades] ([ActividadID], [DestinoID], [Nombre], [Descripcion], [Precio], [Duracion]) VALUES (9, 12, N'TourBernabeu', N'Visita al Estadio Santiago Bernabeu del Real Madrid', CAST(110.00 AS Decimal(10, 2)), N'3 horas')
INSERT [dbo].[Actividades] ([ActividadID], [DestinoID], [Nombre], [Descripcion], [Precio], [Duracion]) VALUES (10, 6, N'Pueblos Magicos', N'turismo en pueblos magicos recorrido', CAST(26.00 AS Decimal(10, 2)), N'3 horas')
INSERT [dbo].[Actividades] ([ActividadID], [DestinoID], [Nombre], [Descripcion], [Precio], [Duracion]) VALUES (11, 5, N'Paseo por la Ciudad', N'paseo por las calles principales de Italia visita a tempos antiguos', CAST(45.00 AS Decimal(10, 2)), N'1 hora')
INSERT [dbo].[Actividades] ([ActividadID], [DestinoID], [Nombre], [Descripcion], [Precio], [Duracion]) VALUES (12, 10, N'Caminata Rumbo a la Montaña ', N'caminata de 20 KM', CAST(10.00 AS Decimal(10, 2)), N'4 horas')
INSERT [dbo].[Actividades] ([ActividadID], [DestinoID], [Nombre], [Descripcion], [Precio], [Duracion]) VALUES (13, 8, N'Tour por la Ciudad', N'Tour por Universal Studios', CAST(60.00 AS Decimal(10, 2)), N'2 horas')
INSERT [dbo].[Actividades] ([ActividadID], [DestinoID], [Nombre], [Descripcion], [Precio], [Duracion]) VALUES (15, 7, N'Caminata Montañesa ', N'Caminata de 2 horas y media', CAST(23.00 AS Decimal(10, 2)), N'4 horas')

SET IDENTITY_INSERT [dbo].[Actividades] OFF;


SET IDENTITY_INSERT [dbo].[Usuarios] ON;

INSERT [dbo].[Usuarios] ([UsuarioID], [Nombre], [Email], [Contrasena], [FechaRegistro], [TipoUsuario]) VALUES (1, N'Kevin', N'kevin.pes2011@hotmail.com', N'966a42659a02191aac2b9ccefd4c82c6b09bf7be50feab0ce95cef31f7e3b79c', NULL, N'Registrado')
INSERT [dbo].[Usuarios] ([UsuarioID], [Nombre], [Email], [Contrasena], [FechaRegistro], [TipoUsuario]) VALUES (2, N'sofia', N'sofia@gmail.com', N'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', NULL, N'Invitado')
INSERT [dbo].[Usuarios] ([UsuarioID], [Nombre], [Email], [Contrasena], [FechaRegistro], [TipoUsuario]) VALUES (3, N'Administrador', N'admin@example.com', N'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', CAST(N'2023-12-03T00:45:27.410' AS DateTime), N'Administrador')

SET IDENTITY_INSERT [dbo].[Usuarios] OFF;


SET IDENTITY_INSERT [dbo].[Busquedas] ON;

INSERT [dbo].[Busquedas] ([BusquedaID], [UsuarioID], [FechaBusqueda], [ParametrosBusqueda]) VALUES (5, 1, CAST(N'2023-12-03T00:58:00.000' AS DateTime), N'cancun')
INSERT [dbo].[Busquedas] ([BusquedaID], [UsuarioID], [FechaBusqueda], [ParametrosBusqueda]) VALUES (6, 1, CAST(N'2023-11-09T00:58:00.000' AS DateTime), N'Nueva York')
INSERT [dbo].[Busquedas] ([BusquedaID], [UsuarioID], [FechaBusqueda], [ParametrosBusqueda]) VALUES (7, 1, CAST(N'2023-12-03T00:59:00.000' AS DateTime), N'Japon')
INSERT [dbo].[Busquedas] ([BusquedaID], [UsuarioID], [FechaBusqueda], [ParametrosBusqueda]) VALUES (9, 1, CAST(N'2023-12-03T01:28:00.000' AS DateTime), N'paris')
INSERT [dbo].[Busquedas] ([BusquedaID], [UsuarioID], [FechaBusqueda], [ParametrosBusqueda]) VALUES (10, 2, CAST(N'2023-11-08T02:52:00.000' AS DateTime), N'Montaña ')
INSERT [dbo].[Busquedas] ([BusquedaID], [UsuarioID], [FechaBusqueda], [ParametrosBusqueda]) VALUES (11, 3, CAST(N'2023-11-15T02:52:00.000' AS DateTime), N'Ciudad')
INSERT [dbo].[Busquedas] ([BusquedaID], [UsuarioID], [FechaBusqueda], [ParametrosBusqueda]) VALUES (12, 2, CAST(N'2023-11-26T02:53:00.000' AS DateTime), N'Playa')
INSERT [dbo].[Busquedas] ([BusquedaID], [UsuarioID], [FechaBusqueda], [ParametrosBusqueda]) VALUES (13, 3, CAST(N'2023-11-16T02:53:00.000' AS DateTime), N'Cayala')
INSERT [dbo].[Busquedas] ([BusquedaID], [UsuarioID], [FechaBusqueda], [ParametrosBusqueda]) VALUES (14, 1, CAST(N'2023-11-21T02:54:00.000' AS DateTime), N'Madrid')
INSERT [dbo].[Busquedas] ([BusquedaID], [UsuarioID], [FechaBusqueda], [ParametrosBusqueda]) VALUES (15, 3, CAST(N'2023-12-01T02:54:00.000' AS DateTime), N'Hollywod')

SET IDENTITY_INSERT [dbo].[Busquedas] OFF;



SET IDENTITY_INSERT [dbo].[Destinos_Aleatorios] ON;

INSERT [dbo].[Destinos_Aleatorios] ([DestinoAleatorioID], [DestinoID], [FechaGeneracion]) VALUES (3, 4, CAST(N'2023-12-03T00:53:00.000' AS DateTime))
INSERT [dbo].[Destinos_Aleatorios] ([DestinoAleatorioID], [DestinoID], [FechaGeneracion]) VALUES (4, 1, CAST(N'2023-12-03T01:29:00.000' AS DateTime))
INSERT [dbo].[Destinos_Aleatorios] ([DestinoAleatorioID], [DestinoID], [FechaGeneracion]) VALUES (5, 3, CAST(N'2023-11-10T13:29:00.000' AS DateTime))
INSERT [dbo].[Destinos_Aleatorios] ([DestinoAleatorioID], [DestinoID], [FechaGeneracion]) VALUES (6, 2, CAST(N'2023-12-02T01:29:00.000' AS DateTime))
INSERT [dbo].[Destinos_Aleatorios] ([DestinoAleatorioID], [DestinoID], [FechaGeneracion]) VALUES (7, 12, CAST(N'2023-11-21T02:54:00.000' AS DateTime))
INSERT [dbo].[Destinos_Aleatorios] ([DestinoAleatorioID], [DestinoID], [FechaGeneracion]) VALUES (8, 11, CAST(N'2023-11-07T02:55:00.000' AS DateTime))
INSERT [dbo].[Destinos_Aleatorios] ([DestinoAleatorioID], [DestinoID], [FechaGeneracion]) VALUES (9, 9, CAST(N'2023-11-19T02:55:00.000' AS DateTime))
INSERT [dbo].[Destinos_Aleatorios] ([DestinoAleatorioID], [DestinoID], [FechaGeneracion]) VALUES (10, 6, CAST(N'2023-11-29T14:55:00.000' AS DateTime))
INSERT [dbo].[Destinos_Aleatorios] ([DestinoAleatorioID], [DestinoID], [FechaGeneracion]) VALUES (11, 7, CAST(N'2023-11-21T02:55:00.000' AS DateTime))
INSERT [dbo].[Destinos_Aleatorios] ([DestinoAleatorioID], [DestinoID], [FechaGeneracion]) VALUES (12, 5, CAST(N'2023-12-01T14:56:00.000' AS DateTime))
INSERT [dbo].[Destinos_Aleatorios] ([DestinoAleatorioID], [DestinoID], [FechaGeneracion]) VALUES (13, 10, CAST(N'2023-11-23T18:56:00.000' AS DateTime))
INSERT [dbo].[Destinos_Aleatorios] ([DestinoAleatorioID], [DestinoID], [FechaGeneracion]) VALUES (14, 5, CAST(N'2023-12-03T09:09:00.000' AS DateTime))

SET IDENTITY_INSERT [dbo].[Destinos_Aleatorios] OFF;
