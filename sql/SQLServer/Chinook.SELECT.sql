USE Chinook
GO

-- TOP 1

SELECT TOP 1 * FROM Album ORDER BY 1 DESC
SELECT TOP 1 * FROM Artist ORDER BY 1 DESC
SELECT TOP 1 * FROM Customer ORDER BY 1 DESC
SELECT TOP 1 * FROM CustomerDocument ORDER BY 1 DESC
SELECT TOP 1 * FROM Employee ORDER BY 1 DESC
SELECT TOP 1 * FROM Genre ORDER BY 1 DESC
SELECT TOP 1 * FROM Invoice ORDER BY 1 DESC
SELECT TOP 1 * FROM InvoiceLine ORDER BY 1 DESC
SELECT TOP 1 * FROM MediaType ORDER BY 1 DESC
SELECT TOP 1 * FROM Playlist ORDER BY 1 DESC
--SELECT TOP 1 * FROM PlaylistTrack ORDER BY 1 DESC
SELECT TOP 1 * FROM Track ORDER BY 1 DESC

-- MIN & MAX

SELECT 'Album', MIN(AlbumId), MAX(AlbumId) FROM Album UNION ALL
SELECT 'Artist', MIN(ArtistId), MAX(ArtistId) FROM Artist UNION ALL
SELECT 'Customer', MIN(CustomerId), MAX(CustomerId) FROM Customer UNION ALL
SELECT 'CustomerDocument', MIN(CustomerDocumentId), MAX(CustomerDocumentId) FROM CustomerDocument UNION ALL
SELECT 'Employee', MIN(EmployeeId), MAX(EmployeeId) FROM Employee UNION ALL
SELECT 'Genre', MIN(GenreId), MAX(GenreId) FROM Genre UNION ALL
SELECT 'Invoice', MIN(InvoiceId), MAX(InvoiceId) FROM Invoice UNION ALL
SELECT 'InvoiceLine', MIN(InvoiceLineId), MAX(InvoiceLineId) FROM InvoiceLine UNION ALL
SELECT 'MediaType', MIN(MediaTypeId), MAX(MediaTypeId) FROM MediaType UNION ALL
SELECT 'Playlist', MIN(PlaylistId), MAX(PlaylistId) FROM Playlist UNION ALL
--SELECT 'PlaylistTrack', MIN(PlaylistTrackId), MAX(PlaylistTrackId) FROM PlaylistTrack UNION ALL
SELECT 'Track', MIN(TrackId), MAX(TrackId) FROM Track

/*
-- DELETE

DELETE FROM InvoiceLine WHERE InvoiceLineId > 2240
DELETE FROM Invoice WHERE InvoiceId > 412

DELETE FROM CustomerDocument
DELETE FROM Customer WHERE CustomerId > 59
DELETE FROM Employee WHERE EmployeeId > 8

DELETE FROM PlaylistTrack WHERE PlaylistId > 18 OR TrackId > 3503
DELETE FROM Playlist WHERE PlaylistId > 18
DELETE FROM Track WHERE TrackId > 3503
DELETE FROM MediaType WHERE MediaTypeId > 5
DELETE FROM Genre WHERE GenreId > 25
DELETE FROM Album WHERE AlbumId > 347
DELETE FROM Artist WHERE ArtistId > 275

-- IDENTITY

DBCC CHECKIDENT (Artist, RESEED, 275)
DBCC CHECKIDENT (Album, RESEED, 347)
DBCC CHECKIDENT (Genre, RESEED, 25)
DBCC CHECKIDENT (MediaType, RESEED, 5)
DBCC CHECKIDENT (Track, RESEED, 3503)
DBCC CHECKIDENT (Playlist, RESEED, 18)
--PlaylistTrack

DBCC CHECKIDENT (Employee, RESEED, 8)
DBCC CHECKIDENT (Customer, RESEED, 59)
DBCC CHECKIDENT (CustomerDocument, RESEED, 0)

DBCC CHECKIDENT (Invoice, RESEED, 412)
DBCC CHECKIDENT (InvoiceLine, RESEED, 2240)
*/
