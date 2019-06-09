namespace PiWol.WakeOnLanService.Abstraction.Validators
{
    public interface ISubnetMaskValidator
    {
        bool IsValid(string netmask);
    }
}