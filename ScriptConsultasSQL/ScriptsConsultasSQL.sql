
-- Seleccionamos la BD
USE GestionVentasDB;

-- Escriba una consulta para obtener el nombre del cliente y el total de cada factura generada.
SELECT 
    I.ID AS IDFactura,
    C.CName AS NombreCliente,
    I.Total AS TotalFactura
FROM 
    Invoice I
INNER JOIN 
    Client C ON I.ClientId = C.ID;


-- Escriba una consulta para obtener el producto más vendido (en cantidad) en todas las facturas.

SELECT TOP 1 
    P.PName AS NombreProducto,
    SUM(ID.Quantity) AS TotalVendido
FROM 
    InvoiceDetail ID
INNER JOIN 
    Products P ON ID.ProductId = P.ID
GROUP BY 
    P.PName
ORDER BY 
    TotalVendido DESC;


-- Escriba una consulta para listar los clientes que no han realizado ninguna compra.

SELECT 
    C.ID,
    C.CName AS NombreCliente
FROM 
    Client C
LEFT JOIN 
    Invoice I ON C.ID = I.ClientId
WHERE 
    I.ID IS NULL;