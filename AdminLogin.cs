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
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void Lo_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String password;
            //email = textBox1.Text;
            password = textBox2.Text;

            if ( string.IsNullOrEmpty(textBox2.Text))
            {
              
                MessageBox.Show(this, "Please input KEY", "Title", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //MessageBox.Show("Please input KEY");
            }
            else if ( password == "123")
            {
                Admindashboard ad1 = new Admindashboard();
                ad1.Show();
                this.Close();
            }
            //if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            //{
            //    MessageBox.Show("Please input Username and Password");
            //}
            //else if (email == "admin@gmail.com" || password == "admin")
            //{
            //    Admindashboard ad1 = new Admindashboard();
            //    ad1.Show();
            //    this.Hide();
            //}
            else
            {
                MessageBox.Show("INCORRECT");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home A1 = new Home();
            A1.Show();
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
