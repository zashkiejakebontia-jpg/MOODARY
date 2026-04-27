namespace Moodary
{
    partial class UserControl1
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1 = new Moodary.BufferedScrollPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.Panel();
            this.moodButton5 = new System.Windows.Forms.Button();
            this.moodButton1 = new System.Windows.Forms.Button();
            this.moodButton2 = new System.Windows.Forms.Button();
            this.moodButton3 = new System.Windows.Forms.Button();
            this.moodButton4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(405, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(307, 89);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = global::Moodary.Properties.Resources.account_setting_icon_symbol_design_illustration_vector_removebg_preview;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Location = new System.Drawing.Point(969, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 62);
            this.button1.TabIndex = 15;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.Font = new System.Drawing.Font("Segoe UI Emoji", 16.2F, System.Drawing.FontStyle.Bold);
            this.button3.Location = new System.Drawing.Point(898, 8);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(59, 54);
            this.button3.TabIndex = 16;
            this.button3.Text = "📅";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(3, 104);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(426, 453);
            this.panel1.TabIndex = 17;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint_1);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.moodButton5);
            this.flowLayoutPanel1.Controls.Add(this.moodButton1);
            this.flowLayoutPanel1.Controls.Add(this.moodButton2);
            this.flowLayoutPanel1.Controls.Add(this.moodButton3);
            this.flowLayoutPanel1.Controls.Add(this.moodButton4);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(494, 104);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(483, 99);
            this.flowLayoutPanel1.TabIndex = 19;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // moodButton5
            // 
            this.moodButton5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.moodButton5.BackgroundImage = global::Moodary.Properties.Resources.happy;
            this.moodButton5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.moodButton5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moodButton5.Location = new System.Drawing.Point(16, 12);
            this.moodButton5.Name = "moodButton5";
            this.moodButton5.Size = new System.Drawing.Size(70, 70);
            this.moodButton5.TabIndex = 4;
            this.moodButton5.UseVisualStyleBackColor = false;
            // 
            // moodButton1
            // 
            this.moodButton1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.moodButton1.BackgroundImage = global::Moodary.Properties.Resources.sad;
            this.moodButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.moodButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moodButton1.Location = new System.Drawing.Point(108, 12);
            this.moodButton1.Margin = new System.Windows.Forms.Padding(3, 3, 22, 3);
            this.moodButton1.Name = "moodButton1";
            this.moodButton1.Size = new System.Drawing.Size(70, 70);
            this.moodButton1.TabIndex = 0;
            this.moodButton1.UseVisualStyleBackColor = false;
            // 
            // moodButton2
            // 
            this.moodButton2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.moodButton2.BackgroundImage = global::Moodary.Properties.Resources.fear;
            this.moodButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.moodButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moodButton2.Location = new System.Drawing.Point(203, 12);
            this.moodButton2.Margin = new System.Windows.Forms.Padding(3, 3, 22, 3);
            this.moodButton2.Name = "moodButton2";
            this.moodButton2.Size = new System.Drawing.Size(70, 70);
            this.moodButton2.TabIndex = 1;
            this.moodButton2.UseVisualStyleBackColor = false;
            // 
            // moodButton3
            // 
            this.moodButton3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.moodButton3.BackgroundImage = global::Moodary.Properties.Resources.angry;
            this.moodButton3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.moodButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moodButton3.Location = new System.Drawing.Point(298, 12);
            this.moodButton3.Margin = new System.Windows.Forms.Padding(3, 3, 22, 3);
            this.moodButton3.Name = "moodButton3";
            this.moodButton3.Size = new System.Drawing.Size(70, 70);
            this.moodButton3.TabIndex = 2;
            this.moodButton3.UseVisualStyleBackColor = false;
            // 
            // moodButton4
            // 
            this.moodButton4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.moodButton4.BackgroundImage = global::Moodary.Properties.Resources.disgust1;
            this.moodButton4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.moodButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moodButton4.Location = new System.Drawing.Point(393, 12);
            this.moodButton4.Margin = new System.Windows.Forms.Padding(3, 3, 22, 3);
            this.moodButton4.Name = "moodButton4";
            this.moodButton4.Size = new System.Drawing.Size(70, 70);
            this.moodButton4.TabIndex = 3;
            this.moodButton4.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = global::Moodary.Properties.Resources._57166a67_fd05_4e75_a653_961b24594083;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Location = new System.Drawing.Point(435, 199);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(606, 359);
            this.button2.TabIndex = 20;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Moodary.Properties.Resources._1b04a7d7_549a_454d_aef6_e3bfc47cdde2;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(1068, 607);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private Moodary.BufferedScrollPanel panel1;
        private System.Windows.Forms.Panel flowLayoutPanel1;
        private System.Windows.Forms.Button moodButton1;
        private System.Windows.Forms.Button moodButton2;
        private System.Windows.Forms.Button moodButton3;
        private System.Windows.Forms.Button moodButton4;
        private System.Windows.Forms.Button moodButton5;
        private System.Windows.Forms.Button button2;
    }
}
