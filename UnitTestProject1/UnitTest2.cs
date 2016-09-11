using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            Test test = new Test();
            test.Name = "123";
        }
    }




    public class MyClass : IValidatableObject
    {
        public bool IsReminderChecked { get; set; }
        public bool EmailAddress { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IsReminderChecked)
            {
                // How can I validate the EmailAddress field using
                // the Custom Validation Attribute found below?
            }
            return null;
        }
    }


    // Custom Validation Attribute - used in more than one place
    public class EmailValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var email = value as string;

            if (string.IsNullOrEmpty(email))
                return false;

            try
            {
               
            }
            catch (FormatException)
            {
                return false;
            }

            return true;
        }
    }


    public class Test : IValidatableObject
    {
        [EmailValidationAttribute()]
        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new ValidationResult("Error 1");
            var list = new List<ValidationResult>();
            list.Add(result);
            return list;
        }
    }



    public interface IValidationService
    {
        bool DoesPlaceExist(string place);
    }

    public class RedisValidationService : IValidationService
    {
        public bool DoesPlaceExist(string place)
        {
            if (place.Equals("123"))
                return true;

            return false;
        }
    }

    public class PlaceValidationAttribute : ValidationAttribute
    {
        private readonly IValidationService _service;

        public PlaceValidationAttribute(IValidationService service)
        {
            _service = service;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var isValid = _service.DoesPlaceExist(value as string);
            return ValidationResult.Success;
        }
    }
}
