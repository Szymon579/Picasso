using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace WordsGame
{
    public partial class Form1 : Form
    {
        private Client client;
        private Server server;

        public Form1()
        {
            InitializeComponent();
            initPainting();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            messageTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(SubmitMessage);
            setUsernameButton.Enabled = false;
        }

        private async void hostButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Setting up the server...");
            hostButton.Enabled = false;
            try
            {
                await Task.Run(() =>
                {
                    server = new Server(4000);
                    server.WaitForConnection();
                });
            }
            catch (IOException error)
            {
                Console.WriteLine(error.ToString());
            }
        }

        private async void clientButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Connecting as a client...");
            clientButton.Enabled = false;
            setUsernameButton.Enabled = true;
            
            try
            {
                await Task.Run(() =>
                {
                    client = new Client(4000);
                    client.MessageReceived += messageReceived_Event;
                    client.CanvasReceived += canvasReceived_Event;
                    client.StartListeningForData();
                });
            }
            catch (IOException error)
            {
                Console.WriteLine(error.ToString());
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

        private void setUsernameButton_Click(object sender, EventArgs e)
        {
            if (usernameTextBox.Text != string.Empty)
            {
                client.SendMessage(usernameTextBox.Text);
                client.SetUsername(usernameTextBox.Text);
            }
            setUsernameButton.Enabled = false;
        }


        Bitmap bmp;
        Graphics graphics;
        bool paint = false;
        Point pCurrent, pPrevious;
        Pen pen = new Pen(Color.Black, 2);

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
    }
}