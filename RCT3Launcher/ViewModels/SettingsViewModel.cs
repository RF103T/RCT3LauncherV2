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
using System.Windows.Controls;
using System.Windows.Media;

namespace RCT3Launcher.ViewModels
{
	class SettingsViewModel : ViewModelBase
	{
		public int LanguageSetting
		{
			get
			{
				return (int)OptionsManager.Instance.GetOptionObject<LanguageOption>(OptionType.Language).Value;
			}
		}

		private GameInstallationsOption gameInstallationsOption = null;
		public ObservableCollection<GameInstallation> GameInstallationItems
		{
			get
			{
				if (gameInstallationsOption == null)
					gameInstallationsOption = OptionsManager.Instance.GetOptionObject<GameInstallationsOption>(OptionType.GameInstallation);
				return gameInstallationsOption.Value;
			}
			set
			{
				if (gameInstallationsOption == null)
					gameInstallationsOption = OptionsManager.Instance.GetOptionObject<GameInstallationsOption>(OptionType.GameInstallation);
				if (!gameInstallationsOption.Value.Equals(value))
				{
					gameInstallationsOption.Value = value;
					RaisePropertyChanged(nameof(GameInstallationItems));
				}
			}
		}

		public List<DrawingImage> GameInstallationItemIcons
		{
			get
			{
				return GameInstallationIconHelper.DarkIconDrawingImages;
			}
		}

		private CommandBase<GameInstallation> deleteGamePathCommand;
		public CommandBase<GameInstallation> DeleteGamePathCommand
		{
			get
			{
				if (deleteGamePathCommand == null)
				{
					deleteGamePathCommand = new CommandBase<GameInstallation>(
						new Action<GameInstallation>(
							item =>
							{
								int id = item.ID;
								for (int i = id; i < GameInstallationItems.Count; i++)
									GameInstallationItems[i].ID--;
								GameInstallationItems.Remove(item);
							}
						)
					);
				}
				return deleteGamePathCommand;
			}
		}

		private CommandBase<TextBox> chooseGamePathCommand;
		public CommandBase<TextBox> ChooseGamePathCommand
		{
			get
			{
				if (chooseGamePathCommand == null)
				{
					chooseGamePathCommand = new CommandBase<TextBox>(
						new Action<TextBox>(
							textBox =>
							{
								CommonOpenFileDialog dialog = new CommonOpenFileDialog()
								{
									IsFolderPicker = true
								};
								if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
									textBox.SetValue(TextBox.TextProperty, dialog.FileName);
							}
						)
					);
				}
				return chooseGamePathCommand;
			}
		}
	}
}
