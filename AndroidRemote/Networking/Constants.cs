﻿namespace MusicBeePlugin.AndroidRemote.Networking
{
    internal static class Constants
    {
        

        #region Protocol 2. Basic functionality
        public const string Error = "error";
        public const string Player = "player";
        public const string Protocol = "protocol";
        public const string PlayerName = "MusicBee";
        public const float ProtocolVersion = 2;
        public const string PluginVersion = "pluginversion";
        public const string NotAllowed = "notallowed";
        #endregion

        #region Protocol 2. API calls
        public const string PlayerStatus = "playerstatus";
        public const string PlayerRepeat = "playerrepeat";
        public const string PlayerScrobble = "scrobbler";
        public const string PlayerShuffle = "playershuffle";
        public const string PlayerMute = "playermute";
        public const string PlayerPlayPause = "playerplaypause";
        public const string PlayerPrevious = "playerprevious";
        public const string PlayerNext = "playernext";
        public const string PlayerStop = "playerstop";
        public const string PlayerState = "playerstate";
        public const string PlayerVolume = "playervolume";
        public const string PlayerAutoDj = "playerautodj";

        public const string NowPlayingTrack = "nowplayingtrack";
        public const string NowPlayingCover = "nowplayingcover";
        public const string NowPlayingPosition = "nowplayingposition";
        public const string NowPlayingLyrics = "nowplayinglyrics"; 
        public const string NowPlayingRating = "nowplayingrating";
        public const string NowPlayingLfmRating = "nowplayinglfmrating";
        public const string NowPlayingList = "nowplayinglist";
        public const string NowPlayingListChanged = "nowplayinglistchanged";
        public const string NowPlayingListPlay = "nowplayinglistplay";
        public const string NowPlayingListRemove = "nowplayinglistremove";
        public const string NowPlayingListMove = "nowplayinglistmove";
        public const string NowPlayingListSearch = "nowplayinglistsearch";
        public const string NowPlayingListQueue = "nowplayinglistqueue";
        
        public const string LibrarySearchArtist = "librarysearchartist";
        public const string LibrarySearchAlbum = "librarysearchalbum";
        public const string LibrarySearchGenre = "librarysearchgenre";
        public const string LibrarySearchTitle = "librarysearchtitle";

        public const string LibraryArtistAlbums = "libraryartistalbums";
        public const string LibraryGenreArtists = "librarygenreartists";
        public const string LibraryAlbumTracks = "libraryalbumtracks";
        
        public const string LibraryQueueGenre = "libraryqueuegenre";
        public const string LibraryQueueArtist = "libraryqueueartist";
        public const string LibraryQueueAlbum = "libraryqueuealbum";
        public const string LibraryQueueTrack = "libraryqueuetrack";

        public const string PlaylistList = "playlistlist"; 

        #endregion

        #region Protocol 2.1. API calls

        public const string PlaylistGetFiles = "playlistgetfiles";
        public const string PlaylistPlayNow = "playlistplaynow";
        public const string PlaylistRemove = "playlistremove";
        public const string PlaylistMove = "playlistmove";
        public const string PlaylistAddFiles = "playlistaddfiles";
        public const string PlaylistCreate = "playlistcreate";

        public const string Playlists = "playlists";
        public const string Library = "library";
        public const string LibraryMeta = "librarymeta";
        public const string Ping = "ping";
        public const string Nowplaying = "nowplaying";

        #endregion

        #region Protocol 2. SocketMessage type identifiers.

        public const string Request = "req";
        public const string Reply = "rep";
        public const string Message = "msg";

        #endregion

        
    }
}