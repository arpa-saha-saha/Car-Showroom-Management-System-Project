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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Registration : Form
    {
        private LogP loginForm;
        public Registration(LogP login)
        {
            InitializeComponent();
            this.loginForm = login;
        }



        public Registration()
        {
            InitializeComponent();
        }

        private void signIn_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void gender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void signUp_Click(object sender, EventArgs e)
        {
            string fname = txtFName.Text;
            string uname = txtUName.Text;
            string pass = txtPassword.Text;
            
            string userType = "";

            if (string.IsNullOrWhiteSpace(fname))
            {
                MessageBox.Show("Please enter your Fullname.");
                return;
            }
            if (string.IsNullOrWhiteSpace(uname))
            {
                MessageBox.Show("Please enter a Username.");
                return;
            }
            if (string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Please enter a Password.");
                return;
            }

            if (rbStaff.Checked == true)
            {
                userType = "Staff";
            }
            else if (rbCustomer.Checked == true)
            {
                userType = "Customer";
            }
            if (string.IsNullOrWhiteSpace(userType))
            {
                MessageBox.Show("Please select a usertype.");
                return;
            }
            try
            {
                
                var con = new SqlConnection();
                con.ConnectionString =ApplicationHelper.ConnectionPath;
                con.Open();

                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"insert into UserInfo values ('{fname}','{uname}','{pass}','{userType}', 'False')";
                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Registration Successful.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void nid_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void signIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void eyePicBox_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
        }

        private void eyePicBox_MouseLeave(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }

        private void eyePicBox_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void back1_Click(object sender, EventArgs e)
        {
            this.Hide();
            loginForm.Show();
        }
    }
}
