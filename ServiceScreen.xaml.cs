// Author:  Thomas Owca
// Project: Tams' Pizzeria
// File:    ServiceScreen.xaml.cs

using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace Tams__Pizzeria
{
    /// <summary>
    /// This window's logic is stored in this file. This is where the majority of the programming takes place.
    /// Many event handlers are found throughout that handle the logic of creating a new customer order, viewing the restaurant menu and
    /// viewing past orders. Custom user controls could be used for each separate 'page' of this window but some hiding and displaying of 
    /// individual controls worked out for the best since this isn't a super complex application.
    /// </summary>
    public partial class ServiceScreen : Window
    {
        public ServiceScreen()
        {
            InitializeComponent();
            
            // Hiding the grid for the food menu and the data grid for the past orders.
            // Sets the custom user control for creating a customer order to be visible as default for this window.
            dataGridPastOrders.Visibility = Visibility.Hidden;
            OurMenuGrid.Visibility = Visibility.Hidden;
            CreateOrderUserControl.Visibility = Visibility.Visible;
        }

        // ************************************* Event Handling For Mouse Events ********************************************
        // These next few methods handle the color changes that take place on specific mouse events for each label that
        // acts like a custom button. This summary relates to the next 8 mouse event handler functions below.
        // ******************************************************************************************************************
        private void LblCreateOrder_MouseEnter(object sender, MouseEventArgs e)
        {
            lblCreateOrder.Background = Brushes.DarkSlateGray;
        }

        private void LblOurMenu_MouseEnter(object sender, MouseEventArgs e)
        {
            lblOurMenu.Background = Brushes.DarkSlateGray;
        }

        private void LblPastOrders_MouseEnter(object sender, MouseEventArgs e)
        {
            lblPastOrders.Background = Brushes.DarkSlateGray;
        }

        private void LblCreateOrder_MouseLeave(object sender, MouseEventArgs e)
        {
            lblCreateOrder.Background = Brushes.Black;
        }

        private void LblOurMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            lblOurMenu.Background = Brushes.Black;
        }

        private void LblPastOrders_MouseLeave(object sender, MouseEventArgs e)
        {
            lblPastOrders.Background = Brushes.Black;
        }

        private void LblLogOut_MouseEnter(object sender, MouseEventArgs e)
        {
            lblLogOut.Background = Brushes.DarkSlateGray;
        }

        private void LblLogOut_MouseLeave(object sender, MouseEventArgs e)
        {
            lblLogOut.Background = Brushes.Black;
        }

        // This event handler is called when the user clicks the label that acts as a button. It bascially asks the user to confirm the
        // logging out of the Employee Service System Portal. If the user logs out, then the main window will be launched - ready for a 
        // user to log into the portal again.
        private void LblLogOut_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Create a new MessageBox to ask user to confirm that they truly intend to exit the application.
            MessageBoxResult messageBoxResult = 
                System.Windows.MessageBox.Show("Are you sure you want to log out?", "Exit", System.Windows.MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                // Hide the current window.
                this.Hide();

                // Open and run the .exe file for the application. Bringing the user back to the main window.
                System.Diagnostics.Process.Start("Tams Pizzeria.exe");

                // Exit the previous run window.
                Environment.Exit(0);

            }
        }

        // This event handler is called when the window gets closed. 
        private void Window_Closed(object sender, EventArgs e)
        {
            // Kills any background process if the .exe gets closed 
            // but keeps running in the background.
            System.Diagnostics.Process.GetCurrentProcess().Kill(); 
        }

        // This event handler is called when the user clicks on the label/button that displays the past customer orders.
        private void lblPastOrders_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Make the data grid visibile. Hides the user control for ordering a pizza and hides the food menu grid.
            dataGridPastOrders.Visibility = Visibility.Visible;
            CreateOrderUserControl.Visibility = Visibility.Hidden;
            OurMenuGrid.Visibility = Visibility.Hidden;

            // Set the title at the top that relates to the current tab/page selection.
            tabLabelText.Content = "Order History";

            // Sets up the connectionString to the database.
            string connectionString = null;
            connectionString = @"Data Source=THOMASPC\THOMASSQL;Initial Catalog=PizzaDB;User ID=sa;Password=M4rica";

            // Create a new SqlConnection object and passes in the connectionString as an argument to the constructor.
            SqlConnection cnn = new SqlConnection(connectionString);

            // Create a string that is assigned the SQL command that will be executed.
            string getData = "SELECT OrderID AS 'Order #', OrderTIME AS 'Date Time', FirstName AS 'Name', Crust AS 'Crust', Size AS 'Size', " +
                "FirstTopping AS 'First Topping', SecondTopping AS 'Second Topping', ThirdTopping AS 'Third Topping', SubTotal AS 'Sub Total', " +
                "FinalTotal AS 'Total', TPCI# AS 'TPCI #', OnlineOrder AS 'Online Order', PromotionalCode AS 'Promotional Code' FROM PastOrders ORDER BY OrderID DESC";

            // Create the SqlCommand object and pass in the string with the command and the SqlConnection as arguments.
            SqlCommand cmd = new SqlCommand(getData, cnn);

            // Create the data adapter that will be used to fill the DataTable.
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            // Create the table (DataTable).
            DataTable dtRecord = new DataTable();

            // Use the adapter to fill the data table.
            sda.Fill(dtRecord);

            // Set the source of the data grid as the newly populated data table using the default view.
            dataGridPastOrders.ItemsSource = dtRecord.DefaultView;

            // Setting the visibility of a few properties belonging to the data grid.
            dataGridPastOrders.HeadersVisibility = DataGridHeadersVisibility.Column;
            dataGridPastOrders.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            dataGridPastOrders.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;

            // Setting several data grid properties to true or false.
            dataGridPastOrders.AutoGenerateColumns = true;
            dataGridPastOrders.CanUserAddRows = false;
            dataGridPastOrders.CanUserResizeRows = false;
            dataGridPastOrders.CanUserResizeColumns = false;
            dataGridPastOrders.CanUserSortColumns = true;
            dataGridPastOrders.CanUserReorderColumns = false;
            dataGridPastOrders.CanUserDeleteRows = false;
            dataGridPastOrders.IsReadOnly = true;

            // Sets the height of the data grid.
            dataGridPastOrders.RowHeight = 27.1;

            // Sets the background and foreground colors for the data grid.
            dataGridPastOrders.RowBackground = Brushes.Crimson;
            dataGridPastOrders.AlternatingRowBackground = Brushes.Tomato;
            dataGridPastOrders.Foreground = Brushes.White;
        }

        // This event handler is called when the user clicks the label button for the restaurant menu.
        private void lblOurMenu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Set the page/tab label that is the title for the current page/tab selection.
            tabLabelText.Content = "Our Menu";

            // Sets the visibility of the tabs/pages accordingly based on the current selection.
            dataGridPastOrders.Visibility = Visibility.Hidden;
            CreateOrderUserControl.Visibility = Visibility.Hidden;
            OurMenuGrid.Visibility = Visibility.Visible;
        }

        // This event handler is called when the user clicks the label button for the restaurant menu.
        private void lblCreateOrder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Set the page/tab label that is the title for the current page/tab selection.
            tabLabelText.Content = "Create Order";

            // Sets the visibility of the tabs/pages accordingly based on the current selection.
            dataGridPastOrders.Visibility = Visibility.Hidden;
            CreateOrderUserControl.Visibility = Visibility.Visible;
            OurMenuGrid.Visibility = Visibility.Hidden;
        }
    }
}
