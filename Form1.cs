using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;
namespace oop_theory_sir_junaid_project_final
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30");
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           
            if (checkBox1.Checked)
            {
                PasswordTb.UseSystemPasswordChar = false; 
            }
            else
            {
                PasswordTb.UseSystemPasswordChar = true; 
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            unameTb.Text = "";
            PasswordTb.Text = "";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from UserTb1 where UserName='" + unameTb.Text + "' and UserPassword='" + PasswordTb.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                ManageCustomer cust = new ManageCustomer();
                cust.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong userName and Password");
            }
            con.Close();
        }
    }
    }

