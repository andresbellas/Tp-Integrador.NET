CREATE DATABASE Restaurante;
GO

USE Restaurante;
GO

CREATE TABLE Rol (
    Id_Rol INT PRIMARY KEY IDENTITY(1,1),
    Nombre_Rol VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(255)
    Baja BIT NOT NULL DEFAULT 0,
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


INSERT INTO Estado (Nombre_Estado, Descripcion) VALUES
('Libre', 'Mesa disponible para ser ocupada'),     
('Ocupado', 'Mesa actualmente ocupada');   


INSERT INTO Mesa (Nro_Mesa, Nro_Legajo, Id_Estado) VALUES
(1, NULL, 1),
(2, NULL, 1),
(3, NULL, 1),
(4, NULL, 1),
(5, NULL, 1);

-- Mesas ocupadas (atendidas por Andrés Bellas, legajo 1002)
INSERT INTO Mesa (Nro_Mesa, Nro_Legajo, Id_Estado) VALUES
(6, 1002, 2),
(7, 1002, 2),
(8, 1002, 2),
(9, 1002, 2),
(10, 1002, 2);

INSERT INTO MedioDePago (Nombre_Pago, Descripcion) VALUES
('Efectivo', 'Pago en efectivo'),
('Tarjeta de Crédito', 'Pago con tarjeta de crédito'),
('Débito', 'Pago con tarjeta de débito');

-- pedidos asociados a mesas ocupadas
INSERT INTO Pedidos (Nro_Pedido, Fecha_Pedido, Id_Estado, Id_Mesa) VALUES
(1001, '2025-06-27', 2, 6),
(1002, '2025-06-27', 2, 7),
(1003, '2025-06-27', 2, 8),
(1004, '2025-06-27', 2, 9),
(1005, '2025-06-27', 2, 10);


INSERT INTO Insumos (Sku, Nombre, Precio, Cantidad) VALUES
('BUR001', 'Hamburguesa Simple', 2000, 50),
('PAP001', 'Papas Fritas', 1500, 100),
('REF001', 'Refresco', 1000, 200);


INSERT INTO ItemPedidos (Id_Pedido, Sku, Cantidad, Total) VALUES
(1, 'BUR001', 1, 2000),
(1, 'REF001', 1, 1000);

-- pedido 1002
INSERT INTO ItemPedidos (Id_Pedido, Sku, Cantidad, Total) VALUES
(2, 'PAP001', 1, 1500),
(2, 'REF001', 2, 2000);

-- pedido 1003
INSERT INTO ItemPedidos (Id_Pedido, Sku, Cantidad, Total) VALUES
(3, 'BUR001', 2, 4000);

-- pedido 1004
INSERT INTO ItemPedidos (Id_Pedido, Sku, Cantidad, Total) VALUES
(4, 'PAP001', 1, 1500),
(4, 'BUR001', 1, 2000);

-- pedido 1005
INSERT INTO ItemPedidos (Id_Pedido, Sku, Cantidad, Total) VALUES
(5, 'REF001', 3, 3000);


INSERT INTO Cobranza (Id_Pago, Id_Pedido, Total) VALUES
(1, 1, 3000),   -- efectivo, pedido 1001
(2, 2, 3500);   -- tarjeta de crédito, pedido 1002
