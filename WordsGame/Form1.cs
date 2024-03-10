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
        private Client client;
        private Server server;
        bool host;

        Bitmap bmp;
        Graphics graphics;
        bool paint = false;
        Point pCurrent;
        Point pPrevious;
        Pen pen = new Pen(Color.Black, 2);

        private List<Panel> panelList;

        public MainForm()
        {
            host = false;

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

            ShowPanel(menuPanel);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            messageTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(SubmitMessage);
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
            }
        }

        private async void MakeClient(string host, string port, string username)
        {
            try
            {
                await Task.Run(() =>
                {
                    client = new Client(host, port);
                    //client.MessageReceived += messageReceived_Event;
                    //client.CanvasReceived += canvasReceived_Event;
                    client.LogicReceived += logicReceived_Event;
                    client.StartListeningForData();

                    client.SendMessage(username);
                    client.SetUsername(username);


                    //pictureBoxHandler = new PictureBoxHandler(ref client, ref pictureBox);
                    //pictureBoxHandler.InitPainting();
                    //pictureBox.MouseDown += pictureBoxHandler.MouseDownEvent;
                    //pictureBox.MouseUp += pictureBoxHandler.MouseUpEvent;
                    //pictureBox.MouseMove += pictureBoxHandler.MouseMoveEvent;
                });
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
                SetStatusMessage("Connection failed");
            }
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


        // ----------------------- MENU PANEL -----------------------
        private void createGameButton_Click(object sender, EventArgs e)
        {
            host = true;
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

            ShowPanel(lobbyPanel);
            client.SendLogic(LogicController.playerConnected);
        }



        // ----------------------- LOBBY PANEL -----------------------
        private void startGameButton_Click(object sender, EventArgs e)
        {
            //ShowPanel(chooseWordPanel);    
            client.SendLogic(LogicController.gameStart);
        }


        // ----------------------- LOBBY PANEL -----------------------


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
        private void messageReceived_Event(object sender, DataEventArgs e)
        {
            string message = Encoding.UTF8.GetString(e.data);
            Console.WriteLine(message);
            trafficTextBox.Text += message + '\n';

            SetStatusMessage("Message received");
        }

        private void canvasReceived_Event(object sender, DataEventArgs e)
        {
            bmp = DataTypeHandler.MakeBitmapFromData(e.data);
            graphics = Graphics.FromImage(bmp);
            pictureBox.Image = bmp;

            SetStatusMessage("Canvas received");
        }
        private void logicReceived_Event(object sender, DataEventArgs e)
        {
            SetStatusMessage("Logic received");

            byte logicCode = e.data[0];

            if (logicCode == LogicController.setAsArtist)
            {
                SetStatusMessage("setAsArtist");
                ShowPanel(chooseWordPanel);
            }
            else if (logicCode == LogicController.setAsGuesser)
            {
                SetStatusMessage("setAsGuesser");
                ShowPanel(gameplayPanel);
                pictureBox.Enabled = false;
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
        }

        // ----------------------- CHOOSE WORD PANEL -----------------------
        private void word1Button_Click(object sender, EventArgs e)
        {
            client.SendLogic(LogicController.sendChoosenWord, word1Button.Text);
            ShowPanel(gameplayPanel);
        }

        private void word2Button_Click(object sender, EventArgs e)
        {
            client.SendLogic(LogicController.sendChoosenWord, word2Button.Text);
            ShowPanel(gameplayPanel);
        }

        private void word3Button_Click(object sender, EventArgs e)
        {
            client.SendLogic(LogicController.sendChoosenWord, word3Button.Text);
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