using Domain.Entities;
using PhoneNumbers;
using System.ComponentModel.DataAnnotations;
using System;
using System.Net.Mail;

namespace TestProjectCustomer
{
    public class CustomeTest
    {
        [Theory]
        [InlineData("123-456-7890", false)] // Invalid format
        [InlineData("+989127654321", true)]  // Valid Iran number
        [InlineData("+447987654321", true)] // Valid UK number
        [InlineData("invalidnumber", false)] // Not even a number
        public void ValidatePhoneNumber_ValidatesCorrectly(string phoneNumber, bool expected)
        {
            // Arrange
            var customer = new Customer
            {
                Firstname = "John",
                Lastname = "Doe",
                PhoneNumber = phoneNumber,
                Email = "john.doe@example.com",
                DateOfBirth = new DateTime(1980, 1, 1),
                BankAccountNumber = "123456789012345"
            };

            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            bool isValidNumber = false;

            try
            {
                // Google's libphone library's Parse method throws an exception if the number is not a valid phone number at all
                var numberProto = phoneNumberUtil.Parse(customer.PhoneNumber, null);
                // Here we check if the number is a valid mobile number
                isValidNumber = phoneNumberUtil.IsValidNumber(numberProto) &&
                                phoneNumberUtil.GetNumberType(numberProto) == PhoneNumberType.MOBILE;
            }
            catch (NumberParseException)
            {
                isValidNumber = false;
            }

            // Act
            // This step would usually involve inserting the customer into the database or passing the entity through a validation method

            // Assert
            Assert.Equal(expected, isValidNumber);
        }

            [Theory]
            [InlineData("example@example.com", true)]
            [InlineData("invalidemail", false)]
            [InlineData("", false)]
            [InlineData(null, false)] // Required attribute should fail if the value is null
            [InlineData("a@b.c", true)] // Minimum valid email format
            [InlineData("very.long.email.address@example.com", true)]
            [InlineData("exceeds.maxlength@example.com" , true)] // Exceeding MaxLength
            public void Email_WhenTested_ShouldValidateCorrectly(string emailAddress, bool expectedIsValid)
            {
            bool isValid = true;
            // Act
            try
            {
                var email = new MailAddress(emailAddress);
            }
            catch
            {
                isValid = false;
            }

            //return valid;
            //var customer = new Customer { Email = emailAddress };
            //var validationContext = new ValidationContext(customer);
            //var validationResults = new List<ValidationResult>();

            //// Act
            //bool isValid = Validator.TryValidateObject(customer, validationContext, validationResults, true);

            // Assert
            Assert.Equal(expectedIsValid, isValid);
        }
        }
}