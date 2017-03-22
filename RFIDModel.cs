using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Windows.Forms;
using Impinj.OctaneSdk;
using System.Threading;
using System.IO;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace RFIDReader
{
    public class RFIDModel: INotifyPropertyChanged
    {
        public const int reader_port = 5084;
        private const int maxBufferSize = 5000;
        private const string impinj_mode = "default";

        private bool logging;
        public bool Logging
        {
            get
            {
                return logging;
            }
            set
            {
                logging = value;
                NotifyPropertyChanged();
            }
        }

        private string logfile;
        public string Logfile
        {
            get
            {
                return logfile;
            }
            set
            {
                logfile = value;
                NotifyPropertyChanged();
            }
        }

        private string reader_ip;
        public string ReaderIP
        {
            get { return reader_ip; }
            set
            {
                reader_ip = value;
                NotifyPropertyChanged();
            }
        } 
        
        private int seq = 0;

        private ReaderMode mode;
        public ReaderMode Mode
        {
            get { return mode; }
            set
            {
                mode = value;
                NotifyPropertyChanged();
            }
        }

        private List<Antenna> antennas;
        public List<Antenna> Antennas
        {
            get { return antennas; }
            private set
            {
                antennas = value;
                NotifyPropertyChanged();
            }
        }

        private string tagMask;
        public string TagMask
        {
            get { return tagMask; }
            set
            {
                tagMask = value;
                NotifyPropertyChanged();
            }
        }

        private bool filter;
        public bool Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                NotifyPropertyChanged();
            }
        }

        private ushort delayMs;
        public ushort DelayMS
        {
            get { return delayMs; }
            set
            {
                delayMs = value;
                NotifyPropertyChanged();
            }
        }

        private bool running;
        public bool Running
        {
            get { return running; }
            private set
            {
                running = value;
                NotifyPropertyChanged();
            }
        }
                
        private StreamWriter log;
        private ImpinjReader rdr;

        private RFIDServer server;
        private ConcurrentQueue<RFIDResult> results;

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler newResultEvent;
        public event EventHandler startedEvent;
        public event EventHandler stoppedEvent;
        public event EventHandler<ClientEventArgs> clientConnectedEvent;
        public event EventHandler<ClientEventArgs> clientDisconnectedEvent;

        public RFIDModel()
        {
            logging = false;
            Logfile = "";
            ReaderIP = "127.0.0.1";
            Antennas = new List<Antenna>();
            Filter = false;
            DelayMS = 1;
            Running = false;

            results = new ConcurrentQueue<RFIDResult>();
            server = new RFIDServer(results);
            server.clientConnectedEvent += new EventHandler<ClientEventArgs>(clientConnectedPassthrough);
            server.clientDisconnectedEvent += new EventHandler<ClientEventArgs>(clientDisconnectedPassthrough);
        }
       
        private void restartIfRunning()
        {
            if (Running)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (s, e) => { disconnect(); connect(); };
                worker.RunWorkerAsync();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));            
        }

        private void clientConnectedPassthrough(object sender, ClientEventArgs e)
        {
            clientConnectedEvent?.Invoke(this, e);
        }

        private void clientDisconnectedPassthrough(object sender, ClientEventArgs e)
        {
            clientDisconnectedEvent?.Invoke(this, e);
        }

        private void LoadSettings(string file="settings.xml")
        {

            Settings rdr_settings = Settings.Load(file);
            try
            {

                rdr_settings.Antennas.DisableAll();
                //antenna = view.textBox1.Text;
                //string[] words = antenna.Split(' ');
                foreach (Antenna antenna in Antennas)
                {
                    rdr_settings.Antennas.GetAntenna(antenna.ID).IsEnabled = antenna.enabled;
                    rdr_settings.Antennas.GetAntenna(antenna.ID).TxPowerInDbm = antenna.power;
                    rdr_settings.Antennas.GetAntenna(antenna.ID).MaxRxSensitivity = antenna.maxSensitivity;
                }

                rdr_settings.ReaderMode = Mode;

                rdr_settings.Report.IncludeAntennaPortNumber = true;

                // Send a tag report for every tag read.
                rdr_settings.Report.Mode = ReportMode.Individual;

                if (Filter)
                {

                    rdr_settings.Filters.TagFilter1.MemoryBank = MemoryBank.Epc;

                    rdr_settings.Filters.TagFilter1.BitPointer = BitPointers.Epc;
                    // Only match tags with EPCs that start with "xxxx"
                    rdr_settings.Filters.TagFilter1.TagMask = TagMask;
                    // This filter is 16 bits long (one word).
                    //rdr_settings.Filters.TagFilter1.BitCount = 16;
                    rdr_settings.Filters.Mode = TagFilterMode.OnlyFilter1;

                }


                rdr.ApplySettings(rdr_settings);
                Console.WriteLine("Settings Applied");
                rdr_settings = rdr.QuerySettings();
                Console.WriteLine(rdr_settings.Report);



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnTagsReported(ImpinjReader sender, TagReport report)
        {
            foreach (Tag tag in report)
            {
                if (results.Count == maxBufferSize)
                {
                    RFIDResult temp;
                    while (!results.TryDequeue(out temp)) Thread.Sleep(100);
                }

                RFIDResult r = new RFIDResult(sender.Name, seq++, impinj_mode, tag);
                r.AnalysisEPC();
                results.Enqueue(r);
                if (logging)
                    log.WriteLine(r.makeMeAString());
                newResultEvent(this, EventArgs.Empty);
            }
        }
       
        private void openLogFile()
        {
            log = new StreamWriter(Logfile, true);
        }

        private void disconnect()
        {
            if (rdr != null)
            {
                rdr.Stop();
                rdr.Disconnect();
                rdr = null;
                stoppedEvent(this, EventArgs.Empty);
            }

            if (logging)
                log.Close();
        }

        private void connect() 
        {
            rdr = new ImpinjReader();
            rdr.Name = ReaderIP + ":" + reader_port;
            Console.WriteLine("connecting to: " + ReaderIP + ":" + reader_port);
            //rdr.Connect(reader_ip, reader_port);
            rdr.Connect(ReaderIP);

            rdr.ApplyDefaultSettings();
            rdr.TagsReported += OnTagsReported;
            LoadSettings();
            rdr.Start();
            startedEvent(this, EventArgs.Empty);
        }

        public void start()
        {
            if (logging) { openLogFile(); }
            if (rdr != null) disconnect();
            try
            {
                Running = true;
                connect(); //this starts the tag reader
            } catch (OctaneSdkException e)
            {
                Debug.WriteLine(e.ToString());
                Running = false;
                throw;
            }
            server.start();
            return;
        }

        public void stop()
        {
            try
            {
                disconnect();
            } catch (OctaneSdkException e)
            {
                Debug.WriteLine(e.ToString());
            }
            finally
            {
                server.stop();
                Running = false;
            }
            
        }
    }
}
