using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using System.IO;
using Microsoft.Phone.Tasks;




namespace PushToPC
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constants


        // Constructor
        public MainPage()
        {
            InitializeComponent();
            if (FirstRun() == true)
            {
                PushPivot.IsEnabled = false;
                MainPivot.SelectedItem = SettingsPivot;
                textBlock4.Text = "Welcome, you'll need to run setup in order to configure the application.";
                donateButton.IsEnabled = false;
            }

        }


        // Declare the MarketplaceDetailTask object with page scope
        // so we can access it from event handlers.
        MarketplaceDetailTask _marketPlaceDetailTask = new MarketplaceDetailTask();



        private bool FirstRun()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("FirstRunSetup") == false)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool GetIPAddress()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("IP") && (IsolatedStorageSettings.ApplicationSettings.Contains("Port")))
            {
                return true;
            }

            else
            {
                MessageBox.Show("Internal Error, please re-run setup.");
                return false;
            }

        }

        #region UI Validation
        /// <summary>
        /// Validates the txtInput TextBox
        /// </summary>
        /// <returns>True if the txtInput TextBox contains valid data, otherwise 
        /// False.
        ///</returns>
        private bool ValidateInput()
        {

            // txtInput must contain some text
            if (String.IsNullOrWhiteSpace(txtInput.Text))
            {
                MessageBox.Show("Please enter a webpage URL.");
                return false;
            }

            return true;
        }

        #endregion

        private void pushButton_Click(object sender, RoutedEventArgs e)
        {

            // Make sure we can perform this action with valid data
            if (ValidateInput() && GetIPAddress())
            {

                // Instantiate the SocketClient
                SocketClient client = new SocketClient();


                // Attempt to connect to the server
                string IP = IsolatedStorageSettings.ApplicationSettings["IP"].ToString();
                string port = IsolatedStorageSettings.ApplicationSettings["Port"].ToString();
                int ECHO_PORT = Convert.ToInt32(port);
                string result = client.Connect(IP, ECHO_PORT);

                // Attempt to send our webpage.
                result = client.Send(txtInput.Text);

                // Receive a response from the web server.

                result = client.Receive();


                // Close the socket connection explicitly.
                client.Close();

                MessageBox.Show(result);

                #region History
                IsolatedStorageFile pushhistory = IsolatedStorageFile.GetUserStoreForApplication();

                

                /*StreamWriter historyWriter = new StreamWriter(new IsolatedStorageFileStream("History.txt", FileMode.Append, pushhistory));
                historyWriter.Write("\n"+txtInput.Text);
                historyWriter.Close();
                */
                #endregion

            }
        }

        private void setupButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SetupInfo.xaml", UriKind.Relative));
        }

        private void donateButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Donation.xaml", UriKind.Relative));
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            _marketPlaceDetailTask.Show();
        }




    }
}