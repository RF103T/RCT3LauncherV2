using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RCT3Launcher.Views.ViewHelpers.ValidationRules
{
	public class ValueNotNullRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			if (value != null && value.ToString() != string.Empty)
				return ValidationResult.ValidResult;
			return new ValidationResult(false, App.Current.Resources["ValidationRule_ValueNullError"]);
		}

		public static bool _validate(object value)
		{
			if (value != null && value.ToString() != string.Empty)
				return true;
			return false;
		}
	}
}
