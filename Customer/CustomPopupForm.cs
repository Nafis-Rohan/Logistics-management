using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace project
{
    public partial class CustomPopupForm : Form
    {
        public CustomPopupForm(string productId, string name, string price, string cun, string cue, string cup, string cuadd, int availableQuantity)
        {
            InitializeComponent();

            // Set the labels and initial values
            label1.Text = "PRODUCT ID : " + productId;
            label2.Text = "NAME: " + name;
            label3.Text = "PRICE: " + price;

            numericUpDown1.Maximum = availableQuantity;
            numericUpDown1.Minimum = 0;

            label4.Text = "Hey!  " + cun;
            label5.Text = "Email: " + cue;
            label6.Text = "Phone No: " + cup;
            label7.Text = "Address: " + cuadd;
        }

        public int QuantitySelected => (int)numericUpDown1.Value;

        private void button1_Click(object sender, EventArgs e)
        {
            int selectedQuantity = (int)numericUpDown1.Value;

            if (selectedQuantity > 0 && selectedQuantity <= (int)numericUpDown1.Maximum) // Check if a value is selected within limits
            {
                // Retrieve the data from the form
                int quantity = selectedQuantity;
                string pproductId = label1.Text.Replace("PRODUCT ID : ", "");
                string customerEmail = label5.Text.Replace("Email: ", ""); // Extract the customer email

                // Set the state and order date (you can modify this accordingly)
                string state = "Pending"; // Example state
                DateTime orderDate = DateTime.Now; // Example order date

                using (SqlConnection connection = new SqlConnection(databaseConnection.database()))
                {
                    try
                    {
                        connection.Open();

                        // Update the quantity in the PRODUCT table
                        string updateQuery = "UPDATE PRODUCT SET QUANTITY = QUANTITY - @Quantity WHERE P_IDENTIFIER = @ProductId";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@Quantity", quantity);
                        updateCommand.Parameters.AddWithValue("@ProductId", pproductId);

                        // Execute the update command
                        int updateRowsAffected = updateCommand.ExecuteNonQuery();

                        if (updateRowsAffected > 0)
                        {
                            // Calculate the total price
                            float price = float.Parse(label3.Text.Replace("PRICE: ", ""));
                            float totalPrice = (float)quantity * price;

                            // Update was successful, proceed with the insert query
                            string insertQuery = "INSERT INTO [dbo].[ORDER] (CUSTOMER_EMAIL, PRODUCT_ID, QUANTITY, PRICE, STATE, ORDER_DATE) " +
                                                 "VALUES (@CustomerEmail, @ProductId, @Quantity, @TotalPrice, @State, @OrderDate)";
                            SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                            insertCommand.Parameters.AddWithValue("@CustomerEmail", customerEmail);
                            insertCommand.Parameters.AddWithValue("@ProductId", pproductId);
                            insertCommand.Parameters.AddWithValue("@Quantity", quantity);
                            insertCommand.Parameters.AddWithValue("@TotalPrice", totalPrice);
                            insertCommand.Parameters.AddWithValue("@State", state);
                            insertCommand.Parameters.AddWithValue("@OrderDate", orderDate);

                            // Execute the insert command
                            int insertRowsAffected = insertCommand.ExecuteNonQuery();

                            if (insertRowsAffected > 0)
                            {
                                MessageBox.Show("Order placed successfully! \nWe believe our customer please pay after receiving the product");
                                this.Close(); // Close the popup form after successful order placement
                            }
                            else
                            {
                                MessageBox.Show("Failed to place the order. Please try again.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Failed to update product quantity. Please try again.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Selected quantity should be greater than 0 and less than or equal to available quantity. Not sufficient.");
            }
        }

        private void CustomPopupForm_Load(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            
                // Disable keyboard input for the NumericUpDown control
                e.Handled = true;
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
