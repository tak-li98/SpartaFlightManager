using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using Manager;

namespace GUI
{

    public partial class AirlineWindow : Window
    {
        private AirlineManager _airlineManager = new ();

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
        public AirlineWindow()
        {
            InitializeComponent();
            CentreScreen();
            PopulateViewList();
        }


        public void PopulateViewList()
        {
            airlineBoard.ItemsSource = _airlineManager.ReturnAirlineAndRegion();
        }

        private void FlightBoardButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new MainWindow());
        }

        private void AirportsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new AirportWindow());
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
            WindowState = WindowState.Minimized;
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
            if (airlineBoard.SelectedItem != null)
            {
                _airlineManager.SetSelectedAirline(airlineBoard.SelectedItem);
            }
        }

        private void MoreInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var airlineSelected = _airlineManager.SelectedAirlineRegion.AirlineName.Replace(" ", "");
            ProcessStartInfo link = new ();
            link.UseShellExecute = true;
            link.FileName = "https://www.google.com/search?q=" + airlineSelected + "+airline";
            Process.Start(link);
        }
    }
}
