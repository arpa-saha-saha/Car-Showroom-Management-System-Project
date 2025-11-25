using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace CarShowroomManagement
{
    public partial class Option : Form
    {
        private LogP loginForm;

        public Option(LogP login)
        {
            InitializeComponent();
            loginForm = login;
        }

        public Option()
        {
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            loginForm.Show();
        }





        private void button1_Click(object sender, EventArgs e)
        {
            CarCatalog catalog = new CarCatalog(this, loginForm);
            catalog.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
            loginForm.Show();


        }

        private void btnOffers_Click(object sender, EventArgs e)
        {
            Offers op = new Offers();
            op.Show();
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {

        }

        private void Option_Load(object sender, EventArgs e)
        {
            lblWelcomeMessage.Text = $"Welcome , {ApplicationHelper.FullName}[{ApplicationHelper.UserType}]";
            if(ApplicationHelper.UserType == "Staff")
            {
                btnStaffInfo.Enabled = false;
                btnAddCar.Enabled = false;
                btnAllUsers.Enabled = false;
                btnBrand.Enabled = false;
                btnCarType.Enabled = false;
                
                btnDealer.Enabled = false;
               


            }
        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            AddCar adc = new AddCar(this,loginForm);
            adc.Show();
            this.Hide();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            AllUser au = new AllUser();
            au.Show();
            
        }

        private void btnStaffInfo_Click(object sender, EventArgs e)
        {
            staffInfo si = new staffInfo(this,loginForm);
            si.Show();
            this.Hide();
        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
            Brand brand = new Brand(this,loginForm);
            brand.Show();
            this.Hide();
        }

        private void btnCarType_Click(object sender, EventArgs e)
        {
            CarType ct = new CarType(this,loginForm);
            ct.Show();
            this.Hide();
        }

        private void btnDealer_Click(object sender, EventArgs e)
        {
            CarDealer ct = new CarDealer(this, loginForm);
            ct.Show();
            this.Hide();
        }

        private void btnCarBooking_Click(object sender, EventArgs e)
        {
            CarBooking cb = new CarBooking(this, loginForm);
            cb.Show();
            
        }

        private void btnCarSell_Click(object sender, EventArgs e)
        {
            CarSale cs = new CarSale(this,loginForm);
            cs.Show();
            this.Hide();
        }

        private void btnCustomerInfo_Click(object sender, EventArgs e)
        {
            CustomerInfo ci = new CustomerInfo(this,loginForm);
            ci.Show();
            this.Hide();
        }
    }
}
