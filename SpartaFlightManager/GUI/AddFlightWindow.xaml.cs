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
        private PilotManager _pilotManager = new PilotManager();
        private AirlineManager _airlineManager = new AirlineManager();
        private FlightManager _flightManager = new FlightManager();
        private PlaneManager _planeManager = new PlaneManager();
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
    }
}
