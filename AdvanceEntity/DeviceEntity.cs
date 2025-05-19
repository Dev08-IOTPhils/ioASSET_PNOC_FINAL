namespace AdvanceEntity
{
    public class DeviceEntity : BaseEntity
    {
        public bool Active { get; set; }

        public string Description { get; set; }

        //Device
        public int DeviceID { get; set; }

        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public bool DeviceTypeActive { get; set; }
        public string DeviceTypeDescription { get; set; }
        //Device Type
        public int DeviceTypeID { get; set; }

        public string DeviceUsageType { get; set; }
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public string PortString { get; set; }
    }
}