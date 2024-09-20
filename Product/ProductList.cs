using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace project
{
    public partial class ProductList : UserControl
    {
        private string useremail;
        public ProductList(string useremail)
        {
            InitializeComponent();
            this.useremail = useremail;
            
        }

        #region Properties

        private string _message1;
        private string _message2;
        private string _message3;
        private string _message4;
        private Button _button1;
        private Image _icon1;

        private void button1_Click(object sender, EventArgs e)
        {

            // Check if Message3 is equal to "0"
            if (Message3 == "AVAILABLE : 0")
            {
                MessageBox.Show("Product is not available");
                // Handle the case when the product is not available
            }

            else if (CheckOrderStatus(useremail))
            {
                // Order status is acceptable, invoke the event
                PurButtonClicked?.Invoke(this, e);
            }
            // else
            // {
            //     MessageBox.Show("Cannot purchase. Order status is not acceptable.");
            // }
        }


        [Category("Custom Props")]
        public string Message1
        {
            get { return _message1; }
            set { _message1 = value; lbIMessage1.Text = value; }
        }

        [Category("Custom Props")]
        public string Message2
        {
            get { return _message2; }
            set { _message2 = value; lbIMessage2.Text = value; }
        }

        [Category("Custom Props")]
        public string Message3
        {
            get { return _message3; }
            set { _message3 = value; lbIMessage3.Text = value; }
        }

        [Category("Custom Props")]
        public string Message4
        {
            get { return _message4; }
            set { _message4 = value; lbIMessage4.Text = value; }
        }

        [Category("Custom Props")]
        public Image Image1
        {
            get { return _icon1; }
            set { _icon1 = value; pictureBox1.Image = value; }
        }

        [Category("Custom Props")]
        public Button Button1
        {
            get { return _button1; }
            set { _button1 = value; }
        }

        [Category("Custom Props")]
        public event EventHandler PurButtonClicked;

        #endregion

        private bool CheckOrderStatus(string userEmail)
        {
            string query = "SELECT STATE FROM [ORDER] WHERE CUSTOMER_EMAIL = @UserEmail " +
                           "AND STATE IN ('On the way', 'Pending', 'Accepted')";

            using (SqlConnection connection = new SqlConnection(databaseConnection.database()))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserEmail", userEmail);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            MessageBox.Show("Please wait until your current order is delivered.");
                            return false; // Orders in relevant states exist, don't allow the purchase
                        }
                        else
                        {
                            return true; // No orders in relevant states, allow the purchase
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking order status: " + ex.Message);
                    return false; // Assume an error means order status is not acceptable
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }

}
