using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Impinj.OctaneSdk;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace RFIDReader
{
    public partial class RFIDController : Form
    {
        private RFIDModel model;
        private Thread modelThread;

        public RFIDController()
        {
            InitializeComponent();
            model = new RFIDModel();
            modelThread = new Thread(new ThreadStart(() => { if (!model.Running) model.start(); }));

            modeComboBox.Items.AddRange(new object[] {
                ReaderMode.MaxThroughput,
                ReaderMode.Hybrid,
                ReaderMode.DenseReaderM4,
                ReaderMode.DenseReaderM8
            });

            model.clientConnectedEvent += new EventHandler<ClientEventArgs>(onClientConnected);
            model.clientDisconnectedEvent += new EventHandler<ClientEventArgs>(onClientDisconnected);
            model.startedEvent += new EventHandler(onStarted);
            model.stoppedEvent += new EventHandler(onStopped);
            model.newResultEvent += new EventHandler(onNewResult);

            Binding bind = new Binding("Enabled", model, "Running");
            bind.Format += SwitchBool;
            bind.Parse += SwitchBool;
            connect_button.DataBindings.Add(bind);

            disconnect_button.DataBindings.Add("Enabled", model, "Running");
            modeComboBox.SelectedIndex = 0;
        }

        private void SwitchBool(object sender, ConvertEventArgs e)
        {
            e.Value = !((bool)e.Value);
        }

        private void connect_button_Click(object sender, System.EventArgs e)
        {
            try
            {
                modelThread.Start();
            }
    
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void onNewResult(object sender, EventArgs e)
        {
            Action update = new Action(() =>
            {

            });
            if (InvokeRequired) Invoke(update);
            else update();
        }

        private void onStarted(object sender, EventArgs e)
        {
            Action update = new Action(() =>
            {
                statusLabel.ForeColor = Color.Green;
                statusLabel.Text = "Status: Connected";
            });

            if (InvokeRequired) Invoke(update);
            else update();
        }

        private void onStopped(object sender, EventArgs e)
        {
            Action update = new Action(() =>
            {
                statusLabel.ForeColor = Color.Red;
                statusLabel.Text = "Status: Disconnected";
            });

            if (InvokeRequired)
            {
                Invoke(update);
            }
            else
            {
                update();
            }
        } 

        private void onClientDisconnected(object sender, ClientEventArgs e)
        {
            Action update = new Action(() => clientsCountLabel.Text = "Connected Clients: " + e.ClientCount);
            if (InvokeRequired)
            {
                Invoke(update);
            }
            else
            {
                update();
            }
        }

        private void onClientConnected(object sender, ClientEventArgs e)
        {
            Action update = new Action(() => clientsCountLabel.Text = "Connected Clients: " + e.ClientCount);
            if (InvokeRequired)
            {
                Invoke(update);
            }
            else
            {
                update();
            }
        }

        private void disconnect_button_Click(object sender, System.EventArgs e)
        {
            if (model.Running)
                model.stop();
        }

        private void RFIDInterface_Load(object sender, System.EventArgs e)
        {
            //Initialize model values
            model.ReaderIP = readerIPTextField.Text;
            model.Logfile = logfileTextBox.Text;

            string antenna = antennaTextBox.Text;
            ushort power = Convert.ToUInt16(powerTextBox.Text);
            string[] words = antenna.Split(' ');
            model.Antennas.Clear();
            foreach (string word in words)
            {
                model.Antennas.Add(new Antenna(Convert.ToUInt16(word), true, power));
            }

            //model.mode = (ReaderMode)modeComboBox.SelectedItem;
            model.TagMask = filterTextBox.Text;
            model.DelayMS = Convert.ToUInt16(delayTextBox.Text);
        }

        private void RFIDInterface_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (model.Running)
                model.stop();
        }

        private void logfileTextBox_TextChanged(object sender, EventArgs e)
        {
            model.Logfile = logfileTextBox.Text + ".txt";
        }

        private void antennaTextBox_TextChanged(object sender, EventArgs e)
        {
            string antenna = antennaTextBox.Text;
            string[] words = antenna.Split(' ');
            model.Antennas.Clear();
            foreach (string word in words)
            {
                Antenna temp;
                try
                {
                    temp = new Antenna(Convert.ToUInt16(word));
                } catch (FormatException)
                {
                    //word was not an integer
                    continue;
                }
                model.Antennas.Add(temp);
            }
        }

        private void modeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            model.Mode = (ReaderMode)modeComboBox.SelectedItem;
        }

        private void powerTextBox_TextChanged(object sender, EventArgs e)
        {
            ushort power = Convert.ToUInt16(powerTextBox.Text);

            string antenna = antennaTextBox.Text;
            string[] words = antenna.Split(' ');
            model.Antennas.Clear();
            
            foreach (string word in words)
            {
                model.Antennas.Add(new Antenna(Convert.ToUInt16(word), true, power));
            }
        }

        private void filterTextBox_TextChanged(object sender, EventArgs e)
        {
            model.TagMask = filterTextBox.Text;
        }

        private void logButton_CheckedChanged(object sender, EventArgs e)
        {
            model.Logging = logButton.Checked;
        }

        private void filterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            model.Filter = filterCheckBox.Checked;
        }

        private void readerIPTextField_TextChanged(object sender, EventArgs e)
        {
            model.ReaderIP = readerIPTextField.Text;
        }

        private void delayTextBox_TextChanged(object sender, EventArgs e)
        {
            model.DelayMS = Convert.ToUInt16(delayTextBox.Text);
        }
    }
}
