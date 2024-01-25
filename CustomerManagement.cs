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
    public partial class CustomerManagement : UserControl
    {
        public CustomerManagement()
        {
            InitializeComponent();
        }

        private void CustomerManagement_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(databaseConnection.database());
            con.Open();
            SqlCommand sq1 = new SqlCommand("select EMAIL,NAME,PHONENO,ADDRESS from CUSTOMER", con);
            DataTable dt = new DataTable();
            SqlDataReader sdr = sq1.ExecuteReader();
            dt.Load(sdr);

            dataGridView1.DataSource = dt; 
            con.Close();
        }
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Prompt user for removal confirmation and reasons
                DialogResult result = MessageBox.Show("Are you sure you want to remove ?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {



                    try
                    {
                        DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                        string pIdentifier = selectedRow.Cells["EMAIL"].Value.ToString();

                        // Insert data into REJECTIONS table
                        //Reject.InsertIntoRejections(pIdentifier, reason, ConnectionString);

                        // Remove party from PARTY table
                        RemoveCustomer(pIdentifier);
                    }
                    catch (Exception )
                    {
                        //MessageBox.Show("Error: " + ex.Message);
                        MessageBox.Show("This Customer is on order state");
                    }


                }
            }
            else
            {
                MessageBox.Show("Please select a row to remove.");
            }
        }
        private void RemoveCustomer(string pID)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.database()))
            {
                try
                {
                    connection.Open();

                    string removePartyQuery = "DELETE FROM CUSTOMER WHERE EMAIL = @pID";
                    SqlCommand removePartyCommand = new SqlCommand(removePartyQuery, connection);
                    removePartyCommand.Parameters.AddWithValue("@pID", pID);
                    removePartyCommand.ExecuteNonQuery();

                    button2.PerformClick();
                }
                catch (Exception )
                {
                    MessageBox.Show("This Customer is on order state");
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(databaseConnection.database());
            con.Open();
            SqlCommand sq1 = new SqlCommand("select EMAIL,NAME,PHONENO,ADDRESS from CUSTOMER", con);
            SqlDataAdapter ds = new SqlDataAdapter(sq1);
            DataTable dt = new DataTable();

            ds.Fill(dt);

            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
