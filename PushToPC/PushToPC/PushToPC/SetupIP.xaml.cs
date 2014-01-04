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

namespace PushToPC
{
    public partial class SetupIP : PhoneApplicationPage
    {
        

        public SetupIP()
        {
            InitializeComponent();
            SetControls();
        }

        private bool IsIPThere()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("IP") == true)
            {
                IsolatedStorageSettings.ApplicationSettings.Remove("IP");
                IsolatedStorageSettings.ApplicationSettings.Add("IP", txtRemoteHost.Text);
                IsolatedStorageSettings.ApplicationSettings.Remove("Port");
                IsolatedStorageSettings.ApplicationSettings.Add("Port", portTextBox.Text);
                IsolatedStorageSettings.ApplicationSettings.Save();
                return true;
            }
                
            else if (IsolatedStorageSettings.ApplicationSettings.Contains("IP") == false)
            {
                IsolatedStorageSettings.ApplicationSettings.Add("IP", txtRemoteHost.Text);
                IsolatedStorageSettings.ApplicationSettings.Add("Port", portTextBox.Text);
                IsolatedStorageSettings.ApplicationSettings.Save();
                return true;
            }

            else
            {
                MessageBox.Show("Funky Bug Here, bro.");
                return false;
            }

        }

        //A method that restricts input to numbers only for a callable textbox control.
        private void SetInputScope(TextBox textBoxControl)
        {
            InputScopeNameValue digitsInputNameValue = InputScopeNameValue.TelephoneNumber;
            textBoxControl.InputScope = new InputScope()
            {
                Names = { new InputScopeName() { NameValue = digitsInputNameValue } }
            };
        }

        //Set the restriction here.
        private void SetControls()
        {
            SetInputScope(txtRemoteHost);
            SetInputScope(portTextBox);
        }

        /// <summary>
        /// Validates the txtRemoteHost TextBox, Port Entry Space, and Whether or not the port is a integer.
        /// </summary>
        /// <returns>True if the txtRemoteHost contains valid data as well as the Port Textbox, and the Portnumber is an integer.
        /// otherwise False
        /// </returns>
        private bool ValidateRemoteHost()
        {
            // The txtRemoteHost must contain some text
            if (String.IsNullOrWhiteSpace(txtRemoteHost.Text))
            {
                MessageBox.Show("No IP Address is specified.");
                return false;
            }

            return true;
        }

        private bool ValidateRemotePort()
        {

            if (String.IsNullOrWhiteSpace(portTextBox.Text))
            {
                MessageBox.Show("No Port is specified.");
                return false;
            }

            return true;
        }

        private bool isPortNumber()
        {
            string port = IsolatedStorageSettings.ApplicationSettings["Port"].ToString();
            int portisnumber;
            bool portnumber = int.TryParse(port, out portisnumber);
            if (portnumber == false)
            {
                MessageBox.Show("Please enter a number only for a port.");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
                if (IsIPThere() && ValidateRemoteHost() && ValidateRemotePort() && isPortNumber())
                {
                    if (IsolatedStorageSettings.ApplicationSettings.Contains("FirstRunSetup") == true) //Here to fix a problem in which you re-run setup, and the file is already there.
                    { //If the file is there, because you've already run setup, delete it and re-add it. 
                        IsolatedStorageSettings.ApplicationSettings.Remove("FirstRunSetup");
                        IsolatedStorageSettings.ApplicationSettings.Add("FirstRunSetup", "Setup configured correctly.");
                    }

                    else //Otherwise, just add it.
                    {
                        IsolatedStorageSettings.ApplicationSettings.Add("FirstRunSetup", "Setup configured correctly.");
                    }
                    
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }

        }
    }
}