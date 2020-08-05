using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioPlayer.Views;

namespace AudioPlayer.ViewModel
{
    class AudioPlayerMenuViewModel
    {
        readonly AudioPlayerMenu audioPlayerMenu;

        public AudioPlayerMenuViewModel(AudioPlayerMenu audioPlayerMenu)
        {
            this.audioPlayerMenu = audioPlayerMenu;
        }
    }
}
