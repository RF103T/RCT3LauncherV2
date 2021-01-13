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
	/// SaveManagerPage.xaml 的交互逻辑
	/// </summary>
	public partial class SaveManagerPage : Page
	{
		private HashSet<object> errorSet = new HashSet<object>();

		public SaveManagerPage()
		{
			InitializeComponent();
		}

		private void SaveNameValidationError(object sender, ValidationErrorEventArgs e)
		{
			if (e.Action == ValidationErrorEventAction.Added)
				errorSet.Add((e.OriginalSource as FrameworkElement).DataContext);
			else
				errorSet.Remove((e.OriginalSource as FrameworkElement).DataContext);
		}

		private void multipleModeCheckBox_Checked(object sender, RoutedEventArgs e)
		{
			listBox.SelectionMode = SelectionMode.Multiple;
			UpdateListBoxStyle();
		}

		private void multipleModeCheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			listBox.SelectionMode = SelectionMode.Single;
			UpdateListBoxStyle();
		}

		private void dataShowStyleSwitchBox_Checked(object sender, RoutedEventArgs e)
		{
			listBox.Tag = "Grid";
			UpdateListBoxStyle();
		}

		private void dataShowStyleSwitchBox_Unchecked(object sender, RoutedEventArgs e)
		{
			listBox.Tag = "List";
			UpdateListBoxStyle();
		}

		private void UpdateListBoxStyle()
		{
			StringBuilder builder = new StringBuilder(listBox.Tag.ToString());
			if (listBox.SelectionMode == SelectionMode.Single)
				builder.Append("_ListBox_Style");
			else
				builder.Append("_Multiple_ListBox_Style");
			listBox.SetValue(ListBox.StyleProperty, App.Current.Resources[builder.ToString()]);
		}
	}
}
