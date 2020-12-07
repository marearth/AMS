using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMS
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        Form3 thirdform = new Form3();
        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            string s1 = Convert.ToString(textBox1.Text);
            string s2 = Convert.ToString(textBox2.Text);
            if (s1 == "root" && s2 == "123mysql")
            {
                thirdform.Show();
                this.Close();
            }
            else
                MessageBox.Show("INVALID USER OR CODE!", "Warning!");
           
        }
    }
}
