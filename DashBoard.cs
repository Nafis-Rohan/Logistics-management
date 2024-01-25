using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class DashBoard : Form
    {
        string gemail;
        public DashBoard(string email)
        {
            gemail = email;
            InitializeComponent();
            
        }

        static DashBoard dobj;

        
       
        public Panel PnlContainer
        {
            get { return panel4; }
            set { panel4 = value; }
        }

        public Button ProductButton
        {
            get { return Product; }
            set { Product = value; }
        }

     
        

        private void DashBoard_Load(object sender, EventArgs e)
        {
            dobj = this;

            

            UCProduct up = new UCProduct(gemail);
            panel4.Controls.Add(up);

            

            

            
        }

        private void Product_Click(object sender, EventArgs e)
        {
            
            panel4.Controls["UCProduct"].Show();
            
            
            
            

        }

        

        private void MyOrderB_Click(object sender, EventArgs e)
        {
            try
            {
                OrderPopUp opu = new OrderPopUp(gemail);
                opu.ShowDialog();

                // Check if the form is disposed before trying to access it
                if (!opu.IsDisposed)
                {
                    // Access the order identifier using the GetOrderIdentifier() method
                    string orderIdentifier = opu.GetOrderIdentifier();

                    // Do something with the orderIdentifier here instead of displaying it in a message box
                    // For example, you can assign it to another control's text property
                    //Label1.Text = "Order Identifier: " + orderIdentifier;

                    // Or perform any other necessary action using the orderIdentifier
                    // ...
                }
                else
                {
                    // Handle the case where the form is disposed
                    MessageBox.Show("The order form has been closed.");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("please order anything you want from prduct!  " );
            }


        }

        

        

        

        private void ProfileB_Click(object sender, EventArgs e)
        {

            ProfileForm pf = new ProfileForm(gemail);
            pf.Show();
            dobj.Hide();
            
        }

        private void back_Click(object sender, EventArgs e)
        {
            customerLogin cs = new customerLogin();
            cs.Show();
            this.Hide();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            History h = new History(gemail);
            h.Show();
            this.Hide();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;

        }
    }
}
