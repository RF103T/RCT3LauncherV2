using Microsoft.WindowsAPICodePack.Dialogs;
using RCT3Launcher.Models;
using RCT3Launcher.Option;
using RCT3Launcher.Option.LauncherOptions;
using RCT3Launcher.ViewModels.BaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCT3Launcher.ViewModels
{
	class SettingsViewModel : ViewModelBase
	{
		public int LanguageSetting
		{
			get
			{
				return (int)OptionsManager.GetOptionObject<LanguageOption>(OptionsManager.OptionType.Language).Value;
			}
		}

		private GameInstallationsOption gameInstallationsOption = null;
		public ObservableCollection<GameInstallation> GameInstallationItems
		{
			get
			{
				if(gameInstallationsOption == null)
					gameInstallationsOption = OptionsManager.GetOptionObject<GameInstallationsOption>(OptionsManager.OptionType.GameInstallation);
				return gameInstallationsOption.Value;
			}
			set
			{
				if (gameInstallationsOption == null)
					gameInstallationsOption = OptionsManager.GetOptionObject<GameInstallationsOption>(OptionsManager.OptionType.GameInstallation);
				if (!gameInstallationsOption.Value.Equals(value))
				{
					gameInstallationsOption.Value = value;
					RaisePropertyChanged(nameof(GameInstallationItems));
				}
			}
		}

		private CommandBase<GameInstallation> chooseGamePathCommand;
		public CommandBase<GameInstallation> ChooseGamePathCommand
		{
			get
			{
				if (chooseGamePathCommand == null)
				{
					chooseGamePathCommand = new CommandBase<GameInstallation>(
						new Action<GameInstallation>(
							installation =>
							{
								CommonOpenFileDialog dialog = new CommonOpenFileDialog()
								{
									IsFolderPicker = true
								};
								if(dialog.ShowDialog() == CommonFileDialogResult.Ok)
									installation.FullNamePath = dialog.FileName;
							}
						)
					);
				}
				return chooseGamePathCommand;
			}
		}
	}
}
