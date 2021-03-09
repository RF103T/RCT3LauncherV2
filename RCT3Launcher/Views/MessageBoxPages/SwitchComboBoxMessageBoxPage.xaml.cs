using RCT3Launcher.Classes.MessageBox;
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
	public partial class SwitchComboBoxMessageBoxPage : MessageBoxPage, IValueMessageBoxPage<int>
	{
		public string Text
		{
			get => textBlock.Text;
			set
			{
				if (textBlock.Text != value)
					textBlock.Text = value;
			}
		}

		public SwitchComboBoxMessageBoxPage()
		{
			InitializeComponent();
		}

		public static SwitchComboBoxMessageBoxPage Create(string text, IEnumerable<SwitchComboBoxItemModel> comboBoxItems)
		{
			SwitchComboBoxMessageBoxPage instance = MessageBoxPageFactory.GetInstance<SwitchComboBoxMessageBoxPage>();
			instance.Text = text;
			foreach (SwitchComboBoxItemModel item in comboBoxItems)
				instance.switchComboBox.Items.Add(item);
			return instance;
		}

		public int GetReturnValue()
		{
			return switchComboBox.SelectedIndex;
		}
	}
}
