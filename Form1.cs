using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace CarShowroomManagement
{
    public partial class LogP : Form
    {
        public LogP()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = false;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = true;
            timer1.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string un = txtUN.Text;
            string pass = txtPass.Text;
            

            try 
            {
                var con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.ConnectionPath;
                con.Open();

                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"Select * from UserInfo where UserName ='{un}' and Password = '{pass}'";

                DataTable dt = new DataTable();
                SqlDataAdapter adp= new SqlDataAdapter(cmd);
                adp.Fill(dt);

                if (dt.Rows.Count != 1)
                {
                    MessageBox.Show("Invalid User-Name or Password","Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string fullName = dt.Rows[0]["FullName"].ToString();
                string userType = dt.Rows[0]["UserType"].ToString();
                bool isApproval = Convert.ToBoolean(dt.Rows[0]["isApproval"]);

                ApplicationHelper.FullName = fullName; 
                ApplicationHelper.UserType = userType;

                


                txtUN.Clear();
                txtPass.Clear();

                if (userType == "Admin")
                {
                    MessageBox.Show("Welcome " + fullName, "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Option op = new Option(this);
                    op.Show(this);
                    this.Hide();
                }
                else if (userType == "Customer")
                {
                    MessageBox.Show("Welcome " + fullName, "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    COption co = new COption(this);
                    co.Show(this);
                    this.Hide();
                }
                else
                {
                    if (userType == "Staff" && isApproval == true)
                    {
                        MessageBox.Show("Welcome " + fullName, "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Option op = new Option(this);
                        op.Show(this);
                        this.Hide();

                    }
                    else
                    {
                        MessageBox.Show("Your account is not approved yet.You are logged into Customer Page.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        

                        COption co = new COption(this);
                        co.Show(this);
                        this.Hide();

                    }

                    con.Close();
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.Blue;
        }

        private void btnSignUp_MouseLeave(object sender, EventArgs e)
        {
            Button btn = ( Button )sender;
            btn.BackColor = Color.Black;
        }

        private void LogP_Load(object sender, EventArgs e)
        {

        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            Registration rg = new Registration(this);
            rg.Show(this);
            this.Hide();
        }
    }
}
