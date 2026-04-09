using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Moodary
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            this.Load += UserControl1_Load;
            panel1.Paint += Panel1_Paint;
            flowLayoutPanel1.Paint += flowLayoutPanel1_Paint;

            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.BackColor = Color.Transparent;
            button1.ForeColor = Color.Black;

            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.BackColor = Color.Transparent;  
            button2.ForeColor = Color.Black;
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(247, 240, 222);
            flowLayoutPanel1.BackColor = Color.FromArgb(247, 240, 222);

            ApplyRoundedCorners(panel1, 20);
            ApplyRoundedCorners(flowLayoutPanel1, 30);
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            DrawRoundedBorder(panel1, e, 20);
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            DrawRoundedBorder(flowLayoutPanel1, e, 30);
        }

        private void ApplyRoundedCorners(Control control, int radius)
        {
            if (control.Width <= 0 || control.Height <= 0)
                return;

            Rectangle bounds = new Rectangle(0, 0, control.Width, control.Height);
            using (GraphicsPath path = GetRoundedPath(bounds, radius))
            {
                control.Region = new Region(path);
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

        private void DrawRoundedBorder(Control control, PaintEventArgs e, int radius)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, control.Width - 1, control.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, radius))
            using (Pen pen = new Pen(Color.Black, 2))
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            ApplyRoundedCorners(panel1, 20);
            ApplyRoundedCorners(flowLayoutPanel1, 30);

            panel1.Invalidate();
            flowLayoutPanel1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserControl2 userControl2 = new UserControl2();
            userControl2.Location = new Point(0, 0);

            this.Controls.Add(userControl2);
            userControl2.BringToFront();
        }
    }
}