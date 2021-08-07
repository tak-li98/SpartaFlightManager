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
    /// Interaction logic for PilotWindow.xaml
    /// </summary>
    public partial class PilotWindow : Window
    {
        PilotManager _pilotManager = new PilotManager();
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
        public PilotWindow()
        {
            InitializeComponent();
            CentreScreen();
            PopulateViewList();
        }
        public void PopulateViewList()
        {
            pilotBoard.ItemsSource = _pilotManager.RetrieveAll();
        }
        private void FlightBoardButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new MainWindow());
        }

        private void AirportsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new AirportWindow());
        }

        private void AirlinesButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new AirlineWindow());
        }

        private void PlanesButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new PlaneWindow());
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditPilotButton.IsEnabled = true;
            _pilotManager.SetSelectedPilot(pilotBoard.SelectedItem);
        }

        private void AddPilotButton_Click(object sender, RoutedEventArgs e)
        {
            pilotBoard.Visibility = Visibility.Hidden;
        }

        private void minimiseBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void EditPilotButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
