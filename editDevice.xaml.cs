﻿using System.Windows;
using System.Windows.Input;

namespace Wake_On_Lan_Lite
{
    /// <summary>
    /// Logique d'interaction pour editDevice.xaml
    /// </summary>
    public partial class editDevice : Window
    {
        private MainWindow main;

        public editDevice(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
        }

        // Can execute
        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        // Minimize
        private void CommandBinding_Executed_Minimize(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        // Close
        private void CommandBinding_Executed_Close(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        //Function that add or update device
        private void editDeviceClick(object sender, RoutedEventArgs e)
        {
            fileControl file = new fileControl();
            if(editDeviceButton.Content.ToString() == "Add")
            {
                file.addAddress(new Device() { ID = file.getAllAddresses().Count, NAME = nameTextBox.Text, ADDRESS = addressTextBox.Text });
            }
            else
            {
                Device updateDevice = (Device)main.deviceList.SelectedItem;
                file.updateAddress(new Device() { ID = updateDevice.ID, NAME = nameTextBox.Text, ADDRESS = addressTextBox.Text });
            }
            this.main.dataRefresh();

            this.Close();
        }
    }
}