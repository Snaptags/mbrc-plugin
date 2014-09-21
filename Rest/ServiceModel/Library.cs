﻿using MusicBeePlugin.AndroidRemote.Utilities;
using MusicBeePlugin.Rest.ServiceModel.Type;
using ServiceStack.ServiceHost;

namespace MusicBeePlugin.Rest.ServiceModel
{
    [Route("/library/tracks","GET")]
    public class GetLibraryTracks : IReturn<PaginatedResponse>
    {
        public int limit { get; set; }
        public int offset { get; set; }
    }

    [Route("/library/tracks/{id}", "GET")]
    public class GetLibraryTrack : IReturn<LibraryTrack>
    {
        public int id { get; set; }
    }

    [Route("/library/artists", "GET")]
    public class GetLibraryArtists : IReturn<PaginatedResponse>
    {
        public int limit { get; set; }
        public int offset { get; set; }
    }

    [Route("/library/artists/{id}", "GET")]
    public class GetLibraryArtist : IReturn<LibraryArtist>
    {
        public int id { get; set; }
    }

    [Route("/library/albums", "GET")]
    public class GetLibraryAlbums : IReturn<PaginatedResponse>
    {
        public int limit { get; set; }
        public int offset { get; set; }
    }

    [Route("/library/albums/{id}", "GET")]
    public class GetLibraryAlbum : IReturn<LibraryAlbum>
    {
        public int id { get; set; }
    }

    [Route("/library/genres", "GET")]
    public class GetLibraryGenres : IReturn<PaginatedResponse>
    {
        public int limit { get; set; }
        public int offset { get; set; }
    }

    [Route("/library/genres/{id}", "GET")]
    public class GetLibraryGenre : IReturn<LibraryGenre>
    {
        public int id { get; set; }
    }
    
        
}