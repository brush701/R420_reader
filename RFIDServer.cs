using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFIDReader
{
    public class ClientEventArgs : EventArgs
    {
        public int ClientCount { get; private set; }
        public ClientEventArgs(int count) { ClientCount = count; }
    }

    public class RequestEventArgs: EventArgs
    {
        public uint requestedAntenna { get; private set; }
        public uint requestedPower { get; private set; }
        public RequestEventArgs(uint antenna, uint power)
        {
            requestedAntenna = antenna;
            requestedPower = power;
        }
    }

    class RFIDServer
    {
        private List<NetworkStream> clients = new List<NetworkStream>();
        private object clientsLock = new object();
        private TcpListener tcpListener;
        private Thread connectionThread;

        public ConcurrentQueue<RFIDResult> results;
        Thread txThread;

        private bool stopRequested;
        private int port;
        public ushort delayMs { get; set; }

        public event EventHandler<ClientEventArgs> clientConnectedEvent;
        public event EventHandler<ClientEventArgs> clientDisconnectedEvent;
        public event EventHandler<RequestEventArgs> clientRequestEvent;

        public RFIDServer(ConcurrentQueue<RFIDResult> results, ushort port=14)
        {
            this.results = results;
            this.port = port;
            tcpListener = new TcpListener(IPAddress.Any, port);

            connectionThread = new Thread(new ThreadStart(ListenForClients));
            txThread = new System.Threading.Thread(new System.Threading.ThreadStart(send));

            stopRequested = true;
        }

        private void ListenForClients()
        {
            this.tcpListener.Start();

            while (!stopRequested)
            {
                Console.WriteLine("Waiting for a connection...");
                //blocks until a client has connected to the server
                TcpClient client = this.tcpListener.AcceptTcpClient();

                //create a thread to handle communication 
                //with connected client
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
            tcpListener.Stop();
        }

        private void HandleClientComm(object client)
        {
            try
            {
                TcpClient tcpClient = (TcpClient)client;
                tcpClient.SendBufferSize = 100000000;
                NetworkStream clientStream = tcpClient.GetStream();
                lock (clientsLock) clients.Add(clientStream);
                Console.WriteLine("client received. total clients:" + clients.Count);
                clientConnectedEvent(this, new ClientEventArgs(clients.Count));
                bool bClosed = false;
                byte[] buff = new byte[1];
                byte[] myReadBuffer = new byte[1024];
                
                string[] antennaInfoChange = { "1", "30" };

                while (!bClosed && !stopRequested && tcpClient.Client.Poll(0, SelectMode.SelectRead))
                {                    
                    if (tcpClient.Client.Receive(buff, SocketFlags.Peek) == 0)
                    {
                        // Client disconnected
                        bClosed = true;
                        lock (clientsLock) clients.Remove(clientStream);
                        tcpClient.Close();
                        clientDisconnectedEvent?.Invoke(this, new ClientEventArgs(clients.Count));
                        return;
                    }

                    StringBuilder myCompleteMessage = new StringBuilder();
                    int numberOfBytesRead = 0;

                    // Incoming message may be larger than the buffer size. 
                    do
                    {
                        numberOfBytesRead = clientStream.Read(myReadBuffer, 0, myReadBuffer.Length);
                        myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
                    } while (clientStream.DataAvailable);

                    Debug.WriteLine(myCompleteMessage.ToString());
                    antennaInfoChange = myCompleteMessage.ToString().Split(' ');
                    Debug.WriteLine(antennaInfoChange[0]);
                    Debug.WriteLine(antennaInfoChange[1]);

                    uint antenna = Convert.ToUInt16(antennaInfoChange[0]);
                    uint power = Convert.ToUInt16(antennaInfoChange[1]);

                    clientRequestEvent?.Invoke(this, new RequestEventArgs(antenna, power));
                    Thread.Sleep(1000);
                }
                lock (clientsLock) clients.Remove(clientStream);
                tcpClient.Close();
                clientDisconnectedEvent?.Invoke(this, new ClientEventArgs(clients.Count));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        private void sendToClients(String msg)
        {
            for (int i = 0; i < clients.Count; i++)
            {

                NetworkStream clientStream = clients[i];
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] buffer = encoder.GetBytes(msg);

                try
                {
                    clientStream.Write(buffer, 0, buffer.Length);
                    clientStream.Flush();
                }
                catch (System.IO.IOException ex)
                {
                    lock(clientsLock) clients.Remove(clientStream);
                    clientDisconnectedEvent?.Invoke(this, new ClientEventArgs(clients.Count));
                    Debug.WriteLine(ex.ToString());
                }
            }
        }

        private void send()
        {
            while (!stopRequested)
            {
                if (results.Count > 0)
                {
                    RFIDResult result;
                    while (!results.TryDequeue(out result)) Thread.Sleep(10);
                    if (result != null)
                    {
                        sendToClients(";" + result.makeMeAString() + "\n");
                        Thread.Sleep(delayMs);
                    }
                }
            }
            foreach (NetworkStream client in clients) { client.Close(); }
        }

        public void start()
        {
            stopRequested = false;
            connectionThread.Start();
            txThread.Start();
        }

        public void stop()
        {
            stopRequested = true;
            if (connectionThread.ThreadState == System.Threading.ThreadState.Running)
                connectionThread.Join();
            if (txThread.ThreadState == System.Threading.ThreadState.Running)
                txThread.Join();
        }
       
    }
}
