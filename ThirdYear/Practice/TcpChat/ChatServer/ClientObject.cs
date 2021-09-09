using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Text;

namespace ChatServer
{
    public class ClientObject
    {
        protected internal string Id { get; private set; }
        protected internal NetworkStream Stream { get; private set; }
        string userName;
        TcpClient client;
        ServerObject server;
        public ClientObject(TcpClient tcpClient, ServerObject serverObject)
        {
            Id = Guid.NewGuid().ToString();
            client = tcpClient;
            server = serverObject;
            serverObject.AddConnection(this);
        }
        public void Process()
        {
            try
            {
                Stream = client.GetStream();
                string message = GetMessage();

                string[] NamePasword = message.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                userName = NamePasword[0];

                if (!Registration(userName, NamePasword[1]))
                {
                    Console.WriteLine("Error");
                    server.BroadcastMessage("Error", this.Id);
                    return;
                }
                if (!ServerObject.firstConnect)
                {
                    long p = 0;
                    long q = 0;
                    do
                    {
                        Random random = new Random();
                        p = random.Next(100, 300);
                        q = random.Next(100, 300);
                        //p = 101;
                        //q = 103;
                    } while (!(RSA.IsTheNumberSimple(p) && RSA.IsTheNumberSimple(q) && p != q));
                    ServerObject.n = p * q;
                    long m = (p - 1) * (q - 1);
                    ServerObject.d = RSA.Calculate_d(m);
                    ServerObject.e_ = RSA.Calculate_e(ServerObject.d, m);
                    ServerObject.firstConnect = true;
                }
                message = userName;
                message += ServerObject.e_.ToString() + " " + ServerObject.n.ToString() + " " + ServerObject.d.ToString() + " ";
                server.BroadcastMessage(message, this.Id);
                Console.WriteLine(message);
                while (true)
                {
                    try
                    {
                        message = GetMessage();
                        if (message.Length == 0)
                        {
                            throw new Exception();
                        }
                        message = String.Format("{0}: {1}", userName, message);
                        Console.WriteLine(message);
                        server.BroadcastMessage(message, this.Id);
                    }
                    catch
                    {
                        message = userName;
                        Console.WriteLine(message);
                        server.BroadcastMessage(message, this.Id);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                server.RemoveConnection(this.Id);
                Close();
            }
        }

        private bool Registration(string NameUser, string Password)
        {
            DataTable dt_user = Select("SELECT * FROM [dbo].[users] WHERE [login] = '" + NameUser + "' AND [password] = '" + Password + "'");
            if (dt_user.Rows.Count > 0)    
            {
                return true;     
            }
            return false;
        }

        public DataTable Select(string selectSQL)
        {
            DataTable dataTable = new DataTable("dataBase");
            try
            {
                SqlConnection sqlConnection = new SqlConnection("server=COMPUTER\\SQLEXPRESS;Trusted_Connection=Yes;DataBase=TcpClient;");
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = selectSQL;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception e)
            {
                server.BroadcastMessage(e.Message, this.Id);
            }
            return dataTable;
        }

        private string GetMessage()
        {
            byte[] data = new byte[64];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (Stream.DataAvailable);

            return builder.ToString();
        }

        protected internal void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (client != null)
                client.Close();
        }
    }
}