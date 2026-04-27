using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Moodary
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            button7.FlatStyle = FlatStyle.Flat;
            button7.FlatAppearance.BorderSize = 0;
            button7.BackColor = Color.Transparent;
            button7.ForeColor = Color.Black;
            button7.Font = new Font("Segoe UI", 10);
            button7.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button7.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button7.UseVisualStyleBackColor = false;

            // BUTTON 2
            button6.FlatStyle = FlatStyle.Flat;
            button6.FlatAppearance.BorderSize = 0;
            button6.BackColor = Color.Transparent;
            button6.ForeColor = Color.Black;
            button6.Font = new Font("Segoe UI", 10);
            button6.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button6.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button6.UseVisualStyleBackColor = false;

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


        private void StyleNavTextButton(Button button, bool selected)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = Color.Transparent;
            button.ForeColor = selected ? Color.Black : Color.FromArgb(120, 112, 98);
            button.Font = new Font("Segoe UI", 15F, selected ? FontStyle.Bold : FontStyle.Regular);
            button.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button.UseVisualStyleBackColor = false;
        }

        private void StyleNavPillButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = Color.Transparent;
            button.ForeColor = Color.Black;
            button.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            button.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button.UseVisualStyleBackColor = false;
            button.Paint += PillButton_Paint;
        }

        private void PillButton_Paint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
            {
                return;
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, button.Width - 1, button.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 20))
            using (Pen pen = new Pen(Color.Black, 2))
            {
                button.Region = new Region(path);
                e.Graphics.DrawPath(pen, path);
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            path.StartFigure();
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void ProfilePanel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel == null)
                return;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, panel.Width, panel.Height);
                panel.Region = new Region(path);
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, panel.Width, panel.Height);

            if (panel.BackgroundImage != null)
            {
                e.Graphics.DrawImage(panel.BackgroundImage, rect);
            }

            using (Pen borderPen = new Pen(Color.Black, 2))
            {
                e.Graphics.DrawEllipse(borderPen, 1, 1, panel.Width - 3, panel.Height - 3);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            Close();

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
