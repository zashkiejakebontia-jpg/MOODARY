using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Moodary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.BackColor = Color.White;
            button1.ForeColor = Color.Black;
            button1.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;

            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.BackColor = Color.Transparent;
            button2.ForeColor = Color.Black;
            button2.Font = new Font("Segoe UI", 10);
            button2.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button2.FlatAppearance.MouseDownBackColor = Color.Transparent;

            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
            button3.BackColor = Color.Transparent;
            button3.ForeColor = Color.Black;
            button3.Font = new Font("Segoe UI", 10);
            button3.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button3.FlatAppearance.MouseDownBackColor = Color.Transparent;

            button4.FlatStyle = FlatStyle.Flat;
            button4.FlatAppearance.BorderSize = 0;
            button4.BackColor = Color.White;
            button4.ForeColor = Color.Black;
            button4.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            button4.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button4.FlatAppearance.MouseDownBackColor = Color.Transparent;

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

            Shown += (s, e) =>
            {
                MakeButtonRounded(button1, 20);
                MakeButtonRounded(button4, 20);
                MakeTextBoxRounded(textBox1, 20);
                MakeTextBoxRounded(textBox2, 20);

                button1.Paint += (s2, e2) => DrawRoundedBorder(button1, e2, 20);
                button4.Paint += (s2, e2) => DrawRoundedBorder(button4, e2, 20);
                textBox1.Paint += (s2, e2) => DrawRoundedBorder(textBox1, e2, 20);
                textBox2.Paint += (s2, e2) => DrawRoundedBorder(textBox2, e2, 20);
            };

            BackColor = Color.FromArgb(255, 220, 100);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter your username and password.");
                return;
            }

            try
            {
                UserAccount user = UserRepository.GetUserByUsername(username);
                if (user == null || !PasswordHasher.VerifyPassword(password, user.PasswordHash))
                {
                    MessageBox.Show("Invalid login.");
                    return;
                }

                SharedData.SetCurrentUser(user);
                SharedData.ReplaceJournalEntries(JournalRepository.GetEntriesForUser(user.Id));

                Form3 main = new Form3();
                main.Show();
                main.BringToFront();
                main.Activate();
                Hide();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login failed: " + ex.Message);
            }
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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            Hide();
        }
    }
}
