using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using PiWol.Authentication.Abstraction.Validators;

namespace PiWol.Authentication.WebApp.Validators
{
    public class NetworkValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var validator = validationContext.GetService<INetworkValidator>();
            if (!validator.IsValid(value.ToString()))
            {
                return new ValidationResult("Network is not valid.");
            }

            return ValidationResult.Success;
        }
    }
}