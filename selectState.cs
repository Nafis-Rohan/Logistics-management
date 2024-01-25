using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class selectState : Form
    {
        private string selectedOrderID;
        public selectState(string orderID)
        {
            InitializeComponent();
            selectedOrderID = orderID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.database()))
            {
                try
                {
                    connection.Open();

                    if (comboBox1.SelectedItem != null)
                    {
                        string selectedValue = comboBox1.SelectedItem.ToString();

                        // Update the state in the ORDER table
                        string updateQuery = "UPDATE [ORDER] SET STATE = @State WHERE O_IDENTIFIER = @oID";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@State", selectedValue);
                        updateCommand.Parameters.AddWithValue("@oID", selectedOrderID);
                        updateCommand.ExecuteNonQuery();

                        MessageBox.Show("Order state updated successfully!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please select a state from the comboBox.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating order state: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        private void selectState_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(databaseConnection.database());
            con.Open();
            SqlCommand sq1 = new SqlCommand("select * from [ORDER]", con);
            DataTable dt = new DataTable();
            SqlDataReader sdr = sq1.ExecuteReader();
            SqlDataAdapter ds = new SqlDataAdapter(sq1);
            dt.Load(sdr);
            ds.Fill(dt);
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            employeeDashboard empd = new employeeDashboard(selectedOrderID);
            empd.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
