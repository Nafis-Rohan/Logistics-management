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
    public partial class UcOrder : UserControl
    {
        public UcOrder()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void UcOrder_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(databaseConnection.database());
            con.Open();
            SqlCommand sq1 = new SqlCommand("select * from [ORDER]", con);
            DataTable dt = new DataTable();
            SqlDataReader sdr = sq1.ExecuteReader();
            dt.Load(sdr);

            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    string oIdentifier = selectedRow.Cells["O_IDENTIFIER"].Value.ToString();

                    // Pass the selected order ID to selectState form
                    selectState selectStateForm = new selectState(oIdentifier);
                    selectStateForm.ShowDialog();

                    // Refresh the data after the update (if needed)
                    button2.PerformClick();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to update.");
            }
        }

        private void upState(string oID)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.database()))
            {
                try
                {
                    connection.Open();

                    // Assuming you have a column named STATE in your ORDER table
                    

                    string updateQuery = "UPDATE [ORDER] SET STATE = @State WHERE O_IDENTIFIER = @oID";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);

                    // Assuming you have a parameter named @State in your UPDATE query
                    // You need to replace it with the actual column name from your ORDER table
                    updateCommand.Parameters.AddWithValue("@State", "NewStateValue");

                    updateCommand.Parameters.AddWithValue("@oID", oID);

                    updateCommand.ExecuteNonQuery();

                    button2.PerformClick();  // Not sure what this does, but it seems to be related to refreshing the data
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

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(databaseConnection.database());
            con.Open();
            SqlCommand sq1 = new SqlCommand("select * from [ORDER]", con);
            SqlDataAdapter ds = new SqlDataAdapter(sq1);
            DataTable dt = new DataTable();

            ds.Fill(dt);

            dataGridView1.DataSource = dt;
            con.Close();
        }
    }
}
