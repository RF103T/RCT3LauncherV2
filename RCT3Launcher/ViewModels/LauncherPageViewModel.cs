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
		private int gameInstallationSelectedIndex;
		public int GameInstallationSelectedIndex
		{
			get
			{
				return gameInstallationSelectedIndex;
			}
			set
			{
				gameInstallationSelectedIndex = value < 0 ? 0 : value;
				RaisePropertyChanged(nameof(GameInstallationSelectedIndex));
			}
		}

		public CommandBase<GameInstallation> LaunchGameCommand
		{
			get
			{
				if (launchGameCommand == null)
				{
					launchGameCommand = new CommandBase<GameInstallation>(
							installation =>
							{
								if (GlobalStateHelper.HasGameRunning)
								{
									MessageBox.Show(res =>
									{
										if (res == MessageBoxResult.Yes)
											installation.GameProcess = Process.Start(installation.GameFileFullName);
									},
									new TextMessageBoxPage(Application.Current.Resources["LauncherPage_Repeat_Run_Warning_MessageBoxText"].ToString()),
									Application.Current.Resources["Text_Warning"].ToString(), MessageBoxButton.YesNo);
								}
								else
									installation.GameProcess = Process.Start(installation.GameFileFullName);
							}
						);
				}
				return launchGameCommand;
			}
		}
		private CommandBase<GameInstallation> launchGameCommand;
	}
}
