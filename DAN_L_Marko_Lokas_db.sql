IF DB_ID('AudioPlayer') IS NULL
CREATE DATABASE AudioPlayer
GO

USE AudioPlayer

ALTER DATABASE AudioPlayer COLLATE Croatian_CI_AS;
GO


-- Drop Foreign Keys
IF OBJECT_ID('tblSong', 'U') IS NOT NULL 
	ALTER TABLE tblSong DROP CONSTRAINT FK_User_Song;

--===================================================================

-- Drop Primary Keys
IF OBJECT_ID('tblUser', 'U') IS NOT NULL 
	ALTER TABLE tblUser DROP CONSTRAINT PK_User;
IF OBJECT_ID('tblSong', 'U') IS NOT NULL 
	ALTER TABLE tblSong DROP CONSTRAINT PK_Song;
IF OBJECT_ID('tblListenedSong', 'U') IS NOT NULL 
	ALTER TABLE tblListenedSong DROP CONSTRAINT PK_ListenedSong;

--===================================================================

-- Drop tables
IF OBJECT_ID('tblUser', 'U') IS NOT NULL 
	DROP TABLE tblUser;
IF OBJECT_ID('tblSong', 'U') IS NOT NULL 
	DROP TABLE tblSong;
IF OBJECT_ID('tblListenedSong', 'U') IS NOT NULL 
	DROP TABLE tblListenedSong;

--===================================================================

-- Create tables
CREATE TABLE tblUser(
	UserID				int identity(1,1) NOT NULL,
	UsernameUser		nvarchar (50) NOT NULL,
	PasswordUser		nvarchar (50) NOT NULL
	)
CREATE TABLE tblSong(
	SongID			int identity(1,1) NOT NULL,
	SongName		nvarchar (50) NOT NULL,
	SongAuthor		nvarchar (50) NOT NULL,
	SongDuration	nvarchar(9) NOT NULL, 
	PlaylistUser	int NOT NULL
	)
CREATE TABLE tblListenedSong(
	ListenedSongID		int identity(1,1) NOT NULL,
	SongName			nvarchar (50) NOT NULL,
	ReleaseTime			nvarchar (50) NOT NULL,
	CompletionTime		nvarchar (50) NOT NULL,
	ListensUser			nvarchar (30) NOT NULL
	)
--===================================================================

-- Add constraints for PK
ALTER TABLE tblUser 
	ADD CONSTRAINT PK_User
	PRIMARY KEY (UserID)

ALTER TABLE tblSong 
	ADD CONSTRAINT PK_Song
	PRIMARY KEY (SongID)

ALTER TABLE tblListenedSong 
	ADD CONSTRAINT PK_ListenedSong
	PRIMARY KEY (ListenedSongID)
--===================================================================

-- Add constraints for FK
ALTER TABLE tblSong 
	ADD CONSTRAINT FK_User_Song
	FOREIGN KEY (PlaylistUser) 
	REFERENCES tblUser (UserID)
--===================================================================