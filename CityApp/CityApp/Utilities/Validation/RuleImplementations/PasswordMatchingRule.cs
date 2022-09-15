using System;
using CityApp.Utilities.Validation.Abstractions;

namespace CityApp.Utilities.Validation.Rules
{
    public class PasswordMatchingRule : IValidationRule<string>
    {
        public string PasswordToCompare { get; set; }

        public string ValidationMessage { get; set; }
        public bool Check(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            return string.Equals(value, PasswordToCompare, StringComparison.Ordinal);
        }
    }
}
