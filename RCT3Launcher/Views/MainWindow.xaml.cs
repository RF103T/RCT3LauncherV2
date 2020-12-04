using System.Windows;
using System.Windows.Input;

namespace RCT3Launcher
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			NavigationCommands.BrowseBack.InputGestures.Clear();
			InitializeComponent();
		}
	}
}
