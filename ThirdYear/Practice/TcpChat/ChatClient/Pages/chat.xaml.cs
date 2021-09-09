using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ChatClient.Pages
{
    public partial class chat : Page
    {
        char[] characters = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' ,'?' };
        private List<string> RSA_Endoce(string s, long e, long n)
        {
            List<string> result = new List<string>();
            BigInteger bi;
            for (int i = 0; i < s.Length; i++)
            {
                int index = Array.IndexOf(characters, s[i]);
                bi = new BigInteger(index);
                bi = BigInteger.Pow(bi, (int)e);
                BigInteger n_ = new BigInteger((int)n);
                bi = bi % n_;
                result.Add(bi.ToString());
            }
            return result;
        }
        private string RSA_Dedoce(List<string> input, long d, long n)
        {
            string result = "";
            BigInteger bi;
            foreach (string item in input)
            {
                bi = new BigInteger(Convert.ToDouble(item));
                bi = BigInteger.Pow(bi, (int)d);
                BigInteger n_ = new BigInteger((int)n);
                bi = bi % n_;
                int index = Convert.ToInt32(bi.ToString());
                result += characters[index].ToString();
            }
            return result;
        }
        private string message;
        private login login;
        public MainWindow _mainWindow;
        public chat _chat(MainWindow mainWindow, login login)
        {
            _mainWindow = mainWindow;
            this.login = login;
            HistoryBOX.Items.Add(login.userName);
            return this;
        }
        public chat()
        {
            InitializeComponent();
        }
        long e_, n, d;
        internal void SendMessage()
        {
            message = InputBOX.Text;
            message = message.ToUpper();
            List<string> result = RSA_Endoce(message, e_, n);
            string mesToServ = "";
            for (int i = 0; i < result.Count; i++)
            {
                mesToServ += (result[i] + " ");
            }
            byte[] data = Encoding.Unicode.GetBytes(mesToServ);
            login.stream.Write(data, 0, data.Length);
        }
        internal void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = login.stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (login.stream.DataAvailable);
                    string message = builder.ToString();
                    List<string> input = new List<string>();
                    string[] words = message.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 1; i < words.Length; i++)
                    {
                        input.Add(words[i]);
                    }
                    string result = words[0] +  " ";
                    if ("c" != words[3])
                    {
                        result += RSA_Dedoce(input, d, n);
                    }
                    else
                    {
                        for (int i = 1; i < words.Length-3; i++)
                        {
                            result+=words[i] +" ";
                        }
                        e_ = long.Parse(words[words.Length - 3]);
                        n = long.Parse(words[words.Length - 2]);
                        d = long.Parse(words[words.Length - 1]);
                    }
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                    {
                        HistoryBOX.Items.Add(result);
                    });
                }
                catch (Exception e)
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                    {
                        _mainWindow.OpenPage(MainWindow.pages.login);
                    });
                    Disconnect();
                    break;
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
            InputBOX.Clear();
        }
        internal void Disconnect()
        {
            if (login.stream != null)
                login.stream.Close();
            if (login.client != null)
                login.client.Close();
            if (login.receiveThread != null)
                login.receiveThread.Abort();
        }
    }
}
