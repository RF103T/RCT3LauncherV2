using RCT3Launcher.Models;
using RCT3Launcher.Option;
using RCT3Launcher.Option.LauncherOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCT3Launcher
{
	class GlobalStateHelper
	{
		public static bool HasGameRunning
		{
			get
			{
				IEnumerable<GameInstallation> gameInstallationItems = OptionsManager.Instance.GetOptionObject<GameInstallationsOption>(OptionType.GameInstallation).Value;
				foreach (GameInstallation installation in gameInstallationItems)
					if (!installation.IsGameProcessExited)
						return true;
				return false;
			}
		}

		public static GameInstallation RunningGameInfo
		{
			get
			{
				IEnumerable<GameInstallation> gameInstallationItems = OptionsManager.Instance.GetOptionObject<GameInstallationsOption>(OptionType.GameInstallation).Value;
				foreach (GameInstallation installation in gameInstallationItems)
					if (!installation.IsGameProcessExited)
						return installation;
				return null;
			}
		}
	}
}
