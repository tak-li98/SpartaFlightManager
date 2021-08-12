using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Media;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for LoadingScreenWindow.xaml
    /// </summary>
    public partial class LoadingScreenWindow : Window
    {
        SoundPlayer player = new SoundPlayer("sfmIntro.wav");

        public void CentreScreen()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = Width;
            double windowHeight = Height;
            Left = (screenWidth / 2) - (windowWidth / 2);
            Top = (screenHeight / 2) - (windowHeight / 2);
        }
        public void OpenWindow(Window window)
        {
            window.Show();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Close();
        }
        public LoadingScreenWindow()
        {
            InitializeComponent();
            player.LoadAsync();
            CentreScreen();
        }

        private async void startBtn_Click(object sender, RoutedEventArgs e)
        {
            player.Play();
            welcome.Visibility = Visibility.Visible;
            onboard.Visibility = Visibility.Visible;
            gif.Visibility = Visibility.Visible;
            startBtn.Visibility = Visibility.Hidden;
            title.Visibility = Visibility.Hidden;
            await Task.Delay(2500);
            OpenWindow(new MainWindow());
        }
    }
}
