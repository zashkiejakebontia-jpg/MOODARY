using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Reflection;

namespace Moodary
{
    public partial class UserControl1 : UserControl
    {
        private string selectedMood = string.Empty;
        private Button selectedMoodButton;
        private readonly Dictionary<Button, Rectangle> defaultMoodBounds = new Dictionary<Button, Rectangle>();

        public UserControl1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            UpdateStyles();
            Load += UserControl1_Load;
            panel1.Paint += Panel1_Paint;
            ConfigureMoodButtons();

            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.BackColor = Color.Transparent;
            button1.ForeColor = Color.Black;

            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
            button3.BackColor = Color.Transparent;
            button3.ForeColor = Color.Black;
            button3.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button3.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button3.TextAlign = ContentAlignment.MiddleCenter;

            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.BackColor = Color.Transparent;
            button2.ForeColor = Color.Black;

        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(247, 240, 222);
            flowLayoutPanel1.BackColor = Color.FromArgb(250, 243, 221);
            flowLayoutPanel1.Visible = true;
            flowLayoutPanel1.BringToFront();
            EnableDoubleBuffer(panel1);
            EnableDoubleBuffer(flowLayoutPanel1);

            CaptureMoodButtonBounds();
            ApplyRoundedCorners(panel1, 20);
            ApplyRoundedCorners(flowLayoutPanel1, 34);
            ApplyMoodButtonShapes();
            RefreshJournalList();
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            DrawRoundedBorder(panel1, e, 20);
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            DrawRoundedBorder(flowLayoutPanel1, e, 34);
        }

