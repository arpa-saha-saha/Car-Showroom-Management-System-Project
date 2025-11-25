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
    public partial class CarDealer : Form
    {
        private Option optionForm;
        private LogP loginForm;

        public CarDealer()
        {
            InitializeComponent();
        }

        public CarDealer(Option option,LogP login  )
        {
            InitializeComponent();
            this.optionForm = option;
            this.loginForm = login;
            
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
                cmd.CommandText = "select * from CarDealer";

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                dgvDealer.AutoGenerateColumns = false;
                dgvDealer.DataSource = dt;
                dgvDealer.Refresh();
                dgvDealer.ClearSelection();

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CarDealer_Load(object sender, EventArgs e)
        {
            this.LoadData();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            NewData();

        }

        private void NewData()
        {
            txtId.Text = "Auto Generated";
            txtDealer.Text = "";

            dgvDealer.ClearSelection();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            string dealer = txtDealer.Text;
            if (string.IsNullOrWhiteSpace(dealer))
            {
                MessageBox.Show("Please enter Dealer Name.");
                return;
            }

            string query = "";

            if (id == "Auto Generated")
            {
                query = $"insert into CarDealer values ('{dealer}')";
            }
            else
            {
                query = $"update CarDealer set DealerName ='{dealer}' where id = {id}";
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
                    cmd.CommandText = $"delete from CarDealer where id = '{id}' ";
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


            string title = txtDealer.Text;
        
    }

        private void dgvDealer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            txtId.Text = dgvDealer.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtDealer.Text = dgvDealer.Rows[e.RowIndex].Cells[1].Value.ToString();
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

        private void CarDealer_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }
    }
 }

