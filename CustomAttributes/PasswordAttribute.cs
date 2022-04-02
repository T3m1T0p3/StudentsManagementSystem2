using StudentManagementSystem2.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem2.CustomAttributes
{
    public class PasswordAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object option,ValidationContext context)
        {
            var credential= (LoginCredential)context.ObjectInstance;
            if (credential.Password.Length < 8 || credential.Password.Length > 12)
            {
                return new ValidationResult("Invalid Password");
            }
            return ValidationResult.Success;
        }
    }
}
