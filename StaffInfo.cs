using CarShowroomManagement;
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

namespace WindowsFormsApp1
{
    public partial class staffInfo : Form
    {
        private LogP loginForm;
        private Option optionForm;

        public staffInfo( Option option, LogP login)
        {
            InitializeComponent();
           this.loginForm = login;
           this.optionForm = option;
        }

        public staffInfo()
        {
            InitializeComponent();
        }

        private void staffInfo_Load(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void newBtn_Click(object sender, EventArgs e)
        {
            this.NewData();
        }

        private void NewData()
        {
            txtID.Text = "Auto Generated";
            cmbFullName.SelectedValue = -1;
            txtSal.Text = "";
            dtpJoin.Value = DateTime.Now;
            cmbGender.SelectedItem = -1;
            cmbPos.SelectedItem = -1;

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
                cmd.CommandText = @"select StaffInfo.ID,UserInfo.FullName,StaffInfo.Salary,StaffInfo.JoiningDate, StaffInfo.Gender,StaffInfo.Position,UserInfo.ID as UiId from StaffInfo  INNER JOIN UserInfo on StaffInfo.UiId = UserInfo.ID;select ID, FullName FROM UserInfo where UserType = 'Staff';select * from UserInfo where UserType = 'Staff'";

                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);

                dgvData.AutoGenerateColumns = false;
                dgvData.DataSource = ds.Tables[0];
                dgvData.Refresh();
                dgvData.ClearSelection();

                cmbFullName.DataSource = ds.Tables[1];
                cmbFullName.DisplayMember = "FullName";
                cmbFullName.ValueMember = "ID";
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void refBtn_Click(object sender, EventArgs e)
        {
            this.LoadData();
            this.NewData();
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            

            txtID.Text = dgvData.Rows[e.RowIndex].Cells[0].Value.ToString();
            cmbFullName.SelectedValue = dgvData.Rows[e.RowIndex].Cells[6].Value;  
            txtSal.Text = dgvData.Rows[e.RowIndex].Cells[2].Value.ToString();
            dtpJoin.Value = Convert.ToDateTime(dgvData.Rows[e.RowIndex].Cells[3].Value);
            cmbGender.Text = dgvData.Rows[e.RowIndex].Cells[4].Value?.ToString();
            cmbPos.Text = dgvData.Rows[e.RowIndex].Cells[5].Value?.ToString();
        }

        private void dltBtn_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
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
                    cmd.CommandText = $"delete from StaffInfo where id = '{id}' ";
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

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            
            if (cmbFullName.SelectedValue==null)
            {
                MessageBox.Show("Error: Full Name is required.");
                return;
            }
            string fullName = cmbFullName.SelectedValue.ToString();

            
            string uiId = cmbFullName.SelectedValue.ToString();
            string sal = txtSal.Text;
            if (string.IsNullOrWhiteSpace(sal))
            {
                MessageBox.Show("Please enter  Salary.");
                return;
            }

            DateTime joiningDate = dtpJoin.Value;


            if (cmbGender.SelectedItem == null)
            {
                MessageBox.Show("Error: Gender is required.");
                return;
            }
            string gender = cmbGender.SelectedItem.ToString();

            if (cmbPos.SelectedItem == null)
            {
                MessageBox.Show("Error: Position is required.");
                return;
            }
            string position = cmbPos.SelectedItem.ToString();




            string query = "";

            if (id == "Auto Generated")
            {
                query = $"insert into StaffInfo  values ('{uiId}', '{sal}', '{joiningDate}', '{gender}', '{position}')";
            }
            else
            {
                query  = $"update StaffInfo set UiId = '{uiId}', Salary = '{sal}', JoiningDate = '{joiningDate}', Gender = '{gender}', Position = '{position}' where ID = '{id}'";

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            optionForm.Show();
        }

        private void btnlgt_Click(object sender, EventArgs e)
        {
            this.Hide();
            loginForm.Show();

        }
    }
}
