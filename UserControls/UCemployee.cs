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
    public partial class UCemployee : UserControl
    {

         
        public UCemployee()
        {
            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
          employeeRegistration empR = new employeeRegistration();
          empR.ShowDialog();
          
          Admindashboard ad = new Admindashboard();
          ad.Hide();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        

        private void UCemployee_Load(object sender, EventArgs e)
        {
            
            SqlConnection con = new SqlConnection(databaseConnection.database());
            con.Open();
            SqlCommand sq1 = new SqlCommand("select E_IDENTIFIER,E_NAME,PHONE,SALARY,TYPE from EMPLOYEE", con);
            DataTable dt = new DataTable();
            SqlDataReader sdr = sq1.ExecuteReader();
            dt.Load(sdr);

            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Prompt user for removal confirmation and reasons
                DialogResult result = MessageBox.Show("Are you sure you want to remove the Employee?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {
                    

                    
                        try
                        {
                            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                            string pIdentifier = selectedRow.Cells["E_IDENTIFIER"].Value.ToString();

                            // Insert data into REJECTIONS table
                            //Reject.InsertIntoRejections(pIdentifier, reason, ConnectionString);

                            // Remove party from PARTY table
                            RemoveParty(pIdentifier);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    
                    
                }
            }
            else
            {
                MessageBox.Show("Please select a row to remove.");
            }
            //RemoveEmployee re = new RemoveEmployee();
            //re.ShowDialog();


        }
        private void RemoveParty(string pID)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.database()))
            {
                try
                {
                    connection.Open();

                    string removePartyQuery = "DELETE FROM EMPLOYEE WHERE E_IDENTIFIER = @pID";
                    SqlCommand removePartyCommand = new SqlCommand(removePartyQuery, connection);
                    removePartyCommand.Parameters.AddWithValue("@pID", pID);
                    removePartyCommand.ExecuteNonQuery();

                    button3.PerformClick();
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

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(databaseConnection.database());
            con.Open();
            SqlCommand sq1 = new SqlCommand("select E_IDENTIFIER,E_NAME,PHONE,SALARY,TYPE from EMPLOYEE", con);
            SqlDataAdapter ds = new SqlDataAdapter(sq1);
            DataTable dt = new DataTable();

            ds.Fill(dt);

            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
