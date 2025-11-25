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
    public partial class CustomerInfo : Form
    {
        private LogP loginForm;
        private Option optionForm;
        public CustomerInfo()
        {
            InitializeComponent();
        }

        public CustomerInfo(Option option,LogP login)
        {
            InitializeComponent();
            this.optionForm = option;
            this.loginForm = login;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.LoadData();
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
                cmd.CommandText = "Select * from CustomerInfo";
                DataTable dt = new DataTable();
                var adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                con.Close();
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
            this.NewData();

        }
        private void NewData()
        {
            txtID.Text = "Auto generated";
            txtFullName.Text = "";
            rbMale.Checked = false;

            rbFemale.Checked = false;
            txtPhone.Text = "";
            txtAdd.Text = "";
            dgvData.ClearSelection();


        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            txtID.Text = dgvData.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtFullName.Text = dgvData.Rows[e.RowIndex].Cells[1].Value.ToString();
            string gender = dgvData.Rows[e.RowIndex].Cells[2].Value.ToString();
            if (gender == "Male")
            {
                rbMale.Checked = true;
                rbFemale.Checked = false;
            }
            else if (gender == "Female")
            {
                rbMale.Checked = false;
                rbFemale.Checked = true;

            }
            else
            {
                rbMale.Checked = false;
                rbFemale.Checked = false;
            }
            txtPhone.Text = Convert.ToString(dgvData.Rows[e.RowIndex].Cells[3].Value);
            txtAdd.Text = dgvData.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.NewData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string name = txtFullName.Text;
            if (id == "Auto generated")
            {
                MessageBox.Show("Please select a row first");
                return;
            }
            var result = MessageBox.Show("Are you Sure?", "Confirmation", MessageBoxButtons.YesNo);
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
                    cmd.CommandText = $"Delete from CustomerInfo where id={id}";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfull");
                    this.LoadData();
                    this.NewData();
                    con.Close();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string name = txtFullName.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter  Fullname.");
                return;
            }

            string gender = "";
            if (rbMale.Checked)
                gender = "Male";
            else if (rbFemale.Checked)
                gender = "Female";
            else
            {
                MessageBox.Show("Please select a gender.");
                return;
            }

            string phone = txtPhone.Text;
            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Please enter Phone Number.");
                return;
            }
            string Add = txtAdd.Text;
            if (string.IsNullOrWhiteSpace(Add))
            {
                MessageBox.Show("Please enter Address.");
                return;
            }
            string query = "";
            if (id == "Auto generated")
            {
                query = $"insert into CustomerInfo values ('{name}','{gender}','{phone}','{Add}')";

            }
            else
            {
                query = $"update CustomerInfo set FullName = '{name}', Gender='{gender}',PhoneNumber = '{phone}',Address='{Add}' where ID='{id}'";
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
                MessageBox.Show("Successful");
                this.LoadData();
                this.NewData();
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

