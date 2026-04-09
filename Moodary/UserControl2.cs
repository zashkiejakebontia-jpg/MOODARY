using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moodary
{
    public partial class UserControl2 : UserControl
    {
        public UserControl2()
        {
            InitializeComponent();

            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.BackColor = Color.White;
            button1.ForeColor = Color.Black;
            button1.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button1.UseVisualStyleBackColor = false;

            this.Load += (s, e) =>
            {
                MakeButtonRounded(button1, 20);
                button1.Paint += (s2, e2) => DrawRoundedBorder(button1, e2, 20);

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
            Form5 form5 = new Form5();
            form5.Show();

            Form parentForm = this.FindForm();
            if (parentForm != null)
            {
                parentForm.Hide();
            }
        }
    }
}
