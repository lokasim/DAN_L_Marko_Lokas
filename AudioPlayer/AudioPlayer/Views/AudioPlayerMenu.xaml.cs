using AudioPlayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AudioPlayer.Views
{
    /// <summary>
    /// Interaction logic for AudioPlayerMenu.xaml
    /// </summary>
    public partial class AudioPlayerMenu : Window
    {
        public AudioPlayerMenu()
        {
            InitializeComponent();
            this.DataContext = new AudioPlayerMenuViewModel(this);
            this.Language = XmlLanguage.GetLanguage("sr-SR");
        }

        private void DragMe(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {
            }
        }
    }
}