        private void ConfigureMoodButtons()
        {
            moodButton5.Tag = "Happy";
            moodButton1.Tag = "Sad";
            moodButton2.Tag = "Fear";
            moodButton3.Tag = "Angry";
            moodButton4.Tag = "Disgusted";

            foreach (Button moodButton in GetMoodButtons())
            {
                moodButton.FlatStyle = FlatStyle.Flat;
                moodButton.FlatAppearance.BorderSize = 0;
                moodButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 251, 240);
                moodButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(245, 237, 215);
                moodButton.BackColor = Color.WhiteSmoke;
                moodButton.ForeColor = Color.Gray;
                moodButton.Text = string.Empty;
                moodButton.Cursor = Cursors.Hand;
                moodButton.BackgroundImageLayout = ImageLayout.Zoom;
                moodButton.Paint += MoodButton_Paint;
                moodButton.MouseEnter += MoodButton_MouseEnter;
                moodButton.MouseLeave += MoodButton_MouseLeave;
                moodButton.Click += MoodButton_Click;
            }
        }

        private IEnumerable<Button> GetMoodButtons()
        {
            yield return moodButton1;
            yield return moodButton2;
            yield return moodButton3;
            yield return moodButton4;
            yield return moodButton5;
        }

        private void ApplyMoodButtonShapes()
        {
            foreach (Button moodButton in GetMoodButtons())
            {
                ApplyRoundedCorners(moodButton, moodButton.Width / 2);
                moodButton.Invalidate();
            }
        }

        private void MoodButton_Paint(object sender, PaintEventArgs e)
        {
            Button moodButton = sender as Button;
            if (moodButton == null)
            {
                return;
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(1, 1, moodButton.Width - 3, moodButton.Height - 3);

            using (GraphicsPath path = new GraphicsPath())
            using (Pen pen = new Pen(Color.FromArgb(60, 60, 60), 2))
            {
                path.AddEllipse(rect);
                moodButton.Region = new Region(path);
                e.Graphics.DrawEllipse(pen, rect);
            }
        }

        private void MoodButton_Click(object sender, EventArgs e)
        {
            Button moodButton = sender as Button;
            selectedMood = moodButton?.Tag?.ToString() ?? string.Empty;
            SetSelectedMoodButton(moodButton);
        }

        private void MoodButton_MouseEnter(object sender, EventArgs e)
        {
            Button moodButton = sender as Button;
            if (moodButton == null || moodButton == selectedMoodButton)
            {
                return;
            }

            SetMoodButtonScale(moodButton, 1.12f);
        }

        private void MoodButton_MouseLeave(object sender, EventArgs e)
        {
            Button moodButton = sender as Button;
            if (moodButton == null || moodButton == selectedMoodButton)
            {
                return;
            }

            ResetMoodButtonScale(moodButton);
        }

        private void CaptureMoodButtonBounds()
        {
            if (defaultMoodBounds.Count > 0)
            {
                return;
            }

            foreach (Button moodButton in GetMoodButtons())
            {
                defaultMoodBounds[moodButton] = moodButton.Bounds;
            }
        }

        private void SetSelectedMoodButton(Button moodButton)
        {
            if (selectedMoodButton != null && selectedMoodButton != moodButton)
            {
                ResetMoodButtonScale(selectedMoodButton);
            }

            selectedMoodButton = moodButton;

            if (selectedMoodButton != null)
            {
                SetMoodButtonScale(selectedMoodButton, 1.18f);
            }
        }

        private void SetMoodButtonScale(Button moodButton, float scale)
        {
            if (moodButton == null || !defaultMoodBounds.ContainsKey(moodButton))
            {
                return;
            }

            Rectangle originalBounds = defaultMoodBounds[moodButton];
            int scaledWidth = (int)(originalBounds.Width * scale);
            int scaledHeight = (int)(originalBounds.Height * scale);
            int left = originalBounds.Left - ((scaledWidth - originalBounds.Width) / 2);
            int top = originalBounds.Top - ((scaledHeight - originalBounds.Height) / 2);

            moodButton.Bounds = new Rectangle(left, top, scaledWidth, scaledHeight);
            moodButton.BringToFront();
            ApplyRoundedCorners(moodButton, moodButton.Width / 2);
            moodButton.Invalidate();
        }

        private void ResetMoodButtonScale(Button moodButton)
        {
            if (moodButton == null || !defaultMoodBounds.ContainsKey(moodButton))
            {
                return;
            }

            moodButton.Bounds = defaultMoodBounds[moodButton];
            ApplyRoundedCorners(moodButton, moodButton.Width / 2);
            moodButton.Invalidate();
        }

        private void ApplyRoundedCorners(Control control, int radius)
        {
            if (control.Width <= 0 || control.Height <= 0)
            {
                return;
            }

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
            ApplyRoundedCorners(flowLayoutPanel1, 34);
            ApplyMoodButtonShapes();
            panel1.Invalidate();
            flowLayoutPanel1.Invalidate();
        }

        public void RefreshJournalList()
        {
            panel1.Controls.Clear();
            panel1.AutoScroll = true;

            if (SharedData.JournalEntries.Count == 0)
            {
                Label emptyLabel = new Label
                {
                    AutoSize = false,
                    Width = panel1.ClientSize.Width - 30,
                    Height = 70,
                    Left = 12,
                    Top = 12,
                    Text = "No journal entries yet.",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.Transparent
                };
                panel1.Controls.Add(emptyLabel);
                return;
            }

            int top = 12;
            foreach (JournalEntry entry in SharedData.JournalEntries.OrderByDescending(item => item.LastUpdated))
            {
                Button entryButton = new Button
                {
                    Width = panel1.ClientSize.Width - 30,
                    Height = 64,
                    Left = 12,
                    Top = top,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = panel1.BackColor,
                    ForeColor = Color.Black,
                    Font = new Font("Segoe UI Emoji", 12, FontStyle.Bold),
                    Text = string.Empty,
                    Tag = entry,
                    Cursor = Cursors.Hand
                };

                entryButton.FlatAppearance.BorderSize = 0;
                entryButton.FlatAppearance.MouseOverBackColor = panel1.BackColor;
                entryButton.FlatAppearance.MouseDownBackColor = panel1.BackColor;
                entryButton.Click += EntryButton_Click;
                entryButton.Paint += EntryButton_Paint;
                panel1.Controls.Add(entryButton);
                top += 74;
            }
        }

        private void EntryButton_Paint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
            {
                return;
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            Rectangle rect = new Rectangle(0, 0, button.Width - 1, button.Height - 1);
            JournalEntry entry = button.Tag as JournalEntry;

            using (GraphicsPath path = GetRoundedPath(rect, 16))
            using (Pen pen = new Pen(Color.Black, 2))
            {
                button.Region = new Region(path);
                e.Graphics.DrawPath(pen, path);
            }

            if (entry == null)
            {
                return;
            }

            Rectangle contentRect = Rectangle.Inflate(rect, -16, -10);
            Rectangle emojiRect = new Rectangle(contentRect.Right - 36, contentRect.Top, 36, contentRect.Height);
            Rectangle textRect = new Rectangle(contentRect.Left, contentRect.Top, contentRect.Width - 48, contentRect.Height);

            TextRenderer.DrawText(
                e.Graphics,
                "\U0001F4D6 | " + entry.DisplayTitle,
                button.Font,
                textRect,
                Color.Black,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            if (!string.IsNullOrWhiteSpace(entry.DisplayMoodEmoji))
            {
                TextRenderer.DrawText(
                    e.Graphics,
                    entry.DisplayMoodEmoji,
                    button.Font,
                    emojiRect,
                    Color.Black,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        private void EntryButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            JournalEntry entry = button?.Tag as JournalEntry;
            if (entry != null)
            {
                OpenEditor(entry);
            }
        }

        public void OpenEditor(JournalEntry entry = null, string mood = null)
        {
            UserControl3 editor = new UserControl3(this, entry, mood);
            editor.Location = new Point(0, 0);
            Controls.Add(editor);
            editor.BringToFront();
        }

        public void CloseEditor(UserControl3 editor)
        {
            if (editor != null && Controls.Contains(editor))
            {
                Controls.Remove(editor);
                editor.Dispose();
            }

            SharedData.ReplaceJournalEntries(JournalRepository.GetEntriesForUser(SharedData.CurrentUserId));
            RefreshJournalList();
        }

        public void DeleteJournalEntry(JournalEntry entry)
        {
            if (entry == null || entry.Id == 0)
            {
                return;
            }

            JournalRepository.DeleteEntry(entry.Id, SharedData.CurrentUserId);
            SharedData.ReplaceJournalEntries(JournalRepository.GetEntriesForUser(SharedData.CurrentUserId));
            RefreshJournalList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenEditor(null, selectedMood);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserControl2 userControl2 = new UserControl2();
            userControl2.Location = new Point(0, 0);
            Controls.Add(userControl2);
            userControl2.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (Form6 calendarForm = new Form6())
            {
                calendarForm.ShowDialog();
            }
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
        }

        private void EnableDoubleBuffer(Control control)
        {
            if (control == null)
            {
                return;
            }

            PropertyInfo doubleBufferedProperty = typeof(Control).GetProperty(
                "DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);

            doubleBufferedProperty?.SetValue(control, true, null);
        }
    }
}
