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

namespace Demo4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        public void loadData()
        {
            string connString = @"uid=sa;pwd=123456789;Initial Catalog=Cars;Data Source=NGHIALPGSE63314\LPGN";
            SqlConnection conn = new SqlConnection(connString);
            string query = "select * from Inventory ";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                da.Fill(dt);
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
            dataGridView1.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                txtCarId.Text = dataGridView1.SelectedRows[0].Cells["CarID"].Value.ToString();
                txtMake.Text = dataGridView1.SelectedRows[0].Cells["Make"].Value.ToString();
                txtColor.Text = dataGridView1.SelectedRows[0].Cells["Color"].Value.ToString();
                txtPetName.Text = dataGridView1.SelectedRows[0].Cells["PetNames"].Value.ToString();
            }
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            string connString = @"uid=sa;pwd=123456789;Initial Catalog=Cars;Data Source=NGHIALPGSE63314\LPGN";
            SqlConnection conn = new SqlConnection(connString);
            string query = "insert Inventory values(@CarID,@Make,@Color,@PetNames)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@CarID", Convert.ToInt32(txtCarId.Text));
            cmd.Parameters.AddWithValue("@Make", txtMake.Text);
            cmd.Parameters.AddWithValue("@Color",txtColor.Text);
            cmd.Parameters.AddWithValue("@PetNames", txtPetName.Text);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    loadData();
                }
                else
                {
                    MessageBox.Show("Insert failed!!!");
                }
            }
            catch (Exception se)
            {
                throw new Exception(se.Message);
            }
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            string connString = @"uid=sa;pwd=123456789;Initial Catalog=Cars;Data Source=NGHIALPGSE63314\LPGN";
            SqlConnection conn = new SqlConnection(connString);
            string query = "update Inventory set Make = @Make,Color = @Color,PetNames =@PetNames where CarID = @CarID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@CarID", Convert.ToInt32(txtCarId.Text));
            cmd.Parameters.AddWithValue("@Make", txtMake.Text);
            cmd.Parameters.AddWithValue("@Color", txtColor.Text);
            cmd.Parameters.AddWithValue("@PetNames", txtPetName.Text);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    loadData();
                }
                else
                {
                    MessageBox.Show("Update failed");
                }
            }
            catch (Exception se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            string connString = @"uid=sa;pwd=123456789;Initial Catalog=Cars;Data Source=NGHIALPGSE63314\LPGN";
            SqlConnection conn = new SqlConnection(connString);
            string query = "delete Inventory where CarID = @CarID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@CarID", Convert.ToInt32(txtCarId.Text));
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    loadData();
                }
                else
                {
                    MessageBox.Show("Delete failed");
                }
            }
            catch (Exception se)
            {
                MessageBox.Show(se.Message);
            }
        }
    }
}
