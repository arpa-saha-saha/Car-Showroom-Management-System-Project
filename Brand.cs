using CarShowroomManagement;
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
using System.Xml.Linq;

namespace WindowsFormsApp1
{

    public partial class Brand : Form
    {
        private Option optionForm;
        private LogP loginForm;
        public Brand(Option option,LogP login)
        {
            InitializeComponent();
            this.optionForm = option;
            this.loginForm = login;
        }
        public Brand()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.optionForm.Show();
            this.Close();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Close();
            this.optionForm.Hide();
            this.loginForm.Show();
        }

        private void LoadData()
        {
            try
            {
                var con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.ConnectionPath;
                con.Open();

                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from CarBrand";

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                dgvBrand.AutoGenerateColumns = false;
                dgvBrand.DataSource = dt;
                dgvBrand.Refresh();
                dgvBrand.ClearSelection();

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void NewData()
        {
            txtId.Text = "Auto Generated";
            txtBrand.Text = "";

            dgvBrand.ClearSelection();
        }

        private void Brand_Load(object sender, EventArgs e)
        {
            LoadData();
            NewData();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            NewData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            string brand = txtBrand.Text;
            if (string.IsNullOrWhiteSpace(brand))
            {
                MessageBox.Show("Please enter Brand Name");
                return;
            }

            string query = "";

            if (id == "Auto Generated")
            {
                query = $"insert into CarBrand values ('{brand}')";
            }
            else
            {
                query = $"update CarBrand set BrandName ='{brand}' where id = {id}";
            }

            try
            {
                var con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.ConnectionPath;
                con.Open();

                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Done");

                this.LoadData();
                this.NewData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            if (id == "Auto Generated")
            {
                MessageBox.Show("Please select a row first", "Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Are you sure?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No)
                return;

            if (result == DialogResult.Yes)
            {
                try
                {
                    var con = new SqlConnection();
                    con.ConnectionString = ApplicationHelper.ConnectionPath;
                    con.Open();

                    var cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = $"delete from CarBrand where id = '{id}' ";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Delete Done");

                    this.LoadData();
                    this.NewData();


                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            }

        private void dgvBrand_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            txtId.Text = dgvBrand.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtBrand.Text = dgvBrand.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void Brand_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }
    }
}
