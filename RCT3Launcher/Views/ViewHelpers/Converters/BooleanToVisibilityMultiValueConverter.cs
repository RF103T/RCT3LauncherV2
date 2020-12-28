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
	public class BooleanToVisibilityMultiValueConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			bool temp = false;
			foreach (object value in values)
				temp |= System.Convert.ToBoolean(value);
			return !temp ? Visibility.Collapsed : Visibility.Visible;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
