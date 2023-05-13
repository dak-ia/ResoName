using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace ResoName
{
    public class FileNameRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Debug.Print("input");
            if (!Regex.IsMatch((string)value, "([\\\\\\/\\:\\*\\?\\\"\\>\\<\\|])"))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, null);
            }
        }
    }
}