using System;

using Impinj.OctaneSdk;


namespace RFIDReader
{
    
    public class RFIDResult
    {
        public int sequenceno { get; set; }
        public string ts { get; set; }
        public string mode { get; set; }
        public string Epc { get; set; }
        public string tagID { get; set; }
        public string readerID { get; set; }
        public ushort AntennaPortNumber { get; set; }
        public double ChannelInMhz { get; set; }
        public ulong FirstSeenTime { get; set; }
        public ulong LastSeenTime { get; set; }
        public double PeakRSSIInDbm { get; set; }
        public ushort TagSeenCount { get; set; }
        public double rawX { get; set; }
        public double rawY { get; set; }
        public double rawZ { get; set; }
        public double CorrectedPercentX { get; set; }
        public double CorrectedPercentY { get; set; }
        public double CorrectedPercentZ { get; set; }
        public double XinG { get; set; }
        public double YinG { get; set; }
        public double ZinG { get; set; }
        public Impinj.OctaneSdk.TagData Tid { get; set; }
        public double RfDopplerFrequency { get; set; }
        public double PhaseAngleInRadians { get; set; }
        public ushort Crc { get; set; }
        public ushort PcBits { get; set; }
        public Impinj.OctaneSdk.GpsCoordinates GpsCoordinates { get; set; }
        public Impinj.OctaneSdk.TagModelDetails ModelDetails { get; set; }
		public string FirstSeenTimeLoc { get; set; }
        public string timeref { get; set; }
        public double trfid { get; set; }
        public double tref { get; set; }
        //construct result from tag

