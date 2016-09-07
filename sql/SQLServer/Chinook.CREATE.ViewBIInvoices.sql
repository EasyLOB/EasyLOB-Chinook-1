USE Chinook
GO

-- SELECT * FROM ViewBIInvoices ORDER BY InvoiceId,TrackName

DROP VIEW ViewBIInvoices
GO

CREATE VIEW ViewBIInvoices
AS
SELECT
    Invoice.InvoiceId
    ,CAST(Invoice.InvoiceDate AS date) InvoiceDate
    ,STR(DATEPART(Year, Invoice.InvoiceDate),4)
		+ '-' + REPLACE(STR(DATEPART(Month, Invoice.InvoiceDate),2),' ','0') YearMonth
    ,STR(DATEPART(Year, Invoice.InvoiceDate),4) Year
    ,REPLACE(STR(DATEPART(Month, Invoice.InvoiceDate),2),' ','0') Month
    ,REPLACE(STR(DATEPART(Day, Invoice.InvoiceDate),2),' ','0') Day
    ,REPLACE(STR(DATEPART(Week, Invoice.InvoiceDate),2),' ','0') Week
	--
    ,Invoice.CustomerId
    ,Customer.FirstName + ' ' + Customer.LastName CustomerName
    ,Customer.City
    ,COALESCE(Customer.State, '') State
    ,Customer.Country
	--
    ,Customer.SupportRepId EmployeeId
    ,Employee.FirstName + ' ' + Employee.LastName EmployeeName
	--
	,Artist.ArtistId
	,Artist.Name ArtistName
	--
	,Album.AlbumId
	,Album.Title AlbumTitle
	--
    ,InvoiceLine.TrackId
    ,Track.Name TrackName
	--
    ,InvoiceLine.Quantity Quantity
    ,CAST(InvoiceLine.UnitPrice AS float) UnitPrice
    ,CAST(InvoiceLine.UnitPrice * InvoiceLine.UnitPrice AS float) Total
FROM
    Invoice
    INNER JOIN Customer ON
        Customer.CustomerId = Invoice.CustomerId
    INNER JOIN Employee ON
        Employee.EmployeeId = Customer.SupportRepId
    INNER JOIN InvoiceLine ON
        InvoiceLine.InvoiceId = Invoice.InvoiceId
    INNER JOIN Track ON
        InvoiceLine.TrackId = Track.TrackId
    INNER JOIN Album ON
        Album.AlbumId = Track.AlbumId
    INNER JOIN Artist ON
        Artist.ArtistId = Album.ArtistId
GO