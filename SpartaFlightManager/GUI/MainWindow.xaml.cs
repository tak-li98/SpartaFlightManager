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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Manager;


namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FlightManager _flightManager = new FlightManager();
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
        public MainWindow()
        {
            InitializeComponent();
            CentreScreen();
            PopulateListView();

        }

        private void PopulateListView()
        {
            var results = _flightManager.ReturnFlightBoardInfoFromFlights();
            for (int i = 0; i < results.Length / 12; i++)
            {
                var row = new
                {
                    ID = results[i, 1]
                    ,
                    FlightNumber = $"{results[i, 0]}0{results[i, 1]}"
                    ,
                    Departure = $"{results[i, 3]} ({results[i, 5]})"
                    ,
                    Arrival = $"{results[i, 9]} ({results[i, 11]})"
                    ,
                    Date = results[i, 6]
                    ,
                    Time = results[i, 7]
                    ,
                    Status = results[i, 8]
                };
                flightBoard.Items.Add(row);
            }

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FullDetailsButton.IsEnabled = true;
        }

        private void FullDetailsButton_Click(object sender, RoutedEventArgs e)
        {

            var inputArr = flightBoard.SelectedValue.ToString();
            var sub = inputArr.Substring(inputArr.IndexOf("= ") + 2, inputArr.IndexOf(",") - inputArr.IndexOf("=") - 2);
            var flightDetailsWindow = new FlightDetailsWindow(Int32.Parse(sub));
            OpenWindow(new FlightDetailsWindow(Int32.Parse(sub)));
        }


        private void AirlinesButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new AirlineWindow());
        }

        private void AirportsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new AirportWindow());
        }

        private void PilotsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new PilotWindow());        }

        private void AddFlightsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new AddFlightWindow());
        }

        private void PlanesButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new PlaneWindow());
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
    }
}
