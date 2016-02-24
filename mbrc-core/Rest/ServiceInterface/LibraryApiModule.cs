﻿namespace MusicBeePlugin.Rest.ServiceInterface
{
    using MusicBeePlugin.Modules;

    using Nancy;

    public class LibraryApiModule : NancyModule
    {
        private readonly LibraryModule _module;

        public LibraryApiModule(LibraryModule module)
        {
            this._module = module;

            this.Get["/library/tracks"] = _ =>
                {
                    var limit = (int)this.Request.Query["limit"];
                    var offset = (int)this.Request.Query["offset"];
                    var after = (int)this.Request.Query["after"];

                    return this.Response.AsJson(this._module.GetAllTracks(limit, offset, after));
                };

            this.Get["/library/tracks/{id}"] = parameters =>
                {
                    var id = (int)parameters.id;
                    return this.Response.AsJson(this._module.GetTrackById(id));
                };

            this.Get["/library/artists"] = _ =>
                {
                    var limit = (int)this.Request.Query["limit"];
                    var offset = (int)this.Request.Query["offset"];
                    var after = (int)this.Request.Query["after"];

                    return this.Response.AsJson(this._module.GetAllArtists(limit, offset, after));
                };

            this.Get["/library/artists/{id}"] = parameters =>
                {
                    var id = (int)parameters.id;
                    return this.Response.AsJson(this._module.GetArtistById(id));
                };

            this.Get["/library/genres"] = _ =>
                {
                    var limit = (int)this.Request.Query["limit"];
                    var offset = (int)this.Request.Query["offset"];
                    var after = (int)this.Request.Query["after"];

                    return this.Response.AsJson(this._module.GetAllGenres(limit, offset, after));
                };

            this.Get["/library/albums"] = _ =>
                {
                    var limit = (int)this.Request.Query["limit"];
                    var offset = (int)this.Request.Query["offset"];
                    var after = (int)this.Request.Query["after"];

                    return this.Response.AsJson(this._module.GetAllAlbums(limit, offset, after));
                };

            this.Get["/library/covers"] = _ =>
                {
                    var limit = (int)this.Request.Query["limit"];
                    var offset = (int)this.Request.Query["offset"];
                    var after = (int)this.Request.Query["after"];
                    return this.Response.AsJson(this._module.GetAllCovers(limit, offset, after));
                };

            this.Get["/library/covers/{id}"] = parameters =>
                {
                    var id = (int)parameters.id;
                    return this.Response.AsJson(this._module.GetLibraryCover(id, true));
                };

            this.Get["/library/covers/{id}/raw"] =
                parameters =>
                    {
                        return new Response
                                   {
                                       ContentType = "image/jpeg", 
                                       Contents = stream => this._module.GetCoverData(parameters.id)
                                   };
                    };
        }
    }
}