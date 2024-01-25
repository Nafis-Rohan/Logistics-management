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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void GetStarted_Click(object sender, EventArgs e)
        {
            //if (radioButton1.Checked)
            //{
            //    customerLogin clg = new customerLogin();
            //    clg.Show();
            //    this.Hide();
            //}

            //else if(radioButton2.Checked)
            //{ EmployeeLogin elg = new EmployeeLogin();
            //elg.Show(); 
            //this.Hide();
            //}

            customerLogin clg = new customerLogin();
            clg.Show();
            this.Hide();



        }

        private void Customer_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminLogin alg = new AdminLogin();
            alg.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
