namespace WordsGame
{
    partial class MainForm
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
            label1 = new Label();
            messageTextBox = new TextBox();
            trafficTextBox = new RichTextBox();
            hostTextBox = new Label();
            pictureBox = new PictureBox();
            labelPictureBox = new Label();
            gameplayPanel = new Panel();
            menuPanel = new Panel();
            createGameButton = new Button();
            joinGameButton = new Button();
            joinGamePanel = new Panel();
            connectButton = new Button();
            usernameTextBox = new TextBox();
            gameServerTextBox = new TextBox();
            label3 = new Label();
            label2 = new Label();
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            createGamePanel = new Panel();
            hostButton = new Button();
            hostUsernameTextBox = new TextBox();
            hostServerTextBox = new TextBox();
            label4 = new Label();
            label5 = new Label();
            lobbyPanel = new Panel();
            label7 = new Label();
            roundsUpDown = new NumericUpDown();
            label6 = new Label();
            startGameButton = new Button();
            playersTextBox = new RichTextBox();
            chooseWordPanel = new Panel();
            word3Button = new Button();
            word2Button = new Button();
            word1Button = new Button();
            artistPanel = new Panel();
            wordToDrawLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            gameplayPanel.SuspendLayout();
            menuPanel.SuspendLayout();
            joinGamePanel.SuspendLayout();
            statusStrip.SuspendLayout();
            createGamePanel.SuspendLayout();
            lobbyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)roundsUpDown).BeginInit();
            chooseWordPanel.SuspendLayout();
            artistPanel.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(360, 275);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 3;
            label1.Text = "Message:";
            // 
            // messageTextBox
            // 
            messageTextBox.Location = new Point(360, 293);
            messageTextBox.Name = "messageTextBox";
            messageTextBox.Size = new Size(295, 23);
            messageTextBox.TabIndex = 4;
            // 
            // trafficTextBox
            // 
            trafficTextBox.Location = new Point(360, 132);
            trafficTextBox.Name = "trafficTextBox";
            trafficTextBox.ReadOnly = true;
            trafficTextBox.Size = new Size(295, 132);
            trafficTextBox.TabIndex = 5;
            trafficTextBox.Text = "";
            // 
            // hostTextBox
            // 
            hostTextBox.AutoSize = true;
            hostTextBox.Location = new Point(360, 114);
            hostTextBox.Name = "hostTextBox";
            hostTextBox.Size = new Size(42, 15);
            hostTextBox.TabIndex = 6;
            hostTextBox.Text = "Traffic:";
            // 
            // pictureBox
            // 
            pictureBox.BackColor = SystemColors.ButtonHighlight;
            pictureBox.Location = new Point(10, 77);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(330, 236);
            pictureBox.TabIndex = 11;
            pictureBox.TabStop = false;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.MouseMove += pictureBox_MouseMove;
            pictureBox.MouseUp += pictureBox_MouseUp;
            // 
            // labelPictureBox
            // 
            labelPictureBox.AutoSize = true;
            labelPictureBox.Location = new Point(10, 59);
            labelPictureBox.Name = "labelPictureBox";
            labelPictureBox.Size = new Size(48, 15);
            labelPictureBox.TabIndex = 12;
            labelPictureBox.Text = "Canvas:";
            // 
            // gameplayPanel
            // 
            gameplayPanel.Controls.Add(labelPictureBox);
            gameplayPanel.Controls.Add(pictureBox);
            gameplayPanel.Controls.Add(label1);
            gameplayPanel.Controls.Add(messageTextBox);
            gameplayPanel.Controls.Add(trafficTextBox);
            gameplayPanel.Controls.Add(hostTextBox);
            gameplayPanel.Location = new Point(12, 12);
            gameplayPanel.Name = "gameplayPanel";
            gameplayPanel.Size = new Size(665, 329);
            gameplayPanel.TabIndex = 13;
            // 
            // menuPanel
            // 
            menuPanel.Controls.Add(createGameButton);
            menuPanel.Controls.Add(joinGameButton);
            menuPanel.Location = new Point(12, 12);
            menuPanel.Name = "menuPanel";
            menuPanel.Size = new Size(665, 329);
            menuPanel.TabIndex = 13;
            // 
            // createGameButton
            // 
            createGameButton.Location = new Point(254, 91);
            createGameButton.Name = "createGameButton";
            createGameButton.Size = new Size(162, 60);
            createGameButton.TabIndex = 1;
            createGameButton.Text = "Create game";
            createGameButton.UseVisualStyleBackColor = true;
            createGameButton.Click += createGameButton_Click;
            // 
            // joinGameButton
            // 
            joinGameButton.Location = new Point(254, 176);
            joinGameButton.Name = "joinGameButton";
            joinGameButton.Size = new Size(162, 60);
            joinGameButton.TabIndex = 0;
            joinGameButton.Text = "Join Game";
            joinGameButton.UseVisualStyleBackColor = true;
            joinGameButton.Click += joinGameButton_Click;
            // 
            // joinGamePanel
            // 
            joinGamePanel.Controls.Add(connectButton);
            joinGamePanel.Controls.Add(usernameTextBox);
            joinGamePanel.Controls.Add(gameServerTextBox);
            joinGamePanel.Controls.Add(label3);
            joinGamePanel.Controls.Add(label2);
            joinGamePanel.Location = new Point(12, 12);
            joinGamePanel.Name = "joinGamePanel";
            joinGamePanel.Size = new Size(665, 329);
            joinGamePanel.TabIndex = 14;
            // 
            // connectButton
            // 
            connectButton.Location = new Point(265, 198);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(159, 23);
            connectButton.TabIndex = 4;
            connectButton.Text = "Join";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += connectButton_Click;
            // 
            // usernameTextBox
            // 
            usernameTextBox.Location = new Point(265, 160);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(159, 23);
            usernameTextBox.TabIndex = 3;
            usernameTextBox.Text = "Player1";
            // 
            // gameServerTextBox
            // 
            gameServerTextBox.Location = new Point(265, 109);
            gameServerTextBox.Name = "gameServerTextBox";
            gameServerTextBox.Size = new Size(159, 23);
            gameServerTextBox.TabIndex = 2;
            gameServerTextBox.Text = "192.168.93.1:4000";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(265, 142);
            label3.Name = "label3";
            label3.Size = new Size(63, 15);
            label3.TabIndex = 1;
            label3.Text = "Username:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(265, 91);
            label2.Name = "label2";
            label2.Size = new Size(75, 15);
            label2.TabIndex = 0;
            label2.Text = "Game server:";
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip.Location = new Point(0, 355);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(687, 22);
            statusStrip.TabIndex = 15;
            statusStrip.Text = "Test";
            // 
            // statusLabel
            // 
            statusLabel.DisplayStyle = ToolStripItemDisplayStyle.Text;
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(66, 17);
            statusLabel.Text = "statusLabel";
            // 
            // createGamePanel
            // 
            createGamePanel.Controls.Add(hostButton);
            createGamePanel.Controls.Add(hostUsernameTextBox);
            createGamePanel.Controls.Add(hostServerTextBox);
            createGamePanel.Controls.Add(label4);
            createGamePanel.Controls.Add(label5);
            createGamePanel.Location = new Point(12, 12);
            createGamePanel.Name = "createGamePanel";
            createGamePanel.Size = new Size(665, 329);
            createGamePanel.TabIndex = 16;
            // 
            // hostButton
            // 
            hostButton.Location = new Point(265, 198);
            hostButton.Name = "hostButton";
            hostButton.Size = new Size(159, 23);
            hostButton.TabIndex = 11;
            hostButton.Text = "Create game";
            hostButton.UseVisualStyleBackColor = true;
            hostButton.Click += hostButton_Click;
            // 
            // hostUsernameTextBox
            // 
            hostUsernameTextBox.Location = new Point(265, 162);
            hostUsernameTextBox.Name = "hostUsernameTextBox";
            hostUsernameTextBox.Size = new Size(159, 23);
            hostUsernameTextBox.TabIndex = 8;
            hostUsernameTextBox.Text = "Host";
            // 
            // hostServerTextBox
            // 
            hostServerTextBox.Location = new Point(265, 111);
            hostServerTextBox.Name = "hostServerTextBox";
            hostServerTextBox.Size = new Size(159, 23);
            hostServerTextBox.TabIndex = 7;
            hostServerTextBox.Text = "192.168.93.1:4000";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(265, 144);
            label4.Name = "label4";
            label4.Size = new Size(63, 15);
            label4.TabIndex = 6;
            label4.Text = "Username:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(265, 93);
            label5.Name = "label5";
            label5.Size = new Size(75, 15);
            label5.TabIndex = 5;
            label5.Text = "Game server:";
            // 
            // lobbyPanel
            // 
            lobbyPanel.Controls.Add(label7);
            lobbyPanel.Controls.Add(roundsUpDown);
            lobbyPanel.Controls.Add(label6);
            lobbyPanel.Controls.Add(startGameButton);
            lobbyPanel.Controls.Add(playersTextBox);
            lobbyPanel.Location = new Point(12, 12);
            lobbyPanel.Name = "lobbyPanel";
            lobbyPanel.Size = new Size(665, 329);
            lobbyPanel.TabIndex = 17;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(383, 41);
            label7.Name = "label7";
            label7.Size = new Size(72, 15);
            label7.TabIndex = 4;
            label7.Text = "No. Rounds:";
            // 
            // roundsUpDown
            // 
            roundsUpDown.Location = new Point(383, 59);
            roundsUpDown.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            roundsUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            roundsUpDown.Name = "roundsUpDown";
            roundsUpDown.Size = new Size(72, 23);
            roundsUpDown.TabIndex = 3;
            roundsUpDown.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(472, 41);
            label6.Name = "label6";
            label6.Size = new Size(47, 15);
            label6.TabIndex = 2;
            label6.Text = "Players:";
            // 
            // startGameButton
            // 
            startGameButton.Location = new Point(472, 242);
            startGameButton.Name = "startGameButton";
            startGameButton.Size = new Size(164, 32);
            startGameButton.TabIndex = 1;
            startGameButton.Text = "Start game";
            startGameButton.UseVisualStyleBackColor = true;
            startGameButton.Click += startGameButton_Click;
            // 
            // playersTextBox
            // 
            playersTextBox.Location = new Point(472, 59);
            playersTextBox.Name = "playersTextBox";
            playersTextBox.Size = new Size(164, 177);
            playersTextBox.TabIndex = 0;
            playersTextBox.Text = "";
            // 
            // chooseWordPanel
            // 
            chooseWordPanel.Controls.Add(word3Button);
            chooseWordPanel.Controls.Add(word2Button);
            chooseWordPanel.Controls.Add(word1Button);
            chooseWordPanel.Location = new Point(12, 12);
            chooseWordPanel.Name = "chooseWordPanel";
            chooseWordPanel.Size = new Size(665, 329);
            chooseWordPanel.TabIndex = 18;
            // 
            // word3Button
            // 
            word3Button.Location = new Point(383, 150);
            word3Button.Name = "word3Button";
            word3Button.Size = new Size(104, 41);
            word3Button.TabIndex = 2;
            word3Button.Text = "word_3";
            word3Button.UseVisualStyleBackColor = true;
            word3Button.Click += word3Button_Click;
            // 
            // word2Button
            // 
            word2Button.Location = new Point(273, 150);
            word2Button.Name = "word2Button";
            word2Button.Size = new Size(104, 41);
            word2Button.TabIndex = 1;
            word2Button.Text = "word_2";
            word2Button.UseVisualStyleBackColor = true;
            word2Button.Click += word2Button_Click;
            // 
            // word1Button
            // 
            word1Button.Location = new Point(163, 150);
            word1Button.Name = "word1Button";
            word1Button.Size = new Size(104, 41);
            word1Button.TabIndex = 0;
            word1Button.Text = "word_1";
            word1Button.UseVisualStyleBackColor = true;
            word1Button.Click += word1Button_Click;
            // 
            // artistPanel
            // 
            artistPanel.Controls.Add(wordToDrawLabel);
            artistPanel.Location = new Point(12, 12);
            artistPanel.Name = "artistPanel";
            artistPanel.Size = new Size(665, 329);
            artistPanel.TabIndex = 19;
            // 
            // wordToDrawLabel
            // 
            wordToDrawLabel.AutoSize = true;
            wordToDrawLabel.Location = new Point(300, 17);
            wordToDrawLabel.Name = "wordToDrawLabel";
            wordToDrawLabel.Size = new Size(77, 15);
            wordToDrawLabel.TabIndex = 0;
            wordToDrawLabel.Text = "word to draw";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(687, 377);
            Controls.Add(statusStrip);
            Controls.Add(gameplayPanel);
            Controls.Add(menuPanel);
            Controls.Add(createGamePanel);
            Controls.Add(artistPanel);
            Controls.Add(chooseWordPanel);
            Controls.Add(lobbyPanel);
            Controls.Add(joinGamePanel);
            Name = "MainForm";
            Text = " ";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            gameplayPanel.ResumeLayout(false);
            gameplayPanel.PerformLayout();
            menuPanel.ResumeLayout(false);
            joinGamePanel.ResumeLayout(false);
            joinGamePanel.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            createGamePanel.ResumeLayout(false);
            createGamePanel.PerformLayout();
            lobbyPanel.ResumeLayout(false);
            lobbyPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)roundsUpDown).EndInit();
            chooseWordPanel.ResumeLayout(false);
            artistPanel.ResumeLayout(false);
            artistPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private TextBox messageTextBox;
        private RichTextBox trafficTextBox;
        private Label hostTextBox;
        private PictureBox pictureBox;
        private Label labelPictureBox;
        private Panel gameplayPanel;
        private Panel menuPanel;
        private Button joinGameButton;
        private Button createGameButton;
        private Panel joinGamePanel;
        private Button connectButton;
        private TextBox usernameTextBox;
        private TextBox gameServerTextBox;
        private Label label3;
        private Label label2;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private Panel createGamePanel;
        private TextBox hostUsernameTextBox;
        private TextBox hostServerTextBox;
        private Label label4;
        private Label label5;
        private Button hostButton;
        private Panel lobbyPanel;
        private Label label6;
        private Button startGameButton;
        private RichTextBox playersTextBox;
        private NumericUpDown roundsUpDown;
        private Label label7;
        private Panel chooseWordPanel;
        private Button word3Button;
        private Button word2Button;
        private Button word1Button;
        private Panel artistPanel;
        private Label wordToDrawLabel;
    }
}