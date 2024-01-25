using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class changePassCustomer : Form
    {
        private String gemail;
        private String pattern = @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";
        private System.Windows.Forms.ErrorProvider errorProvider1 = new System.Windows.Forms.ErrorProvider();
        private System.Windows.Forms.ErrorProvider errorProvider2 = new System.Windows.Forms.ErrorProvider();
        public changePassCustomer(String gemail)
        {
            InitializeComponent();
            this.gemail = gemail;
        }

        private void changePassCustomer_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(databaseConnection.database());
            con.Open();
            SqlCommand sq1 = new SqlCommand("select * from CUSTOMER", con);
            DataTable dt = new DataTable();
            SqlDataReader sdr = sq1.ExecuteReader();
            SqlDataAdapter ds = new SqlDataAdapter(sq1);
            dt.Load(sdr);
            ds.Fill(dt);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox3.Text, pattern) == false)
            {
                textBox3.Focus();
                errorProvider1.SetError(this.textBox3, "Password must contain minimum 8 words \n With atleast a Character and a capital latter");
            }

            else
            {
                errorProvider1.Clear();

            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text != textBox3.Text)
            {
                textBox4.Focus();
                errorProvider2.SetError(this.textBox4, "mismatch");
            }
            else
            {
                errorProvider2.Clear();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string enteredEmail = textBox1.Text;
            string enteredOldPass = textBox2.Text;
            string newPass = textBox3.Text;
            string confirmPass = textBox4.Text;

            if (string.IsNullOrEmpty(enteredEmail))
            {
                MessageBox.Show("Input email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            (string emailFromDatabase, string passwordFromDatabase) = RetrieveEmailFromDatabase(enteredEmail, enteredOldPass);

            if (enteredEmail != emailFromDatabase)
            {
                MessageBox.Show("Input proper Email or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (string.IsNullOrEmpty(enteredOldPass))
            {
                MessageBox.Show("Input oldpass", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            if (enteredOldPass != passwordFromDatabase)
            {
                MessageBox.Show("Input proper current password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(newPass))
            {
                MessageBox.Show("Input new password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Regex.IsMatch(textBox3.Text, pattern) == false)
            {
                MessageBox.Show("Password must contain minimum 8 words \n With atleast a Character and a capital latter", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (newPass != confirmPass)
            {
                MessageBox.Show("Passwords do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Update the password in the database
            SqlConnection con = new SqlConnection(databaseConnection.database());
            con.Open();
            SqlCommand sq1 = new SqlCommand(@"UPDATE CUSTOMER SET PASSWORD = @NewPassword WHERE EMAIL = @EnteredEmail", con);
            sq1.Parameters.AddWithValue("@NewPassword", newPass);
            sq1.Parameters.AddWithValue("@EnteredEmail", enteredEmail);
            sq1.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Password Changed Successfully");
        }

        public (string Email, string Password) RetrieveEmailFromDatabase(string enteredEmail, string enteredOldPass)
        {
            string connectionString = databaseConnection.database(); // Replace with your actual connection string

            string email = string.Empty;
            string password = string.Empty;

            // Your SQL query to retrieve the email
            string query = "SELECT EMAIL, PASSWORD FROM CUSTOMER WHERE EMAIL = @EnteredEmail AND PASSWORD = @EnteredOLDPASS";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EnteredEmail", enteredEmail);
                command.Parameters.AddWithValue("@EnteredOLDPASS", enteredOldPass);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    email = reader["EMAIL"].ToString();
                    password = reader["PASSWORD"].ToString();
                }
                reader.Close();
            }

            return (email, password);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            ProfileForm pf = new ProfileForm(gemail);
            pf.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
