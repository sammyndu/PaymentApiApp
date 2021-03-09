using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Helpers
{
    public class CreditCardValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                long creditCard = long.Parse(value.ToString());

                if (CreditCardVerification.IsValid(creditCard))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Invalid Credit Card");
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + " is required");
            }
        }
    }

}
