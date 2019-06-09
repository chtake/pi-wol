using System;

namespace PiWol.WakeOnLanService.Abstraction.Models
{
    public class HostModel
    {
        public Guid Id { get; set; }

        public string Hostname { get; set; }

        public string IpAddress { get; set; }

        public string MacAddress { get; set; }

        public string Netmask { get; set; }

        public HostStatus Status { get; set; }
    }
}