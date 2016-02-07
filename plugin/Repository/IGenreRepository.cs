﻿
namespace MusicBeePlugin.Repository
{
    using System.Collections.Generic;

    using MusicBeePlugin.Rest.ServiceModel.Type;

    /// <summary>
    /// The GenreRepository interface.
    /// </summary>
    public interface IGenreRepository
    {
        /// <summary>
        /// Gets a specific genre from the repository by the genre id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="LibraryGenre"/>.
        /// </returns>
        LibraryGenre GetGenre(long id);

        void SaveGenre(LibraryGenre genre);

        void SaveGenres(ICollection<LibraryGenre> genres);

        ICollection<LibraryGenre> GetAllGenres();

        ICollection<LibraryGenre> GetGenrePage(int offset, int limit);

        ICollection<LibraryGenre> GetUpdatedGenres(int offset, int limit, long epoch);

        ICollection<LibraryGenre> GetCachedGenres();

        ICollection<LibraryGenre> GetDeletedGenres();

        void DeleteGenres(ICollection<LibraryGenre> genres);

        int GetGenreCount();
    }
}