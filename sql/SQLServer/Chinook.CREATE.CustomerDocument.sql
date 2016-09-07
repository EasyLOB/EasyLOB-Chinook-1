USE Chinook
GO

-- DROP TABLE CustomerDocument
-- TRUNCATE TABLE CustomerDocument

CREATE TABLE CustomerDocument
(
    CustomerDocumentId integer IDENTITY(1, 1) NOT NULL, 
    CustomerId integer NOT NULL, 
    Description varchar(100) NOT NULL,
	FileAcronym varchar(10) NOT NULL -- docx | xlsx | pdf | ...
)
ALTER TABLE CustomerDocument ADD CONSTRAINT PK_CustomerDocument
    PRIMARY KEY (CustomerDocumentId)
CREATE INDEX IX_CustomerDocument_01 ON CustomerDocument(CustomerId)
CREATE INDEX IX_CustomerDocument_02 ON CustomerDocument(FileAcronym)
ALTER TABLE CustomerDocument ADD CONSTRAINT FK_CustomerDocument_01
    FOREIGN KEY(CustomerId) REFERENCES Customer(CustomerId) ON UPDATE CASCADE

/*
INSERT INTO CustomerDocument VALUES (1, 'Excel', 'xlsx')
INSERT INTO CustomerDocument VALUES (1, 'PDF', 'pdf')
INSERT INTO CustomerDocument VALUES (1, 'Word', 'docx')
*/
