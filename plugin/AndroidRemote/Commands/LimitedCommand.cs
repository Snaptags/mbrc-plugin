﻿using MusicBeePlugin.AndroidRemote.Interfaces;

namespace MusicBeePlugin.AndroidRemote.Commands
{
    /// <summary>
    /// A Command that has limitations. Thus functionality can be restricted if the client
    /// doesn't have the necessary permissions to run the command.
    /// </summary>
    public abstract class LimitedCommand : ICommand
    {
        public abstract void Execute(IEvent eEvent);
        /// <summary>
        /// Permissions required by the command in order to run.
        /// </summary>
        /// <returns>The required command permissions.</returns>
        public abstract CommandPermissions GetPermissions();
    }
}