using CarShowroomManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class COption : Form
    {
        
        private LogP cloginForm;
        public COption( LogP login)
        {
            InitializeComponent();
           
            this.cloginForm = login;
        }
        public COption() 
        { 
        }

        private void COption_Load(object sender, EventArgs e)
        {
            lblWelcomeMessage.Text = $"Welcome , {ApplicationHelper.FullName}[{ApplicationHelper.UserType}]";
        }

      

        

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Close();
         
            cloginForm.Show();

        }

        private void btnCarBooking_Click(object sender, EventArgs e)
        {
            CBooking cb = new CBooking(this,cloginForm);
            cb.Show();
            this.Hide();
        }

        private void btnCarCatalog_Click(object sender, EventArgs e)
        {
            CCarCustomer cc = new CCarCustomer(this, cloginForm);
            cc.Show();
            this.Hide();
        }

        private void btnMyInfo_Click(object sender, EventArgs e)
        {
            MyInfo mi = new MyInfo(this,cloginForm);
            mi.Show();
            this.Hide();
        }
    }
}
