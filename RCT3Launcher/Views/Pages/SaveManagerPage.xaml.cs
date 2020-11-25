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
		public SaveManagerPage()
		{
			InitializeComponent();
		}

		private void multipleModeCheckBox_Checked(object sender, RoutedEventArgs e)
		{
			listBox.SelectionMode = SelectionMode.Multiple;
		}

		private void multipleModeCheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			listBox.SelectionMode = SelectionMode.Single;
		}
	}
}
