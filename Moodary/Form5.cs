using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Moodary
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();

            // BUTTON 1
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.BackColor = Color.Transparent;
            button1.ForeColor = Color.Black;
            button1.Font = new Font("Segoe UI", 10);
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button1.UseVisualStyleBackColor = false;

            // BUTTON 2
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.BackColor = Color.Transparent;
            button2.ForeColor = Color.Black;
            button2.Font = new Font("Segoe UI", 10);
            button2.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button2.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button2.UseVisualStyleBackColor = false;

            // BUTTON 3
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
            button3.BackColor = Color.White;
            button3.ForeColor = Color.Black;
            button3.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            button3.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button3.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button3.UseVisualStyleBackColor = false;

            // BUTTON 4
            button4.FlatStyle = FlatStyle.Flat;
            button4.FlatAppearance.BorderSize = 0;
            button4.BackColor = Color.White;
            button4.ForeColor = Color.Black;
            button4.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            button4.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button4.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button4.UseVisualStyleBackColor = false;

            // PUNYAWAAAAAAAA
            this.Shown += (s, e) =>
            {
                MakeButtonRounded(button3, 20);
                MakeButtonRounded(button4, 20);


                button3.Paint += (s2, e2) => DrawRoundedBorder(button3, e2, 20);
                button4.Paint += (s2, e2) => DrawRoundedBorder(button4, e2, 20);
            };

            this.BackColor = Color.FromArgb(255, 220, 100);
        }

        private void MakeButtonRounded(Button button, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            int w = button.Width;
            int h = button.Height;

            path.AddArc(0, 0, h, h, 90, 180);
            path.AddLine(h / 2, 0, w - h / 2, 0);
            path.AddArc(w - h, 0, h, h, 270, 180);
            path.AddLine(w - h / 2, h, h / 2, h);

            path.CloseFigure();
            button.Region = new Region(path);
        }

        private void DrawRoundedBorder(Control control, PaintEventArgs e, int radius)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, control.Width - 1, control.Height - 1);
            GraphicsPath path = new GraphicsPath();

            int d = radius * 2;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            using (Pen pen = new Pen(Color.Black, 2))
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }
    }
}
