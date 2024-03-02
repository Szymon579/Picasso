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
            clientButton.Location = new Point(414, 16);
            clientButton.Margin = new Padding(3, 4, 3, 4);
            clientButton.Name = "clientButton";
            clientButton.Size = new Size(121, 45);
            clientButton.TabIndex = 1;
            clientButton.Text = "Client";
            clientButton.UseVisualStyleBackColor = true;
            clientButton.Click += clientButton_Click;
            // 
            // hostButton
            // 
            hostButton.Location = new Point(14, 16);
            hostButton.Margin = new Padding(3, 4, 3, 4);
            hostButton.Name = "hostButton";
            hostButton.Size = new Size(121, 45);
            hostButton.TabIndex = 2;
            hostButton.Text = "Host";
            hostButton.UseVisualStyleBackColor = true;
            hostButton.Click += hostButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(414, 373);
            label1.Name = "label1";
            label1.Size = new Size(70, 20);
            label1.TabIndex = 3;
            label1.Text = "Message:";
            // 
            // messageTextBox
            // 
            messageTextBox.Location = new Point(414, 397);
            messageTextBox.Margin = new Padding(3, 4, 3, 4);
            messageTextBox.Name = "messageTextBox";
            messageTextBox.Size = new Size(337, 27);
            messageTextBox.TabIndex = 4;
            // 
            // trafficTextBox
            // 
            trafficTextBox.Location = new Point(414, 183);
            trafficTextBox.Margin = new Padding(3, 4, 3, 4);
            trafficTextBox.Name = "trafficTextBox";
            trafficTextBox.ReadOnly = true;
            trafficTextBox.Size = new Size(337, 174);
            trafficTextBox.TabIndex = 5;
            trafficTextBox.Text = "";
            // 
            // hostTextBox
            // 
            hostTextBox.AutoSize = true;
            hostTextBox.Location = new Point(414, 159);
            hostTextBox.Name = "hostTextBox";
            hostTextBox.Size = new Size(53, 20);
            hostTextBox.TabIndex = 6;
            hostTextBox.Text = "Traffic:";
            // 
            // usernameTextBox
            // 
            usernameTextBox.Location = new Point(414, 109);
            usernameTextBox.Margin = new Padding(3, 4, 3, 4);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(121, 27);
            usernameTextBox.TabIndex = 8;
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(414, 85);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(78, 20);
            nameLabel.TabIndex = 7;
            nameLabel.Text = "Username:";
            // 
            // setUsernameButton
            // 
            setUsernameButton.Location = new Point(542, 109);
            setUsernameButton.Margin = new Padding(3, 4, 3, 4);
            setUsernameButton.Name = "setUsernameButton";
            setUsernameButton.Size = new Size(63, 31);
            setUsernameButton.TabIndex = 10;
            setUsernameButton.Text = "Set";
            setUsernameButton.UseVisualStyleBackColor = true;
            setUsernameButton.Click += setUsernameButton_Click;
            // 
            // pictureBox
            // 
            pictureBox.BackColor = SystemColors.ButtonHighlight;
            pictureBox.Location = new Point(14, 109);
            pictureBox.Margin = new Padding(3, 4, 3, 4);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(377, 315);
            pictureBox.TabIndex = 11;
            pictureBox.TabStop = false;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.MouseMove += pictureBox_MouseMove;
            pictureBox.MouseUp += pictureBox_MouseUp;
            // 
            // labelPictureBox
            // 
            labelPictureBox.AutoSize = true;
            labelPictureBox.Location = new Point(14, 85);
            labelPictureBox.Name = "labelPictureBox";
            labelPictureBox.Size = new Size(58, 20);
            labelPictureBox.TabIndex = 12;
            labelPictureBox.Text = "Canvas:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(764, 448);
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
            Margin = new Padding(3, 4, 3, 4);
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