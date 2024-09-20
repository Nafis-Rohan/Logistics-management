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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace project
{
    public partial class ReStoke : Form
    {
        private string pId;
        public ReStoke(string pId)
        {
            InitializeComponent();
            this.pId = pId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.database()))
            {
                try
                {
                    connection.Open();

                    if (textBox1.Text != null)
                    {
                        string selectedValue = textBox1.Text.ToString();
                        int intValue = int.Parse(selectedValue);

                        // Update the state in the ORDER table
                        string updateQuery = "UPDATE PRODUCT SET QUANTITY = @Quantity WHERE P_IDENTIFIER = @pId";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@Quantity", intValue);
                        updateCommand.Parameters.AddWithValue("@pId", pId);
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

        private void ReStoke_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(databaseConnection.database());
            con.Open();
            SqlCommand sq1 = new SqlCommand("select * from PRODUCT", con);
            DataTable dt = new DataTable();
            SqlDataReader sdr = sq1.ExecuteReader();
            SqlDataAdapter ds = new SqlDataAdapter(sq1);
            dt.Load(sdr);
            ds.Fill(dt);
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            employeeDashboard empd = new employeeDashboard(pId);
            empd.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
