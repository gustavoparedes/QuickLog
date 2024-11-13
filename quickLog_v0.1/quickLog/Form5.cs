using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quickLog
{
    public partial class About_Form : Form
    {
        public About_Form()
        {
            InitializeComponent();
            string message = "This program is Opensource and I wrote it for the computer forensics classes with free tools given at " +
                "Interne Solutions S.A.S. in Bogota, Colombia." +
                "Bugs or comments please email me at gustavo.paredes@internet-solutions.com.co. Thank you for using Quick Log.";

            textBox1.Text = message;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void About_Form_Load(object sender, EventArgs e)
        {

        }
    }
}
