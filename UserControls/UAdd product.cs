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
    public partial class UAdd_product : UserControl
    {
        private byte[] Pic;
        public UAdd_product()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve the values from the textboxes
                string pName = textBox1.Text;
                string price = textBox2.Text;
                string pQuantity = textBox3.Text;
                string ptype = textBox4.Text;

                // Check if all fields are filled before proceeding
                //if (string.IsNullOrWhiteSpace(partyName) || Logo == null)
                //{
                //    MessageBox.Show("Please fill name and upload the logo.");
                //    return;
                //}

                using (SqlConnection connection = new SqlConnection(databaseConnection.database()))
                {
                    connection.Open();

                    string query = "INSERT INTO PRODUCT (P_NAME, PRICE, QUANTITY, TYPE, PIC) " +
                                   "VALUES (@pName, @price, @pQuantity, @ptype, @Pic)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@pName", pName);
                    command.Parameters.AddWithValue("@price", price);
                    command.Parameters.AddWithValue("@pQuantity", pQuantity);
                    command.Parameters.AddWithValue("@ptype", ptype);
                    command.Parameters.AddWithValue("@Pic", Pic);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Product added successfully!");
                    }
                    else
                    {
                        MessageBox.Show("No data added. Something went wrong!!");
                    }
                }

                textBox1.Clear();
                textBox2.Clear(); 
                textBox3.Clear();
                textBox4.Clear(); 
                
            }
            catch (Exception )
            {
                MessageBox.Show("Please fillup required uploads");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select Logo";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Read the selected image file as bytes and store it in logo variable
                        string filePath = openFileDialog.FileName;
                        Pic = File.ReadAllBytes(filePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }
    }
}
