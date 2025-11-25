using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarShowroomManagement
{
    public partial class Offers : Form
    {
        public Offers()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently No Offers Available", "Offer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently No Offers Available", "Offer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently No Offers Available", "Offer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Offers_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }

        private void Offers_Load(object sender, EventArgs e)
        {

        }
    }
}
