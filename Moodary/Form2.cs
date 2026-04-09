using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moodary
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.BackColor = Color.Transparent;
            button2.ForeColor = Color.Black;
            button2.Font = new Font("Segoe UI", 10);

            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.BackColor = Color.Transparent;
            button1.ForeColor = Color.Black;
            button1.Font = new Font("Segoe UI", 10);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            this.Close();
                }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
