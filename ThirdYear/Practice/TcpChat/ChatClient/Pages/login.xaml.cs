using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatClient.Pages;

namespace ChatClient.Pages
{
    public partial class login : Page
    {
        internal int port { get; set; }
        internal string userName { get; set; }
        internal string host { get; set; }
        internal static TcpClient client { get; set; }
        internal static NetworkStream stream { get; set; }
        private chat chat;
        private MainWindow _mainWindow;
        internal static Thread receiveThread { get; set; }

        public login _login(MainWindow mainWindow, chat chat)
        {
            _mainWindow = mainWindow;
            this.chat = chat;
            return this;
        }

        public login()
        {
            InitializeComponent();
        }

        private void EnterBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string NamePasword = loginBOX.Text + " " + passwordBOX.Password;
                IsConnecting(NamePasword);
                _mainWindow.OpenPage(MainWindow.pages.chat);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void IsConnecting(string name)
        {
            userName = loginBOX.Text;
            host = HostBox.Text;//"127.0.0.1";
            port = Int32.Parse(PortBOX.Text);
            client = new TcpClient();
            client.Connect(host, port);
            stream = client.GetStream();
            string message = name;
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);
            receiveThread = new Thread(new ThreadStart(chat.ReceiveMessage));
            receiveThread.Start();
        }


    }
}
