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
//using System.Data.SqlClient;

namespace oop_theory_sir_junaid_project_final
{
    public partial class ManageCustomer : Form

    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30");
      

        public ManageCustomer()
        {
            InitializeComponent();
        }
        void populate()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                con.Open();

                string Myquery = "SELECT * FROM CustomerTb1";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                CustomersGV.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                "INSERT INTO CustomerTb1 (CustomerID, CustomerName, CompanyName, PhoneNumber, Address) VALUES (@id,@name,@company,@phone,@address)", con);

                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(CustomerIdTb1.Text));
                cmd.Parameters.AddWithValue("@name", CustomerNameTb2.Text);
                cmd.Parameters.AddWithValue("@company", CustomerCompanyTb3.Text);
                cmd.Parameters.AddWithValue("@phone", CustomerPhoneNumberTb4.Text);
                cmd.Parameters.AddWithValue("@address", CustomerAdressTb5.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Customer Added Successfully");

                con.Close();

                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void CustomersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustomerIdTb1.Text = CustomersGV.Rows[e.RowIndex].Cells[0].Value.ToString();
            CustomerNameTb2.Text = CustomersGV.Rows[e.RowIndex].Cells[1].Value.ToString();
            CustomerCompanyTb3.Text = CustomersGV.Rows[e.RowIndex].Cells[2].Value.ToString();
            CustomerPhoneNumberTb4.Text = CustomersGV.Rows[e.RowIndex].Cells[3].Value.ToString();
            CustomerAdressTb5.Text = CustomersGV.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (CustomerIdTb1.Text == "")
            {
                MessageBox.Show("Enter the Customer ID");
            }
            else
            {
                con.Open();

                string myquery = "DELETE FROM CustomerTb1 WHERE CustomerID = " + CustomerIdTb1.Text;

                SqlCommand cmd = new SqlCommand(myquery, con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Customer successfully deleted");

                con.Close();
                populate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE CustomerTb1 SET CustomerName=@name, CompanyName=@company, PhoneNumber=@phone, Address=@address WHERE CustomerID=@id", con);

                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(CustomerIdTb1.Text));
                cmd.Parameters.AddWithValue("@name", CustomerNameTb2.Text);
                cmd.Parameters.AddWithValue("@company", CustomerCompanyTb3.Text);
                cmd.Parameters.AddWithValue("@phone", CustomerPhoneNumberTb4.Text);
                cmd.Parameters.AddWithValue("@address", CustomerAdressTb5.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Customer Successfully Updated");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                populate();
            }
        }

        private void ManageCustomer_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }
    }
    }

