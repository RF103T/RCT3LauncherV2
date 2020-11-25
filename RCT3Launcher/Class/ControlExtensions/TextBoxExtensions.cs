using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace RCT3Launcher.ControlExtensions
{
	class TextBoxExtensions
	{
		public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.RegisterAttached("PlaceholderText",
			typeof(string), typeof(TextBoxExtensions),
			new PropertyMetadata("", (obj, e) =>
			{

			}
		));

		public static string GetPlaceholderText(DependencyObject obj)
		{
			if (!(obj is TextBox))
				throw new ArgumentNullException();
			return (string)obj.GetValue(PlaceholderTextProperty);
		}

		public static void SetPlaceholderText(DependencyObject obj, string value)
		{
			if (!(obj is TextBox))
				throw new ArgumentNullException();
			obj.SetValue(PlaceholderTextProperty, value);
		}
	}
}
