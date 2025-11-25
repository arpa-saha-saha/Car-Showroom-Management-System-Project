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
    public partial class CCarCustomer : Form
    {
        private COption optionForm;
        private LogP loginForm;

        public CCarCustomer(COption option, LogP login)
        {
            InitializeComponent();
            this.optionForm = option;
            this.loginForm = login;
        }
        public CCarCustomer()
        {
            InitializeComponent();
        }

        private void CCarCustomer_Load(object sender, EventArgs e)
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            optionForm.Show();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            loginForm.Show();
        }
    }
}
