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
    public partial class History : Form
    {
        private string gemail;
        public History(string email)
        {
            gemail = email;
            InitializeComponent();
        }

        private void History_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(databaseConnection.database());
            con.Open();

            SqlCommand sq1 = new SqlCommand("SELECT * FROM [ORDER] WHERE CUSTOMER_EMAIL = @gemail", con);
            sq1.Parameters.AddWithValue("@gemail", gemail); // Add parameter value

            DataTable dt = new DataTable();
            SqlDataAdapter ds = new SqlDataAdapter(sq1);
            ds.Fill(dt);

            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DashBoard ds = new DashBoard(gemail);
            ds.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
