USE Chinook
GO

-- CREATE INDEX

CREATE INDEX IX_PlaylistTrack_PlaylistIdTrackId ON PlaylistTrack(PlaylistId, TrackId)
