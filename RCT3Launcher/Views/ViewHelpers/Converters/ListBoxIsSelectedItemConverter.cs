using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RCT3Launcher.Views.ViewHelpers.Converters
{
	[ValueConversion(typeof(int), typeof(bool))]
	class ListBoxIsSelectedItemConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			int index = System.Convert.ToInt32(value);
			return index != -1; 
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
