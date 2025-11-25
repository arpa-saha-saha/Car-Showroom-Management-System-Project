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

namespace WindowsFormsApp1
{
    
    public partial class CarSale : Form
    {
        private LogP loginForm;
        private Option optionForm;

        public CarSale(Option option,LogP login)
        {
            InitializeComponent();
            this.loginForm = login;
            optionForm = option;
        }

        public CarSale()
        {
            InitializeComponent();
        }

        private void CarSale_Load(object sender, EventArgs e)
        {
            this.LoadData();
            this.NewData();
        }
        private void newBtn_Click(object sender, EventArgs e)
        {
            this.NewData();
        }
        private void NewData()
        {
            txtBoxID.Text = "Auto Generated";
            cmbCarID.SelectedValue = -1;
            cmbCustomerName.SelectedValue= -1;

            dtpSaleDate.Value = DateTime.Now;
            cmbPm.SelectedIndex = -1;
            txtBill.Text = "";
            dgvData.ClearSelection();
        }

        private void LoadData()
        {
            try
            {
                
                var con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.ConnectionPath ;
                con.Open();

                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select CarSale.ID, CarInfo.ID 'CarID',CustomerInfo.FullName ,CarSale.SaleDate,CarSale.PaymentMethod,CarSale.TotalBill,CarSale.CustomerID from CarSale inner join CarInfo on CarSale.CiID = CarInfo.ID inner join CustomerInfo on CarSale.CustomerID = CustomerInfo.ID;select ID from CarInfo; select * From CustomerInfo";

                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);

                dgvData.AutoGenerateColumns = false;
                dgvData.DataSource = ds.Tables[0];
                dgvData.Refresh();
                dgvData.ClearSelection();

                cmbCarID.DataSource = ds.Tables[1];
                cmbCarID.DisplayMember = "ID";
                cmbCarID.ValueMember = "ID";

                cmbCustomerName.DataSource = ds.Tables[2];
                cmbCustomerName.DisplayMember = "FullName";
                cmbCustomerName.ValueMember = "ID";



                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            txtBoxID.Text = dgvData.Rows[e.RowIndex].Cells[0].Value.ToString();
            cmbCarID.SelectedValue = dgvData.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmbCustomerName.SelectedValue = dgvData.Rows[e.RowIndex].Cells[7].Value.ToString();

            dtpSaleDate.Value = Convert.ToDateTime(dgvData.Rows[e.RowIndex].Cells[3].Value.ToString());
            cmbPm.Text = dgvData.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtBill.Text = dgvData.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void refBtn_Click(object sender, EventArgs e)
        {
            this.LoadData();
            this.NewData();
        }

        private void dltBtn_Click(object sender, EventArgs e)
        {
            string id = txtBoxID.Text;
            if (id == "Auto Generated")
            {
                MessageBox.Show("Please select a row first", "Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Are you sure?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;

            try
            {
                
                var con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.ConnectionPath;
                con.Open();

                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"delete from CarSale where ID = '{id}'";
                cmd.ExecuteNonQuery();

                MessageBox.Show("Delete Completed");
                this.LoadData();
                this.NewData();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            string id = txtBoxID.Text;

            if (cmbCarID.SelectedValue == null)
            {
                MessageBox.Show("Error: CarID is required.");
                return;
            }

            string ciid = cmbCarID.SelectedValue.ToString();
            string customer = cmbCustomerName.SelectedValue?.ToString();

            DateTime saleDate = dtpSaleDate.Value;

            if (cmbPm.SelectedItem == null)
            {
                MessageBox.Show("Error: Payment method is required.");
                return;
            }
            string payment = cmbPm.SelectedItem.ToString();

            string totalBill = txtBill.Text;

            if (string.IsNullOrWhiteSpace(customer))
            {
                MessageBox.Show("Error: Customer name is required.");
                return;
            }

            string query = "";
            
            if (id == "Auto Generated")
            {
                query = $"insert into CarSale values ('{ciid}', '{customer}', '{saleDate}', '{payment}', '{totalBill}')";
                
            }
            else
            {
                query = $"update CarSale set CiID = '{ciid}', SaleDate = '{saleDate}', PaymentMethod = '{payment}', TotalBill = '{totalBill}' where ID = '{id}'";
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
                
                



                MessageBox.Show("Operation Successful");
                this.LoadData();
                this.NewData();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            optionForm.Show();
        }

        private void btnlog_Click(object sender, EventArgs e)
        {
            this.Hide();
            loginForm.Show();
        }
    }
}
