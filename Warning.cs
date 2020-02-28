using System;
using System.Windows.Forms;

namespace Coach
{
    public partial class Warning : Form
    {
      
        public Warning()
        {
            InitializeComponent();

        }


        private void button1_Click(object sender, EventArgs e)
        {

            Application.OpenForms["Form1"].WindowState = FormWindowState.Normal;

            this.Close();
        }
    }
}
