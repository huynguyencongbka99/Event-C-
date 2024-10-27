using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Prog;


namespace Prog
{
    class EventArgs_huy : EventArgs
    {
        public string s1 { set; get; }
        public int i1 { set; get; }
        public string s2 { set; get; }
        public EventArgs_huy() { s1 = ""; i1 = 0; s2 = ""; }
        public EventArgs_huy(string s1, int i1, string s2)
        {
            this.s1 = s1;
            this.i1 = i1;
            this.s2 = s2;
        }

    }

    class Server
    {
        public event EventHandler<EventArgs_huy> ServerEventHandler;
        public void ServerPublish()
        {
            while (true)
            {
                Random rnd = new Random();
                string opr1 = rnd.Next(1, 1000).ToString();
                int opr2 = rnd.Next(1, 1000);
                string opr3 = rnd.Next(1, 1000).ToString();

                ServerEventHandler?.Invoke(this, new EventArgs_huy(opr1, opr2, opr3));
                Thread.Sleep(1000);
            }

        }
    }

    class Client1
    {
        public void SubtoServer(Server server)
        {
            server.ServerEventHandler += ClientReceivedInforFromServer;
        }
        public void ClientReceivedInforFromServer(object sender, EventArgs_huy e)
        {
            Console.WriteLine("========================================================");
            Console.WriteLine($"String 1 received from server: {e.s1}");
            Console.WriteLine($"Number 1 received from server: {e.i1}");
            Console.WriteLine($"String 2 received from server: {e.s2}");
            Console.WriteLine("========================================================");
        }
    }

    class Client2
    {
        public void SubtoServer(Server server)
        {
            server.ServerEventHandler += ClientReceivedInforFromServer;
        }
        public void ClientReceivedInforFromServer(object sender, EventArgs_huy e)
        {
            Console.WriteLine("//===================================================//");
            Console.WriteLine($"{e.s1}, birthday: {e.i1}, from {e.s2}");
            Console.WriteLine("//====================================================//");
        }
    }

    class ClientLog
    {
        public void SubtoServer(Server server)
        {
            server.ServerEventHandler += ClientReceivedInforFromServer;
        }
        public void ClientReceivedInforFromServer(object sender, EventArgs_huy e)
        {
            DateTime currentDateTime = DateTime.Now;
            string minute = currentDateTime.Minute.ToString();
            string docPath = $"F:/A_Works/Study/SelfLearningC#/EventHandler-TCP-IP/{minute}.txt";
            using (StreamWriter outputFile = new StreamWriter(docPath, true))
            {
                outputFile.WriteLine($"{currentDateTime.ToString()}, {e.s1}, birthday: {e.i1}, from {e.s2}");
            }
        }
    }

    class ClientTCP_IP
    {
        //Define the server IP address and port
        string serverIp;
        int port;
        NetworkStream stream;

        public ClientTCP_IP(string sip, int port)
        {
            this.serverIp = sip;
            this.port = port;

            //Create a TCP listener that listens on the specified IP address and port
            TcpClient client = new TcpClient();
            client.Connect(serverIp, port);
            stream = client.GetStream();
            Console.WriteLine("Connected to Server.");
        }

        public void SubtoServer(Server server)
        {
            server.ServerEventHandler += ClientSendInforsToServer;
        }
        public void ClientSendInforsToServer(object sender, EventArgs_huy e)
        {
            DateTime currentDateTime = DateTime.Now;
            string message = $"{currentDateTime.ToString()} , Name: {e.s1}, DateOfBirth: {e.i1}, Address: {e.s2}";
            byte[] data = Encoding.ASCII.GetBytes(message); 

            //Send data to server
            stream.Write(data, 0, data.Length);
        }
    }

    class Prgs
    {
        static void Main(string[] args)
        {
            Server server1 = new Server();
            Client1 client1 = new Client1();
            Client2 client2 = new Client2();
            ClientLog clientLog = new ClientLog();
            ClientTCP_IP clientTCP_IP = new ClientTCP_IP("192.168.0.10", 5000);
            client1.SubtoServer(server1);
            client2.SubtoServer(server1);
            clientLog.SubtoServer(server1);
            clientTCP_IP.SubtoServer(server1);
            server1.ServerPublish();
        }
    }
}