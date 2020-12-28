using System;
using System.Windows;
using System.Windows.Controls;

namespace RCT3Launcher.ControlExtensions
{
	public class ScrollViewerExtensions : ScrollViewer
	{
		public static readonly DependencyProperty AlwaysScrollToEndProperty = DependencyProperty.RegisterAttached("AlwaysScrollToEnd", typeof(bool), typeof(ScrollViewerExtensions), new FrameworkPropertyMetadata(false, AlwaysScrollToEndChanged));
		private static bool _autoScroll;

		private static void AlwaysScrollToEndChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (sender is ScrollViewer scroll)
			{
				bool alwaysScrollToEnd = (e.NewValue != null) && (bool)e.NewValue;
				if (alwaysScrollToEnd)
				{
					scroll.ScrollToEnd();
					scroll.ScrollChanged += _ScrollChanged;
				}
				else
				{
					scroll.ScrollChanged -= _ScrollChanged;
				}
			}
			else
				throw new InvalidOperationException("The attached AlwaysScrollToEnd property can only be applied to ScrollViewer instances.");
		}

		public static bool GetAlwaysScrollToEnd(ScrollViewer scroll)
		{
			if (scroll == null)
				throw new ArgumentNullException("scroll");
			return (bool)scroll.GetValue(AlwaysScrollToEndProperty);
		}


		public static void SetAlwaysScrollToEnd(ScrollViewer scroll, bool alwaysScrollToEnd)
		{
			if (scroll == null)
				throw new ArgumentNullException("scroll");
			scroll.SetValue(AlwaysScrollToEndProperty, alwaysScrollToEnd);
		}


		private static void _ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			if (!(sender is ScrollViewer scroll))
				throw new InvalidOperationException("The attached AlwaysScrollToEnd property can only be applied to ScrollViewer instances.");
			if (e.ExtentHeightChange == 0)
				_autoScroll = scroll.VerticalOffset == scroll.ScrollableHeight;
			if (_autoScroll && e.ExtentHeightChange != 0)
				scroll.ScrollToVerticalOffset(scroll.ExtentHeight);
		}
	}
}
