USE Chinook;

-- SELECT * FROM ViewBIInvoices ORDER BY InvoiceId,TrackName

DROP VIEW ViewBIInvoices;

CREATE VIEW ViewBIInvoices
AS
SELECT
    Invoice.InvoiceId
    ,CAST(Invoice.InvoiceDate AS date) InvoiceDate
    ,CAST(EXTRACT(YEAR FROM Invoice.InvoiceDate) AS char(4))
		+ '-' + REPLACE(CAST(EXTRACT(MONTH FROM Invoice.InvoiceDate) AS char(2)),' ','0') YearMonth
    ,CAST(EXTRACT(YEAR FROM Invoice.InvoiceDate) AS char(4)) Year
    ,REPLACE(CAST(EXTRACT(MONTH FROM Invoice.InvoiceDate) AS char(2)),' ','0') Month
    ,REPLACE(CAST(EXTRACT(DAY FROM Invoice.InvoiceDate) AS char(2)),' ','0') Day
    ,REPLACE(CAST(EXTRACT(WEEK FROM Invoice.InvoiceDate) AS char(2)),' ','0') Week
	--
    ,Invoice.CustomerId
    ,CONCAT(Customer.FirstName,' ',Customer.LastName) CustomerName
    ,Customer.City
    ,COALESCE(Customer.State, '') State
    ,Customer.Country
	--
    ,CONCAT(Employee.FirstName,' ',Employee.LastName) EmployeeName
    ,Customer.SupportRepId EmployeeId
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
    ,CAST(Invoice.Total AS decimal) InvoiceTotal
    ,CAST(InvoiceLine.UnitPrice AS decimal) TrackUnitPrice
    ,InvoiceLine.Quantity TrackQuantity
    ,CAST(InvoiceLine.UnitPrice * InvoiceLine.UnitPrice AS decimal) TrackTotal
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
