USE Chinook
GO

-- Invoices

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
    ,InvoiceLine.Quantity Quantity
    ,CAST(InvoiceLine.UnitPrice AS float) UnitPrice
    ,CAST(InvoiceLine.UnitPrice * InvoiceLine.UnitPrice AS float) Total
	--
    ,Invoice.CustomerId
    --,Customer.FirstName + ' ' + Customer.LastName CustomerName
    --,Customer.City
    --,COALESCE(Customer.State, '') State
    --,Customer.Country
	----
    --,Customer.SupportRepId EmployeeId
    --,Employee.FirstName + ' ' + Employee.LastName EmployeeName
	--
    ,InvoiceLine.TrackId
    --,Track.Name TrackName
	----
	--,Track.AlbumId
	--,Album.Title AlbumTitle
	----
	--,Album.ArtistId
	--,Artist.Name ArtistName
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
ORDER BY
	InvoiceId,
	InvoiceLineId
GO

-- Customers

SELECT
    Customer.CustomerId
    ,Customer.FirstName + ' ' + Customer.LastName CustomerName
    ,Customer.City
    ,COALESCE(Customer.State, '') State
    ,Customer.Country
    ,Customer.SupportRepId EmployeeId
FROM
    Customer
ORDER BY
	CustomerId
GO

-- Employees

SELECT
	Employee.EmployeeId
	,Employee.FirstName + ' ' + Employee.LastName EmployeeName
FROM
    Employee
ORDER BY
	EmployeeId
GO

-- Tracks

SELECT
	Track.TrackId
    ,Track.Name TrackName
	,Track.AlbumId
FROM
	Track
ORDER BY
	1
GO

-- Albums

SELECT
	Album.AlbumId
	,Album.Title AlbumTitle
	,Album.ArtistId
FROM
    Album
ORDER BY
	AlbumId
GO

-- Artists

SELECT
	Artist.ArtistId
	,Artist.Name ArtistName
FROM
    Artist
ORDER BY
	ArtistId
GO
