using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class employeeDashboard : Form
    {
        string gID;
        public employeeDashboard(string ID)
        {
            gID = ID;

            InitializeComponent();
        }

        static employeeDashboard eobj;

        private void employeeDashboard_Load(object sender, EventArgs e)
        {
            eobj = this;

            UCproductview ucpv = new UCproductview();
            panel4.Controls.Add(ucpv);


            UAdd_product uad = new UAdd_product();
            panel4.Controls.Add(uad);

            UcOrder uo = new UcOrder();
            panel4.Controls.Add(uo);
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            EmployeeProfile eP = new EmployeeProfile(gID);
            eP.Show();
            this.Close();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Add_Click(object sender, EventArgs e)
        {
            panel4.Controls["UAdd_product"].Show();
            panel4.Controls["UCproductview"].Hide();
            panel4.Controls["UcOrder"].Hide();
        }

        private void back_Click(object sender, EventArgs e)
        {
            customerLogin c = new customerLogin();
            c.Show();
            this.Hide();
        }

        private void Product_Click(object sender, EventArgs e)
        {
            panel4.Controls["UCproductview"].Show();
            panel4.Controls["UAdd_product"].Hide();
            panel4.Controls["UcOrder"].Hide();

        }

        private void FAQ_Click(object sender, EventArgs e)
        {
            panel4.Controls["UCproductview"].Hide();
            panel4.Controls["UAdd_product"].Hide();
            panel4.Controls["UcOrder"].Show();
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
