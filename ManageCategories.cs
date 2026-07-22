using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace oop_theory_sir_junaid_project_final
{
    public partial class ManageCategories : Form
    {
        public ManageCategories()
        {
            InitializeComponent();
            populate();
        }

        SqlConnection con = new SqlConnection(
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30");

        // Display Data
        void populate()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM CategoryTb1", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                CategoriesGV.DataSource = dt;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ADD
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO CategoryTb1(CategoryID, CategoryName) VALUES(@id,@name)", con);

                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(CategoryIdTb1.Text));
                cmd.Parameters.AddWithValue("@name", CategoryNameTb2.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Category Added Successfully");

                con.Close();

                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // EDIT
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE CategoryTb1 SET CategoryName=@name WHERE CategoryID=@id", con);

                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(CategoryIdTb1.Text));
                cmd.Parameters.AddWithValue("@name", CategoryNameTb2.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Category Updated Successfully");

                con.Close();

                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // DELETE
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM CategoryTb1 WHERE CategoryID=@id", con);

                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(CategoryIdTb1.Text));

                cmd.ExecuteNonQuery();

                MessageBox.Show("Category Deleted Successfully");

                con.Close();

                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void CategoriesGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                CategoryIdTb1.Text = CategoriesGV.Rows[e.RowIndex].Cells[0].Value.ToString();
                CategoryNameTb2.Text = CategoriesGV.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
        }


        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (CategoryIdTb1.Text == "")
            {
                MessageBox.Show("Enter the Category ID");
            }
            else
            {
                con.Open();

                string myquery = "DELETE FROM CategoryTb1 WHERE CategoryId = " + CategoryIdTb1.Text;

                SqlCommand cmd = new SqlCommand(myquery, con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Category successfully deleted");

                con.Close();
                populate();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            if (CategoryIdTb1.Text == "" || CategoryNameTb2.Text == "")
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                con.Open();

                string query = "UPDATE CategoryTb1 SET CategoryName=@name WHERE CategoryID=@id";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(CategoryIdTb1.Text));
                cmd.Parameters.AddWithValue("@name", CategoryNameTb2.Text);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("Category Updated Successfully");
                }
                else
                {
                    MessageBox.Show("Category ID not found.");
                }
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

        private void CategoriesGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                CategoryIdTb1.Text = CategoriesGV.Rows[e.RowIndex].Cells[0].Value.ToString();
                CategoryNameTb2.Text = CategoriesGV.Rows[e.RowIndex].Cells[1].Value.ToString();
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
    