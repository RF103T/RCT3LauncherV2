using Markdig;
using Neo.Markdig.Xaml;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace RCT3Launcher.Views.MessageBoxPages
{
	/// <summary>
	/// 可以显示Markdown的对话框
	/// </summary>
	public partial class MarkDownMessageBoxPage : Page, IMessageBoxPage
	{
		public MarkDownMessageBoxPage(string markDownBody)
		{
			InitializeComponent();
			LoadMarkDown(markDownBody);
		}

		public async void LoadMarkDown(string markDownBody)
		{
			string xaml = "";
			await Task.Run(() =>
			{
				xaml = MarkdownXaml.ToXaml(markDownBody, new MarkdownPipelineBuilder().UseXamlSupportedExtensions().Build());
			});
			richTextBox.Document = XamlReader.Parse(xaml) as FlowDocument;
		}
	}
}
