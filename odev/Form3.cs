using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace odev
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            Form4 f4 = new Form4();
            if (radioButton1.Checked == true)
            {
                f1.Show();
                this.Hide();
            }
            else if (radioButton2.Checked == true)
            {
                f4.Show();
                this.Hide();
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
