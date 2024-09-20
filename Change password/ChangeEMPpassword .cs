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
    public partial class ChangeEMPpassword : Form
    {
        string gID;
        private String pattern = @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";
        private System.Windows.Forms.ErrorProvider errorProvider1 = new System.Windows.Forms.ErrorProvider();
        private System.Windows.Forms.ErrorProvider errorProvider2 = new System.Windows.Forms.ErrorProvider();

        public ChangeEMPpassword(string ID)
        {
            InitializeComponent();
            gID = ID;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string enteredEmail = textBox1.Text;
            string enteredOldPass = textBox2.Text;
            string newPass = textBox3.Text;
            string confirmPass = textBox4.Text;


            (string emailFromDatabase, string passwordFromDatabase) = RetrieveEmailFromDatabase(enteredEmail, enteredOldPass);


            if (enteredEmail != emailFromDatabase)
            {
                MessageBox.Show("Input proper Eid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (newPass != confirmPass)
            {
                MessageBox.Show("Passwords do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Update the password in the database
            SqlConnection con = new SqlConnection(databaseConnection.database());
            con.Open();
            SqlCommand sq1 = new SqlCommand(@"UPDATE EMPLOYEE SET PASSWORD = @NewPassword WHERE E_IDENTIFIER  = @EnteredEmail", con);
            sq1.Parameters.AddWithValue("@NewPassword", newPass);
            sq1.Parameters.AddWithValue("@EnteredEmail", enteredEmail);
            sq1.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Password Changed Successfully");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ChangeEMPpassword_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(databaseConnection.database());
            con.Open();
            SqlCommand sq1 = new SqlCommand("select * from EMPLOYEE", con);
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
                errorProvider1.SetError(this.textBox3, "make it strong");
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
        public (string Email, string Password) RetrieveEmailFromDatabase(string enteredEmail, string enteredOldPass)
        {
            string connectionString = databaseConnection.database();

            string email = string.Empty;
            string password = string.Empty;

            string query = "SELECT E_IDENTIFIER, PASSWORD FROM EMPLOYEE WHERE E_IDENTIFIER = @EnteredEmail AND PASSWORD = @EnteredOLDPASS";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EnteredEmail", enteredEmail);
                command.Parameters.AddWithValue("@EnteredOLDPASS", enteredOldPass);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    email = reader["E_IDENTIFIER"].ToString();
                    password = reader["PASSWORD"].ToString();
                }
                reader.Close();
            }

            return (email, password);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            EmployeeProfile cep = new EmployeeProfile(gID);
            cep.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
