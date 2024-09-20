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
    public partial class customerLogin : Form
    {
        public customerLogin()
        {
            InitializeComponent();
            // Enable the form to receive key events before controls
            this.KeyPreview = true;

            // Subscribe to the KeyPress event
            this.KeyPress += customerLogin_KeyPress;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegistrationForm A1 = new RegistrationForm();
            A1.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Home A1 = new Home();
            A1.Show();
            this.Hide();

        }

        private void customerLogin_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(databaseConnection.database());
            con.Open();
            SqlCommand sq1 = new SqlCommand("select * from CUSTOMER",con);
            DataTable dt = new DataTable();
            SqlDataReader sdr = sq1.ExecuteReader();
            dt.Load(sdr);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(databaseConnection.database());


            if (string.IsNullOrEmpty(maskedTextBox1.Text) || string.IsNullOrEmpty(maskedTextBox1.Text))
            {
                    MessageBox.Show("input Name or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

            }

            try
            {
                con.Open(); // Open the connection

                String querry1 = "SELECT * FROM CUSTOMER WHERE EMAIL = @EMAIL AND PASSWORD = @PASSWORD";
                String querry2 = "SELECT * FROM EMPLOYEE WHERE E_IDENTIFIER = @E_IDENTIFIER AND PASSWORD = @PASSWORD";
                SqlCommand cmd1 = new SqlCommand(querry1, con);
                SqlCommand cmd2 = new SqlCommand(querry2, con);
                cmd1.Parameters.AddWithValue("@EMAIL", maskedTextBox1.Text);
                cmd1.Parameters.AddWithValue("@PASSWORD", maskedTextBox2.Text);

                cmd2.Parameters.AddWithValue("@E_IDENTIFIER", maskedTextBox1.Text);
                cmd2.Parameters.AddWithValue("@PASSWORD", maskedTextBox2.Text);

                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);

                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                sda1.Fill(dt1);
                sda2.Fill(dt2);

                if (dt1.Rows.Count > 0)
                {
                    //"@Name" = textBox1.Text;
                    // "@Pass" = textBox2.Text;

                    DashBoard d = new DashBoard(maskedTextBox1.Text);
                    d.Show();
                    this.Hide();
                }
                

                else if (dt2.Rows.Count > 0)
                {
                    employeeDashboard empD = new employeeDashboard(maskedTextBox1.Text);
                    empD.Show();
                    this.Hide();
                }

                else
                {
                    MessageBox.Show("Invalid Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    maskedTextBox1.Clear();
                    maskedTextBox2.Clear();

                    maskedTextBox1.Focus();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close(); // Close the connection in the finally block
            }

        }
        private void customerLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Trigger the Click event of your specific button
                button1.PerformClick(); // Replace 'yourButton' with the actual name of your button

                // Optionally, suppress the beep sound when Enter is pressed
                e.Handled = true;
            }
        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
           

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true)
            {
                maskedTextBox2.UseSystemPasswordChar= false;
            }
            else
            {
                maskedTextBox2.UseSystemPasswordChar = true;
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
