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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.BackColor = Color.Transparent;
            button1.ForeColor = Color.Black;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button1.UseVisualStyleBackColor = false;

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserControl1 userControl1 = new UserControl1();
            userControl1.Location = new Point(0, 0);

            this.Controls.Add(userControl1);
            userControl1.BringToFront();
        }
    }
}
