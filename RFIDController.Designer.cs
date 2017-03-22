namespace RFIDReader
{
    partial class RFIDController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.connect_button = new System.Windows.Forms.Button();
            this.disconnect_button = new System.Windows.Forms.Button();
            this.readerIPTextField = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.clientsCountLabel = new System.Windows.Forms.Label();
            this.readFramesLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.logfileTextBox = new System.Windows.Forms.TextBox();
            this.logButton = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.antennaTextBox = new System.Windows.Forms.TextBox();
            this.powerTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.filterCheckBox = new System.Windows.Forms.CheckBox();
            this.filterTextBox = new System.Windows.Forms.TextBox();
            this.delayTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.modeComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // connect_button
            // 
            this.connect_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.connect_button.Location = new System.Drawing.Point(730, 25);
            this.connect_button.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.connect_button.Name = "connect_button";
            this.connect_button.Size = new System.Drawing.Size(196, 42);
            this.connect_button.TabIndex = 0;
            this.connect_button.Text = "Connect";
            this.connect_button.UseVisualStyleBackColor = true;
            this.connect_button.Click += new System.EventHandler(this.connect_button_Click);
            // 
            // disconnect_button
            // 
            this.disconnect_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.disconnect_button.Location = new System.Drawing.Point(730, 75);
            this.disconnect_button.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.disconnect_button.Name = "disconnect_button";
            this.disconnect_button.Size = new System.Drawing.Size(196, 42);
            this.disconnect_button.TabIndex = 1;
            this.disconnect_button.Text = "Disconnect";
            this.disconnect_button.UseVisualStyleBackColor = true;
            this.disconnect_button.Click += new System.EventHandler(this.disconnect_button_Click);
            // 
            // readerIPTextField
            // 
            this.readerIPTextField.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.readerIPTextField.Location = new System.Drawing.Point(262, 48);
            this.readerIPTextField.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.readerIPTextField.Name = "readerIPTextField";
            this.readerIPTextField.Size = new System.Drawing.Size(420, 41);
            this.readerIPTextField.TabIndex = 2;
            this.readerIPTextField.Text = "192.168.1.3";
            this.readerIPTextField.TextChanged += new System.EventHandler(this.readerIPTextField_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(40, 52);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "Reader IP name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(44, 119);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(841, 31);
            this.label2.TabIndex = 16;
            this.label2.Text = "When connected to reader, it would stream RF Parameters to port 14";
            // 
            // clientsCountLabel
            // 
            this.clientsCountLabel.AutoSize = true;
            this.clientsCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.clientsCountLabel.Location = new System.Drawing.Point(44, 175);
            this.clientsCountLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.clientsCountLabel.Name = "clientsCountLabel";
            this.clientsCountLabel.Size = new System.Drawing.Size(267, 31);
            this.clientsCountLabel.TabIndex = 17;
            this.clientsCountLabel.Text = "Connected Clients: 0";
            // 
            // readFramesLabel
            // 
            this.readFramesLabel.AutoSize = true;
            this.readFramesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.readFramesLabel.Location = new System.Drawing.Point(344, 173);
            this.readFramesLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.readFramesLabel.Name = "readFramesLabel";
            this.readFramesLabel.Size = new System.Drawing.Size(199, 31);
            this.readFramesLabel.TabIndex = 23;
            this.readFramesLabel.Text = "Read frames: 0";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.statusLabel.ForeColor = System.Drawing.Color.Red;
            this.statusLabel.Location = new System.Drawing.Point(658, 173);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(273, 31);
            this.statusLabel.TabIndex = 24;
            this.statusLabel.Text = "Status: Disconnected";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(40, 244);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 31);
            this.label3.TabIndex = 25;
            this.label3.Text = "Filename";
            // 
            // logfileTextBox
            // 
            this.logfileTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.logfileTextBox.Location = new System.Drawing.Point(166, 240);
            this.logfileTextBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.logfileTextBox.Name = "logfileTextBox";
            this.logfileTextBox.Size = new System.Drawing.Size(110, 38);
            this.logfileTextBox.TabIndex = 26;
            this.logfileTextBox.Text = "log";
            this.logfileTextBox.TextChanged += new System.EventHandler(this.logfileTextBox_TextChanged);
            // 
            // logButton
            // 
            this.logButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.logButton.AutoSize = true;
            this.logButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.logButton.Location = new System.Drawing.Point(292, 238);
            this.logButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.logButton.Name = "logButton";
            this.logButton.Size = new System.Drawing.Size(69, 41);
            this.logButton.TabIndex = 27;
            this.logButton.Text = "Log";
            this.logButton.UseVisualStyleBackColor = true;
            this.logButton.CheckedChanged += new System.EventHandler(this.logButton_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(388, 242);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 31);
            this.label4.TabIndex = 28;
            this.label4.Text = "Antenna #";
            // 
            // antennaTextBox
            // 
            this.antennaTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.antennaTextBox.Location = new System.Drawing.Point(532, 238);
            this.antennaTextBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.antennaTextBox.Name = "antennaTextBox";
            this.antennaTextBox.Size = new System.Drawing.Size(126, 38);
            this.antennaTextBox.TabIndex = 29;
            this.antennaTextBox.Text = "1";
            this.antennaTextBox.TextChanged += new System.EventHandler(this.antennaTextBox_TextChanged);
            // 
            // powerTextBox
            // 
            this.powerTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.powerTextBox.Location = new System.Drawing.Point(788, 238);
            this.powerTextBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.powerTextBox.Name = "powerTextBox";
            this.powerTextBox.Size = new System.Drawing.Size(96, 38);
            this.powerTextBox.TabIndex = 23;
            this.powerTextBox.Text = "30";
            this.powerTextBox.TextChanged += new System.EventHandler(this.powerTextBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(680, 240);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 31);
            this.label5.TabIndex = 31;
            this.label5.Text = "Power";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.Location = new System.Drawing.Point(44, 298);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 31);
            this.label6.TabIndex = 32;
            this.label6.Text = "Filter";
            // 
            // filterCheckBox
            // 
            this.filterCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.filterCheckBox.AutoSize = true;
            this.filterCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.filterCheckBox.Location = new System.Drawing.Point(352, 294);
            this.filterCheckBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.filterCheckBox.Name = "filterCheckBox";
            this.filterCheckBox.Size = new System.Drawing.Size(60, 41);
            this.filterCheckBox.TabIndex = 33;
            this.filterCheckBox.Text = "On";
            this.filterCheckBox.UseVisualStyleBackColor = true;
            this.filterCheckBox.CheckedChanged += new System.EventHandler(this.filterCheckBox_CheckedChanged);
            // 
            // filterTextBox
            // 
            this.filterTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.filterTextBox.Location = new System.Drawing.Point(124, 294);
            this.filterTextBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(216, 38);
            this.filterTextBox.TabIndex = 34;
            this.filterTextBox.Text = "E2003098191500771234";
            this.filterTextBox.TextChanged += new System.EventHandler(this.filterTextBox_TextChanged);
            // 
            // delayTextBox
            // 
            this.delayTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.delayTextBox.Location = new System.Drawing.Point(844, 300);
            this.delayTextBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.delayTextBox.Name = "delayTextBox";
            this.delayTextBox.Size = new System.Drawing.Size(46, 38);
            this.delayTextBox.TabIndex = 35;
            this.delayTextBox.Text = "1";
            this.delayTextBox.TextChanged += new System.EventHandler(this.delayTextBox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label7.Location = new System.Drawing.Point(692, 302);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 31);
            this.label7.TabIndex = 36;
            this.label7.Text = "Delay(ms)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label8.Location = new System.Drawing.Point(454, 302);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 31);
            this.label8.TabIndex = 37;
            this.label8.Text = "Mode";
            // 
            // modeComboBox
            // 
            this.modeComboBox.FormattingEnabled = true;
            this.modeComboBox.Location = new System.Drawing.Point(532, 300);
            this.modeComboBox.Name = "modeComboBox";
            this.modeComboBox.Size = new System.Drawing.Size(121, 33);
            this.modeComboBox.TabIndex = 38;
            this.modeComboBox.SelectedValueChanged += new System.EventHandler(this.modeComboBox_SelectedValueChanged);
            // 
            // RFIDController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 352);
            this.Controls.Add(this.modeComboBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.delayTextBox);
            this.Controls.Add(this.filterTextBox);
            this.Controls.Add(this.filterCheckBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.powerTextBox);
            this.Controls.Add(this.antennaTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.logButton);
            this.Controls.Add(this.logfileTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.readFramesLabel);
            this.Controls.Add(this.clientsCountLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.readerIPTextField);
            this.Controls.Add(this.disconnect_button);
            this.Controls.Add(this.connect_button);
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "RFIDController";
            this.Text = "RFID Reader Interface";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RFIDInterface_FormClosed);
            this.Load += new System.EventHandler(this.RFIDInterface_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connect_button;
        private System.Windows.Forms.Button disconnect_button;
        private System.Windows.Forms.TextBox readerIPTextField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label clientsCountLabel;
        private System.Windows.Forms.Label readFramesLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox logfileTextBox;
        private System.Windows.Forms.CheckBox logButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox antennaTextBox;
        private System.Windows.Forms.TextBox powerTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox filterCheckBox;
        private System.Windows.Forms.TextBox filterTextBox;
        private System.Windows.Forms.TextBox delayTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox modeComboBox;
    }
}