        public RFIDResult(string rdr, int n, string m, Tag tt)
        {
            sequenceno = n;
            ts = tt.FirstSeenTime.LocalDateTime.ToUniversalTime().ToString("o");
            mode = m;
            Epc = tt.Epc.ToHexString();
            readerID = rdr;
            AntennaPortNumber = tt.AntennaPortNumber;
            ChannelInMhz = tt.ChannelInMhz;
            FirstSeenTime = tt.FirstSeenTime.Utc;
            LastSeenTime = tt.LastSeenTime.Utc;
            PeakRSSIInDbm = tt.PeakRssiInDbm;
            TagSeenCount = tt.TagSeenCount;
            Tid = tt.Tid;
            RfDopplerFrequency = tt.RfDopplerFrequency;
            PhaseAngleInRadians = tt.PhaseAngleInRadians;
            Crc = tt.Crc;
            PcBits = tt.PcBits;
            GpsCoordinates = tt.GpsCoodinates;
            ModelDetails = tt.ModelDetails;
			FirstSeenTimeLoc = tt.FirstSeenTime.LocalDateTime.ToString("o");
            FirstSeenTimeLoc = FirstSeenTimeLoc.Remove(FirstSeenTimeLoc.Length - 10);
            FirstSeenTimeLoc = FirstSeenTimeLoc.Substring(FirstSeenTimeLoc.Length - 12);
            string[] word1 = FirstSeenTimeLoc.Split(':');
            trfid = Convert.ToDouble(word1[0]) * 60 * 60 * 1000 + Convert.ToDouble(word1[1]) * 60 * 1000 + Convert.ToDouble(word1[2]) * 1000;
			timeref = DateTime.Now.ToString("o");
            timeref = timeref.Remove(timeref.Length - 10);
            timeref = timeref.Substring(timeref.Length - 12);
            string[] words = timeref.Split(':');
            tref = Convert.ToDouble(words[0]) * 60 * 60 * 1000 + Convert.ToDouble(words[1]) * 60 * 1000 + Convert.ToDouble(words[2]) * 1000;


        }
        //Turn into a string
        public string makeMeAString()
        {
            string res = "";
            res += sequenceno.ToString();
            res += ",";
            //res += ts.ToString();
            //res += ",";
            //res += mode.ToString();
            //res += ',';
            res += tagID.ToString();
            res += ',';
            //res += Epc.ToString();
            //res += ',';
            //res += readerID.ToString();
            //res += ',';

            res += AntennaPortNumber.ToString();
            res += ',';
            res += ChannelInMhz.ToString();
            res += ',';
            //res += rawX.ToString();
            //res += ',';
            //res += rawY.ToString();
            //res += ',';
            //res += rawZ.ToString();
            //res += ',';
            //res += CorrectedPercentX.ToString();
            //res += ',';
            //res += CorrectedPercentY.ToString();
            //res += ',';
            //res += CorrectedPercentZ.ToString();
            //res += ',';
            //res += XinG.ToString();
            //res += ',';
            //res += YinG.ToString();
            //res += ',';
            //res += ZinG.ToString();
            //res += ',';
            res += trfid;
            res += ',';
            //res += FirstSeenTime.ToString();
            //res += ',';
            //res += LastSeenTime.ToString();
            //res += ',';
            res += PeakRSSIInDbm.ToString();
            res += ',';
            //res += TagSeenCount.ToString();
            //res += ',';
            res += Tid.ToString();
            res += ',';
            res += RfDopplerFrequency.ToString("F");
            res += ',';
            res += PhaseAngleInRadians.ToString("F");
			res += ',';
            res += tref;
            //res += ',';
            //res += Crc.ToString();
            //res += ',';
            //res += PcBits.ToString();
            //res += ',';
            //res += GpsCoordinates.ToString();
            //res += ',';
            //res += ModelDetails.ModelName;
            return res;
        }
        //start CP from teck
        public void AnalysisEPC()
        {
            string tagType = this.Epc.Substring(0, 2);
            if (tagType.Equals("0B"))
            {
                this.tagID = this.Epc.Substring(this.Epc.Length - 6, 6);

                // accel data (raw)
                string data = "";
                data = this.Epc.Substring(6, 4);
                this.rawX = Convert.ToInt32(data, 16);
                data = this.Epc.Substring(2, 4);
                this.rawY = Convert.ToInt32(data, 16);
                data = this.Epc.Substring(10, 4);
                this.rawZ = Convert.ToInt32(data, 16);

                // accel data (corrected)
                this.CorrectedPercentX = this.rawX;
                this.CorrectedPercentX = (this.CorrectedPercentX < 0 || this.CorrectedPercentX > 1024) ? 0 : this.CorrectedPercentX;
                this.CorrectedPercentX = 100.0f * this.CorrectedPercentX / 1024.0f;
                this.CorrectedPercentX = 100 - this.CorrectedPercentX;

                this.CorrectedPercentY = this.rawY;
                this.CorrectedPercentY = (this.CorrectedPercentY < 0 || this.CorrectedPercentY > 1024) ? 0 : this.CorrectedPercentY;
                this.CorrectedPercentY = 100.0f * this.CorrectedPercentY / 1024.0f;
                this.CorrectedPercentY = 100 - this.CorrectedPercentY;

                this.CorrectedPercentZ = this.rawZ;
                this.CorrectedPercentZ = (this.CorrectedPercentZ < 0 || this.CorrectedPercentZ > 1024) ? 0 : this.CorrectedPercentZ;
                this.CorrectedPercentZ = 100.0f * this.CorrectedPercentZ / 1024.0f;

                // different tags should have different corrections
                if (this.tagID == "410104")
                {
                    this.CorrectedPercentX *= 0.888888889f;
                    this.CorrectedPercentY *= 0.870748299f;
                    this.CorrectedPercentZ *= 1.071020408f;
                }
                else if (tagID == "410105")
                {
                    this.CorrectedPercentX *= 0.878216123f;
                    this.CorrectedPercentY *= 0.869269949f;
                    this.CorrectedPercentZ *= 1.113633952f;
                }

                // accel value in g
                this.XinG = this.CorrectedPercentX * 0.142857f - 7.1428f;
                this.YinG = this.CorrectedPercentY * 0.142857f - 7.1428f;
                this.ZinG = this.CorrectedPercentZ * 0.1111f - 5.55f;
            }
            else
            {
                this.tagID = this.Epc;
            }
        }
        //end CP from teck
    }
}
