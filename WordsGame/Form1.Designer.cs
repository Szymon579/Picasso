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
            clientTextBox = new TextBox();
            trafficTextBox = new RichTextBox();
            hostTextBox = new Label();
            textBox1 = new TextBox();
            nameLabel = new Label();
            setUsernameButton = new Button();
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
            label1.Location = new Point(444, 115);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 3;
            label1.Text = "Message:";
            // 
            // clientTextBox
            // 
            clientTextBox.Location = new Point(444, 133);
            clientTextBox.Name = "clientTextBox";
            clientTextBox.Size = new Size(345, 23);
            clientTextBox.TabIndex = 4;
            // 
            // trafficTextBox
            // 
            trafficTextBox.Location = new Point(12, 82);
            trafficTextBox.Name = "trafficTextBox";
            trafficTextBox.Size = new Size(378, 326);
            trafficTextBox.TabIndex = 5;
            trafficTextBox.Text = "";
            // 
            // hostTextBox
            // 
            hostTextBox.AutoSize = true;
            hostTextBox.Location = new Point(12, 64);
            hostTextBox.Name = "hostTextBox";
            hostTextBox.Size = new Size(42, 15);
            hostTextBox.TabIndex = 6;
            hostTextBox.Text = "Traffic:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(444, 82);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(106, 23);
            textBox1.TabIndex = 8;
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(820, 463);
            Controls.Add(setUsernameButton);
            Controls.Add(textBox1);
            Controls.Add(nameLabel);
            Controls.Add(hostTextBox);
            Controls.Add(trafficTextBox);
            Controls.Add(clientTextBox);
            Controls.Add(label1);
            Controls.Add(hostButton);
            Controls.Add(clientButton);
            Name = "Form1";
            Text = "Scribidio";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button clientButton;
        private Button hostButton;
        private Label label1;
        private TextBox clientTextBox;
        private RichTextBox trafficTextBox;
        private Label hostTextBox;
        private TextBox textBox1;
        private Label nameLabel;
        private Button setUsernameButton;
    }
}