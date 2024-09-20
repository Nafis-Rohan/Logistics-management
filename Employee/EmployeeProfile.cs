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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace project
{
    public partial class EmployeeProfile : Form
    {
        string gID;
        public EmployeeProfile(string gID)
        {
            InitializeComponent();
            this.gID = gID;
            employeeDetails();
        }

        private void EmployeeProfile_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangeEMPpassword cngP = new ChangeEMPpassword(gID);
            cngP.Show();
            this.Hide();
        }
        private void employeeDetails()
        {
            // Fetch data from NOMINEE using the provided GivenID
            using (SqlConnection connection = new SqlConnection(databaseConnection.database()))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT E_IDENTIFIER, E_NAME, PHONE, SALARY,TYPE " +
                                   "FROM EMPLOYEE WHERE E_IDENTIFIER = @gID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@gID", gID);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        // Populate the Nominee object properties
                        //CUSTOMER.PHONENO = reader["N_IDENTIFIER"].ToString();
                        //CUSTOMER.NAME = reader["N_NAME"].ToString();
                        //CUSTOMER.EMAIL = reader["N_EMAIL"].ToString();

                        //if (reader["LOGO"] != DBNull.Value)
                        //{
                        //    nominee.LOGO = (byte[])reader["LOGO"];
                        //}

                        label2.Text = "ID: " + reader["E_IDENTIFIER"].ToString();
                        label1.Text = "Name:  " + reader["E_NAME"].ToString();
                        label4.Text = "Phone: " + reader["PHONE"].ToString();
                        label5.Text = "Salary: " + reader["SALARY"].ToString();
                        label3.Text = "Type: " + reader["TYPE"].ToString();

                        // Update UI with Nominee object data
                        //    label1.Text = "Temp ID: " + nominee.N_IDENTIFIER;
                        //    label2.Text = "Name: " + nominee.N_NAME;
                        //    label7.Text = "Email: " + nominee.N_EMAIL;
                        //    label4.Text = "Welcome, " + nominee.N_NAME;

                        //    if (nominee.LOGO != null)
                        //    {
                        //        using (MemoryStream ms = new MemoryStream(nominee.LOGO))
                        //        {
                        //            pictureBox1.Image = Image.FromStream(ms);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        // Handle the case where no image is available
                        //        // pictureBox1.Image = YourDefaultImage;
                        //    }
                    }
                    else
                    {
                        MessageBox.Show(" data not found!!");
                    }

                    reader.Close();
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
            employeeDashboard empD = new employeeDashboard(gID);
            empD.Show();
            this.Hide();
        }
    }
}
