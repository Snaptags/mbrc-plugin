﻿using System;
using System.Windows.Forms;
using MusicBeeRemoteCore.Core.Model;
using MusicBeeRemoteCore.Remote.Enumerations;

namespace MusicBeeRemoteCore.Core.ApiAdapters
{
    public interface IPlayerApiAdapter
    {
        ShuffleState GetShuffleState();

        Repeat GetRepeatMode();

        bool ToggleRepeatMode();

        bool ScrobblingEnabled();

        bool PlayNext();

        bool PlayPrevious();

        bool StopPlayback();

        bool PlayPause();

        bool Play();

        bool Pause();

        PlayerStatus GetStatus();

        PlayerState GetState();

        bool ToggleScrobbling();

        int GetVolume();

        bool SetVolume(int volume);

        void ToggleShuffleLegacy();

        bool GetShuffleLegacy();

        ShuffleState SwitchShuffle();

        bool IsMuted();

        bool ToggleMute();

        void ToggleAutoDjLegacy();

        bool IsAutoDjEnabledLegacy();
    }
}