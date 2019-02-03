// Author:  Thomas Owca
// Project: Tams' Pizzeria
// File:    MainWindow.xaml.cs

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Threading;

namespace Tams__Pizzeria
{
    /// <summary>
    /// This window is the main window that the user will be asked to log into.
    /// This application connects to a database and checks the users' credentials
    /// to check for validity. If valid, then the next screen will show. Otherwise,
    /// the user will need to try again.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Initializes the username and password textboxes to null (empty).
            txtBoxUserName.Text = null;
            passwordBox.Password = null;
        }

        // DispatcherTimer is used as a Timer. It is set up later as a delay to allow the next process to initialize before launching.
        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        // Creating two instances of additional screens/windows that will be launched 
        // when the user's credential are authorized during the log-in process.
        private SuccessScreen successScreen = new SuccessScreen();
        private ServiceScreen serviceScreen = new ServiceScreen();

        // This event-handler method is called when the user clicks the "Log In" button.
        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            // Initializing the connectionString to null and declaring a SqlConnection object reference variable.
            string connection = null;
            SqlConnection cnn;

            // Assigning the connection information for SQL Server access.
            connection = @"Data Source=THOMASPC\THOMASSQL;Initial Catalog=PizzaDB;User ID=sa;Password=M4rica";
            
            // Creating a new instance of SqlConnection and passing in the connectionString to the class constructor.
            cnn = new SqlConnection(connection);
            
            bool success = false;
            try
            {
                // Open connection to the database table.
                cnn.Open();

                // The SQL command that selects the username and password from the 
                string Get_Data = "SELECT Username, Password FROM Employee_Users";
                
                // Instantiating the SqlCommand object that takes in parameters for the command and for the SqlConnection object. 
                SqlCommand cmd = new SqlCommand(Get_Data, cnn);

                // Creating a SqlDataReader object and assigning the object (cmd) that has the SQL connection and the SQL command information.
                // This class allows to read and extract records from the database's tables.
                SqlDataReader reader = cmd.ExecuteReader();
                
                // Keeps reading while there is an another record available to be read in the table.
                while (reader.Read())
                {
                    // Extracts the record at the provided column and assigns the string representation of the username and password
                    // to the appropriate variable. 
                    string username = reader.GetString(0); //column 1 | field 1
                    string password = reader.GetString(1); //column 2 | field 2

                    // Checks if the inputted user credentials match the ones found in the table.
                    if (txtBoxUserName.Text == username && passwordBox.Password == password)
                    {
                        success = true;

                        // Hides the current window. Following this, the successScreen window will appear.
                        this.Hide();
                        successScreen.Show();

                        // Creates the new EventHandler that handles the timer.
                        dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);

                        // Initializes the interval that the ticking of the timer occurrs at. Currently set to 3 second intervals.
                        dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
                        
                        // Starts the timer.
                        dispatcherTimer.Start();

                        // Resets the two text boxes for the user credentials.
                        txtBoxUserName.Text = null;
                        passwordBox.Password = null;

                        // Exits the while loop.
                        break;
                    }
                }
                // Close the connection.
                cnn.Close();
            }
            catch
            {
                // Displays 'error message' if an exception is caught or an error is detected during execution.
                MessageBox.Show("Connection with server has failed.");
            }

            // Display failure message when log-in credentials have not been authorized or are not found in the database table.
            if (!success)
                MessageBox.Show("Log in failed.");

        }

        // This event-handler method is called when the user's log-in is authenticated. 
        // This method handles the timer and the hiding/showing of window(s).
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            successScreen.Hide();
            serviceScreen.Show();
            dispatcherTimer.Stop();
        }

        // This event-handler method is called when the window gets closed.
        private void Window_Closed(object sender, EventArgs e)
        {
            // Kills any background process(es) if the .exe gets closed 
            // but keeps running in the background.
            System.Diagnostics.Process.GetCurrentProcess().Kill(); 
        }

        // This event-handler is called when the user clicks on the button for changing/updating password.
        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a new MessageBox to ask user to confirm that they truly intend to run this external application.
                MessageBoxResult messageBoxResult =
                    System.Windows.MessageBox.Show("Are you sure you want to do this?", "Update Password/Security", System.Windows.MessageBoxButton.YesNo);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    // TO DO - Open and run the .exe file for the application.
                    MessageBox.Show("Sorry this feature is not available in this build.");
                }
            }
            catch
            {
                MessageBox.Show("Sorry, this feature isn't available yet in this build.");
            }
        }
    } 
}
