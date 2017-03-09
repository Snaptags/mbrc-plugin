using System;

namespace MusicBeePlugin.PartyMode.Core
{
    public class ServerCommandEventArgs : EventArgs
    {
        public ServerCommandEventArgs(string client, string command, bool isCmdAllowed)
        {
            Command = command;
            Client = client;
            IsCommandAllowed = isCmdAllowed;
        }

        public string Client;
        public bool IsCommandAllowed;
        public string Command;
    }
}