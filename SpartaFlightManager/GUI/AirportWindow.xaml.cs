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
    /// Interaction logic for AirportWindow.xaml
    /// </summary>
    public partial class AirportWindow : Window
    {
        AirportManager _airportManager = new AirportManager();
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
        public AirportWindow()
        {
            InitializeComponent();
            CentreScreen();
            PopulateViewList();
        }
        public void PopulateViewList()
        {
            airportBoard.ItemsSource = _airportManager.ReturnAirportAndRegion();
        }
        private void MoreInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var airport = _airportManager.SelectedAirport.AirportID;
            ProcessStartInfo link = new ProcessStartInfo();
            link.UseShellExecute = true;
            link.FileName = "https://www.google.com/search?q=" + airport + " airport";
            Process.Start(link);
        }

        private void FlightBoardButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new MainWindow());
        }

        private void AirlinesButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new AirlineWindow());
        }

        private void PilotsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new PilotWindow());
        }

        private void PlanesButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new PlaneWindow());
        }

        private void minimiseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Application Exit", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void airlineBoard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MoreInfoButton.IsEnabled = true;
            _airportManager.SetSelectedAirport(airportBoard.SelectedItem);
        }
    }
}
