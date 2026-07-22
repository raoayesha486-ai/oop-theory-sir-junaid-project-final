using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace oop_theory_sir_junaid_project_final
{
    public partial class ManageUsers : Form
    {
        public ManageUsers()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30");
        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        void populate()
        {
            try
            {
                con.Open();
                string Myquery = "select * from UserTb1";
                SqlDataAdapter da = new SqlDataAdapter(Myquery,con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                UsersGV.DataSource = ds.Tables[0];
                
                con.Close();
            }
            catch
            {

            }
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
         
            
                try
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO UserTb1 (UserID, UserName, UserFullName, UserPassword, UserPhoneNumber) VALUES (@id, @username, @fullname, @password, @phone)", con);

                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(UserIdTb1.Text));
                    cmd.Parameters.AddWithValue("@username", UserNameTb2.Text);
                    cmd.Parameters.AddWithValue("@fullname", UserFullNameTb3.Text);
                    cmd.Parameters.AddWithValue("@password", UserPasswordTb4.Text);
                    cmd.Parameters.AddWithValue("@phone", UserPhoneNumberTb6.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("User Successfully Added");
                    con.Close();
                    populate();
            }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }



            }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (UserPhoneNumberTb6.Text == "")
            {
                MessageBox.Show("Enter the users phone number");
            }
            else
            {
                con.Open();
                string myquery = "delete from userTb1 where UserPhoneNumber='"+UserPhoneNumberTb6.Text+"';";
                SqlCommand cmd = new SqlCommand(myquery, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Users sucessfully deleted");
                con.Close();
                populate();
            }
        }

        private void UsersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UserIdTb1.Text = UsersGV.Rows[e.RowIndex].Cells[0].Value.ToString();
            UserNameTb2.Text = UsersGV.Rows[e.RowIndex].Cells[1].Value.ToString();
            UserFullNameTb3.Text = UsersGV.Rows[e.RowIndex].Cells[2].Value.ToString();
            UserPasswordTb4.Text = UsersGV.Rows[e.RowIndex].Cells[3].Value.ToString();
            UserPhoneNumberTb6.Text = UsersGV.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                "UPDATE UserTb1 SET UserName=@username, UserFullName=@fullname, UserPassword=@password, UserPhoneNumber=@phone WHERE UserID=@id", con);

                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(UserIdTb1.Text));
                cmd.Parameters.AddWithValue("@username", UserNameTb2.Text);
                cmd.Parameters.AddWithValue("@fullname", UserFullNameTb3.Text);
                cmd.Parameters.AddWithValue("@password", UserPasswordTb4.Text);
                cmd.Parameters.AddWithValue("@phone", UserPhoneNumberTb6.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("User Successfully Updated");

                con.Close();
                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }
    }
    }
    
