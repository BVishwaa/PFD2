using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using EldenGuide.Controllers;
using EldenGuide.DAL;

namespace EldenGuide.Models
{
    public class UserEmailValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Get the email value to validate
            string email = value as string;

            // Get an instance of the AuthDAL class
            AuthDAL authContext = (AuthDAL)validationContext.GetService(typeof(AuthDAL));

            // Check if the email already exists in the database
            bool emailExists = authContext.CheckEmailExists(email); // Assuming you have this method in AuthDAL

            if (emailExists)
            {
                // Email already exists
                return new ValidationResult("Email address already exists!");
            }
            else
            {
                // Email is unique
                return ValidationResult.Success;
            }
        }
        //protected override ValidationResult IsValid(
        //    object value, ValidationContext validationContext)
        //{
        //    // Get the email value to validate
        //    string email = Convert.ToString(value);
        //    // Get an instance of the CustDAL class
        //    var userContext = new AuthDAL();

        //    // Casting the validation context to the "CustRegistrationModel" model class
        //    User RegisterCustomer = (User)validationContext.ObjectInstance;
        //    // Get the Member Id from the member instance
        //    int memberID = RegisterCustomer.MemberID;
        //    if (custContext.IsEmailExist(email, memberID))
        //    {
        //        // validation failed
        //        return new ValidationResult
        //        ("Email address already exists!");
        //    }
        //    else
        //    {
        //        // validation passed
        //        return ValidationResult.Success;
        //    }

        //}
    }
}
