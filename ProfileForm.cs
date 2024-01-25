using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace project
{
    public partial class ProfileForm : Form
    {
        private string gemail;
        public ProfileForm(string email)
        {
            gemail = email;
            InitializeComponent();
            CustomerDetails();
        }


        private void DashboardB_Click(object sender, EventArgs e)
        {
            DashBoard ds = new DashBoard(gemail);
            ds.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            changePassCustomer cngp = new changePassCustomer(gemail);
            cngp.Show();
            this.Hide();
        }

        private void CustomerDetails()
        {
            // Fetch data from NOMINEE using the provided GivenID
            using (SqlConnection connection = new SqlConnection(databaseConnection.database()))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT EMAIL, PASSWORD, NAME, PHONENO, ADDRESS " +
                                   "FROM CUSTOMER WHERE EMAIL = @gemail";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@gemail",gemail);

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

                        label2.Text = "Email: " + reader["EMAIL"].ToString();
                        label1.Text = "Name:  " + reader["NAME"].ToString();
                        label3.Text = "Phone: " + reader["PHONENO"].ToString();
                        label4.Text = "Address: " + reader["ADDRESS"].ToString();

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

        private void ProfileForm_Load(object sender, EventArgs e)
        {

        }
    }
}
