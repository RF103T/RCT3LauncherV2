using RCT3Launcher.Controls;
using RCT3Launcher.Models;
using RCT3Launcher.Option;
using RCT3Launcher.Option.EventArgs;
using RCT3Launcher.Option.LauncherOptions;
using RCT3Launcher.ViewModels.BaseClass;
using RCT3Launcher.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace RCT3Launcher.ViewModels
{
	class GuidePageViewModel : ViewModelBase
	{
		public GuidePageViewModel()
		{

		}

		public ObservableCollection<GameInstallation> GameInstallationItems
		{
			get
			{
				SettingsViewModel settings = Application.Current.Resources["settingsViewModel"] as SettingsViewModel;
				return settings.GameInstallationItems;
			}
			set
			{
				SettingsViewModel settings = Application.Current.Resources["settingsViewModel"] as SettingsViewModel;
				settings.GameInstallationItems = value;
			}
		}

		private CommandBase<SwitchComboBox> languageSwitchComboBoxCommand;
		public CommandBase<SwitchComboBox> LanguageSwitchComboBoxCommand
		{
			get
			{
				if (languageSwitchComboBoxCommand == null)
				{
					languageSwitchComboBoxCommand = new CommandBase<SwitchComboBox>(
						new Action<SwitchComboBox>(
							self =>
							{
								LanguageOption setting = OptionsManager.GetOptionObject<LanguageOption>(OptionsManager.OptionType.Language);
								switch (self.SelectedIndex)
								{
									case 0:
									{
										setting.Value = LanguageOption.LanguageParameter.zh_CN;
										break;
									}
									case 1:
									{
										setting.Value = LanguageOption.LanguageParameter.en_US;
										break;
									}
								}
							}
							)
						);
				}
				return languageSwitchComboBoxCommand;
			}
		}

		private CommandBase<RoutedEventArgs> addNewGamePathCommand;
		public CommandBase<RoutedEventArgs> AddNewGamePathCommand
		{
			get
			{
				if (addNewGamePathCommand == null)
				{
					addNewGamePathCommand = new CommandBase<RoutedEventArgs>(
						new Action<RoutedEventArgs>(
							args => GameInstallationItems.Add(
								new GameInstallation()
								{
									Name = "配置" + (GameInstallationItems.Count + 1),
									IconIndex = 0,
									FullNamePath = "",
									ID = GameInstallationItems.Count + 1
								}
							)
						)
					);
				}
				return addNewGamePathCommand;
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

		private CommandBase<Page> applyCommand;
		public CommandBase<Page> ApplyCommand
		{
			get
			{
				if (applyCommand == null)
				{
					applyCommand = new CommandBase<Page>(
						new Action<Page>(
							page =>
							{
								page.NavigationService.Navigate(new LauncherPage());
							}
						)
					);
				}
				return applyCommand;
			}
		}
	}
}
