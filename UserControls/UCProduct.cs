using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class UCProduct : UserControl
    {
        // Define a list to store ProductList controls
        List<ProductList> productListItems = new List<ProductList>();
        private string gemail;
        private List<ProductList> originalNomItems;
        public UCProduct(string email)
        {
            gemail = email;

            InitializeComponent();
            populateNomineeItems();
        }

        private void panelProduct_Paint(object sender, PaintEventArgs e)
        {

        }

        private void populateNomineeItems()
        {
            // Clear controls outside the loop to avoid clearing on each iteration
            if (flowLayoutPanel1.Controls.Count > 0)
            {
                flowLayoutPanel1.Controls.Clear();
            }

            using (SqlConnection connection = new SqlConnection(databaseConnection.database()))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT P_IDENTIFIER, P_NAME, QUANTITY, PRICE, TYPE, PIC FROM PRODUCT";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ProductList nomineeItem = new ProductList(gemail);
                        string prefix1 = "PRODUCT ID : ";
                        nomineeItem.Message1 = prefix1 + reader["P_IDENTIFIER"].ToString();
                        string prefix2 = "AVAILABLE : ";
                        nomineeItem.Message3 = prefix2 + reader["QUANTITY"].ToString();
                        string prefix3 = "NAME : ";
                        nomineeItem.Message2 = prefix3 + reader["P_NAME"].ToString();
                        string prefix4 = "PRICE : ";
                        nomineeItem.Message4 = prefix4 + reader["PRICE"].ToString();

                        // Load the logo image into the PictureBox
                        if (reader["PIC"] != DBNull.Value)
                        {
                            byte[] logoBytes = (byte[])reader["PIC"];
                            using (MemoryStream ms = new MemoryStream(logoBytes))
                            {
                                nomineeItem.Image1 = Image.FromStream(ms);
                            }
                        }

                        nomineeItem.PurButtonClicked += (sender, e) =>
                        {
                            string productId = nomineeItem.Message1.Replace("PRODUCT ID : ", "");
                            string name = nomineeItem.Message2.Replace("NAME : ", ""); ;
                            string price = nomineeItem.Message4.Replace("PRICE : ", "");

                            using (SqlConnection customerConnection = new SqlConnection(databaseConnection.database()))
                            {
                                try
                                {
                                    customerConnection.Open();

                                    string customerQuery = "SELECT NAME, EMAIL, PHONENO, ADDRESS FROM CUSTOMER WHERE EMAIL = @Email";
                                    SqlCommand customerCommand = new SqlCommand(customerQuery, customerConnection);
                                    customerCommand.Parameters.AddWithValue("@Email", gemail);

                                    SqlDataReader customerReader = customerCommand.ExecuteReader();

                                    if (customerReader.Read())
                                    {
                                        string customerName = customerReader["NAME"].ToString();
                                        string customerPhoneNo = customerReader["PHONENO"].ToString();
                                        string customerAddress = customerReader["ADDRESS"].ToString();

                                        // Retrieve the available quantity for the product
                                        int availableQuantity = Convert.ToInt32(nomineeItem.Message3.Replace("AVAILABLE : ", "")); // Extract available quantity

                                        using (var customPopup = new CustomPopupForm(productId, name, price, customerName, gemail, customerPhoneNo, customerAddress, availableQuantity))
                                        {
                                            if (customPopup.ShowDialog() == DialogResult.OK)
                                            {
                                                int selectedQuantity = customPopup.QuantitySelected;

                                                if (selectedQuantity > availableQuantity)
                                                {
                                                    MessageBox.Show("Selected quantity exceeds available quantity!");
                                                    return; // Prevent further processing if the selected quantity exceeds available quantity
                                                }

                                                // Handle the selected quantity here...
                                                MessageBox.Show($"Confirmed! Quantity: {selectedQuantity}");
                                            }
                                        }
                                    }

                                    customerReader.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error: " + ex.Message);
                                }
                                finally
                                {
                                    customerConnection.Close();
                                }
                            }
                        };

                        // Add each nominee item to the list and flowLayoutPanel1
                        productListItems.Add(nomineeItem);
                        // Add each nominee item to the flowLayoutPanel
                        flowLayoutPanel1.Controls.Add(nomineeItem);
                    }
                    originalNomItems = flowLayoutPanel1.Controls.OfType<ProductList>().ToList();
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

       

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                List<ProductList> nomItemsToSearch = originalNomItems ?? flowLayoutPanel1.Controls.OfType<ProductList>().ToList();

                List<ProductList> filteredNominee = nomItemsToSearch.Where(p =>
                    p.Message1.ToLower().Contains(searchTerm.ToLower()) ||
                    p.Message2.ToLower().Contains(searchTerm.ToLower())
                ).ToList();

                flowLayoutPanel1.Controls.Clear();

                foreach (ProductList nom in filteredNominee)
                {
                    flowLayoutPanel1.Controls.Add(nom);
                }
            }
            else
            {
                // If the search term is empty, reload the original nom list
                if (originalNomItems != null)
                {
                    flowLayoutPanel1.Controls.Clear();
                    foreach (ProductList nom in originalNomItems)
                    {
                        flowLayoutPanel1.Controls.Add(nom);
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void UCProduct_Load(object sender, EventArgs e)
        {
            textBox1.Select();
        }
    }
}
