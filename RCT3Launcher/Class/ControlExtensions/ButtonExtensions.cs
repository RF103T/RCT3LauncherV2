using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RCT3Launcher.ControlExtensions
{
	class ButtonExtensions
	{
		public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached("CornerRadius",
			typeof(CornerRadius), typeof(ButtonExtensions),
			new PropertyMetadata(new CornerRadius(0), (obj, e) =>
			{
				if (obj is Button button)
				{
					if (button.OpacityMask == null)
					{
						Border border = new Border
						{
							Background = Brushes.White
						};
						VisualBrush brush = new VisualBrush
						{
							Visual = border
						};
						button.OpacityMask = brush;
						button.SizeChanged += delegate
						{
							border.Width = button.ActualWidth;
							border.Height = button.ActualHeight;
						};
					}
					if (button.OpacityMask is VisualBrush vb)
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
