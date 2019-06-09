using System;

namespace PiWol.WakeOnLanService.Data.Abstraction.Entities
{
    public class HostEntity
    {
        public Guid Id { get; set; }

        public string Hostname { get; set; }

        public string IpAddress { get; set; }

        public string MacAddress { get; set; }

        public string Netmask { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime LastModifiedDateTime { get; set; }
    }
}