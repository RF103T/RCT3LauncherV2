using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace RCT3Launcher.Views.ViewHelpers.Converters
{
	[ValueConversion(typeof(bool),typeof(DrawingImage))]
	class DrawingImageColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool v = System.Convert.ToBoolean(value);
			string str = parameter.ToString();
			if (v)
				return App.Current.Resources["Light_" + str];
			else
				return App.Current.Resources["Dart_" + str];
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
