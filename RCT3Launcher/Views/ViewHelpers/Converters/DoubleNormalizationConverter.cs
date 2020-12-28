using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RCT3Launcher.Views.ViewHelpers.Converters
{
	[ValueConversion(typeof(double), typeof(double))]
	class DoubleNormalizationConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			double v = System.Convert.ToDouble(value);
			return v / System.Convert.ToDouble(parameter);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
