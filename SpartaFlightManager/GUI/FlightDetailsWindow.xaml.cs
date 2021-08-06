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
using Manager;

namespace GUI
{
    /// <summary>
    /// Interaction logic for FlightDetails.xaml
    /// </summary>
    public partial class FlightDetailsWindow : Window
    {
        private FlightDetailsManager _flightDetailsManager = new FlightDetailsManager();
        private PilotManager _pilotManager = new PilotManager();
        private AirlineManager _airlineManager = new AirlineManager();
        private AirportManager _airportManager = new AirportManager();
        private PlaneManager _planeManager = new PlaneManager();
        private FlightStatusManager _flightStatusManager = new FlightStatusManager();
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
        public FlightDetailsWindow()
        {
            InitializeComponent();
            CentreScreen();
            
        }
        public FlightDetailsWindow(int selectedFlightId)
        {
            InitializeComponent();
            CentreScreen();
            _flightDetailsManager.SetFlightDetail(selectedFlightId);
            FillComboBoxesWithItems();
            PopulateFlightDetailsTextBoxes(selectedFlightId);
            
        }
        public void FillComboBoxesWithItems()
        {
            foreach (var item in _pilotManager.RetrieveAll())
            {
                pilotCombo.Items.Add(item.ToString());
            }

            //foreach (var item in _airlineManager.RetrieveAll())
            //{
            //    airlineCombo.Items.Add(item.AirlineName);
            //}
            foreach (var item in _planeManager.RetrieveAll())
            {
                planeCombo.Items.Add(item.PlaneModel);
            }
            foreach (var item in _flightStatusManager.RetrieveAll())
            {
                    statusCombo.Items.Add(item.Status);
            }
        }
        public void PopulateFlightDetailsTextBoxes(int flightId)
        {
            var flightDetailStr = _flightDetailsManager.ReturnFlightDetailIDStrings();
            flightIdTxt.Text = flightDetailStr[0];
            pilotCombo.Text = flightDetailStr[1];
            planeCombo.Text = flightDetailStr[3];
            planeCapacityTxt.Text = flightDetailStr[4];
            passengerNumTxt.Text = flightDetailStr[5];
            durationSlider.Value = Int32.Parse(flightDetailStr[6]);
            durationLbl.Content = $"Flight duration ({flightDetailStr[6]} hrs)";
            departureTxt.Text = _flightDetailsManager.ReturnDepartureStr(flightId);
            regionTxt.Text = _airportManager.ReturnRegionGivenFlightId(flightId);
            arrivalTxt.Text = _flightDetailsManager.ReturnArrivalStr(flightId);
            statusCombo.Text = _flightDetailsManager.ReturnStatusGivenFlightId(flightId);
            airlineCombo.Items.Clear();
            foreach (var item in _airlineManager.ReturnAirlinesGivenRegion(departureTxt.Text))
            {
                airlineCombo.Items.Add(item.ToString());
            }
            airlineCombo.Text = flightDetailStr[2];
        }
        private void FlightBoardButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new MainWindow());
        }

        private void AirlinesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AirportsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PilotsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlanesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteFlight_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            //if (messageBoxResult == MessageBoxResult.Yes)
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            //UPDATE FUNCTION
            var flightId = Int32.Parse(flightIdTxt.Text);
            var pilotId = _pilotManager.ReturnPilotID(pilotCombo.Text);
            var airlineId = _airlineManager.ReturnAirlineID(airlineCombo.Text);
            var planeId = _planeManager.ReturnPlaneID(planeCombo.Text);
            var durationInt = durationSlider.Value;
            var passengerNumInt = Int32.Parse(passengerNumTxt.Text);
            var capacityInt = Int32.Parse(planeCapacityTxt.Text);

            try
            {
                _flightDetailsManager.Update(flightId, flightId, pilotId, airlineId, planeId, passengerNumInt, (int)durationInt, capacityInt);
            }
            catch(Exception ex)
            {
                MessageBox.Show("The passenger number is greater than the capacity!");
                return;
            }
            pilotCombo.IsHitTestVisible = false;
            airlineCombo.IsHitTestVisible = false;
            planeCombo.IsHitTestVisible = false;
            statusCombo.IsHitTestVisible = true;
            durationSlider.IsEnabled = false;
            passengerNumTxt.IsReadOnly = true;
            fdTitle.Content = "Flight Details";
            fdTitle.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void EditField_Click(object sender, RoutedEventArgs e)
        {
            pilotCombo.IsHitTestVisible = true;
            airlineCombo.IsHitTestVisible = true;
            planeCombo.IsHitTestVisible = true;
            statusCombo.IsHitTestVisible = true;
            durationSlider.IsEnabled = true;
            passengerNumTxt.IsReadOnly = false;
            fdTitle.Content = "EDIT MODE";
            fdTitle.Foreground = new SolidColorBrush(Colors.Red);
        }

        private void planeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (sender as ComboBox).SelectedItem.ToString();
            planeCapacityTxt.Text = _planeManager.ReturnCapacity(text).ToString();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            string time = (sender as Slider).Value.ToString();
            durationLbl.Content = $"Flight duration ({time} hrs)";
        }
    }
}
