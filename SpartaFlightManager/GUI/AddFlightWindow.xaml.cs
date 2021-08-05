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
    /// Interaction logic for AddFlightWindow.xaml
    /// </summary>
    public partial class AddFlightWindow : Window
    {
        PilotManager _pilotManager = new PilotManager();
        AirlineManager _airlineManager = new AirlineManager();
        FlightManager _flightManager = new FlightManager();
        PlaneManager _planeManager = new PlaneManager();
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
        public AddFlightWindow()
        {
            InitializeComponent();
            CentreScreen();
            FillComboBoxesWithItems();
        }
        public void FillComboBoxesWithItems()
        {
            foreach (var item in _pilotManager.RetrieveAll())
            {
                pilotCombo.Items.Add(item.ToString());
            }
            foreach (var item in _airlineManager.RetrieveAll())
            {
                airlineCombo.Items.Add(item.AirlineName);
            }
            foreach (var item in _planeManager.RetrieveAll())
            {
                planeCombo.Items.Add(item.PlaneModel);
            }
            foreach (var item in _airportManager.RetrieveAll())
            {
                departCombo.Items.Add(item.ToString());
                arrivalCombo.Items.Add(item.ToString());
            }
        }
        private void FlightBoardButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new MainWindow());
        }

        private void AddFlight_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ClearField_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PresetTimePicker_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {

        }

        private void departCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (sender as ComboBox).SelectedItem.ToString();
            regionTxt.Text = _airportManager.ReturnRegion(text);
            airlineCombo.Items.Clear();
            foreach (var item in _airlineManager.ReturnAirlinesGivenRegion(text))
            {
                airlineCombo.Items.Add(item.ToString());
            }
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
