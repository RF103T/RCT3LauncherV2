using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using RCT3Launcher.Models;

namespace RCT3Launcher.Views.Pages
{
	/// <summary>
	/// GuidePage.xaml 的交互逻辑
	/// </summary>
	public partial class GuidePage : Page
	{
		private BindingExpression applyButtonIsEnableBinding;

		private HashSet<object> errorSet = new HashSet<object>();

		public GuidePage()
		{
			InitializeComponent();
			applyButtonIsEnableBinding = applyButton.GetBindingExpression(Button.IsEnabledProperty);
		}

		private void PreferencesValidationError(object sender, ValidationErrorEventArgs e)
		{
			if (e.Action == ValidationErrorEventAction.Added)
				errorSet.Add((e.OriginalSource as FrameworkElement).DataContext);
			else
				errorSet.Remove((e.OriginalSource as FrameworkElement).DataContext);
			UpdateApplyButton();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			errorSet.Remove((sender as FrameworkElement).DataContext);
			UpdateApplyButton();
		}

		private void UpdateApplyButton()
		{
			if (errorSet.Count == 0)
				applyButton.SetBinding(Button.IsEnabledProperty, applyButtonIsEnableBinding.ParentBindingBase);
			else
				applyButton.IsEnabled = false;
		}
	}
}
