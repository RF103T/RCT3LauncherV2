using RCT3Launcher.Classes.MessageBox;
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
	public partial class TextMessageBoxPage : MessageBoxPage
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

		public TextMessageBoxPage()
		{
			InitializeComponent();
		}

		public static TextMessageBoxPage Create(string text)
		{
			TextMessageBoxPage instance = MessageBoxPageFactory.GetInstance<TextMessageBoxPage>();
			instance.Text = text;
			return instance;
		}
	}
}
