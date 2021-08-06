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
        FlightDetailsManager _flightDetailsManager = new FlightDetailsManager();
        
        public void CentreScreen()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }
        public bool CheckIfFieldsHaveValues()
        {
            if (departCombo.Text != string.Empty && arrivalCombo.Text != string.Empty && pilotCombo.Text != string.Empty && airlineCombo.Text != string.Empty
                && planeCombo.Text != string.Empty && datePicker.Text != string.Empty && PresetTimePicker.Text!=null
                && passengerNumTxt.Text != string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public void CheckIfAddButtonOn()
        {
            if (passengerNumTxt.Text != string.Empty)
            {
                if (Int32.Parse(passengerNumTxt.Text) > Int32.Parse(planeCapacityTxt.Text))
                    {
                        AddFlightButton.ToolTip = "Your passenger number is over capacity.";
                        AddFlightButton.IsEnabled = false;
                        ToolTipService.SetShowOnDisabled(AddFlightButton, true);
                        return;
                    }
            }
            if (!CheckIfFieldsHaveValues())
            {
                AddFlightButton.ToolTip = "Please fill out all the details";
                AddFlightButton.IsEnabled = false;
                ToolTipService.SetShowOnDisabled(AddFlightButton, true);

            }
            else
            {
                AddFlightButton.ToolTip = "Click here when you are ready to submit.";
                AddFlightButton.IsEnabled = true;
            }

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
            foreach (var item in _pilotManager.RetrieveAll()) //Add customisation to partial class if you want specific string in list
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
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Add Flight Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var pilotId = _pilotManager.ReturnPilotID(pilotCombo.Text);
                    var airlineId = _airlineManager.ReturnAirlineID(airlineCombo.Text);
                    var planeId = _planeManager.ReturnPlaneID(planeCombo.Text);
                    var passengerNum = Int32.Parse(passengerNumTxt.Text);
                    var flightDuration = (int)durationSlider.Value;
                    var dateTime = $"{datePicker.Text} {PresetTimePicker.Text}:00";
                    var departId = _airportManager.ReturnAirportIdGivenAirportStr(departCombo.Text);
                    var arrivalId = _airportManager.ReturnAirportIdGivenAirportStr(arrivalCombo.Text);
                    _flightManager.Create(DateTime.Parse(dateTime), departId, arrivalId);
                    _flightDetailsManager.Create(pilotId, airlineId, planeId, passengerNum, flightDuration);
                }

        }

        private void ClearField_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new AddFlightWindow());
            this.Close();
            
        }

        private void PresetTimePicker_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            CheckIfAddButtonOn();
        }

        private void departCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckIfAddButtonOn();
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
            CheckIfAddButtonOn();
            string text = (sender as ComboBox).SelectedItem.ToString();
            planeCapacityTxt.Text = _planeManager.ReturnCapacity(text).ToString();
            passengerNumTxt.IsReadOnly = false;
            passengerNumTxt.ToolTip = null;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           
            string time = (sender as Slider).Value.ToString();
            durationLbl.Content = $"Flight duration ({time} hrs)";
        }

        private void passengerNumTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!CheckIfFieldsHaveValues())
            {
                AddFlightButton.ToolTip = "Please fill out all the details";
                AddFlightButton.IsEnabled = false;
                ToolTipService.SetShowOnDisabled(AddFlightButton, true);
                return;
            }
            CheckIfPassengerNumOverCapacity();
            
        }
        public void CheckIfPassengerNumOverCapacity()
        {

           if (passengerNumTxt.Text != string.Empty)
            {
                if (planeCapacityTxt.Text != string.Empty)
                {
                    if (Int32.Parse(passengerNumTxt.Text) > Int32.Parse(planeCapacityTxt.Text))
                    {

                        AddFlightButton.ToolTip = "Your passenger number is over capacity.";
                        AddFlightButton.IsEnabled = false;
                        ToolTipService.SetShowOnDisabled(AddFlightButton, true);

                    }
                    else
                    {
                        AddFlightButton.ToolTip = "Click here when you are ready to submit.";
                        AddFlightButton.IsEnabled = true;
                    }

                }  
            }
        }
        private void arrivalCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckIfAddButtonOn();
        }

        private void airlineCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckIfAddButtonOn();
        }

        private void selected_DateChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckIfAddButtonOn();
        }

        private void datePicker_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            CheckIfAddButtonOn();
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void minimiseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
