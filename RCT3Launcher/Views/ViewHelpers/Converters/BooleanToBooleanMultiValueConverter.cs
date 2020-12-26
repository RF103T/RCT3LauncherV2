using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace RCT3Launcher.Views.ViewHelpers.Converters
{
	public class BooleanToBooleanMultiValueConverter : IMultiValueConverter
	{
		public bool IsReverse { get; set; }

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			bool temp = true;
			foreach (object value in values)
				if (value != DependencyProperty.UnsetValue)
					temp &= System.Convert.ToBoolean(value);
			return IsReverse ^ temp;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
