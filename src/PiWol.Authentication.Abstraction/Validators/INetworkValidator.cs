namespace PiWol.Authentication.Abstraction.Validators
{
    public interface INetworkValidator
    {
        bool IsValid(string ipNetwork);
    }
}