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

        }

        private void SubmitMessage(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                client.SendMessage(messageTextBox.Text);
                //trafficTextBox.Font.Bold = true;
                trafficTextBox.Text += messageTextBox.Text + "\n";
                //trafficTextBox.Font.Bold = false;
                messageTextBox.Text = string.Empty;
            }
        }
        private void messageReceived_Event(object sender, DataEventArgs e)
        {
            Console.WriteLine("message received: ");
            string message = Encoding.UTF8.GetString(e.data);
            Console.WriteLine(message);
            trafficTextBox.Text += message + '\n';
        }

        private void canvasReceived_Event(object sender, DataEventArgs e)
        {
            Console.WriteLine("canvas received: ");
            //TODO: handle drawing to the bmp

            byte[] dataLengthBytes = new byte[4];
            Array.Copy(e.data, 0, dataLengthBytes, 0, 4);
            int dataLength = BitConverter.ToInt32(dataLengthBytes, 0);

            byte[] bmpData = new byte[dataLength];
            Array.Copy(e.data, 4, bmpData, 0, dataLength);


            // Save the BMP data to a file
            File.WriteAllBytes("received_image.bmp", bmpData);

            Console.WriteLine("BMP image received and saved.");
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
            try
            {
                await Task.Run(() =>
                {
                    client = new Client(4000);
                    client.MessageReceived += messageReceived_Event;
                    client.CanvasReceived += canvasReceived_Event;
                    client.startListenieng();
                });
            }
            catch (IOException error)
            {
                Console.WriteLine(error.ToString());
            }
        }

        private void setUsernameButton_Click(object sender, EventArgs e)
        {
            if (usernameTextBox.Text != string.Empty)
            {
                client.SendMessage(usernameTextBox.Text);
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
            Console.WriteLine("MouseDown");
            paint = true;
            pCurrent = e.Location;
            pPrevious = e.Location;
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            Console.WriteLine("MouseUp");
            paint = false;
            client.SendCanvasUpdate2(bmp);
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            //Console.WriteLine("MouseMove");
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