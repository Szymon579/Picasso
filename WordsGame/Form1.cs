namespace WordsGame
{
    public partial class Form1 : Form
    {
        private Client client;
        private Server server;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            messageTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(SubmitMessage);


        }

        private void SubmitMessage(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                client.sendMessage(messageTextBox.Text);
                //trafficTextBox.Font.Bold = true;
                trafficTextBox.Text += messageTextBox.Text + "\n";
                //trafficTextBox.Font.Bold = false;
                messageTextBox.Text = string.Empty;           
            }
        }

        private async void hostButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Setting up the server...");

            try
            {
                await Task.Run(() =>
                {
                    server = new Server(4000);
                    //server.IncomingMessage += messageReceived_Event;
                    server.WaitForConnection();
                });
            }
            catch (IOException error)
            {
                Console.WriteLine(error.ToString());
            }



        }

        private void messageReceived_Event(object sender, MessageEventArgs e)
        {
            Console.WriteLine(e.Message);
            trafficTextBox.Text += e.Message + '\n';
        }

        private async void clientButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Connecting as a client...");

            try
            {
                await Task.Run(() =>
                {
                    client = new Client(4000);
                    client.MessageReceived += messageReceived_Event;
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
                client.sendMessage(usernameTextBox.Text);
            }

            setUsernameButton.Enabled = false;

            client.sendMessage(usernameTextBox.Text + "\n");
            usernameTextBox.Text = string.Empty;


        }

    }
}