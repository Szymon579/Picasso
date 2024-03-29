using System.Dynamic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WordsGame
{
    public partial class MainForm : Form
    {
        private List<Panel> panelList;

        private Client client;
        private Server server;
        private bool hostApp = false;

        Bitmap bmp;
        Graphics graphics;
        bool paint = false;
        Point pCurrent;
        Point pPrevious;
        Pen pen = new Pen(Color.Black, 2);

        public MainForm()
        {
            InitializeComponent();
            InitPainting();

            panelList = new List<Panel>();
            panelList.Add(gameplayPanel);
            panelList.Add(menuPanel);
            panelList.Add(createGamePanel);
            panelList.Add(joinGamePanel);
            panelList.Add(lobbyPanel);
            panelList.Add(chooseWordPanel);
            panelList.Add(artistPanel);
            panelList.Add(scoresPanel);

            ShowPanel(menuPanel);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            messageTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(SubmitMessage);
        }


        delegate void ShowPanelCallback(Panel panelToShow);
        private void ShowPanel(Panel panelToShow)
        {
            foreach (Panel panel in panelList)
            {
                if (panel.InvokeRequired)
                {
                    ShowPanelCallback call = new ShowPanelCallback(ShowPanel);
                    panel.Invoke(call, new object[] { panelToShow });
                }
                else
                {
                    if (panel.Equals(panelToShow))
                    {
                        panel.Visible = true;
                    }
                    else
                    {
                        panel.Visible = false;
                    }
                }
            }
        }


        delegate void SetTextCallback(ref Button button, string text);
        private void SetText(ref Button button, string text)
        {
            if (button.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                button.Text = text;
            }
        }


        private (string host, string port) ParseAddress(string hostAndPort)
        {
            int colonIndex = hostAndPort.IndexOf(':');
            if (colonIndex == -1)
            {
                SetStatusMessage("Invalid host:port format");
                return (null, null);
            }
            string host = hostAndPort.Substring(0, colonIndex);
            string port = hostAndPort.Substring(colonIndex + 1);

            Console.WriteLine("Host: " + host);
            Console.WriteLine("Port: " + port);

            return (host, port);
        }

        private void SetStatusMessage(string message)
        {
            Console.WriteLine(message);
            statusLabel.Text = message;
        }

        private void NonHostOptionsDisable()
        {
            startGameButton.Enabled = false;
            roundsUpDown.Enabled = false;
        }

        private async void MakeServer(string host, string port)
        {
            try
            {
                await Task.Run(() =>
                {
                    server = new Server(host, port);
                    server.WaitForConnection();
                });
            }
            catch (IOException error)
            {
                Console.WriteLine(error.ToString());
                return;
            }
        }


        private async void MakeClient(string host, string port, string username)
        {
            try
            {
                await Task.Run(() =>
                {
                    client = new Client(host, port);

                    client.LogicReceived += logicReceived_Event;
                    client.StartListeningForData();

                    client.SendMessage(username);
                    client.SetUsername(username);
                });
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
                SetStatusMessage("Connection failed");
                return;
            }

            if (!hostApp)
                NonHostOptionsDisable();
        }


        private void logicReceived_Event(object sender, DataEventArgs e)
        {
            SetStatusMessage("Logic received");

            byte logicCode = e.data[0];

            if (logicCode == LogicController.setAsArtist)
            {
                SetStatusMessage("setAsArtist");
                ShowPanel(chooseWordPanel);
                pictureBox.Enabled = true;
                messageTextBox.Enabled = false;
            }
            else if (logicCode == LogicController.setAsGuesser)
            {
                SetStatusMessage("setAsGuesser");
                ShowPanel(gameplayPanel);
                pictureBox.Enabled = false;
                messageTextBox.Enabled = true;
            }
            else if (logicCode == LogicController.sendWordsToChoose)
            {
                SetStatusMessage("Choose word");

                var words = DataTypeHandler.MakeWordsFromData(e.data);
                word1Button.Text = words.Item1;
                word2Button.Text = words.Item2;
                word3Button.Text = words.Item3;
            }
            else if (logicCode == LogicController.sendBitmap)
            {
                SetStatusMessage("Canvas received");

                bmp = DataTypeHandler.MakeBitmapFromData(e.data);
                graphics = Graphics.FromImage(bmp);
                pictureBox.Image = bmp;
            }
            else if (logicCode == LogicController.sendMessage)
            {
                SetStatusMessage("Message received");

                string message = Encoding.UTF8.GetString(e.data, 1, e.data.Length - 1);
                Console.WriteLine(message);
                trafficTextBox.Text += message + '\n';
            }
            else if (logicCode == LogicController.sendNumOfLetters)
            {
                SetStatusMessage("Number of letters received");

                string noLetters = Encoding.UTF8.GetString(e.data, 1, e.data.Length - 1);
                wordTextBox.Text = noLetters;
            }
            else if (logicCode == LogicController.updateLobby)
            {
                SetStatusMessage("Lobby updated");

                string usernames = Encoding.UTF8.GetString(e.data, 1, e.data.Length - 1);
                playersTextBox.Text = usernames;
            }
            else if (logicCode == LogicController.displayScores)
            {
                SetStatusMessage("Scores");
                ShowPanel(scoresPanel);           
            }
            else if (logicCode == LogicController.endOfRound)
            {
                SetStatusMessage("End of round");
                graphics.Clear(Color.White);
            }
        }



        // ----------------------- MENU PANEL -----------------------
        private void createGameButton_Click(object sender, EventArgs e)
        {
            ShowPanel(createGamePanel);
        }

        private void joinGameButton_Click(object sender, EventArgs e)
        {
            ShowPanel(joinGamePanel);
        }


        // ----------------------- CREATE GAME PANEL -----------------------
        private void hostButton_Click(object sender, EventArgs e)
        {
            string hostAndPort = hostServerTextBox.Text;
            string username = hostUsernameTextBox.Text;

            if (hostAndPort == string.Empty)
            {
                SetStatusMessage("Game server cannot be empty!");
                return;
            }
            if (username == string.Empty)
            {
                SetStatusMessage("Username cannot be empty!");
                return;
            }

            var parsed = ParseAddress(hostAndPort);
            try
            {
                MakeServer(parsed.host, parsed.port);
            }
            catch
            {
                SetStatusMessage("Failed to create a server!");
                return;
            }

            try
            {
                MakeClient(parsed.host, parsed.port, username);
            }
            catch
            {
                SetStatusMessage("Failed to create a client!");
                return;
            }

            hostApp = true;
            ShowPanel(lobbyPanel);
        }

        // ----------------------- JOIN GAME PANEL -----------------------
        private void connectButton_Click(object sender, EventArgs e)
        {
            string hostAndPort = gameServerTextBox.Text;
            string username = usernameTextBox.Text;

            if (hostAndPort == string.Empty)
            {
                SetStatusMessage("Game server cannot be empty!");
                return;
            }
            if (username == string.Empty)
            {
                SetStatusMessage("Username cannot be empty!");
                return;
            }

            var parsed = ParseAddress(hostAndPort);
            try
            {
                MakeClient(parsed.host, parsed.port, username);
            }
            catch
            {
                SetStatusMessage("Failed to Connect!");
                return;
            }

            if (!hostApp)
                NonHostOptionsDisable();

            ShowPanel(lobbyPanel);
            client.SendLogic(LogicController.playerConnected);
        }

        // ----------------------- LOBBY PANEL -----------------------
        private void startGameButton_Click(object sender, EventArgs e)
        {  
            client.SendLogic(LogicController.gameStart);
        }


        // ----------------------- GAMEPLAY PANEL -----------------------
        private void SubmitMessage(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                client.SendMessage(messageTextBox.Text);
                trafficTextBox.Text += messageTextBox.Text + "\n";
                messageTextBox.Text = string.Empty;
            }
        }   


        // ----------------------- CHOOSE WORD PANEL -----------------------
        private void word1Button_Click(object sender, EventArgs e)
        {
            client.SendLogic(LogicController.sendChoosenWord, word1Button.Text);
            wordTextBox.Text = word1Button.Text;
            ShowPanel(gameplayPanel);
        }

        private void word2Button_Click(object sender, EventArgs e)
        {
            client.SendLogic(LogicController.sendChoosenWord, word2Button.Text);
            wordTextBox.Text = word2Button.Text;
            ShowPanel(gameplayPanel);
        }

        private void word3Button_Click(object sender, EventArgs e)
        {
            client.SendLogic(LogicController.sendChoosenWord, word3Button.Text);
            wordTextBox.Text = word3Button.Text;
            ShowPanel(gameplayPanel);
        }




        // ----------------------- CANVAS -----------------------
        void InitPainting()
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(bmp);
            graphics.Clear(Color.White);
            pictureBox.Image = bmp;
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
            pCurrent = e.Location;
            pPrevious = e.Location;

            SetStatusMessage("Mouse down");
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;
            client.SendCanvas(bmp);

            SetStatusMessage("Mouse up");
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (paint)
            {
                pCurrent = e.Location;
                graphics.DrawLine(pen, pCurrent, pPrevious);
                pPrevious = pCurrent;
            }
            pictureBox.Refresh();
        }


    }
}