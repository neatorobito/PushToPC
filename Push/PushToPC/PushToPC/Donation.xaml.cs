using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Notification;
using System.IO.IsolatedStorage;
using System.IO;

namespace PushToPC
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //MessageBox("Thanks for supporting us in our future development and expansion.");
           MessageBoxResult result1 = 
    MessageBox.Show("We utilize PayPal for donations. If you agree with the use of PayPal for this transaction, press Ok.", 
    "Donation", MessageBoxButton.OKCancel);

    if (result1 == MessageBoxResult.OK)
    {
        MessageBox.Show("Thanks for your support! Our PayPal page will open on your computer.");
        // Make sure we can perform this action with valid data
        if (true)
        {

            // Instantiate the SocketClient
            SocketClient client = new SocketClient();


            // Attempt to connect to the server
            string IP = IsolatedStorageSettings.ApplicationSettings["IP"].ToString();
            string port = IsolatedStorageSettings.ApplicationSettings["Port"].ToString();
            int ECHO_PORT = Convert.ToInt32(port);
            string result = client.Connect(IP, ECHO_PORT);

            string paypal = "http://push.reachthepeak.org/#easy-contactnbsphelp-us-out";

            // Attempt to send our webpage.
            result = client.Send(paypal);


            // Receive a response from the web server.

            result = client.Receive();


            // Close the socket connection explicitly.
            client.Close();

            MessageBox.Show(result);

        }
    }
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}