using System.Globalization;
using System.Windows.Controls;

namespace Doodler.Implementation
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string s)
                return new ValidationResult(!string.IsNullOrWhiteSpace(s), "Can't be empty!");
            else
                return new ValidationResult(false, "Please enter a valid string!");
        }
    }
}
