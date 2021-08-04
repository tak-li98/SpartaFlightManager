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
        public MainWindow()
        {
            InitializeComponent();
            PopulateListView();
        }
        public void GetFlightBoardInfo()
        {

        }
        private void PopulateListView()
        {
            var results = _flightManager.ReturnFlightBoardInfoFromFlights();
            for (int i = 0; i < results.Length / 12; i++)
            {
                var row = new { 
                    FlightNumber = $"{results[i, 0]}0{results[i, 1]}"
                    ,Departure = $"{results[i,3]} ({results[i,5]})"
                    ,Arrival = $"{results[i, 9]} ({results[i, 11]})"
                    ,Date= results[i,6]
                    ,Time=results[i,7]
                    ,Status=results[i,8]
                };
                flightBoard.Items.Add(row);
            }

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

    }
}
