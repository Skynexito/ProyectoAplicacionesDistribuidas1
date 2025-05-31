using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Servidor.Modelo.Base_de_datos
{
    public class ConexionDB
    {

        // Cadena de conexión a la base de datos SQL Server

        private SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-4RJH76T;Initial Catalog=ProyectoDistri; Integrated Security=True");

        // Métodos para abrir y cerrar la conexión a la base de datos

        public SqlConnection AbrirConexion()
        {
            if (conexion.State == ConnectionState.Closed) conexion.Open(); return conexion;
        }

        public SqlConnection CerrarConexion()
        {
            if (conexion.State == ConnectionState.Open) conexion.Close(); return conexion;
        }

    }

}
/* -- Script para crear la base de datos y las tablas necesarias para el sistema de votación, y procedimientos almacenados
 
CREATE DATABASE [ProyectoDistri];
GO

USE [ProyectoDistri];
GO
CREATE TABLE LOCALIDAD(
    Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    Nombre VARCHAR(255) NOT NULL,
    CantidadMesas INT NOT NULL
);
GO

CREATE TABLE MESA (
    Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    NumeroMesa INT NOT NULL,
    IdLocalidad INT NOT NULL,
	Estado BIT NOT NULL,
    FOREIGN KEY (IdLocalidad) REFERENCES LOCALIDAD(Id)
);
GO

CREATE TABLE OPCIONES(
    Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    NombreCandidato VARCHAR(255) NOT NULL
);
GO


CREATE TABLE VOTOS (
    Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    Cantidad INT NOT NULL,
    IdMesa INT NOT NULL,
    IdOpcion INT NOT NULL,
    FOREIGN KEY (IdMesa) REFERENCES MESA(Id),
    FOREIGN KEY (IdOpcion) REFERENCES OPCIONES(Id)
);
GO

CREATE TABLE ControlElecciones(
    ID INT PRIMARY KEY DEFAULT 1 CHECK (ID = 1), -- Solo permite ID = 1
    CantidadVotantes INT NOT NULL,
    FechaEleccion DATETIME NOT NULL
);
GO
-- Inserta el registro inicial si no existe
IF NOT EXISTS (SELECT 1 FROM ControlElecciones)
BEGIN
    INSERT INTO ControlElecciones (CantidadVotantes, FechaEleccion)
    VALUES (0, GETDATE()); -- Valores iniciales por defecto
END
GO

CREATE OR ALTER PROCEDURE ActualizarCantidadVotantesPorMesa
    @Cantidad INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Si la tabla está vacía, inserta registro inicial
    IF NOT EXISTS (SELECT 1 FROM ControlElecciones)
    BEGIN
        INSERT INTO ControlElecciones (CantidadVotantes, FechaEleccion)
        VALUES (@Cantidad, GETDATE());
    END
    ELSE
    BEGIN
        -- Actualiza solo la cantidad de votantes
        UPDATE ControlElecciones 
        SET CantidadVotantes = @Cantidad
        WHERE ID = 1;
    END
END
GO

CREATE OR ALTER PROCEDURE ActualizarFechaEleccion
    @Fecha DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Si la tabla está vacía, inserta registro inicial
    IF NOT EXISTS (SELECT 1 FROM ControlElecciones)
    BEGIN
        INSERT INTO ControlElecciones (CantidadVotantes, FechaEleccion)
        VALUES (0, @Fecha);
    END
    ELSE
    BEGIN
        -- Actualiza solo la fecha
        UPDATE ControlElecciones 
        SET FechaEleccion = @Fecha
        WHERE ID = 1;
    END
END

GO

CREATE OR ALTER PROCEDURE ObtenerControlElecciones
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Asegura que exista el registro
    IF NOT EXISTS (SELECT 1 FROM ControlElecciones)
    BEGIN
        INSERT INTO ControlElecciones (CantidadVotantes, FechaEleccion)
        VALUES (0, GETDATE());
    END
    
    SELECT CantidadVotantes, FechaEleccion
    FROM ControlElecciones
    WHERE ID = 1;
END
GO

GO
CREATE PROCEDURE RegistrarLocalidad
    @Nombre VARCHAR(255),
    @CantidadMesas INT
AS
BEGIN
    INSERT INTO LOCALIDAD (Nombre, CantidadMesas)
    VALUES (@Nombre, @CantidadMesas)
END
GO

CREATE PROCEDURE ObtenerLocalidades
AS
BEGIN
    Select *
	FROM LOCALIDAD;
END

GO
CREATE PROCEDURE ContarMesasPorLocalidad
    @IdLocalidad INT
AS
BEGIN
    SELECT COUNT(*) AS TotalMesas
    FROM MESA
    WHERE IdLocalidad = @IdLocalidad
END
GO

CREATE PROCEDURE CantidadMesasDeLocalidad
    @IdLocalidad INT
AS
BEGIN
    SELECT CantidadMesas
    FROM LOCALIDAD
    WHERE Id = @IdLocalidad
END
GO

-- Procedimiento para registrar mesas
CREATE PROCEDURE RegistrarMesa
    @NumeroMesa INT,
    @IdLocalidad INT,
    @Estado BIT = 1  -- Por defecto la mesa está abierta (1)
AS
BEGIN
    INSERT INTO MESA (NumeroMesa, IdLocalidad, Estado)
    VALUES (@NumeroMesa, @IdLocalidad, @Estado)
END
GO

-- Procedimiento para cambiar estado de mesa a cerrada (0)
CREATE PROCEDURE CerrarMesa
	@Id INT
AS BEGIN
UPDATE MESA 
SET Estado = 0 
WHERE Id = @Id
END;

GO

-- Procedimiento para verificar estado de una mesa
CREATE PROCEDURE VerificarEstadoMesa
    @NumeroMesa INT,
	@IdLocalidad INT
AS
BEGIN
    SELECT Estado
    FROM MESA M
    INNER JOIN LOCALIDAD L ON M.IdLocalidad = L.Id
    WHERE M.NumeroMesa = @NumeroMesa 
    AND M.IdLocalidad = @IdLocalidad
END
GO

CREATE PROCEDURE ObtenerIdMesa
    @NumeroMesa INT,
    @IdLocalidad INT
AS
BEGIN
    SELECT Id
    FROM MESA
    WHERE NumeroMesa = @NumeroMesa AND IdLocalidad = @IdLocalidad
END;
GO


-- Procedimiento para registrar opciones
CREATE PROCEDURE RegistrarOpcion
    @NombreCandidato VARCHAR(255)
AS
BEGIN
    INSERT INTO OPCIONES (NombreCandidato)
    VALUES (@NombreCandidato)
END;
GO

-- Procedimiento para obtener todas las opciones
CREATE PROCEDURE ObtenerOpciones
AS
BEGIN
    SELECT Id, NombreCandidato
    FROM OPCIONES
END
GO
-- Procedimiento para registrar votos por número de mesa y localidad
CREATE PROCEDURE RegistrarVotos
    @Cantidad INT,
    @NumeroMesa INT,
    @IdLocalidad INT,
    @IdOpcion INT
AS
BEGIN
    DECLARE @IdMesa INT
    
    -- Obtener el ID de la mesa basado en número y localidad
    SELECT @IdMesa = Id 
    FROM MESA 
    WHERE NumeroMesa = @NumeroMesa AND IdLocalidad = @IdLocalidad
    
    IF @IdMesa IS NOT NULL
    BEGIN
        INSERT INTO VOTOS (Cantidad, IdMesa, IdOpcion)
        VALUES (@Cantidad, @IdMesa, @IdOpcion)
    END
    ELSE
    BEGIN
        RAISERROR('Mesa no encontrada para la localidad especificada', 16, 1)
    END
END
GO

CREATE PROCEDURE ExisteVoto
    @IdMesa INT,
    @IdOpcion INT
AS
BEGIN
    SELECT COUNT(*) AS Existe
    FROM VOTOS
    WHERE IdMesa = @IdMesa AND IdOpcion = @IdOpcion
END
GO


CREATE PROCEDURE ActualizarVotos
    @Cantidad INT,
    @IdMesa INT,
    @IdOpcion INT
AS
BEGIN
    UPDATE VOTOS
    SET Cantidad = @Cantidad
    WHERE IdMesa = @IdMesa AND IdOpcion = @IdOpcion
END
GO


-- Procedimiento para obtener votos por localidad y tipo de opción
CREATE PROCEDURE ObtenerVotosPorLocalidadOpcion
    @IdLocalidad INT,
    @IdOpcion INT = NULL
AS
BEGIN
    SELECT 
        V.Cantidad,
        M.NumeroMesa,
        O.NombreCandidato
    FROM VOTOS V
    INNER JOIN MESA M ON V.IdMesa = M.Id
    INNER JOIN OPCIONES O ON V.IdOpcion = O.Id
    WHERE M.IdLocalidad = @IdLocalidad
    AND (@IdOpcion IS NULL OR V.IdOpcion = @IdOpcion)
END
GO

-- Procedimiento para obtener todos los votos agrupados por tipo de opción
CREATE PROCEDURE ObtenerTotalVotosPorOpcion
AS
BEGIN
    SELECT 
        O.Id AS IdOpcion,
        O.NombreCandidato,
        SUM(V.Cantidad) AS TotalVotos
    FROM VOTOS V
    INNER JOIN OPCIONES O ON V.IdOpcion = O.Id
    GROUP BY O.Id, O.NombreCandidato
    ORDER BY TotalVotos DESC
END
GO
CREATE PROCEDURE ObtenerResumenVotos
    @IdLocalidad INT = NULL,
    @NumeroMesa INT = NULL
AS
BEGIN
    SELECT 
        ISNULL(o.NombreCandidato, 'Voto Especial') AS NombreOpcion,
        SUM(v.Cantidad) AS TotalVotos
    FROM VOTOS v
    INNER JOIN OPCIONES o ON v.IdOpcion = o.Id
    INNER JOIN MESA m ON v.IdMesa = m.Id
    WHERE (@IdLocalidad IS NULL OR m.IdLocalidad = @IdLocalidad)
      AND (@NumeroMesa IS NULL OR m.NumeroMesa = @NumeroMesa)
    GROUP BY o.NombreCandidato
END

GO
Insert into Opciones(NombreCandidato)
Values ('Blancos'),('Nulos'),('Carlos'),('Manuel'),('Ausentes');

SELECT *
FROM OPCIONES

 */