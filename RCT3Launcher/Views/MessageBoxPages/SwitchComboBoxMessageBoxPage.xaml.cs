using RCT3Launcher.Controls.InternalDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RCT3Launcher.Views.MessageBoxPages
{
	/// <summary>
	/// SwitchComboBoxMessageBoxPage.xaml 的交互逻辑
	/// </summary>
	public partial class SwitchComboBoxMessageBoxPage : Page, IValueMessageBoxPage<int>
	{
		public SwitchComboBoxMessageBoxPage(string text, IEnumerable<SwitchComboBoxItemModel> comboBoxItems)
		{
			InitializeComponent();
			textBlock.Text = text;
			foreach (SwitchComboBoxItemModel item in comboBoxItems)
				switchComboBox.Items.Add(item);
		}

		public int GetReturnValue()
		{
			return switchComboBox.SelectedIndex;
		}
	}
}
