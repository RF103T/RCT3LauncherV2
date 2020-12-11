using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RCT3Launcher.Views.ViewHelpers.ValidationRules
{
	public class FilePathValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			Regex regex = new Regex(@"^[a-zA-Z]:(((\\(?! )[^/:*?<>\""|\\]+)+\\?)|(\\)?)\s*$");
			if (regex.IsMatch(value.ToString()))
				return ValidationResult.ValidResult;
			return new ValidationResult(false, Application.Current.Resources["ValidationRule_FilePathError"]);
		}
	}
}
