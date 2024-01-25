using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace project
{
    public partial class RegistrationForm : Form
    {
        private String pattern = @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";
        private System.Windows.Forms.ErrorProvider errorProvider1 = new System.Windows.Forms.ErrorProvider();
        private System.Windows.Forms.ErrorProvider errorProvider2 = new System.Windows.Forms.ErrorProvider();

        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox4.Text, pattern) == false)
            {
                textBox4.Focus();
                errorProvider1.SetError(this.textBox4, "Password must contain minimum 8 words \n With atleast a Character and a capital latter");
            }
            
            else
            {
                errorProvider1.Clear();
                
            }
         
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text != textBox4.Text)
            {
                textBox5.Focus();
                errorProvider2.SetError(this.textBox5, "Mismatch");
            }
            else
            {
                errorProvider2.Clear();

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancel the key press if it's not a digit or a control key
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string emailFormat1 = "@";
            string emailFormat2 = ".com";
            string password = textBox4.Text;
            string enteredEmail = textBox2.Text;

            SqlConnection con = new SqlConnection(databaseConnection.database());
            con.Open();
            SqlCommand sq1 = new SqlCommand("insert into CUSTOMER(EMAIL,PASSWORD,NAME,PHONENO,ADDRESS)values(@EMAIL,@PASSWORD,@NAME,@PHONENO,@ADDRESS)", con);

            //name
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("input Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            //email
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("input email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            string emailFromDatabase = RetrieveEmailFromDatabase(enteredEmail); // Replace this with your actual database retrieval logic

            if (enteredEmail == emailFromDatabase)
            {
                MessageBox.Show("Input different Email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox2.Text.Contains(emailFormat1) && textBox2.Text.Contains(emailFormat2))
            {

                sq1.Parameters.AddWithValue("@EMAIL", textBox2.Text);

            }
            else
            {
                MessageBox.Show("Invalid email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Phone Number
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Input Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string enteredPhoneNumber = textBox3.Text;

            // Check if the entered phone number contains only digits, starts with "01", and is exactly 11 digits long
            if (Regex.IsMatch(enteredPhoneNumber, @"^01\d{9}$") && enteredPhoneNumber.Length == 11)
            {
                sq1.Parameters.AddWithValue("@PHONENO", enteredPhoneNumber);
            }
            else
            {
                MessageBox.Show("Invalid phone number. Please enter a valid 11-digit number starting with '01'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Check if the entered phone number contains only digits, is exactly 11 digits long
            //if (!string.IsNullOrEmpty(textBox3.Text) && textBox3.Text.StartsWith("01"))
            //{
            //    // Store the phone number as a string
            //    sq1.Parameters.Add("@PHONENO", SqlDbType.VarChar).Value = textBox3.Text;
            //}
            //else
            //{
            //    MessageBox.Show("Invalid phone number. Please enter a valid number starting with '01'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //PASSWORD
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("input password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            if (Regex.IsMatch(textBox4.Text, pattern) == false)
            {
                MessageBox.Show("Password must contain minimum 8 words \n With atleast a Character and a capital latter", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            


            if (string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("Confirm password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (textBox5.Text == textBox4.Text)
            {

                sq1.Parameters.AddWithValue("@PASSWORD", password);

            }
            else
            {
                MessageBox.Show(" Password not same. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            if (string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("input Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


            sq1.Parameters.AddWithValue("@NAME", textBox1.Text);
            sq1.Parameters.AddWithValue("@ADDRESS", textBox6.Text);
            sq1.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Registration Completed");

            customerLogin csl = new customerLogin();
            csl.Show();
            this.Hide();


            
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
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
        public string RetrieveEmailFromDatabase(string enteredEmail)
        {
            string connectionString = databaseConnection.database(); // Replace with your actual connection string

            string email = string.Empty;

            // Your SQL query to retrieve the email
            string query = "SELECT EMAIL FROM CUSTOMER WHERE EMAIL = @EnteredEmail;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EnteredEmail", enteredEmail);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {

                    email = (string)reader["EMAIL"];
                }

                reader.Close();
            }

            return email;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            customerLogin A1 = new customerLogin();
            A1.Show();
            this.Hide();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void EXIT_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void MINIMIZE_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
