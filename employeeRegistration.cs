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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace project
{
    public partial class employeeRegistration : Form
    {
        private String pattern = @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";
        private System.Windows.Forms.ErrorProvider errorProvider1 = new System.Windows.Forms.ErrorProvider();
        private System.Windows.Forms.ErrorProvider errorProvider2 = new System.Windows.Forms.ErrorProvider();
        public employeeRegistration()
        {
            InitializeComponent();
        }

        private void employeeRegistration_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(databaseConnection.database());
            con.Open();
            SqlCommand sq1 = new SqlCommand("select * from EMPLOYEE", con);
            DataTable dt = new DataTable();
            SqlDataReader sdr = sq1.ExecuteReader();
            SqlDataAdapter ds = new SqlDataAdapter(sq1);
            dt.Load(sdr);
            ds.Fill(dt);
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string password = textBox3.Text;
            SqlConnection con = new SqlConnection(databaseConnection.database());
            con.Open();
            SqlCommand sq1 = new SqlCommand("insert into EMPLOYEE(PASSWORD,E_NAME,PHONE,SALARY,TYPE)values(@PASSWORD,@E_NAME,@PHONE,@SALARY,@TYPE)", con);

            //name
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("input Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            //Phone Number
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("input Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            string enteredPhoneNumber = textBox2.Text;

            // Check if the entered phone number contains only digits, starts with "01", and is exactly 11 digits long
            if (Regex.IsMatch(enteredPhoneNumber, @"^01\d{9}$") && enteredPhoneNumber.Length == 11)
            {
                sq1.Parameters.AddWithValue("@PHONE", enteredPhoneNumber);
            }
            else
            {
                MessageBox.Show("Invalid phone number. Please enter a valid 11-digit number starting with '01'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //PASSWORD
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("input password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Confirm password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (textBox3.Text == textBox4.Text)
            {

                sq1.Parameters.AddWithValue("@PASSWORD", password);

            }
            else
            {
                MessageBox.Show(" Password not same. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            
            //Salary
            if (comboBox1.SelectedItem != null  )
            {
                string selectedValue = comboBox1.SelectedItem.ToString();
                sq1.Parameters.AddWithValue("@TYPE", selectedValue);
                if (selectedValue == "Manager")
                {
                    float i = 150000.0F;
                    sq1.Parameters.AddWithValue("@SALARY", i);

                }
                if (selectedValue == "Delivery man")
                {
                    float i = 40000.0F;
                    sq1.Parameters.AddWithValue("@SALARY", i);
                }
                if (selectedValue == "HR")
                {
                    float i = 120000.0F;
                    sq1.Parameters.AddWithValue("@SALARY", i);
                }
                if (selectedValue == "CEO")
                {
                    float i = 400000.0F;
                    sq1.Parameters.AddWithValue("@SALARY", i);
                }
                if (selectedValue == "CTO")
                {
                    float i = 130000.0F;
                    sq1.Parameters.AddWithValue("@SALARY", i);
                }
                


            }
           
            else 
            {
                MessageBox.Show("Please select an item");
                return;
            }
            


            sq1.Parameters.AddWithValue("@E_NAME", textBox1.Text);

            sq1.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Registration Completed");

            //Admindashboard ads = new Admindashboard();
            //ads.ShowDialog();
            this.Close();
            
            

        }
        
            private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancel the key press if it's not a digit or a control key
                e.Handled = true;
            }
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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
        }

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
