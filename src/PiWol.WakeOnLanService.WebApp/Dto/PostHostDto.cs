using System.ComponentModel.DataAnnotations;
using PiWol.WakeOnLanService.WebApp.Attributes;

namespace PiWol.WakeOnLanService.WebApp.Dto
{
    public class PostHostDto
    {
        [Required]
        public string Hostname { get; set; }

        [Required]
        [RegularExpression(@"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)")]
        public string IpAddress { get; set; }

        [Required]
        [SubnetMaskValidation]
        public string Netmask { get; set; }

        [RegularExpression(@"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$")]
        public string MacAddress { get; set; }
    }
}