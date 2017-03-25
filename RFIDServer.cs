using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using System.Net.WebSockets;
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
        private static object consoleLock = new object();
        private const int sendChunkSize = 1024;
        private const int receiveChunkSize = 1024;
        private const bool verbose = true;
        private const string key = "ThisIsASecret";
        public TimeSpan Delay
        {
            get;
            set;
        }            

        private CancellationTokenSource source;        
        private AuthenticationStatus authenticationStatus = AuthenticationStatus.UNAUTHENTICATED;

        public ConcurrentQueue<RFIDResult> results;
        Thread serverThread;

        private int port;
        private ClientWebSocket webSocket;
        private static UTF8Encoding encoder = new UTF8Encoding();

        public event EventHandler<ClientEventArgs> ConnectedEvent;
        public event EventHandler<ClientEventArgs> DisconnectedEvent;

        private enum AuthenticationStatus { UNAUTHENTICATED, AUTHENTICATED, FAILED };

        private event EventHandler authenticatedEvent;

        public RFIDServer(ConcurrentQueue<RFIDResult> results, ushort port = 14)
        {
            this.results = results;
            this.port = port;
            source = new CancellationTokenSource();
            Delay = TimeSpan.FromMilliseconds(1000); 
        }

        private async Task Send(ClientWebSocket webSocket, CancellationToken token)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("type", "auth");
            dict.Add("key", key);
            string request = JsonConvert.SerializeObject(dict);

            byte[] buffer = encoder.GetBytes(request);
            if (webSocket.State == WebSocketState.Open)
                await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, token);

            while (webSocket.State == WebSocketState.Open && !token.IsCancellationRequested)
            {
                while (authenticationStatus == AuthenticationStatus.UNAUTHENTICATED)
                {
                    await Task.Delay(Delay);
                    if (authenticationStatus == AuthenticationStatus.FAILED) return;
                }

                RFIDResult result;
                while (!results.TryDequeue(out result)) await Task.Delay(Delay);
                if (result != null)
                {
                    request = JsonConvert.SerializeObject(result);
                    buffer = encoder.GetBytes(request);
                    await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, token);
                    LogStatus(false, buffer, buffer.Length);
                }                
            }
        }

        private async Task Receive(ClientWebSocket webSocket, CancellationToken token)
        {
            byte[] buffer = new byte[receiveChunkSize];
           
            while (webSocket.State == WebSocketState.Open && !token.IsCancellationRequested)
            {
                string response = "";
                WebSocketReceiveResult result = new WebSocketReceiveResult(0, WebSocketMessageType.Text, false);
                while (!result.EndOfMessage)
                {
                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), token);
                    response += encoder.GetString(buffer);
                }
                
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, token);
                }
                else
                {
                    LogStatus(true, buffer, result.Count);
                }
                try
                {
                    Dictionary<string, string> res = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
                
                    if (!res.ContainsKey("type")) throw new JsonException("Invalid response from server");

                    if (res["type"] == "auth")
                    {
                        if (!res.ContainsKey("value")) throw new JsonException("Invalid response from server");
                        if (res["value"] == "success")
                        {
                            authenticatedEvent?.Invoke(this, EventArgs.Empty);
                            authenticationStatus = AuthenticationStatus.AUTHENTICATED;
                        }
                        else authenticationStatus = AuthenticationStatus.FAILED;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }       

        private static void LogStatus(bool receiving, byte[] buffer, int length)
        {
            lock (consoleLock)
            {
                Console.ForegroundColor = receiving ? ConsoleColor.Green : ConsoleColor.Gray;
                //Console.WriteLine("{0} ", receiving ? "Received" : "Sent");

                if (verbose)
                    Console.WriteLine(encoder.GetString(buffer));

                Console.ResetColor();
            }
        }
 

        private async Task<ClientWebSocket> Connect(string uri)
        {
            ClientWebSocket socket = null;

            try
            {
                socket = new ClientWebSocket();
                await socket.ConnectAsync(new Uri(uri), source.Token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: {0}", ex);
            }
            return socket;
        }        
        
        public async Task Start(string uri)
        {
            webSocket = await Connect(uri);
            serverThread = new Thread(new ThreadStart(async () =>
            {
                await Task.WhenAll(Receive(webSocket, source.Token), Send(webSocket, source.Token));
            }));
            serverThread.Start();
        }

        public async Task Stop()
        {
            source.Cancel();
            serverThread.Join();
            if (webSocket.State != WebSocketState.Closed)
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, source.Token);

            if (webSocket != null)
                webSocket.Dispose();
            source.Dispose();
            Debug.WriteLine("");

            lock (consoleLock)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("WebSocket closed.");
                Console.ResetColor();
            }
        }

    }
}
