using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using PiWol.WakeOnLanService.Abstraction.Validators;

namespace PiWol.WakeOnLanService.WebApp.Attributes
{
    public class SubnetMaskValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var validator = validationContext.GetService<ISubnetMaskValidator>();

            if (validator.IsValid(value.ToString()) == false)

            {
                return new ValidationResult("Netmask is not valid.");
            }

            return ValidationResult.Success;
        }
    }
}