using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
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
            if (AddPilotButton.Content.ToString() == "ADD NEW PILOT")
            {
                EditPilotButton.Visibility = Visibility.Hidden;
                pilotBoard.Visibility = Visibility.Hidden;
                addPilotPanel.Visibility = Visibility.Visible;
                AddPilotButton.Content = "GO BACK";
                return;
            }
            if(AddPilotButton.Content.ToString() == "GO BACK")
            {
                pilotBoard.Visibility = Visibility.Visible;
                addPilotPanel.Visibility = Visibility.Hidden;
                AddPilotButton.Content = "ADD NEW PILOT";
                EditPilotButton.Visibility = Visibility.Visible;
                PopulateViewList();
                return;
            }
            
        }

        private void minimiseBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Application Exit", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void EditPilotButton_Click(object sender, RoutedEventArgs e)
        {
            if (EditPilotButton.Content.ToString() == "EDIT PILOT")
            {
                AddPilotButton.Visibility = Visibility.Hidden;
                editPilotPanel.Visibility = Visibility.Visible;
                pilotBoard.Visibility = Visibility.Hidden;
                EditPilotButton.Content = "GO BACK";
                EditPilotButton.ToolTip = "Go back to pilots";
                titleEditCombo.Text = _pilotManager.SelectedPilot.Title;
                firstNameEditTxt.Text = _pilotManager.SelectedPilot.FirstName;
                surnameEditTxt.Text = _pilotManager.SelectedPilot.LastName;
                if (_pilotManager.SelectedPilot.PhotoLink != null)
                {
                    try
                    {
                        imageEditBox.Fill = new ImageBrush
                        {
                            ImageSource = new BitmapImage(new Uri("pack://application:,,,/" + _pilotManager.SelectedPilot.PhotoLink))
                        };
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                 };
                return;
            }
            if (EditPilotButton.Content.ToString() == "GO BACK")
            {
                EditPilotButton.ToolTip = "Edit selected pilot";
                editPilotPanel.Visibility = Visibility.Hidden;
                pilotBoard.Visibility = Visibility.Visible;
                AddPilotButton.Visibility = Visibility.Visible;
                titleCombo.SelectedIndex = -1;
                firstNameTxt.Text = string.Empty;
                surnameTxt.Text = string.Empty;
                PopulateViewList();
                EditPilotButton.Content = "EDIT PILOT";
                return;
            }

        }
        public string PhotoPath;
        private void imageBtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                imageBox.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(filename)) };
            }
            try
            {
                var fileNameToSave = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName) + System.IO.Path.GetExtension(dlg.FileName);
                var imagePath = System.IO.Path.Combine(fileNameToSave);
                File.Copy(dlg.FileName,imagePath);
                PhotoPath = fileNameToSave;
            }
            catch(Exception ex)
            {
                PhotoPath = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName) + System.IO.Path.GetExtension(dlg.FileName);
                MessageBox.Show(ex.Message);
            }
            
        }
        private void imageEditBtn_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                imageEditBox.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(filename)) };
            }
            try
            {
                var fileNameToSave = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName) + System.IO.Path.GetExtension(dlg.FileName);
                var imagePath = System.IO.Path.Combine(fileNameToSave);
                File.Copy(dlg.FileName, imagePath);
                PhotoPath = fileNameToSave;
                _pilotManager.UpdatePhoto(_pilotManager.SelectedPilot.PilotId, PhotoPath);
            }
            catch (Exception ex)
            {
                PhotoPath = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName) + System.IO.Path.GetExtension(dlg.FileName);
                MessageBox.Show(ex.Message);
            }

        }
        private async void addPilotBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Add Pilot Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                _pilotManager.Create(firstNameTxt.Text, surnameTxt.Text, titleCombo.Text,PhotoPath);
                addedLbl.Content = "PILOT ADDED!";
                titleCombo.SelectedIndex = -1;
                firstNameTxt.Text = string.Empty;
                surnameTxt.Text = string.Empty;
                addPilotBtn.IsEnabled = false;
                await Task.Delay(1500);
                addPilotBtn.IsEnabled = true;
                addedLbl.Content = string.Empty;
            }
        }

        private void title_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(titleCombo.Text!=null && firstNameTxt.Text!=string.Empty && surnameTxt.Text != string.Empty)
            {
                addPilotBtn.IsEnabled = true;
                return;
            }
            addPilotBtn.IsEnabled = false;
        }

        private void nameTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (titleCombo.Text != null && firstNameTxt.Text != string.Empty && surnameTxt.Text != string.Empty)
            {
                addPilotBtn.IsEnabled = true;
                return;
            }
            addPilotBtn.IsEnabled = false;
        }

        private async void savePilotBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Save Changes Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                _pilotManager.Update(_pilotManager.SelectedPilot.PilotId, firstNameEditTxt.Text, surnameEditTxt.Text, titleEditCombo.Text);
                savePilotBtn.IsEnabled = false;
                saveLbl.Content = "CHANGES SAVED!";
                await Task.Delay(1500);
                saveLbl.Content = string.Empty;
                savePilotBtn.IsEnabled = true;
            }   
        }

        private void titleEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (titleEditCombo.Text != null && firstNameEditTxt.Text != string.Empty && surnameEditTxt.Text != string.Empty)
            {
                savePilotBtn.IsEnabled = true;
                return;
            }
            savePilotBtn.IsEnabled = false;
        }

        private void nameEditTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (titleEditCombo.Text != null && firstNameEditTxt.Text != string.Empty && surnameEditTxt.Text != string.Empty)
            {
                savePilotBtn.IsEnabled = true;
                return;
            }
            savePilotBtn.IsEnabled = false;
        }
        private void delPilotBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Pilot Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                _pilotManager.Delete(_pilotManager.SelectedPilot.PilotId);
                editPilotPanel.Visibility = Visibility.Hidden;
                pilotBoard.Visibility = Visibility.Visible;
                PopulateViewList();
                EditPilotButton.Content = "EDIT PILOT";
                EditPilotButton.IsEnabled = false;
                AddPilotButton.Visibility = Visibility.Visible;
            }
                
        }
    }
}
