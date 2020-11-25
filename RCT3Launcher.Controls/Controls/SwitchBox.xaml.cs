using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RCT3Launcher.Controls
{
	public partial class SwitchBox : UserControl
	{
		public string NegativeText
		{
			get { return (string)GetValue(NegativeTextProperty); }
			set { SetValue(NegativeTextProperty, value); }
		}
		public static readonly DependencyProperty NegativeTextProperty =
			DependencyProperty.Register("NegativeText", typeof(string), typeof(SwitchBox), new PropertyMetadata(""));

		public Brush NegativeForeground
		{
			get { return (Brush)GetValue(NegativeForegroundProperty); }
			set { SetValue(NegativeForegroundProperty, value); }
		}
		public static readonly DependencyProperty NegativeForegroundProperty =
			DependencyProperty.Register("NegativeForeground", typeof(Brush), typeof(SwitchBox), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 55, 71, 79))));

		public string PositiveText
		{
			get { return (string)GetValue(PositiveTextProperty); }
			set { SetValue(PositiveTextProperty, value); }
		}
		public static readonly DependencyProperty PositiveTextProperty =
			DependencyProperty.Register("PositiveText", typeof(string), typeof(SwitchBox), new PropertyMetadata(""));

		public bool IsChecked
		{
			get { return (bool)GetValue(IsCheckedProperty); }
			set { SetValue(IsCheckedProperty, value); }
		}
		public static readonly DependencyProperty IsCheckedProperty =
			DependencyProperty.Register("IsChecked", typeof(bool), typeof(SwitchBox), new PropertyMetadata(false));

		public SwitchBox()
		{
			InitializeComponent();
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			DoubleAnimation checkedDoubleAnimation = (DoubleAnimation)Template.FindName("CheckedDoubleAnimation", this);

			if (checkedDoubleAnimation == null)
				return;

			checkedDoubleAnimation.To = this.Width / 2;
		}
	}
}
