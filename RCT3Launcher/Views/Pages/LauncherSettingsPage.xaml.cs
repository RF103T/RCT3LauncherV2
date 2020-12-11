using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RCT3Launcher.Views.Pages
{
	/// <summary>
	/// LauncherSettingsPage.xaml 的交互逻辑
	/// </summary>
	public partial class LauncherSettingsPage : Page
	{
		private HashSet<object> errorSet = new HashSet<object>();

		public LauncherSettingsPage()
		{
			InitializeComponent();
		}

		private void SettingsValidationError(object sender, ValidationErrorEventArgs e)
		{
			if (e.Action == ValidationErrorEventAction.Added)
				errorSet.Add((e.OriginalSource as FrameworkElement).DataContext);
			else
				errorSet.Remove((e.OriginalSource as FrameworkElement).DataContext);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			errorSet.Remove((sender as FrameworkElement).DataContext);
		}
	}
}
