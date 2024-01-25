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

namespace project
{
    public partial class Admindashboard : Form
    {
        

        

        public Admindashboard()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Admindashboard_Load(object sender, EventArgs e)
        {
            //aobj = this;

            

            CustomerManagement csm = new CustomerManagement();
            panel3.Controls.Add(csm);

            UCemployee uce = new UCemployee();
            panel3.Controls.Add(uce);

            

            
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CustomerMngButton_Click(object sender, EventArgs e)
        {

            panel3.Controls["CustomerManagement"].Show();
            panel3.Controls["UCemployee"].Hide();
            
        }

        private void Emp_Click(object sender, EventArgs e)
        {
            panel3.Controls["CustomerManagement"].Hide();
            panel3.Controls["UCemployee"].Show();
            
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            
            
        }

        private void ShowPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminLogin A1 = new AdminLogin();
            A1.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
