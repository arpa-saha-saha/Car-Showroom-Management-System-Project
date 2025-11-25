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
    public partial class CarType : Form
    {
        private Option optionForm;
        private LogP loginForm;

        public CarType()
        {
            InitializeComponent();
        }

        public CarType(Option option, LogP login)
        {
            InitializeComponent();
            this.optionForm = option;
            this.loginForm = login;
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

        private void CarType_Load(object sender, EventArgs e)
        {
            this.LoadData();
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
                cmd.CommandText = "select * from CarType";

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                dgvType.AutoGenerateColumns = false;
                dgvType.DataSource = dt;
                dgvType.Refresh();
                dgvType.ClearSelection();

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
            this.NewData();

        }

        private void dgvType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) 
                return;
            
            txtId.Text = dgvType.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtType.Text = dgvType.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewData();
        }
        
        private void NewData()
        {
            txtId.Text = "Auto Generated";
            txtType.Text = "";

            dgvType.ClearSelection();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            if (id == "Auto Generated") 
            {
                MessageBox.Show("Please select a row first", "Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Are you sure?","Information",MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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
                    cmd.CommandText = $"delete from CarType where id = '{id}' ";
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
            
            
            string title = txtType.Text;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            string type = txtType.Text;
            if (string.IsNullOrWhiteSpace(type))
            {
                MessageBox.Show("Please enter Type.");
                return;
            }

            string query = "";

            if(id =="Auto Generated")
            {
                query = $"insert into CarType values ('{type}')";
            }
            else
            {
                query = $"update CarType set Type='{type}' where id = {id}";
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

        private void CarType_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }
    }
}
