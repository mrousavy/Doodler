using System.Globalization;
using System.Windows.Controls;

namespace Doodler.Implementation
{
    /// <summary>
    ///     mrousavy: A validation rule checking if a string is not empty
    /// </summary>
    public class NotEmptyValidationRule : ValidationRule
    {
        private const string InvalidMessage = ""; // "Can't be empty!";

        private const string InvalidTypeMessage = ""; // "Please enter a valid string!";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string s)
                return new ValidationResult(!string.IsNullOrWhiteSpace(s), InvalidMessage);
            return new ValidationResult(false, InvalidTypeMessage);
        }
    }
}