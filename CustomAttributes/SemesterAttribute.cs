using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem2.CustomAttributes
{
    //[AttributeUsage()]
    sealed class SemesterAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object option, ValidationContext context)
        {
            var semester = (string)context.ObjectInstance;

            if (semester != "first" || semester != "second")
            {
                return new ValidationResult("Semester may either be firsts or second");
            }

            return ValidationResult.Success;
        }
    }
}
