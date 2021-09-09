using System;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading;

namespace ChatAttack
{
    class ChatAttack
    {
        static char[] characters = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        private const string host = "127.0.0.1";
        private const int port = 8888;
        static TcpClient client;
        static NetworkStream stream;
        static int e;
        static int n;
        static void Disconnect()
        {
            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();
        }
        static bool attack = false;
        static void ReceiveMessage()
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
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);
                    string[] message = builder.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (message[0] == "p" && message[1] == "i" && message[2] == "n" && message[3] == "c")
                    {
                        attack = true;
                        e = int.Parse(message[4]);
                        n = int.Parse(message[5]);
                        Console.WriteLine("key");
                        Console.WriteLine("e = " + e);
                        Console.WriteLine("n = " + n);
                    }
                    else if (attack)
                    {
                        for (int i = 1; i < message.Length; i++)
                        {
                            Console.WriteLine("attack");
                            Attack(int.Parse(message[i]));
                        }
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                    Disconnect();
                }
            }
        }
        static void Attack(int m)
        {
            BigInteger C = (BigInteger.Pow(new BigInteger(m), e) % n);
            for (int i = 0; i < 1; i++)
            {
                checked
                {
                    BigInteger verx = BigInteger.Pow(new BigInteger(e), i);
                    BigInteger A = BigInteger.Pow((int)C, (int)verx) % n;
                    if ((int)A < characters.Length)
                        Console.Write(characters[(int)A]);
                }
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            client = new TcpClient();
            try
            {
                client.Connect(host, port);
                stream = client.GetStream();
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
