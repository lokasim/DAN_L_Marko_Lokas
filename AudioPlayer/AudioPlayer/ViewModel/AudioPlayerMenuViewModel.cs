using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AudioPlayer.Command;
using AudioPlayer.Views;

namespace AudioPlayer.ViewModel
{
    class AudioPlayerMenuViewModel : ViewModelBase
    {
        readonly AudioPlayerMenu audioPlayerMenu;

        public AudioPlayerMenuViewModel(AudioPlayerMenu audioPlayerMenu)
        {
            this.audioPlayerMenu = audioPlayerMenu;
        }

        private ICommand exit;
        public ICommand Exit
        {
            get
            {
                if (exit == null)
                {
                    exit = new RelayCommand(param => ExitExecute(), param => CanExitExecute());
                }
                return exit;
            }
        }

        /// <summary>
        /// Exit application
        /// </summary>
        private void ExitExecute()
        {
            MessageBoxResult dialog = Xceed.Wpf.Toolkit.MessageBox.Show("Da li želite napustiti aplikaciju?", "Izlaz", MessageBoxButton.YesNo);

            if (dialog == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        private bool CanExitExecute()
        {
            return true;
        }
    }
}
