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


namespace oop_theory_sir_junaid_project_final
{
    public partial class ManageOrders : Form
    {
        public ManageOrders()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30");
      
            void populate()
            {
                try
                {
                    if (con.State == ConnectionState.Open) con.Close(); con.Open(); string Myquery = "SELECT * FROM CustomerTb1"; SqlDataAdapter da = new SqlDataAdapter(Myquery, con); DataSet ds = new DataSet(); da.Fill(ds); CustomersGV.DataSource = ds.Tables[0];
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
        void populateproducts()
        {
            try
            {
                if (con.State == ConnectionState.Open) con.Close(); con.Open(); string Myquery = "SELECT * FROM EquipmentTb1"; SqlDataAdapter da = new SqlDataAdapter(Myquery, con); DataSet ds = new DataSet(); da.Fill(ds); EquipmentGV.DataSource = ds.Tables[0];
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
                //CatCombo.ValueMember = "CategoryName";
                //CatCombo.DataSource = dt;
                SearchCombo.ValueMember = "CategoryName";
                SearchCombo.DataSource = dt;
                con.Close();
            }
            catch
            {

            }
        }
        //void updateproduct()
        //{
        //    con.Open();
        //    int id = Convert.ToInt32(EquipmentGV.CurrentRow.Cells[0].Value.ToString());
        //    int newQty = stock - Convert.ToInt32(QtyTb.Text);
        //    string query = "update EquipmentTb1 set EquipmentQuantity=" + newQty + " where EquipmentId=" + id + ";";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //    populateproducts();


        //}
        //int num = 0;
        //int Uprice, Totalprice, qty;
        //string Equipment;
        int Uprice, Totalprice, qty;
        string Equipment;
        int flag = 0;
        int num = 0;
        int stock;

        DataTable table = new DataTable();
        private void ManageOrders_Load(object sender, EventArgs e)
        {
            table.Columns.Add("No");
            table.Columns.Add("Equipment");
            table.Columns.Add("Quantity");
            table.Columns.Add("Unit Price");
            table.Columns.Add("Total Price");

            OrderGV.DataSource = table;
            populate();
            populateproducts();
            fillcategory();
        }

        private void CustomersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustomerId.Text = CustomersGV.Rows[e.RowIndex].Cells[0].Value.ToString();
        }
        //int flag = 0;
        private void EquipmentGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Equipment = EquipmentGV.Rows[e.RowIndex].Cells[1].Value.ToString();
            qty = Convert.ToInt32(QtyTb.Text);
            //stock = Convert.ToInt32(EquipmentGV.Rows[e.RowIndex].Cells[9].Value.ToString());
            Uprice = Convert.ToInt32(EquipmentGV.Rows[e.RowIndex].Cells[3].Value.ToString());
            Totalprice = qty * Uprice;
            flag = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sum = 0;
            if (string.IsNullOrWhiteSpace(QtyTb.Text))
            {
                MessageBox.Show("Enter the quantity of equipments");
                return;

            }
            //else if (Convert.ToInt32(QtyTb.Text) >= stock)
            //    MessageBox.Show("No Enough Stock Avalable");
            //    if (flag == 0)
            //{
            //    MessageBox.Show("Select the equipment first");
            //    return;
            //}

            //if (!int.TryParse(QtyTb.Text, out qty))
            //{
            //    MessageBox.Show("Quantity must be a number.");
            //    QtyTb.Focus();
            //    return;
            //}

            num++;

            Totalprice = qty * Uprice;

            table.Rows.Add(num, Equipment, qty, Uprice, Totalprice);

            OrderGV.DataSource = table;

            flag = 0;
            sum = sum + Totalprice;
            TotalAmount.Text = "Rs" + sum.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }

        private void SearchCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SearchCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Open) con.Close(); con.Open(); string Myquery = "SELECT * FROM EquipmentTb1 where EquipmentCategory='" + SearchCombo.SelectedValue.ToString() + "'"; SqlDataAdapter da = new SqlDataAdapter(Myquery, con); DataSet ds = new DataSet(); da.Fill(ds); EquipmentGV.DataSource = ds.Tables[0];
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
    }
}

