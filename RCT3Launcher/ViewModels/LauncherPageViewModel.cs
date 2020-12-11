using RCT3Launcher.Models;
using RCT3Launcher.ViewModels.BaseClass;
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
									if (MessageBox.Show("存在正在运行的游戏实例，确定要继续运行？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.No)
										return;
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
