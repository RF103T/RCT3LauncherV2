using Markdig;
using Neo.Markdig.Xaml;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using RCT3Launcher.Classes.MessageBox;

namespace RCT3Launcher.Views.MessageBoxPages
{
	/// <summary>
	/// 可以显示Markdown的对话框
	/// </summary>
	public partial class MarkDownMessageBoxPage : MessageBoxPage
	{
		private string markDownBody;

		public string MarkDownBody
		{
			get => markDownBody;
			set
			{
				if (markDownBody != value)
				{
					markDownBody = value;
					LoadMarkDown(MarkDownBody);
				}
			}
		}

		public MarkDownMessageBoxPage()
		{
			InitializeComponent();
		}

		public static MarkDownMessageBoxPage Create(string markDownBody)
		{
			MarkDownMessageBoxPage instance = MessageBoxPageFactory.GetInstance<MarkDownMessageBoxPage>();
			instance.MarkDownBody = markDownBody;
			return instance;
		}

		public async void LoadMarkDown(string markDownBody)
		{
			progressBar.Visibility = Visibility.Visible;
			string xaml = "";
			await Task.Run(() =>
			{
				xaml = MarkdownXaml.ToXaml(markDownBody, new MarkdownPipelineBuilder().UseXamlSupportedExtensions().Build());
			});
			richTextBox.Document = XamlReader.Parse(xaml) as FlowDocument;
			progressBar.Visibility = Visibility.Hidden;
		}
	}
}
