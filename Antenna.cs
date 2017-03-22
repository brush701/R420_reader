namespace RFIDReader
{
    public class Antenna
    {
        public ushort ID { get; private set; }
        public bool enabled { get; set; }
        public ushort power { get; set; }
        public bool maxSensitivity { get; set; }

        public Antenna(ushort ID, bool enabled = true, ushort power = 30, bool maxSensitivity = true)
        {
            this.ID = ID;
            this.enabled = enabled;
            this.power = power;
            this.maxSensitivity = maxSensitivity;
        }
    }
}