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
    public partial class MyInfo : Form
    {
        private LogP loginForm;
        private COption optionForm;

        public MyInfo(COption cOption, LogP login)
        {
            InitializeComponent();
            this.loginForm = login;
            this.optionForm = cOption;
        }



        public MyInfo()
        {
            InitializeComponent();
        }

        private void NewData()
        {

            txtID.Text = "";
            txtFullName.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";

            dgvData.ClearSelection();

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
                cmd.CommandText = $"select * from UserInfo where FullName = '{ApplicationHelper.FullName}' ";


                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                dgvData.AutoGenerateColumns = false;
                dgvData.DataSource = dt;
                dgvData.Refresh();
                dgvData.ClearSelection();

               


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string pass = txtPassword.Text;
            try
            {

                var con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.ConnectionPath;
                con.Open();

                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"update UserInfo set Password = '{pass}' where FullName = '{ApplicationHelper.FullName}'";
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                dgvData.AutoGenerateColumns = false;
                dgvData.DataSource = dt;
                dgvData.Refresh();
                dgvData.ClearSelection();

                LoadData();
                NewData();




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dgvData.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtFullName.Text = dgvData.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtUserName.Text = dgvData.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPassword.Text = dgvData.Rows[e.RowIndex].Cells[3].Value.ToString();

        }

        private void MyInfo_Load(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void newBtn_Click(object sender, EventArgs e)
        {
            NewData();
        }

        private void refBtn_Click(object sender, EventArgs e)
        {
            LoadData();
            NewData();
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            optionForm.Show();
            this.Hide();
        }

        private void btnlgt_Click(object sender, EventArgs e)
        {
            loginForm.Show();
            this.Hide();
        }
    }
}
