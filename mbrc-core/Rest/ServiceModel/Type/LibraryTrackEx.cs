namespace MusicBeeRemoteCore.Rest.ServiceModel.Type
{
    using System.Collections.Generic;

    using MusicBeeRemoteData.Entities;

    /// <summary>
    ///     The complete form of the <see cref="LibraryTrack" /> class.
    ///     It contains the values in the place of ids.
    /// </summary>
    public class LibraryTrackEx
    {
        /// <summary>
        ///     Default constructor to create a <see cref="LibraryTrackEx" />
        /// </summary>
        public LibraryTrackEx()
        {
        }

        /// <summary>
        ///     Creates a new <see cref="LibraryTrackEx" /> taking information from the
        ///     metadata available in the <paramref name="tags" /> array.
        /// </summary>
        /// <param name="tags">
        ///     An array containing the metadata tags for the track
        /// </param>
        public LibraryTrackEx(IList<string> tags)
        {
            var i = 0;
            this.Artist = tags[i++];
            this.AlbumArtist = tags[i++];
            this.Album = tags[i++];
            this.Genre = tags[i++];
            this.Title = tags[i++];
            this.Year = tags[i++];

            var trackNo = tags[i++];
            var discNo = tags[i];
            int position;
            int disc;
            int.TryParse(trackNo, out position);
            int.TryParse(discNo, out disc);
            this.Position = position;
            this.Disc = disc;
        }

        /// <summary>
        ///     Represents the name of the album the track is part of.
        /// </summary>
        public string Album { get; set; }

        /// <summary>
        ///     Represents the name of the artist that recorded the album the track
        ///     is part of.
        /// </summary>
        public string AlbumArtist { get; set; }

        /// <summary>
        ///     Represents the name of the track's artist.
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        ///     The disc number of the album
        /// </summary>
        public int Disc { get; set; }

        /// <summary>
        ///     Represents the name of the track's genre.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        ///     The id of the <see cref="LibraryTrack" /> entry.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     The path of the music file in the file system.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        ///     Represents the position of the track in the album.
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        ///     Represents the title of the track.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     A string representation of the date or year the album was released.
        /// </summary>
        public string Year { get; set; }
    }
}