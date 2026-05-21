USE CRM_Pruebas;
GO

IF OBJECT_ID('dbo.Vehiculos','U') IS NOT NULL
BEGIN
	DROP TABLE Vehiculos;
END;

CREATE TABLE Vehiculos (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdExterno NVARCHAR(100) NOT NULL,
	Vin NVARCHAR(20) NOT NULL,
	Placas NVARCHAR(20) NULL,
	Marca VARCHAR(20) NULL,
	Modelo VARCHAR(20) NULL,
	Anio INT NULL,
	Color VARCHAR(20) NULL,
	Estado VARCHAR(20) NULL,
	FechaRegistro DATETIME2 NOT NULL DEFAULT GETDATE(),
	FechaActualizacion DATETIME2 NULL
);

CREATE UNIQUE INDEX IX_Vehiculos_Vin
ON Vehiculos(Vin);

CREATE TABLE VehiculoWebhookQueue (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdExterno NVARCHAR(100) NOT NULL,
	Vin NVARCHAR(20) NOT NULL,
	Carga NVARCHAR(MAX) NOT NULL,
	Estado VARCHAR(20) NOT NULL,
	ContadorReintentos INT NOT NULL DEFAULT 0,
	MensajeError VARCHAR(1000) NOT NULL,
	FechaRecepcion DATETIME2 NOT NULL DEFAULT GETDATE(),
	FechaProcesamiento DATETIME2 NULL,
	FechaFinalizacion DATETIME2 NULL
);

CREATE UNIQUE INDEX IX_VehiculoWebhookQueue_IdExterno
ON VehiculoWebhookQueue(IdExterno);