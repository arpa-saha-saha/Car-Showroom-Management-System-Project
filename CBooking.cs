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
    public partial class CBooking : Form
    {
        private COption coptionForm;
        private LogP loginForm;

        public CBooking(COption option, LogP login)
        {
            InitializeComponent();
            this.coptionForm = option;
            this.loginForm = login;
        }

        public CBooking()
        {
            InitializeComponent();
        }

        private void CBooking_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {

        }
        private void NewData()
        {
            txtId.Text = "Auto Generated";
            cmbCustomerName.SelectedValue = -1;
            cmbCarId.SelectedValue = -1;
            dgvData.ClearSelection();

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewData();
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
                cmd.CommandText = $"select CarBooking.ID,UserInfo.FullName,CarInfo.ID as CarID,CarBooking.status from CarBooking inner join UserInfo on CarBooking.UiID = UserInfo.ID inner join CarInfo on CarBooking.CiID = CarInfo.ID where UserInfo.FullName = '{ApplicationHelper.FullName}'; select ID,FullName from UserInfo where FullName = '{ApplicationHelper.FullName}'; select ID from CarInfo";


                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);

                dgvData.AutoGenerateColumns = false;
                dgvData.DataSource = ds.Tables[0];
                dgvData.Refresh();
                dgvData.ClearSelection();

                cmbCustomerName.DataSource = ds.Tables[1];
                cmbCustomerName.DisplayMember = "FullName";
                cmbCustomerName.ValueMember = "ID";

                cmbCarId.DataSource = ds.Tables[2];
                cmbCarId.DisplayMember = "ID";
                cmbCarId.ValueMember = "ID";




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            NewData();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            coptionForm.Show();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            loginForm.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;

            if (cmbCustomerName.SelectedValue == null)
            {
                MessageBox.Show("Error: Please select your Name.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            string userId = cmbCustomerName.SelectedValue.ToString();




            if (cmbCarId.SelectedItem == null)
            {
                MessageBox.Show("Error: Car ID is required.","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string carId = cmbCarId.SelectedValue.ToString();




            try
            {

                var con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.ConnectionPath;
                con.Open();

                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"INSERT INTO CarBooking (UiID, CiID, status) VALUES ('{userId}', '{carId}', 'False')";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Booking Successful");

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
                    cmd.CommandText = $"delete from CarBooking where id = '{id}' ";
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
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            txtId.Text = dgvData.Rows[e.RowIndex].Cells[0].Value.ToString();
            cmbCustomerName.Text = dgvData.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmbCarId.Text = dgvData.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
    }
}
