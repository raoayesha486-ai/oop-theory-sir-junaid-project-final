using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace oop_theory_sir_junaid_project_final
{
    public partial class ManageProducts : Form
    {
        public ManageProducts()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30");
        void fillcategory()
        {
            string query = "select * from CategoryTb1";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr;
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CategoryName", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                CatCombo.ValueMember = "CategoryName";
                CatCombo.DataSource = dt;
                SearchCombo.ValueMember= "CategoryName";
                SearchCombo.DataSource = dt;
                con.Close();
            }
            catch
            {

            }
        }
        //void fillSearchcombo()
        //{
        //    string query = "select * from CategoryTb1 where CategoryName='"+SearchCombo.SelectedValue.ToString()+"'";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    SqlDataReader rdr;
        //    try
        //    {
        //        con.Open();
        //        DataTable dt = new DataTable();
        //        dt.Columns.Add("CategoryName", typeof(string));
        //        rdr = cmd.ExecuteReader();
        //        dt.Load(rdr);
        //        CatCombo.ValueMember = "CategoryName";
        //        CatCombo.DataSource = dt;
        //        con.Close();
        //    }
        //    catch
        //    {

        //    }
        //}

        private void ManageProducts_Load(object sender, EventArgs e) { 
            fillcategory();
            populate();
        }
        void populate() {
            try { 
                if (con.State == ConnectionState.Open) con.Close(); con.Open(); string Myquery = "SELECT * FROM EquipmentTb1"; SqlDataAdapter da = new SqlDataAdapter(Myquery, con); DataSet ds = new DataSet(); da.Fill(ds); EquipmentGV.DataSource = ds.Tables[0]; 
            } catch
            (Exception ex) {
                MessageBox.Show(ex.Message); 
            } finally {
                if (con.State == ConnectionState.Open) con.Close(); }
        }

        void filterbycategory()
        {
            try
            {
                if (con.State == ConnectionState.Open) con.Close(); con.Open(); string Myquery = "SELECT * FROM EquipmentTb1 where EquipmentCategory='"+SearchCombo.SelectedValue.ToString()+"'"; SqlDataAdapter da = new SqlDataAdapter(Myquery, con); DataSet ds = new DataSet(); da.Fill(ds); EquipmentGV.DataSource = ds.Tables[0];
            }
            catch
            (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }


        private void EquipmentGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EquipmentIdTb1.Text = EquipmentGV.Rows[e.RowIndex].Cells[0].Value.ToString();
            EquipmentNameTb2.Text = EquipmentGV.Rows[e.RowIndex].Cells[1].Value.ToString();
            EquipmentBrandTb3.Text = EquipmentGV.Rows[e.RowIndex].Cells[2].Value.ToString();
            EquipmentModelTb4.Text = EquipmentGV.Rows[e.RowIndex].Cells[3].Value.ToString();
            EquipmentSerialNumberTb5.Text = EquipmentGV.Rows[e.RowIndex].Cells[4].Value.ToString();
            EquipmentPurchaseDateTb6.Text = EquipmentGV.Rows[e.RowIndex].Cells[5].Value.ToString();
            EquipmentWarantyTb7.Text = EquipmentGV.Rows[e.RowIndex].Cells[6].Value.ToString();
            EquipmentStatusTb8.Text = EquipmentGV.Rows[e.RowIndex].Cells[7].Value.ToString();
            CatCombo.Text = EquipmentGV.Rows[e.RowIndex].Cells[8].Value.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO EquipmentTb1 VALUES(" + EquipmentIdTb1.Text + ",'" + EquipmentNameTb2.Text + "','" + EquipmentBrandTb3.Text + "','" + EquipmentModelTb4.Text + "','" + EquipmentSerialNumberTb5.Text + "','" + EquipmentPurchaseDateTb6.Text + "','" + EquipmentWarantyTb7.Text + "','" + EquipmentStatusTb8.Text + "','" + CatCombo.SelectedValue.ToString() + "')", con);

                //cmd.Parameters.AddWithValue("@id", Convert.ToInt32(EquipmentIdTb1.Text));
                //cmd.Parameters.AddWithValue("@name", );
                //cmd.Parameters.AddWithValue("@Brand", EquipmentBrandTb3.Text);
                //cmd.Parameters.AddWithValue("@Model", EquipmentModelTb4.Text);
                //cmd.Parameters.AddWithValue("@serialNumber", EquipmentSerialNumberTb5.Text);
                //cmd.Parameters.AddWithValue("@purchaseDate", EquipmentPurchaseDateTb6.Text);
                //cmd.Parameters.AddWithValue("@Waranty", EquipmentWarantyTb7.Text);
                //cmd.Parameters.AddWithValue("@Status", EquipmentStatusTb8.Text);


                cmd.ExecuteNonQuery();

                MessageBox.Show("Product Added Successfully");

                con.Close();

                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {

            if (EquipmentIdTb1.Text == "")
            {
                MessageBox.Show("Enter the Equipment ID");
            }
            else
            {
                con.Open();

                string myquery = "DELETE FROM EquipmentTb1 WHERE EquipmentID = " + EquipmentIdTb1.Text;

                SqlCommand cmd = new SqlCommand(myquery, con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Equipment successfully deleted");

                con.Close();
                populate();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE EquipmentTb1 SET EquipmentName='" + EquipmentNameTb2.Text + "', Brand='" + EquipmentBrandTb3.Text + "', Model='" + EquipmentModelTb4.Text + "', SerialNumber='" + EquipmentSerialNumberTb5.Text + "', PurchaseDate='" + EquipmentPurchaseDateTb6.Text + "', Waranty='" + EquipmentWarantyTb7.Text + "', Status='" + EquipmentStatusTb8.Text + "', EquipmentCat='" + CatCombo.Text + "' WHERE EquipmentId=" + EquipmentIdTb1.Text, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Equipment Updated Sucessfully");
                con.Close();
                populate();
            }
            catch
            {

            }
        }
        private void EquipmentIdTb1_TextChanged(object sender, EventArgs e)
        {

        }
        private void SearchCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CatCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e) { 
        

        }

        private void EquipmentCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            filterbycategory();
        }
            


        private void label14_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }
    }
}
