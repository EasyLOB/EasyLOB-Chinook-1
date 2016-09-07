USE Chinook;

-- CREATE INDEX

CREATE INDEX IX_Album_AlbumId ON Album(AlbumId);
CREATE INDEX IX_Album_Title ON Album(Title);
CREATE INDEX IX_Album_ArtistId ON Album(ArtistId);

CREATE INDEX IX_Artist_ArtistId ON Artist(ArtistId);
CREATE INDEX IX_Artist_Name ON Artist(Name);

CREATE INDEX IX_Customer_CustomerId ON Customer(CustomerId);
CREATE INDEX IX_Customer_FirstName ON Customer(FirstName);
CREATE INDEX IX_Customer_LastName ON Customer(LastName);
CREATE INDEX IX_Customer_Company ON Customer(Company);
CREATE INDEX IX_Customer_Address ON Customer(Address);
CREATE INDEX IX_Customer_City ON Customer(City);
CREATE INDEX IX_Customer_State ON Customer(State);
CREATE INDEX IX_Customer_Country ON Customer(Country);
CREATE INDEX IX_Customer_PostalCode ON Customer(PostalCode);
CREATE INDEX IX_Customer_Phone ON Customer(Phone);
CREATE INDEX IX_Customer_Fax ON Customer(Fax);
CREATE INDEX IX_Customer_Email ON Customer(Email);
CREATE INDEX IX_Customer_EmployeeId ON Customer(EmployeeId);

CREATE INDEX IX_Employee_EmployeeId ON Employee(EmployeeId);
CREATE INDEX IX_Employee_LastName ON Employee(LastName);
CREATE INDEX IX_Employee_FirstName ON Employee(FirstName);
CREATE INDEX IX_Employee_Title ON Employee(Title);
CREATE INDEX IX_Employee_ReportsTo ON Employee(ReportsTo);
CREATE INDEX IX_Employee_BirthDate ON Employee(BirthDate);
CREATE INDEX IX_Employee_HireDate ON Employee(HireDate);
CREATE INDEX IX_Employee_Address ON Employee(Address);
CREATE INDEX IX_Employee_City ON Employee(City);
CREATE INDEX IX_Employee_State ON Employee(State);
CREATE INDEX IX_Employee_Country ON Employee(Country);
CREATE INDEX IX_Employee_PostalCode ON Employee(PostalCode);
CREATE INDEX IX_Employee_Phone ON Employee(Phone);
CREATE INDEX IX_Employee_Fax ON Employee(Fax);
CREATE INDEX IX_Employee_Email ON Employee(Email);

CREATE INDEX IX_Genre_GenreId ON Genre(GenreId);
CREATE INDEX IX_Genre_Name ON Genre(Name);

CREATE INDEX IX_Invoice_InvoiceId ON Invoice(InvoiceId);
CREATE INDEX IX_Invoice_CustomerId ON Invoice(CustomerId);
CREATE INDEX IX_Invoice_InvoiceDate ON Invoice(InvoiceDate);
CREATE INDEX IX_Invoice_BillingAddress ON Invoice(BillingAddress);
CREATE INDEX IX_Invoice_BillingCity ON Invoice(BillingCity);
CREATE INDEX IX_Invoice_BillingState ON Invoice(BillingState);
CREATE INDEX IX_Invoice_BillingCountry ON Invoice(BillingCountry);
CREATE INDEX IX_Invoice_BillingPostalCode ON Invoice(BillingPostalCode);
CREATE INDEX IX_Invoice_Total ON Invoice(Total);

CREATE INDEX IX_InvoiceLine_InvoiceLineId ON InvoiceLine(InvoiceLineId);
CREATE INDEX IX_InvoiceLine_InvoiceId ON InvoiceLine(InvoiceId);
CREATE INDEX IX_InvoiceLine_TrackId ON InvoiceLine(TrackId);
CREATE INDEX IX_InvoiceLine_UnitPrice ON InvoiceLine(UnitPrice);
CREATE INDEX IX_InvoiceLine_Quantity ON InvoiceLine(Quantity);

CREATE INDEX IX_MediaType_MediaTypeId ON MediaType(MediaTypeId);
CREATE INDEX IX_MediaType_Name ON MediaType(Name);

CREATE INDEX IX_Playlist_PlaylistId ON Playlist(PlaylistId);
CREATE INDEX IX_Playlist_Name ON Playlist(Name);
CREATE INDEX IX_Playlist_Sys_Date_Time ON Playlist(Sys_Date_Time);
CREATE INDEX IX_PlaylistTrack_PlaylistId ON PlaylistTrack(PlaylistId);
CREATE INDEX IX_PlaylistTrack_TrackId ON PlaylistTrack(TrackId);

CREATE INDEX IX_Track_TrackId ON Track(TrackId);
CREATE INDEX IX_Track_Name ON Track(Name);
CREATE INDEX IX_Track_AlbumId ON Track(AlbumId);
CREATE INDEX IX_Track_MediaTypeId ON Track(MediaTypeId);
CREATE INDEX IX_Track_GenreId ON Track(GenreId);
CREATE INDEX IX_Track_Composer ON Track(Composer);
CREATE INDEX IX_Track_Milliseconds ON Track(Milliseconds);
CREATE INDEX IX_Track_Bytes ON Track(Bytes);
CREATE INDEX IX_Track_UnitPrice ON Track(UnitPrice);
