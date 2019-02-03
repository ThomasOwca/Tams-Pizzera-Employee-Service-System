// Author:  Thomas Owca
// Project: Tams' Pizzeria
// File:    UserControl2.xaml.cs

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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tams__Pizzeria
{
    /// <summary>
    /// This custom user control is used for creating customer pizza orders.
    /// It has validation to make sure all the fields are filled out.
    /// This user control calculates the order cost(s) based on the selections.
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        public UserControl2()
        {
            InitializeComponent();
        }

        // Costs of the base pizzas. Based on chosen style and size together.
        private decimal RegSmall = 8.00M;
        private decimal RegMed = 10.00M;
        private decimal RegLarge = 12.00M;
        private decimal DeepSmall = 10.00M;
        private decimal DeepMed = 12.00M;
        private decimal DeepLarge = 14.00M;
        private decimal ThinSmall = 6.00M;
        private decimal ThinMed = 8.00M;
        private decimal ThinLarge = 10.00M;

        // This event handler is called when the pizza crust selection is changed.
        private void ComboBoxCrust_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If the selection is not the default selection, then enable the control for the pizza size.
            if (comboBoxCrust.SelectedIndex != -1)
            {
                comboBoxPizzaSize.IsEnabled = true;
            }

            // The next if/else if statements are self documenting. Based on the two selections in the combo boxes
            // the cost will be assigned accordingly. If both of these combo boxes have valid selections then the first topping
            // combo box will be enabled for the user to make the next selection.
            if (comboBoxCrust.SelectedIndex == 0 && comboBoxPizzaSize.SelectedIndex == 0) //Regular-Small
            {
                labelCrustCost.Content = RegSmall.ToString();
                comboBoxTopping1.IsEnabled = true;
            }
            else if (comboBoxCrust.SelectedIndex == 0 && comboBoxPizzaSize.SelectedIndex == 1) //Regular-Medium
            {
                labelCrustCost.Content = RegMed.ToString();
                comboBoxTopping1.IsEnabled = true;
            }
            else if (comboBoxCrust.SelectedIndex == 0 && comboBoxPizzaSize.SelectedIndex == 2) //Regular-Large
            {
                labelCrustCost.Content = RegLarge.ToString();
                comboBoxTopping1.IsEnabled = true;
            }
            else if (comboBoxCrust.SelectedIndex == 1 && comboBoxPizzaSize.SelectedIndex == 0) //DeepDish-Small
            {
                labelCrustCost.Content = DeepSmall.ToString();
                comboBoxTopping1.IsEnabled = true;
            }
            else if (comboBoxCrust.SelectedIndex == 1 && comboBoxPizzaSize.SelectedIndex == 1) //DeepDish-Medium
            {
                labelCrustCost.Content = DeepMed.ToString();
                comboBoxTopping1.IsEnabled = true;
            }
            else if (comboBoxCrust.SelectedIndex == 1 && comboBoxPizzaSize.SelectedIndex == 2) //DeepDish-Large
            {
                labelCrustCost.Content = DeepLarge.ToString();
                comboBoxTopping1.IsEnabled = true;
            }
            else if (comboBoxCrust.SelectedIndex == 2 && comboBoxPizzaSize.SelectedIndex == 0) //Thin-Small
            {
                labelCrustCost.Content = ThinSmall.ToString();
                comboBoxTopping1.IsEnabled = true;
            }
            else if (comboBoxCrust.SelectedIndex == 2 && comboBoxPizzaSize.SelectedIndex == 1) //Thin-Medium
            {
                labelCrustCost.Content = ThinMed.ToString();
                comboBoxTopping1.IsEnabled = true;
            }
            else if (comboBoxCrust.SelectedIndex == 2 && comboBoxPizzaSize.SelectedIndex == 2) //Thin-Large
            {
                labelCrustCost.Content = ThinLarge.ToString();
                comboBoxTopping1.IsEnabled = true;
            }

            // These next few lines affect the labels and their contents on the receipt on the right hand side of the UI.
            if (comboBoxCrust.SelectedIndex == -1)
                labelCrust.Content = "Crust/Size";
            else if (comboBoxCrust.SelectedIndex != -1 && comboBoxPizzaSize.SelectedIndex == -1)
                labelCrust.Content =  comboBoxCrust.SelectedValue.ToString() + " - Choose Size";
            else
                labelCrust.Content = comboBoxCrust.SelectedValue.ToString() + " - " + comboBoxPizzaSize.SelectedValue.ToString();

            // Calculate the subtotal up to this point based on the user's selections.
            CalculateSubTotal();

        }

        // This event handler is called when the pizza size is called. The code within is very similar to the selection changed event handler
        // for changes in the pizza crust combo box.
        private void ComboBoxPizzaSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // The next if/else if statements are self documenting. Based on the two selections in the combo boxes
            // the cost will be assigned accordingly. If both of these combo boxes have valid selections then the first topping
            // combo box will be enabled for the user to make the next selection.
            if (comboBoxCrust.SelectedIndex == 0 && comboBoxPizzaSize.SelectedIndex == 0) //Regular-Small
            {
                labelCrustCost.Content = RegSmall.ToString();
                comboBoxTopping1.IsEnabled = true;
            }
            else if (comboBoxCrust.SelectedIndex == 0 && comboBoxPizzaSize.SelectedIndex == 1) //Regular-Medium
            {
                labelCrustCost.Content = RegMed.ToString();
                comboBoxTopping1.IsEnabled = true;
            }
            else if (comboBoxCrust.SelectedIndex == 0 && comboBoxPizzaSize.SelectedIndex == 2) //Regular-Large
            {
                labelCrustCost.Content = RegLarge.ToString();
                comboBoxTopping1.IsEnabled = true;
            }
            else if (comboBoxCrust.SelectedIndex == 1 && comboBoxPizzaSize.SelectedIndex == 0) //DeepDish-Small
            {
                labelCrustCost.Content = DeepSmall.ToString();
                comboBoxTopping1.IsEnabled = true;
            }
            else if (comboBoxCrust.SelectedIndex == 1 && comboBoxPizzaSize.SelectedIndex == 1) //DeepDish-Medium
            {
                labelCrustCost.Content = DeepMed.ToString();
                comboBoxTopping1.IsEnabled = true;
            }
            else if (comboBoxCrust.SelectedIndex == 1 && comboBoxPizzaSize.SelectedIndex == 2) //DeepDish-Large
            {
                labelCrustCost.Content = DeepLarge.ToString();
                comboBoxTopping1.IsEnabled = true;
            }
            else if (comboBoxCrust.SelectedIndex == 2 && comboBoxPizzaSize.SelectedIndex == 0) //Thin-Small
            {
                labelCrustCost.Content = ThinSmall.ToString();
                comboBoxTopping1.IsEnabled = true;
            }
            else if (comboBoxCrust.SelectedIndex == 2 && comboBoxPizzaSize.SelectedIndex == 1) //Thin-Medium
            {
                labelCrustCost.Content = ThinMed.ToString();
                comboBoxTopping1.IsEnabled = true;
            }
            else if (comboBoxCrust.SelectedIndex == 2 && comboBoxPizzaSize.SelectedIndex == 2) //Thin-Large
            {
                labelCrustCost.Content = ThinLarge.ToString();
                comboBoxTopping1.IsEnabled = true;
            }

            // This simply enables the next two combo boxes for the topping selections to be enabled for the user.
            comboBoxTopping2.IsEnabled = true;
            comboBoxTopping3.IsEnabled = true;

            // These next few lines affect the labels and their contents on the receipt on the right hand side of the UI.
            if (comboBoxCrust.SelectedIndex == -1)
                labelCrust.Content = "Crust/Size";
            else if (comboBoxCrust.SelectedIndex != -1 && comboBoxPizzaSize.SelectedIndex == -1)
                labelCrust.Content = comboBoxCrust.SelectedValue.ToString() + " - Choose Size";
            else
                labelCrust.Content = comboBoxCrust.SelectedValue.ToString() + " - " + comboBoxPizzaSize.SelectedValue.ToString();

            // Enables the text box for entering the customer's first name on the order.
            txtBoxFirstName.IsEnabled = true;

            // Calculate the subtotal up to this point based on the user's selections.
            CalculateSubTotal();
        }

        // This event handler is called when the first topping selection is changed.
        private void ComboBoxTopping1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If the topping isn't selected as the default option or isn't selected as 'None', then price the cost at $1.50 for the selection.
            if (comboBoxTopping1.SelectedIndex != 0 && comboBoxTopping1.SelectedIndex != -1)
            {
                labelTopping1.Content = comboBoxTopping1.SelectedValue.ToString();
                labelTopping1Cost.Content = "1.50";
            }
            else
            {
                labelTopping1.Content = " ";
                labelTopping1Cost.Content = "0.00";
                comboBoxTopping2.SelectedIndex = -1;
                comboBoxTopping3.SelectedIndex = -1;
            }

            // Calculate the subtotal up to this point based on the user's selections.
            CalculateSubTotal();
        }

        // This event handler is called when the second topping selection is changed.
        private void ComboBoxTopping2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If the topping isn't selected as the default option or isn't selected as 'None', then price the cost at $0.75 for the selection.
            if (comboBoxTopping2.SelectedIndex != 0 && comboBoxTopping2.SelectedIndex != -1)
            {
                labelTopping2.Content = comboBoxTopping2.SelectedValue.ToString();
                labelTopping2Cost.Content = "0.75";
            }
            else
            {
                labelTopping2.Content = " ";
                labelTopping2Cost.Content = "0.00";
                comboBoxTopping3.SelectedIndex = -1;
            }

            // Calculate the subtotal up to this point based on the user's selections.
            CalculateSubTotal();
        }

        // This event handler is called when the third topping selection is changed.
        private void ComboBoxTopping3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If the topping isn't selected as the default option or isn't selected as 'None', then price the cost at $0.75 for the selection.
            if (comboBoxTopping3.SelectedIndex != 0 && comboBoxTopping3.SelectedIndex != -1)
            {
                labelTopping3.Content = comboBoxTopping3.SelectedValue.ToString();
                labelTopping3Cost.Content = "0.75";
            }
            else
            {
                labelTopping3.Content = " ";
                labelTopping3Cost.Content = "0.00";
            }

            // Calculate the subtotal up to this point based on the user's selections.
            CalculateSubTotal();
        }

        // This event handler is called whenever the Reset/New Order button is clicked.
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // All of the following lines below in this function simply reset the selections to defaults.
            // Also the disabling of the combo boxes after the first one which is the pizza crust selection.
            comboBoxCrust.SelectedIndex = -1;
            comboBoxPizzaSize.SelectedIndex = -1;
            comboBoxPizzaSize.IsEnabled = false;
            comboBoxTopping1.SelectedIndex = -1;
            comboBoxTopping1.IsEnabled = false;
            comboBoxTopping2.SelectedIndex = -1;
            comboBoxTopping2.IsEnabled = false;
            comboBoxTopping3.SelectedIndex = -1;
            comboBoxTopping3.IsEnabled = false;

            // Resets the customer's name in the text box to be empty again.
            txtBoxFirstName.Text = "";
            txtBoxFirstName.Text = null;

            // Disables the text box for the customer's name.
            txtBoxFirstName.IsEnabled = false;

            // Resets the labels on the receipt to the defaults.
            labelCrust.Content = "Crust/Size";
            labelCrustCost.Content = "0.00";
            labelTopping1.Content = "";
            labelTopping1Cost.Content = "0.00";
            labelTopping2.Content = "";
            labelTopping2Cost.Content = "0.00";
            labelTopping3.Content = "";
            labelTopping3Cost.Content = "0.00";
            labelOrderNumValue.Content = "";
            labelOrderNumValue.Visibility = Visibility.Hidden;
            labelSubTotalValue.Content = "0.00";
            labelTotalValue.Content = "0.00";
        }

        // This function simply calculates the subtotal based on the user selections.
        private void CalculateSubTotal()
        {
            // Assign the subtotal by adding the costs together. The costs are converted to be of type - decimal.
            decimal subTotal = Convert.ToDecimal(labelCrustCost.Content) + Convert.ToDecimal(labelTopping1Cost.Content)
                + Convert.ToDecimal(labelTopping2Cost.Content) + Convert.ToDecimal(labelTopping3Cost.Content);

            // Display the subtotal on the receipt.
            labelSubTotalValue.Content = subTotal.ToString();
        }

        // This function calculates the final total based on the current subtotal.
        private void CalculateFinalTotal()
        {
            CalculateSubTotal();
            decimal salesTax = Convert.ToDecimal(labelSalesTaxValue.Content) / 100;
            decimal applicableTax = Convert.ToDecimal(labelSubTotalValue.Content) * salesTax;
            decimal finalTotal = Convert.ToDecimal(labelSubTotalValue.Content) + applicableTax;
            labelTotalValue.Content = finalTotal.ToString("#.##");
        }

        private string maxOrderID;
        private int newOrderID;

        // This event handler is called when the user clicks the submit button on the order screen.
        private void SubmitOrderButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection cnn;

            string connectionString = null;
            connectionString = @"Data Source=THOMASPC\THOMASSQL;Initial Catalog=PizzaDB;User ID=sa;Password=M4rica";

            cnn = new SqlConnection(connectionString);
            bool FieldsVerified = false;

            if (comboBoxPizzaSize.SelectedIndex == -1 || comboBoxCrust.SelectedIndex == -1 || comboBoxTopping1.SelectedIndex == -1
               || comboBoxTopping2.SelectedIndex == -1 || comboBoxTopping3.SelectedIndex == -1 || txtBoxFirstName.Text == null || txtBoxFirstName.Text == "")
            {
                MessageBox.Show("Please fill out all fields.");
            }
            else
                FieldsVerified = true;
           
            if (FieldsVerified)
            {
                try
                {
                    // Opens the connection.
                    cnn.Open();

                    // String representation of the SQL command.
                    string Get_Data = "SELECT MAX(OrderID) FROM dbo.PastOrders";

                    // SqlCommand object allows for the execution of the provided command.
                    SqlCommand cmd = cnn.CreateCommand();
                    cmd.CommandText = Get_Data;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        maxOrderID = reader.GetInt32(0).ToString(); // Retrieves first column.
                    }

                    newOrderID = Convert.ToInt32(maxOrderID) + 1;

                    labelOrderNumValue.Content = newOrderID.ToString();
                    CalculateFinalTotal();
                    labelOrderNumValue.Visibility = Visibility.Visible;
                    
                }
                catch (Exception m)
                {
                    MessageBox.Show("Connection with database has failed.");
                    MessageBox.Show(m.ToString());
                }
                finally
                {
                    cnn.Close();
                }
                 
                // This code below inserts data into the SQL Database table - PastOrders.
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;            // <== lacking
                        command.CommandType = CommandType.Text;
                        command.CommandText = "SET IDENTITY_INSERT dbo.PastOrders ON; " +
                            "INSERT INTO PastOrders (OrderID, OrderTIME, FirstName, Crust, Size, FirstTopping, SecondTopping, ThirdTopping, SubTotal, FinalTotal) " +
                            "VALUES (@orderNum, default, @firstname, @crust, @size, @firsttopping, @secondtopping, @thirdtopping, @subtotal, @finaltotal)" +
                            "SET IDENTITY_INSERT dbo.PastOrders OFF;";

                        command.Parameters.AddWithValue("@orderNum", newOrderID);
                        command.Parameters.AddWithValue("@firstname", txtBoxFirstName.Text);
                        command.Parameters.AddWithValue("@crust", comboBoxCrust.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@size", comboBoxPizzaSize.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@firsttopping", comboBoxTopping1.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@secondtopping", comboBoxTopping2.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@thirdtopping", comboBoxTopping3.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@subtotal", labelSubTotalValue.Content.ToString());
                        command.Parameters.AddWithValue("@finaltotal", labelTotalValue.Content.ToString());

                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (SqlException t)
                        {
                            MessageBox.Show(t.ToString());
                        }
                        finally
                        {

                            connection.Close();
                        }
                    }
                }
            }
        }
    }
}
