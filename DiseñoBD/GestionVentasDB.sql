Create DATABASE GestionVentasDB;
GO

USE GestionVentasDB;
GO

CREATE TABLE Client (
	ID INT IDENTITY PRIMARY KEY,
	CName NVARCHAR(80) NOT NULL,
	Email NVARCHAR(80) UNIQUE NOT NULL,
	Phone NVARCHAR(25) NOT NULL
);

CREATE TABLE Products (
	ID INT IDENTITY PRIMARY KEY,
	PName NVARCHAR(80) NOT NULL,
	Price DECIMAL(10,2) CHECK (Price >= 0 ) NOT NULL,
	Stock INT CHECK (Stock >= 0) NOT NULL
);

CREATE TABLE Invoice (
	ID INT IDENTITY PRIMARY KEY,
	ClientId INT NOT NULL,
	CreationDate DATETIME DEFAULT GETDATE(),
	Total DECIMAL(10,2) NOT NULL,
	CONSTRAINT FK_InvoiceClient FOREIGN KEY (ClientId) REFERENCES Client(ID)
);

CREATE TABLE InvoiceDetail (
	ID INT IDENTITY PRIMARY KEY,
	InvoiceId INT NOT NULL,
	ProductId INT NOT NULL,
	Quantity INT CHECK(Quantity > 0) NOT NULL,
	UnitPrice DECIMAL(10,2) NOT NULL,
	SubTotal AS (Quantity * UnitPrice),
	CONSTRAINT FK_InvoiceDetail_Invoice FOREIGN KEY (InvoiceId) REFERENCES Invoice(ID),
	CONSTRAINT FK_InvoiceDetail_Product FOREIGN KEY (ProductId) REFERENCES Products(ID),
);

-- Creacion de indices para Optimizar las consultas 
CREATE INDEX idx_Invoice_ClientId ON Invoice(ClientId);
CREATE INDEX idx_InvoiceDetail_ProductID ON InvoiceDetail(ProductId);


GO
-- Trigger para actualizar el stock después de una venta
CREATE TRIGGER trg_UpdateStock
ON InvoiceDetail
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE p
    SET p.Stock = p.Stock - i.Quantity
    FROM Products p
    INNER JOIN inserted i ON p.ID = i.ProductId;
END;
GO

-- Trigger para actualizar el valor de la factura después de agregar un detalle de factura
CREATE TRIGGER trg_UpdateInvoiceTotal
ON InvoiceDetail
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE i
    SET i.Total = i.Total + d.SubTotal
    FROM Invoice i
    INNER JOIN inserted d ON i.Id = d.InvoiceId;
END;
GO