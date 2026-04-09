using MySql.Data.MySqlClient;
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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();

            textBox1.BorderStyle = BorderStyle.None;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.Font = new Font("Comic Sans MS", 15);

            textBox2.BorderStyle = BorderStyle.None;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox2.TextAlign = HorizontalAlignment.Center;
            textBox2.Font = new Font("Comic Sans MS", 15);
            textBox2.PasswordChar = '•';

            textBox3.BorderStyle = BorderStyle.None;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox3.TextAlign = HorizontalAlignment.Center;
            textBox3.Font = new Font("Comic Sans MS", 15);
            textBox3.PasswordChar = '•';

            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.BackColor = Color.White;
            button1.ForeColor = Color.Black;
            button1.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.BackColor = Color.White;
            button2.ForeColor = Color.Black;
            button2.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            this.Shown += (s, e) =>
            {
                MakeButtonRounded(button1, 20);
                MakeButtonRounded(button2, 20);
                MakeTextBoxRounded(textBox1, 20);
                MakeTextBoxRounded(textBox2, 20);
                MakeTextBoxRounded(textBox3, 20);

                button1.Paint += (s2, e2) => DrawRoundedBorder(button1, e2, 20);
                button2.Paint += (s2, e2) => DrawRoundedBorder(button1, e2, 20);
                textBox1.Paint += (s2, e2) => DrawRoundedBorder(textBox1, e2, 20);
                textBox2.Paint += (s2, e2) => DrawRoundedBorder(textBox2, e2, 20);
                textBox3.Paint += (s2, e2) => DrawRoundedBorder(textBox3, e2, 20);
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

        private void MakeTextBoxRounded(Control control, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = control.ClientRectangle;

            int d = radius * 2;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            control.Region = new Region(path);
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

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

            private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (username == "" || password == "")
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            try
            {
                using (var conn = DB.GetConnection())
                {
                    string query = "INSERT INTO Users (Username, Password) VALUES (@user, @pass)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", password);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Account created!");

                    Form1 login = new Form1();
                    login.Show();
                    this.Hide();
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                    MessageBox.Show("Username already exists!");
                else
                    MessageBox.Show(ex.Message);
            }
        }
    }
    
}
