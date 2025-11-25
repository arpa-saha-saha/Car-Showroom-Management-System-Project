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
    public partial class AllUser : Form
    {
        public AllUser()
        {
            InitializeComponent();
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
                cmd.CommandText = "Select ID,FullName,UserName,UserType from UserInfo";

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                dgvAllUser.AutoGenerateColumns = false;
                dgvAllUser.DataSource = dt;
                dgvAllUser.Refresh();
                dgvAllUser.ClearSelection();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();

        }

        private void AllUser_Load(object sender, EventArgs e)
        {
            try
            {
                var con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.ConnectionPath;
                con.Open();

                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select ID,FullName,UserName,UserType from UserInfo";

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                dgvAllUser.AutoGenerateColumns = false;
                dgvAllUser.DataSource = dt;
                dgvAllUser.Refresh();
                
                dgvAllUser.ClearSelection();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void staff()
        {
            try
            {
                var con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.ConnectionPath;
                con.Open();

                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select ID,FullName,UserName,UserType,isApproval from UserInfo where UserType = 'Staff'";

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                dgvAllUser.AutoGenerateColumns = false;
                dgvAllUser.DataSource = dt;
                dgvAllUser.Refresh();
                dgvAllUser.ClearSelection();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            staff();
        }

        private void dgvAllUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (dgvAllUser.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            string userId = dgvAllUser.SelectedRows[0].Cells[0].Value.ToString();
            try
            {
                var con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.ConnectionPath;
                con.Open();

                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"update UserInfo set IsApproval='True' where id ={userId}";

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                dgvAllUser.AutoGenerateColumns = false;
                dgvAllUser.DataSource = dt;
                dgvAllUser.Refresh();
                staff();

                dgvAllUser.ClearSelection();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAllUser.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var result = MessageBox.Show("Are you sure?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No)
                return;

            if (result == DialogResult.Yes)
            {
                string userId = dgvAllUser.SelectedRows[0].Cells[0].Value.ToString();
                try
                {
                    var con = new SqlConnection();
                    con.ConnectionString = ApplicationHelper.ConnectionPath;
                    con.Open();

                    var cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = $"delete from UserInfo where id = '{userId}' ";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Delete Done");

                    this.LoadData();



                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (dgvAllUser.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string userId = dgvAllUser.SelectedRows[0].Cells[0].Value.ToString();
            try
            {
                var con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.ConnectionPath;
                con.Open();

                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"update UserInfo set IsApproval='False' where id ={userId}";

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                dgvAllUser.AutoGenerateColumns = false;
                dgvAllUser.DataSource = dt;
                dgvAllUser.Refresh();
                staff() ;

                dgvAllUser.ClearSelection();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void dgvAllUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
