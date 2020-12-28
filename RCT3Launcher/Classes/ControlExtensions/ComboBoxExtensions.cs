using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RCT3Launcher.ControlExtensions
{
	public class ComboBoxExtensions
	{
		public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached("CornerRadius",
			typeof(CornerRadius), typeof(ComboBoxExtensions),
			new PropertyMetadata(new CornerRadius(0), (obj, e) =>
			{
				if (obj is ComboBox combo)
				{
					if (combo.OpacityMask == null)
					{
						Border border = new Border
						{
							Background = Brushes.White
						};
						VisualBrush brush = new VisualBrush
						{
							Visual = border
						};
						combo.OpacityMask = brush;
						combo.SizeChanged += delegate
						{
							border.Width = combo.ActualWidth;
							border.Height = combo.ActualHeight;
						};
					}
					if (combo.OpacityMask is VisualBrush vb)
						if (vb.Visual is Border bd)
							bd.CornerRadius = (CornerRadius)e.NewValue;
				}
			}
		));

		public static CornerRadius GetCornerRadius(DependencyObject obj)
		{
			return (CornerRadius)obj.GetValue(CornerRadiusProperty);
		}

		public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
		{
			obj.SetValue(CornerRadiusProperty, value);
		}
	}
}
