using System;
using System.Drawing;
using System.Windows.Forms;

namespace Moodary
{
    public partial class Form3 : Form
    {
        private UserControl1 homeControl;

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
            ShowHomeControl();
            button1.Visible = false;
        }

        private void ShowHomeControl()
        {
            if (homeControl == null || homeControl.IsDisposed)
            {
                homeControl = new UserControl1();
                homeControl.Location = new Point(0, 0);
                Controls.Add(homeControl);
            }

            homeControl.BringToFront();
            button1.BringToFront();
        }
    }
}
