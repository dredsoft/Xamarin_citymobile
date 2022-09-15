using System.Text.RegularExpressions;
using CityApp.Utilities.Validation.Abstractions;

namespace CityApp.Utilities.Validation.Implementations
{
    public class EmailRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;
            Regex regex = new Regex(CommonConstants.EmailRegex);
            Match match = regex.Match(str);

            return match.Success;
        }
    }
}
