using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace project
{
    public partial class OrderPopUp : Form
    {
        private string gemail;

        public OrderPopUp(String gemail)
        {
            InitializeComponent();
            this.gemail = gemail;
            LoadCustomerDetails();
        }

        private void LoadCustomerDetails()
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.database()))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT TOP 1 O_IDENTIFIER, QUANTITY, PRICE, STATE " +
                     " FROM[ORDER] " +
                      " WHERE CUSTOMER_EMAIL = @userEmail" +
                         "  ORDER BY O_IDENTIFIER DESC; ";


                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@userEmail", gemail);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        label2.Text = "Quantity: " + reader["QUANTITY"].ToString();
                        label1.Text = "Order ID:  " + reader["O_IDENTIFIER"].ToString();
                        label3.Text = "Price : " + reader["PRICE"].ToString();
                        label4.Text = "State : " + reader["STATE"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Customer has no order.");
                        this.Close();
                    }

                    reader.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.database()))
            {
                try
                {
                    connection.Open();

                    // Check the current state of the order
                    string checkStateQuery = "SELECT TOP 1 STATE, PRODUCT_ID, QUANTITY " +
                         "FROM [ORDER] " +
                         "WHERE CUSTOMER_EMAIL = @userEmail " +
                         "ORDER BY ORDER_DATE DESC";

                    using (SqlCommand checkStateCommand = new SqlCommand(checkStateQuery, connection))
                    {
                        checkStateCommand.Parameters.AddWithValue("@userEmail", gemail);

                        using (SqlDataReader reader = checkStateCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string currentState = reader["STATE"].ToString();
                                string productId = reader["PRODUCT_ID"].ToString();
                                int canceledQuantity = Convert.ToInt32(reader["QUANTITY"]);

                                if (currentState.Equals("Cancel", StringComparison.OrdinalIgnoreCase))
                                {
                                    MessageBox.Show("Order state is already 'Cancel'");
                                    //return;
                                }
                                else if (currentState.Equals("Reached", StringComparison.OrdinalIgnoreCase))
                                {
                                    MessageBox.Show("Order is already 'Reached'. You cannot cancel it.");
                                    return;
                                }
                                else // Proceed to cancel orders in other states
                                {
                                    // Close the reader before executing the update commands
                                    reader.Close();

                                    // Update the state of the order to 'Cancel'
                                    string updateOrderQuery = "UPDATE [ORDER] SET STATE = 'Cancel' WHERE CUSTOMER_EMAIL = @userEmail";
                                    using (SqlCommand updateOrderCommand = new SqlCommand(updateOrderQuery, connection))
                                    {
                                        updateOrderCommand.Parameters.AddWithValue("@userEmail", gemail);
                                        updateOrderCommand.ExecuteNonQuery();
                                    }

                                    // Update the quantity in the PRODUCT table
                                    string updateProductQuery = "UPDATE PRODUCT SET QUANTITY = QUANTITY + @CanceledQuantity WHERE P_IDENTIFIER = @ProductId";
                                    using (SqlCommand updateProductCommand = new SqlCommand(updateProductQuery, connection))
                                    {
                                        updateProductCommand.Parameters.AddWithValue("@CanceledQuantity", canceledQuantity);
                                        updateProductCommand.Parameters.AddWithValue("@ProductId", productId);
                                        updateProductCommand.ExecuteNonQuery();
                                    }

                                    MessageBox.Show("Order state updated to 'Cancel' and quantity restored in the PRODUCT table.");
                                    label4.Text = "State : Cancel"; // Update label text if needed
                                }
                            }
                            else
                            {
                                MessageBox.Show("No order found for the customer.");
                            }
                        }
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






        private void OrderPopUp_Load(object sender, EventArgs e)
        {

        }

        public string GetOrderIdentifier()
        {
           // Assuming you have a label displaying the order identifier, replace "label1" with the appropriate control name
           return label4.Text.Replace("State : ", "");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DashBoard ds = new DashBoard(gemail);
            ds.Show();
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
