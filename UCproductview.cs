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
    public partial class UCproductview : UserControl
    {
        public UCproductview()
        {
            InitializeComponent();
        }

        private void UCproductview_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(databaseConnection.database());
            con.Open();
            SqlCommand sq1 = new SqlCommand("select * from PRODUCT", con);
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
                try
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    string pIdentifier = selectedRow.Cells["P_IDENTIFIER"].Value.ToString();

                    // Pass the selected order ID to selectState form
                    ReStoke rs = new ReStoke(pIdentifier);
                    rs.ShowDialog();

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

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(databaseConnection.database());
            con.Open();
            SqlCommand sq1 = new SqlCommand("select * from PRODUCT", con);
            SqlDataAdapter ds = new SqlDataAdapter(sq1);
            DataTable dt = new DataTable();

            ds.Fill(dt);

            dataGridView1.DataSource = dt;
            con.Close();
        }
    }
    //public void upState(string pID)
    //{
    //    using (SqlConnection connection = new SqlConnection(databaseConnection.database()))
    //    {
    //        try
    //        {
    //            connection.Open();

    //            // Assuming you have a column named STATE in your ORDER table


    //            string updateQuery = "UPDATE PRODUCT SET QUANTITY = @quantity WHERE P_IDENTIFIER = @oID";
    //            SqlCommand updateCommand = new SqlCommand(updateQuery, connection);

    //            // Assuming you have a parameter named @State in your UPDATE query
    //            // You need to replace it with the actual column name from your ORDER table
    //            updateCommand.Parameters.AddWithValue("@State", "NewQuantityValue");

    //            updateCommand.Parameters.AddWithValue("@oID", pID);

    //            updateCommand.ExecuteNonQuery();

    //            /button2.PerformClick();  // Not sure what this does, but it seems to be related to refreshing the data
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show("Error: " + ex.Message);
    //        }
    //        finally
    //        {
    //            connection.Close();
    //        }
    //    }
    //}
}
