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
    /// Interaction logic for Airlines.xaml
    /// </summary>
    public partial class AirlineWindow : Window
    {
        private AirlineManager _airlineManager = new AirlineManager();

        public void CentreScreen()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }
        public AirlineWindow()
        {
            InitializeComponent();
            CentreScreen();
            PopulateViewList();
        }

        private void AirlinesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        public void PopulateViewList()
        {
            var list = _airlineManager.ReturnAirlineAndRegion();
            airlineBoard.ItemsSource = list;
        }

        private void FlightBoardButton_Click(object sender, RoutedEventArgs e)
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

        private void minimiseBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void airlineBoard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MoreInfoButton.IsEnabled = true;
            if(airlineBoard.SelectedItem != null)
            {
                _airlineManager.SetSelectedAirline(airlineBoard.SelectedItem);
            }
        }

        private void MoreInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var something = airlineBoard.SelectedItems[0];
            var airlineSelected = _airlineManager.SelectedAirlineRegion.AirlineName.Replace(" ", "");
            ProcessStartInfo link = new ProcessStartInfo();
            link.UseShellExecute = true;
            link.FileName = "https://www.google.com/search?q=" + airlineSelected + "+airline";
            Process.Start(link);
        }
    }
}
