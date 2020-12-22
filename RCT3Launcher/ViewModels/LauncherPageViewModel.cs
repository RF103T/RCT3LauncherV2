using RCT3Launcher.Models;
using RCT3Launcher.ViewModels.BaseClass;
using RCT3Launcher.Views.MessageBoxPages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RCT3Launcher.ViewModels
{
	class LauncherPageViewModel : ViewModelBase
	{
		public CommandBase<GameInstallation> LaunchGameCommand
		{
			get
			{
				if (launchGameCommand == null)
				{
					launchGameCommand = new CommandBase<GameInstallation>(
						new Action<GameInstallation>(
							installation =>
							{
								if (GlobalStateHelper.HasGameRunnings)
								{
									MessageBox.Show(new Action<MessageBoxResult>(res =>
									{
										if (res == MessageBoxResult.Yes)
											installation.GameProcess = Process.Start(installation.GameFileFullName);
									}),
									new TextMessageBoxPage(Application.Current.Resources["LauncherPage_Repeat_Run_Warning_MessageBoxText"].ToString()),
									Application.Current.Resources["Text_Warning"].ToString(), MessageBoxButton.YesNo);
								}
								else
									installation.GameProcess = Process.Start(installation.GameFileFullName);
							}
							)
						);
				}
				return launchGameCommand;
			}
		}
		private CommandBase<GameInstallation> launchGameCommand;
	}
}
