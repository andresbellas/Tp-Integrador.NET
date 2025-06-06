CREATE DATABASE Restaurante;
GO

USE Restaurante;
GO

CREATE TABLE Rol (
    Id_Rol INT PRIMARY KEY IDENTITY(1,1),
    Nombre_Rol VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(255)
);

CREATE TABLE Usuarios (
    Id_Usuario INT PRIMARY KEY IDENTITY(1,1),
    Usuario VARCHAR(100) NOT NULL UNIQUE,
    Contraseña VARCHAR(100) NOT NULL
);

CREATE TABLE Empleados (
    Id_Empleado INT PRIMARY KEY IDENTITY(1,1),
    Nro_Legajo SMALLINT NOT NULL UNIQUE,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Id_Usuario INT NOT NULL,
    Id_Rol INT NOT NULL,
    Baja BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (Id_Usuario) REFERENCES Usuarios(Id_Usuario),
    FOREIGN KEY (Id_Rol) REFERENCES Rol(Id_Rol)
);

CREATE TABLE Estado (
    Id_Estado INT PRIMARY KEY IDENTITY(1,1),
    Nombre_Estado VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(255)
);

CREATE TABLE Mesa (
    Id_Mesa INT PRIMARY KEY IDENTITY(1,1),
    Nro_Mesa SMALLINT NOT NULL,
    Nro_Legajo SMALLINT,
    Id_Estado INT NOT NULL,
    FOREIGN KEY (Nro_Legajo) REFERENCES Empleados(Nro_Legajo),
    FOREIGN KEY (Id_Estado) REFERENCES Estado(Id_Estado)
);

CREATE TABLE Insumos (
    Id_Insumos INT PRIMARY KEY IDENTITY(1,1),
    Sku VARCHAR(50) NOT NULL UNIQUE,
    Nombre VARCHAR(100) NOT NULL,
    Precio FLOAT,
    Cantidad SMALLINT NOT NULL DEFAULT 0
);

CREATE TABLE Pedidos (
    Id_Pedido INT PRIMARY KEY IDENTITY(1,1),
    Nro_Pedido SMALLINT NOT NULL,
    Fecha_Pedido DATE NOT NULL,
    Id_Estado INT NOT NULL,
    Id_Mesa INT NOT NULL,
    FOREIGN KEY (Id_Estado) REFERENCES Estado(Id_Estado),
    FOREIGN KEY (Id_Mesa) REFERENCES Mesa(Id_Mesa)
);

CREATE TABLE ItemPedidos (
    Id_Item INT PRIMARY KEY IDENTITY(1,1),
    Id_Pedido INT NOT NULL,
    Sku VARCHAR(50) NOT NULL,
    Cantidad SMALLINT NOT NULL,
    Total FLOAT NOT NULL,
    FOREIGN KEY (Id_Pedido) REFERENCES Pedidos(Id_Pedido),
    FOREIGN KEY (Sku) REFERENCES Insumos(Sku)
);

CREATE TABLE MedioDePago (
    Id_Pago INT PRIMARY KEY IDENTITY(1,1),
    Nombre_Pago VARCHAR(100) NOT NULL UNIQUE,
    Descripcion VARCHAR(255),
	Habilitado BIT NOT NULL DEFAULT 1 
);

CREATE TABLE Cobranza (
    Id_Cobranza INT PRIMARY KEY IDENTITY(1,1),
    Id_Pago INT NOT NULL,
    Id_Pedido INT NOT NULL,
    Total FLOAT NOT NULL,
    FOREIGN KEY (Id_Pago) REFERENCES MedioDePago(Id_Pago),
    FOREIGN KEY (Id_Pedido) REFERENCES Pedidos(Id_Pedido)
);



INSERT INTO Rol (Nombre_Rol, Descripcion)
VALUES 
('Gerente', 'Responsable general del restaurante'),
('Mesero', 'Atiende mesas y toma pedidos');


INSERT INTO Usuarios (Usuario, Contraseña) VALUES
('bularaiga', '1234'),
('abellas', '1234'),
('nmares', '1234');


INSERT INTO Empleados (Nro_Legajo, Nombre, Apellido, Id_Usuario, Id_Rol, Baja)
VALUES
(1001, 'Braian', 'Ulariaga', 1, 1, 0),  -- Gerente
(1002, 'Andres', 'Bellas', 2, 2, 0),    -- Mesero
(1003, 'Nicolas', 'Mares', 3, 2, 0);    -- Mesero
