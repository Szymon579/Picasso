using System.IO;
using System.Text;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WordsGame
{
    public partial class Form1 : Form
    {
        private Client client;
        private Server server;

        Bitmap bmp;
        Graphics graphics;
        bool paint = false;
        Point pCurrent, pPrevious;
        Pen pen = new Pen(Color.Black, 2);

        private List<Panel> panelList;

        public Form1()
        {
            InitializeComponent();
            initPainting();

            panelList = new List<Panel>();
            panelList.Add(gameplayPanel);
            panelList.Add(menuPanel);
            panelList.Add(createGamePanel);
            panelList.Add(joinGamePanel);

            ShowPanel(menuPanel);
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            messageTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(SubmitMessage);
        }

        private void ShowPanel(Panel panelToShow)
        {
            foreach (Panel panel in panelList)
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

            Console.WriteLine("message received: ");
        }

        private void canvasReceived_Event(object sender, DataEventArgs e)
        {
            bmp = DataParser.MakeBitmapFromData(e.data);
            graphics = Graphics.FromImage(bmp);
            pictureBox.Image = bmp;

            Console.WriteLine("canvas received: ");
        }

        void initPainting()
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

            Console.WriteLine("MouseDown");
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;
            client.SendCanvasUpdate(bmp);

            Console.WriteLine("MouseUp");
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

        private void createGameButton_Click(object sender, EventArgs e)
        {
            ShowPanel(createGamePanel);
        }

        private void joinGameButton_Click(object sender, EventArgs e)
        {
            ShowPanel(joinGamePanel);
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
                    client.MessageReceived += messageReceived_Event;
                    client.CanvasReceived += canvasReceived_Event;
                    client.StartListeningForData();

                    client.SendMessage(username);
                    client.SetUsername(username);
                });
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
                statusMessage("Connection failed");
            }
        }

        private void hostButton_Click(object sender, EventArgs e)
        {
            string hostAndPort = hostServerTextBox.Text;
            string username = hostUsernameTextBox.Text;

            if (hostAndPort == string.Empty)
            {
                statusMessage("Game server cannot be empty!");
                return;
            }

            if (username == string.Empty)
            {
                statusMessage("Username cannot be empty!");
                return;
            }

            var parsed = ParseAddress(hostAndPort);
            try
            {
                MakeServer(parsed.host, parsed.port);

                MakeClient(parsed.host, parsed.port, username);


            }
            catch
            {
                statusMessage("Failed to create a game!");
            }

            ShowPanel(gameplayPanel);
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            string hostAndPort = gameServerTextBox.Text;
            string username = usernameTextBox.Text;

            if (hostAndPort == string.Empty)
            {
                statusMessage("Game server cannot be empty!");
                return;
            }

            if (username == string.Empty)
            {
                statusMessage("Username cannot be empty!");
                return;
            }

            var parsed = ParseAddress(hostAndPort);
            try
            {
                MakeClient(parsed.host, parsed.port, username);
            }
            catch
            {
                statusMessage("Failed to Connect!");
            }

            ShowPanel(gameplayPanel);
        }

        private (string host, string port) ParseAddress(string hostAndPort)
        {
            int colonIndex = hostAndPort.IndexOf(':');
            if (colonIndex == -1)
            {
                statusMessage("Invalid host:port format");
                return (null, null);
            }
            string host = hostAndPort.Substring(0, colonIndex);
            string port = hostAndPort.Substring(colonIndex + 1);

            Console.WriteLine("Host: " + host);
            Console.WriteLine("Port: " + port);

            return (host, port);
        }

        private void statusMessage(string message)
        {
            Console.WriteLine(message);
            statusLabel.Text = message;
        }


    }
}