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
using WindowsFormsApp1;

namespace CarShowroomManagement
{
    public partial class CarCatalog : Form
    {
        private Option optionForm;
        private LogP loginForm;
        public CarCatalog(Option option, LogP login)
        {
            InitializeComponent();
            this.optionForm = option;
            this.loginForm = login;
        }

        public CarCatalog()
        {
            InitializeComponent();
        }

       

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

       

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Close();
            this.optionForm.Hide();
            loginForm.Show();
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            this.optionForm.Show();
            this.Close();
        }

        private void CarCatalog_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }

        

        private void CarCatalog_Load(object sender, EventArgs e)
        {
            try
            {
                var con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.ConnectionPath;
                con.Open();

                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select CarInfo.ID ,CarType.Type,CarBrand.BrandName 'Brand',Model, CC,Color,Price,DateOfPurchase,CarDealer.DealerName, CarInfo.CTID, CarInfo.BID,CarInfo.DID from CarInfo inner join CarType on CarType.ID= CarInfo.CTID inner join CarBrand on CarBrand.ID= CarInfo.BID inner join CarDealer on CarDealer.ID = CarInfo.DID;select * from CarType;select * from CarBrand;select * from CarDealer;";

                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);

                dgvCarCatalog.AutoGenerateColumns = false;
                dgvCarCatalog.DataSource = ds.Tables[0];
                dgvCarCatalog.Refresh();
                dgvCarCatalog.ClearSelection();

                

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                var con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.ConnectionPath;
                con.Open();

                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select CarInfo.ID ,CarType.Type,CarBrand.BrandName 'Brand',Model, CC,Color,Price,DateOfPurchase,CarDealer.DealerName, CarInfo.CTID, CarInfo.BID,CarInfo.DID from CarInfo inner join CarType on CarType.ID= CarInfo.CTID inner join CarBrand on CarBrand.ID= CarInfo.BID inner join CarDealer on CarDealer.ID = CarInfo.DID;select * from CarType;select * from CarBrand;select * from CarDealer;";

                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);

                dgvCarCatalog.AutoGenerateColumns = false;
                dgvCarCatalog.DataSource = ds.Tables[0];
                dgvCarCatalog.Refresh();
                dgvCarCatalog.ClearSelection();



                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
