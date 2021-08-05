﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
            CentreScreen();
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
                    ID = results[i,1]
                    ,FlightNumber = $"{results[i, 0]}0{results[i, 1]}"
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

        private void FullDetailsButton_Click(object sender, RoutedEventArgs e)
        {

            var inputArr =  flightBoard.SelectedValue.ToString();
            var sub = inputArr.Substring(inputArr.IndexOf("= ")+2, inputArr.IndexOf(",") - inputArr.IndexOf("=")-2);
            var flightDetailsWindow = new FlightDetailsWindow(Int32.Parse(sub));
            flightDetailsWindow.Show();
            flightDetailsWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Close();
        }
    }
}
