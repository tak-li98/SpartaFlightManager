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
        public void CentreScreen()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
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
        }
        public void PopulateFlightDetailsTextBoxes()
        {
            var flightDetailStr = _flightDetailsManager.ReturnFlightDetailIDStrings();
            if (_flightDetailsManager.SelectedFlightDetail != 0)
            {
                flightIdTxt.Text = flightDetailStr[0];
                pilotTxt.Text = flightDetailStr[1];
                airlineTxt.Text = flightDetailStr[2];
                planeModelTxt.Text = flightDetailStr[3];
                planeCapacityTxt.Text = flightDetailStr[4];
                passengerNumTxt.Text = flightDetailStr[5];
                flightDurationTxt.Text = flightDetailStr[6];
            }
        }
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Close();
        }
    }
}
