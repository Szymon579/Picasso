namespace WordsGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void hostButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Setting up the server...");

            try
            {
                await Task.Run(() =>
                {
                    Server server = new Server(4000);
                    server.IncomingMessage += messageReceived_Event;
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
            trafficTextBox.Text += e.Message;
        }

        private void clientButton_Click(object sender, EventArgs e)
        {

        }

        private void setUsernameButton_Click(object sender, EventArgs e)
        {

        }


    }
}