using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Moodary
{
    public partial class UserControl2 : UserControl
    {
        private Image profileImage;

        public UserControl2()
        {
            InitializeComponent();

            StyleActionRow(button2);
            StyleActionRow(button3);
            StyleActionRow(button4);
            StyleBackButton(button1);
            StyleBackButton(button5);

            panel2.Cursor = Cursors.Hand;
            panel2.Click += panel2_Click;
            Load += UserControl2_Load;
            Resize += UserControl2_Resize;
            BackColor = Color.FromArgb(255, 220, 100);
        }

        private void UserControl2_Load(object sender, EventArgs e)
        {
            label1.Text = string.IsNullOrWhiteSpace(SharedData.CurrentUsername) ? "Moodary User" : SharedData.CurrentUsername;
            button2.Text = "Change Password";
            button3.Text = "Change Username";
            button4.Text = "Delete Account";
            label6.Text = "\U0001F6E1";
            label7.Text = "\U0001F464";
            label8.Text = "\U0001F5D1";
            label3.Text = "\u203A";
            label4.Text = "\u203A";
            label5.Text = "\u203A";
            label6.Left = 22;
            label7.Left = 22;
            label8.Left = 22;
            label6.Font = new Font("Segoe UI Emoji", 20F, FontStyle.Regular);
            label7.Font = new Font("Segoe UI Emoji", 20F, FontStyle.Regular);
            label8.Font = new Font("Segoe UI Emoji", 20F, FontStyle.Regular);
            label3.Font = new Font("Segoe UI", 23F, FontStyle.Regular);
            label4.Font = new Font("Segoe UI", 23F, FontStyle.Regular);
            label5.Font = new Font("Segoe UI", 23F, FontStyle.Regular);
            label6.BringToFront();
            label7.BringToFront();
            label8.BringToFront();
            label3.BringToFront();
            label4.BringToFront();
            label5.BringToFront();
            panel4.BringToFront();
            panel5.BringToFront();
            ApplyRoundedCorners(panel1, 36);
            LoadProfileImage();
            panel1.Invalidate();
            panel2.Invalidate();
            panel3.Invalidate();
        }

        private void UserControl2_Resize(object sender, EventArgs e)
        {
            ApplyRoundedCorners(panel1, 36);
            panel1.Invalidate();
            panel2.Invalidate();
            panel3.Invalidate();
        }

        private void StyleActionRow(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = Color.Transparent;
            button.ForeColor = Color.Black;
            button.Font = new Font("Segoe UI", 15, FontStyle.Regular);
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Padding = new Padding(84, 0, 0, 0);
            button.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button.UseVisualStyleBackColor = false;
        }

        private void StyleBackButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = Color.Transparent;
            button.ForeColor = Color.Black;
            button.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            button.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button.UseVisualStyleBackColor = false;
            button.Paint += BackButton_Paint;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            DrawRoundedBorder(panel1, e, 36, 5);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(2, 2, panel2.Width - 5, panel2.Height - 5);

            using (SolidBrush fillBrush = new SolidBrush(Color.FromArgb(252, 246, 236)))
            using (Pen borderPen = new Pen(Color.Black, 3))
            {
                e.Graphics.FillEllipse(fillBrush, rect);

                if (profileImage != null)
                {
                    using (GraphicsPath clipPath = new GraphicsPath())
                    {
                        clipPath.AddEllipse(rect);
                        Region previousClip = e.Graphics.Clip;
                        e.Graphics.SetClip(clipPath);
                        e.Graphics.DrawImage(profileImage, rect);
                        e.Graphics.Clip = previousClip;
                    }
                }

                e.Graphics.DrawEllipse(borderPen, rect);
            }
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Choose Profile Picture";
                dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    using (Image selectedImage = Image.FromFile(dialog.FileName))
                    {
                        string profilePath = GetProfileImagePath();
                        Directory.CreateDirectory(Path.GetDirectoryName(profilePath));
                        selectedImage.Save(profilePath, ImageFormat.Png);
                    }

                    LoadProfileImage();
                    panel2.Invalidate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to update profile picture: " + ex.Message);
                }
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, panel3.Width - 1, panel3.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 18))
            using (Pen pen = new Pen(Color.FromArgb(225, 225, 225), 2))
            {
                panel3.Region = new Region(path);
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void BackButton_Paint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
            {
                return;
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, button.Width - 1, button.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 16))
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

        private void ApplyRoundedCorners(Control control, int radius)
        {
            if (control.Width <= 0 || control.Height <= 0)
            {
                return;
            }

            using (GraphicsPath path = GetRoundedPath(new Rectangle(0, 0, control.Width, control.Height), radius))
            {
                control.Region = new Region(path);
            }
        }

        private void DrawRoundedBorder(Control control, PaintEventArgs e, int radius, int thickness)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, control.Width - 1, control.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, radius))
            using (Pen pen = new Pen(Color.Black, thickness))
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void LoadProfileImage()
        {
            if (profileImage != null)
            {
                profileImage.Dispose();
                profileImage = null;
            }

            string profilePath = GetProfileImagePath();
            if (!File.Exists(profilePath))
            {
                return;
            }

            using (FileStream stream = new FileStream(profilePath, FileMode.Open, FileAccess.Read))
            using (Image loaded = Image.FromStream(stream))
            {
                profileImage = new Bitmap(loaded);
            }
        }

        private string GetProfileImagePath()
        {
            string folder = Path.Combine(Application.StartupPath, "ProfilePictures");
            return Path.Combine(folder, "user_" + SharedData.CurrentUserId + ".png");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SharedData.ClearSession();

            Form5 form5 = new Form5();
            form5.Show();

            Form parentForm = FindForm();
            if (parentForm != null)
            {
                parentForm.Hide();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Control parent = Parent;
            if (parent != null)
            {
                parent.Controls.Remove(this);
            }

            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowChangePasswordDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowChangeUsernameDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowDeleteAccountDialog();
        }

        private void ShowChangePasswordDialog()
        {
            using (AccountActionDialog dialog = new AccountActionDialog(
                "Change Password",
                "Current Password",
                "New Password",
                "Confirm Password",
                true))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                string currentPassword = dialog.Value1;
                string newPassword = dialog.Value2;
                string confirmPassword = dialog.Value3;

                if (string.IsNullOrWhiteSpace(currentPassword) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
                {
                    MessageBox.Show("Please fill all fields.");
                    return;
                }

                if (newPassword != confirmPassword)
                {
                    MessageBox.Show("New passwords do not match.");
                    return;
                }

                try
                {
                    UserAccount user = UserRepository.GetUserByUsername(SharedData.CurrentUsername);
                    if (user == null || !PasswordHasher.VerifyPassword(currentPassword, user.PasswordHash))
                    {
                        MessageBox.Show("Current password is incorrect.");
                        return;
                    }

                    UserRepository.UpdatePassword(user.Id, PasswordHasher.HashPassword(newPassword));
                    MessageBox.Show("Password changed successfully.");
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }
            }
        }

        private void ShowChangeUsernameDialog()
        {
            using (AccountActionDialog dialog = new AccountActionDialog(
                "Change Username",
                "Current Password",
                "New Username",
                null,
                false))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                string currentPassword = dialog.Value1;
                string newUsername = dialog.Value2;

                if (string.IsNullOrWhiteSpace(currentPassword) || string.IsNullOrWhiteSpace(newUsername))
                {
                    MessageBox.Show("Please fill all fields.");
                    return;
                }

                try
                {
                    UserAccount user = UserRepository.GetUserByUsername(SharedData.CurrentUsername);
                    if (user == null || !PasswordHasher.VerifyPassword(currentPassword, user.PasswordHash))
                    {
                        MessageBox.Show("Current password is incorrect.");
                        return;
                    }

                    UserRepository.UpdateUsername(user.Id, newUsername.Trim());
                    SharedData.CurrentUsername = newUsername.Trim();
                    label1.Text = SharedData.CurrentUsername;
                    MessageBox.Show("Username changed successfully.");
                }
                catch (MySqlException ex)
                {
                    if (ex.Number == 1062)
                    {
                        MessageBox.Show("That username already exists.");
                    }
                    else
                    {
                        MessageBox.Show("Database error: " + ex.Message);
                    }
                }
            }
        }

        private void ShowDeleteAccountDialog()
        {
            using (AccountActionDialog dialog = new AccountActionDialog(
                "Delete Account",
                "Current Password",
                "Type DELETE",
                null,
                false))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                string currentPassword = dialog.Value1;
                string confirmationText = dialog.Value2;

                if (string.IsNullOrWhiteSpace(currentPassword) || string.IsNullOrWhiteSpace(confirmationText))
                {
                    MessageBox.Show("Please fill all fields.");
                    return;
                }

                if (!string.Equals(confirmationText.Trim(), "DELETE", StringComparison.Ordinal))
                {
                    MessageBox.Show("Please type DELETE to confirm.");
                    return;
                }

                try
                {
                    UserAccount user = UserRepository.GetUserByUsername(SharedData.CurrentUsername);
                    if (user == null || !PasswordHasher.VerifyPassword(currentPassword, user.PasswordHash))
                    {
                        MessageBox.Show("Current password is incorrect.");
                        return;
                    }

                    UserRepository.DeleteUser(user.Id);
                    SharedData.ClearSession();
                    MessageBox.Show("Account deleted.");

                    Form5 form5 = new Form5();
                    form5.Show();

                    Form parentForm = FindForm();
                    if (parentForm != null)
                    {
                        parentForm.Hide();
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }
            }
        }

        private class AccountActionDialog : Form
        {
            private readonly TextBox textBox1;
            private readonly TextBox textBox2;
            private readonly TextBox textBox3;
            private readonly Label label1;
            private readonly Label label2;
            private readonly Label label3;
            private readonly Button saveButton;
            private readonly Button cancelButton;

            public string Value1 => textBox1.Text;
            public string Value2 => textBox2.Text;
            public string Value3 => textBox3.Text;

            public AccountActionDialog(string title, string labelText1, string labelText2, string labelText3, bool hideValues)
            {
                Text = title;
                StartPosition = FormStartPosition.CenterParent;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false;
                MinimizeBox = false;
                ClientSize = new Size(540, string.IsNullOrWhiteSpace(labelText3) ? 320 : 390);
                BackgroundImage = Properties.Resources._1b04a7d7_549a_454d_aef6_e3bfc47cdde2;

                label1 = CreateLabel(labelText1, 28);
                textBox1 = CreateTextBox(60, hideValues);
                label2 = CreateLabel(labelText2, 124);
                textBox2 = CreateTextBox(156, hideValues && title == "Change Password");

                Controls.Add(label1);
                Controls.Add(textBox1);
                Controls.Add(label2);
                Controls.Add(textBox2);

                if (!string.IsNullOrWhiteSpace(labelText3))
                {
                    label3 = CreateLabel(labelText3, 220);
                    textBox3 = CreateTextBox(252, hideValues);
                    Controls.Add(label3);
                    Controls.Add(textBox3);
                }

                saveButton = CreateDialogButton("Save", 160);
                cancelButton = CreateDialogButton("Cancel", 300);
                saveButton.Click += (s, e) => DialogResult = DialogResult.OK;
                cancelButton.Click += (s, e) => DialogResult = DialogResult.Cancel;
                Controls.Add(saveButton);
                Controls.Add(cancelButton);
                Shown += AccountActionDialog_Shown;
            }

            private void AccountActionDialog_Shown(object sender, EventArgs e)
            {
                StyleDialogTextBox(textBox1);
                StyleDialogTextBox(textBox2);
                if (textBox3 != null)
                {
                    StyleDialogTextBox(textBox3);
                }

                StyleDialogButton(saveButton);
                StyleDialogButton(cancelButton);
            }

            private Label CreateLabel(string text, int top)
            {
                return new Label
                {
                    Text = text,
                    Left = 30,
                    Top = top,
                    Width = 220,
                    AutoSize = false,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    BackColor = Color.Transparent
                };
            }

            private TextBox CreateTextBox(int top, bool passwordMode)
            {
                return new TextBox
                {
                    Left = 28,
                    Top = top,
                    Width = 470,
                    Height = 38,
                    BorderStyle = BorderStyle.None,
                    Multiline = true,
                    Font = new Font("Comic Sans MS", 14),
                    PasswordChar = passwordMode ? '•' : '\0'
                };
            }

            private Button CreateDialogButton(string text, int left)
            {
                return new Button
                {
                    Text = text,
                    Left = left,
                    Top = ClientSize.Height - 58,
                    Width = 110,
                    Height = 36,
                    Cursor = Cursors.Hand
                };
            }

            private void StyleDialogTextBox(TextBox textBox)
            {
                textBox.BackColor = Color.White;
                textBox.ForeColor = Color.Black;
                textBox.TextAlign = HorizontalAlignment.Center;
                textBox.Paint += (s, e) => DrawRoundedTextBox(textBox, e, 18);
            }

            private void StyleDialogButton(Button button)
            {
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.BackColor = Color.Transparent;
                button.ForeColor = Color.Black;
                button.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                button.FlatAppearance.MouseOverBackColor = Color.Transparent;
                button.FlatAppearance.MouseDownBackColor = Color.Transparent;
                button.Paint += (s, e) =>
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    Rectangle rect = new Rectangle(0, 0, button.Width - 1, button.Height - 1);
                    using (GraphicsPath path = BuildRoundedPath(rect, 18))
                    using (Pen pen = new Pen(Color.Black, 2))
                    {
                        button.Region = new Region(path);
                        e.Graphics.DrawPath(pen, path);
                    }
                };
            }

            private void DrawRoundedTextBox(TextBox textBox, PaintEventArgs e, int radius)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, textBox.Width - 1, textBox.Height - 1);
                using (GraphicsPath path = BuildRoundedPath(rect, radius))
                using (Pen pen = new Pen(Color.Black, 2))
                {
                    textBox.Region = new Region(path);
                    e.Graphics.DrawPath(pen, path);
                }
            }

            private GraphicsPath BuildRoundedPath(Rectangle bounds, int radius)
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
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
