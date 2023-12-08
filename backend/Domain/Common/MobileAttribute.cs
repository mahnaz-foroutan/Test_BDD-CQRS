using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneNumbers;

namespace Domain.Common
{
    //public bool IsPhoneNumberMobile(string phoneNumber)
    //{
    //    var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
    //    try
    //    {
    //        var numberProto = phoneNumberUtil.Parse(phoneNumber, null);
    //        return phoneNumberUtil.IsValidNumber(numberProto) &&
    //               phoneNumberUtil.GetNumberType(numberProto) == PhoneNumbers.PhoneNumberType.MOBILE;
    //    }
    //    catch (PhoneNumbers.NumberParseException)
    //    {
    //        return false;
    //    }
    //}
    public class MobileAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string phoneNumber)
            {
                var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
                try
                {
                    var numberProto = phoneNumberUtil.Parse(phoneNumber, null);
                    if (phoneNumberUtil.IsValidNumber(numberProto) &&
                        phoneNumberUtil.GetNumberType(numberProto) == PhoneNumbers.PhoneNumberType.MOBILE)
                    {
                        return ValidationResult.Success;
                    }
                }
                catch (PhoneNumbers.NumberParseException)
                {
                    // Ignore exception and consider as invalid number.
                }
            }

            return new ValidationResult("Invalid mobile phone number"); // Customize the error message as needed.
        }
    }
}
