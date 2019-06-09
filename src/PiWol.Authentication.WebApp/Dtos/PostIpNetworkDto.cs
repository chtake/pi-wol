using PiWol.Authentication.WebApp.Validators;

namespace PiWol.Authentication.WebApp.Dtos
{
    public class PostIpNetworkDto
    {
        [NetworkValidator]
        public string IpNetwork { get; set; }
    }
}