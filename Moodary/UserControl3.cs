using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Moodary
{
    public partial class UserControl3 : UserControl
    {
        private readonly UserControl1 journalControl;
        private JournalEntry currentEntry;
        private string selectedMood = string.Empty;
        private float fontSize = 12f;

        public UserControl3() : this(null, null)
        {
        }

        public UserControl3(UserControl1 parentControl, JournalEntry entry)
            : this(parentControl, entry, entry?.Mood)
        {
        }

        public UserControl3(UserControl1 parentControl, JournalEntry entry, string mood)
        {
            InitializeComponent();
            journalControl = parentControl;
            currentEntry = entry;
            selectedMood = string.IsNullOrWhiteSpace(mood) ? string.Empty : mood.Trim();

            //pictureBox1.Image = Properties.Resources.cc039011_8553_4583_93d1_b7f57fe733c5_removebg_preview;
            //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            //pictureBox1.BackColor = Color.Transparent;

            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.BackColor = Color.Transparent;
            button1.ForeColor = Color.Black;
            button1.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button1.Paint += RoundedButton_Paint;

            button10.FlatStyle = FlatStyle.Flat;
            button10.FlatAppearance.BorderSize = 0;
            button10.BackColor = Color.Transparent;
            button10.ForeColor = Color.Black;
            button10.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            button10.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button10.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button10.Paint += RoundedButton_Paint;

            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.BackColor = Color.Transparent;
            button2.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
            button3.BackColor = Color.Transparent;
            button3.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            button8.FlatStyle = FlatStyle.Flat;
            button8.FlatAppearance.BorderSize = 0;
            button8.BackColor = Color.Transparent;
            button8.ForeColor = Color.Black;
            button8.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button8.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button8.Paint += RoundedButton_Paint;

            StyleMiniButton(button4);
            StyleMiniButton(button5);
            StyleMiniButton(button6);
            StyleMiniButton(button7);

            textBox2.BorderStyle = BorderStyle.None;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox2.Font = new Font("Comic Sans MS", 14);
            textBox2.TextAlign = HorizontalAlignment.Center;

            richTextBox1.BorderStyle = BorderStyle.FixedSingle;
            richTextBox1.BackColor = Color.FromArgb(252, 246, 227);
            richTextBox1.Font = new Font("Segoe UI", 12);

            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.BackColor = Color.White;
            comboBox1.Font = new Font("Segoe UI", 10);

            label1.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            label1.ForeColor = Color.Black;

            moodLabel.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            moodLabel.ForeColor = Color.Black;

            Load += UserControl3_Load;
            BackColor = Color.FromArgb(255, 220, 100);
        }

        private void StyleMiniButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = Color.Transparent;
            button.ForeColor = Color.Black;
            button.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            button.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button.Paint += RoundedButton_Paint;
        }

        private void UserControl3_Load(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count == 0)
            {
                foreach (FontFamily font in FontFamily.Families)
                {
                    comboBox1.Items.Add(font.Name);
                }
            }

            if (currentEntry != null)
            {
                selectedMood = currentEntry.Mood;
                textBox2.Text = currentEntry.Title;
                richTextBox1.Text = currentEntry.PlainTextContent;
                if (!string.IsNullOrWhiteSpace(currentEntry.RichTextContent) && currentEntry.RichTextContent.Contains("\\"))
                {
                    try
                    {
                        richTextBox1.Rtf = currentEntry.RichTextContent;
                    }
                    catch
                    {
                        richTextBox1.Text = currentEntry.PlainTextContent;
                    }
                }
            }

            UpdateMoodDisplay();

            fontSize = richTextBox1.Font.Size;
            textBox1.ReadOnly = true;
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.Text = fontSize.ToString("0");

            if (comboBox1.SelectedItem == null)
            {
                comboBox1.SelectedItem = richTextBox1.Font.FontFamily.Name;
            }
        }

        private void UpdateMoodDisplay()
        {
            moodLabel.Text = string.IsNullOrWhiteSpace(selectedMood)
                ? "Mood: Not selected"
                : "Mood: " + selectedMood;
        }

        private void RoundedButton_Paint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
            {
                return;
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, button.Width - 1, button.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, 18))
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (SharedData.CurrentUserId == 0)
            {
                MessageBox.Show("Please log in again.");
                return;
            }

            string title = textBox2.Text.Trim();
            string content = richTextBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show("Please write something before saving.");
                return;
            }

            JournalEntry entryToSave = currentEntry ?? new JournalEntry();
            entryToSave.UserId = SharedData.CurrentUserId;
            entryToSave.Title = title;
            entryToSave.PlainTextContent = richTextBox1.Text;
            entryToSave.RichTextContent = richTextBox1.Rtf;
            entryToSave.Mood = selectedMood;
            entryToSave.LastUpdated = DateTime.Now;

            currentEntry = JournalRepository.SaveEntry(entryToSave);
            MessageBox.Show("Journal entry saved.");

            if (journalControl != null)
            {
                journalControl.CloseEditor(this);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (currentEntry == null || currentEntry.Id == 0)
            {
                MessageBox.Show("There is no saved journal entry to delete yet.");
                return;
            }

            DialogResult result = MessageBox.Show("Delete this journal entry?", "Delete Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
            {
                return;
            }

            JournalRepository.DeleteEntry(currentEntry.Id, SharedData.CurrentUserId);
            MessageBox.Show("Journal entry deleted.");

            if (journalControl != null)
            {
                journalControl.CloseEditor(this);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (journalControl != null)
            {
                journalControl.CloseEditor(this);
            }
            else
            {
                Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fontSize++;
            richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, fontSize, richTextBox1.Font.Style);
            textBox1.Text = fontSize.ToString("0");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (fontSize > 6)
            {
                fontSize--;
                richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, fontSize, richTextBox1.Font.Style);
                textBox1.Text = fontSize.ToString("0");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                richTextBox1.Font = new Font(comboBox1.SelectedItem.ToString(), fontSize, richTextBox1.Font.Style);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ApplyFontStyle(FontStyle.Bold);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ApplyFontStyle(FontStyle.Italic);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ApplyFontStyle(FontStyle.Regular);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ApplyFontStyle(FontStyle.Bold | FontStyle.Italic);
        }

        private void ApplyFontStyle(FontStyle style)
        {
            Font baseFont = richTextBox1.SelectionFont ?? richTextBox1.Font;
            richTextBox1.SelectionFont = new Font(baseFont, style);
            richTextBox1.Focus();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        //private void pictureBox1_Click(object sender, EventArgs e)
        //{

        //}
    }
}
