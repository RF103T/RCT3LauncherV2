using RCT3Launcher.ViewModels.BaseClass;
using RCT3Launcher.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using RCT3Launcher.FileSystemWatchers;

namespace RCT3Launcher
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public SaveFileWatcher saveFileWather;

		protected override void OnStartup(StartupEventArgs e)
		{
			saveFileWather = new SaveFileWatcher();
		}
	}
}
