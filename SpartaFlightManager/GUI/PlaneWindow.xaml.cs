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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using Manager;

namespace GUI
{
    /// <summary>
    /// Interaction logic for PlaneWindow.xaml
    /// </summary>
    public partial class PlaneWindow : Window
    {
        PlaneManager _planeManager = new PlaneManager();
        public void CentreScreen()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        public void OpenWindow(Window window)
        {
            window.Show();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Close();
        }
        public PlaneWindow()
        {
            InitializeComponent();
            CentreScreen();
            PopulateViewList();

        }
        public void PopulateViewList()
        {
            planeBoard.ItemsSource = _planeManager.RetrieveAll();
        }
        private void AirlinesButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new AirlineWindow());
        }

        private void PilotsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new PilotWindow());
        }

        private void AirportsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new AirportWindow());
        }

        private void FlightBoardButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new MainWindow());
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Application Exit", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void minimiseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void planeBoard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MoreInfoButton.IsEnabled = true;
            if (planeBoard.SelectedItem != null)
            {
                _planeManager.SetSelectedPlane(planeBoard.SelectedItem);
            }
        }

        private void MoreInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var planeSelected = _planeManager.SelectedPlane.PlaneModel;
            ProcessStartInfo link = new ProcessStartInfo();
            link.UseShellExecute = true;
            link.FileName = "https://www.google.com/search?q=" + planeSelected;
            Process.Start(link);
        }
    }
}
