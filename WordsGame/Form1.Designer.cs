namespace WordsGame
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            clientButton = new Button();
            hostButton = new Button();
            label1 = new Label();
            messageTextBox = new TextBox();
            trafficTextBox = new RichTextBox();
            hostTextBox = new Label();
            usernameTextBox = new TextBox();
            nameLabel = new Label();
            setUsernameButton = new Button();
            pictureBox = new PictureBox();
            labelPictureBox = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // clientButton
            // 
            clientButton.Location = new Point(444, 12);
            clientButton.Name = "clientButton";
            clientButton.Size = new Size(106, 34);
            clientButton.TabIndex = 1;
            clientButton.Text = "Client";
            clientButton.UseVisualStyleBackColor = true;
            clientButton.Click += clientButton_Click;
            // 
            // hostButton
            // 
            hostButton.Location = new Point(12, 12);
            hostButton.Name = "hostButton";
            hostButton.Size = new Size(106, 34);
            hostButton.TabIndex = 2;
            hostButton.Text = "Host";
            hostButton.UseVisualStyleBackColor = true;
            hostButton.Click += hostButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(444, 357);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 3;
            label1.Text = "Message:";
            // 
            // messageTextBox
            // 
            messageTextBox.Location = new Point(444, 375);
            messageTextBox.Name = "messageTextBox";
            messageTextBox.Size = new Size(352, 23);
            messageTextBox.TabIndex = 4;
            // 
            // trafficTextBox
            // 
            trafficTextBox.Location = new Point(444, 137);
            trafficTextBox.Name = "trafficTextBox";
            trafficTextBox.ReadOnly = true;
            trafficTextBox.Size = new Size(352, 207);
            trafficTextBox.TabIndex = 5;
            trafficTextBox.Text = "";
            // 
            // hostTextBox
            // 
            hostTextBox.AutoSize = true;
            hostTextBox.Location = new Point(444, 119);
            hostTextBox.Name = "hostTextBox";
            hostTextBox.Size = new Size(42, 15);
            hostTextBox.TabIndex = 6;
            hostTextBox.Text = "Traffic:";
            // 
            // usernameTextBox
            // 
            usernameTextBox.Location = new Point(444, 82);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(106, 23);
            usernameTextBox.TabIndex = 8;
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(444, 64);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(63, 15);
            nameLabel.TabIndex = 7;
            nameLabel.Text = "Username:";
            // 
            // setUsernameButton
            // 
            setUsernameButton.Location = new Point(556, 82);
            setUsernameButton.Name = "setUsernameButton";
            setUsernameButton.Size = new Size(55, 23);
            setUsernameButton.TabIndex = 10;
            setUsernameButton.Text = "Set";
            setUsernameButton.UseVisualStyleBackColor = true;
            setUsernameButton.Click += setUsernameButton_Click;
            // 
            // pictureBox
            // 
            pictureBox.BackColor = SystemColors.ButtonHighlight;
            pictureBox.Location = new Point(12, 82);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(397, 316);
            pictureBox.TabIndex = 11;
            pictureBox.TabStop = false;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.MouseMove += pictureBox_MouseMove;
            pictureBox.MouseUp += pictureBox_MouseUp;
            // 
            // labelPictureBox
            // 
            labelPictureBox.AutoSize = true;
            labelPictureBox.Location = new Point(12, 64);
            labelPictureBox.Name = "labelPictureBox";
            labelPictureBox.Size = new Size(48, 15);
            labelPictureBox.TabIndex = 12;
            labelPictureBox.Text = "Canvas:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(820, 463);
            Controls.Add(labelPictureBox);
            Controls.Add(pictureBox);
            Controls.Add(setUsernameButton);
            Controls.Add(usernameTextBox);
            Controls.Add(nameLabel);
            Controls.Add(hostTextBox);
            Controls.Add(trafficTextBox);
            Controls.Add(messageTextBox);
            Controls.Add(label1);
            Controls.Add(hostButton);
            Controls.Add(clientButton);
            Name = "Form1";
            Text = " ";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button clientButton;
        private Button hostButton;
        private Label label1;
        private TextBox messageTextBox;
        private RichTextBox trafficTextBox;
        private Label hostTextBox;
        private TextBox usernameTextBox;
        private Label nameLabel;
        private Button setUsernameButton;
        private PictureBox pictureBox;
        private Label labelPictureBox;
    }
}