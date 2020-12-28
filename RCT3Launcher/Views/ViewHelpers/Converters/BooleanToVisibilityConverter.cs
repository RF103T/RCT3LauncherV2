using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace RCT3Launcher.Views.ViewHelpers.Converters
{
	[ValueConversion(typeof(bool), typeof(Visibility))]
	public class BooleanToVisibilityConverter : IValueConverter
	{
		public bool IsReverse { get; set; }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return IsReverse ^ System.Convert.ToBoolean(value) ? Visibility.Collapsed : Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Visibility visibility = (Visibility)value;
			return visibility != Visibility.Visible;
		}
	}
}
