using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace File_manager.FileManager.ValidationRules
{
    public class PathRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string? path = value as string;
            if(path == null) 
            {
                return new ValidationResult(false, "Path cannot be none");
            }
            if (path == string.Empty)
                return ValidationResult.ValidResult;

            bool isDirectory = Directory.Exists(path);

            if(isDirectory)
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Specific path should be folder");
            }
        }
    }
}
