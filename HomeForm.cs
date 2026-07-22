using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oop_theory_sir_junaid_project_final
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            ManageCategories cad = new ManageCategories();
            cad.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            ManageUsers use = new ManageUsers();
            use.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            ManageProducts prod = new ManageProducts();
            prod.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {

            ManageCustomer cust = new ManageCustomer();
            cust.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            ManageOrders ord = new ManageOrders();
            ord.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Hide();
        }
    }
}
