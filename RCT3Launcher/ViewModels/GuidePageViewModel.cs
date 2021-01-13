using RCT3Launcher.Controls;
using RCT3Launcher.EventSystem;
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
using System.Windows.Media;

namespace RCT3Launcher.ViewModels
{
	public class GuidePageViewModel : ViewModelBase
	{
		public GuidePageViewModel()
		{

		}

		public ObservableCollection<GameInstallation> GameInstallationItems
		{
			get
			{
				SettingsViewModel settings = App.Current.Resources["settingsViewModel"] as SettingsViewModel;
				return settings.GameInstallationItems;
			}
			set
			{
				SettingsViewModel settings = App.Current.Resources["settingsViewModel"] as SettingsViewModel;
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
							self =>
							{
								LanguageOption setting = OptionsManager.Instance.GetOptionObject<LanguageOption>(OptionType.Language);
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
							args => GameInstallationItems.Add(
								new GameInstallation()
								{
									Name = "配置" + (GameInstallationItems.Count + 1),
									IconIndex = 0,
									GameDirectory = "",
									ID = GameInstallationItems.Count + 1
								}
						)
					);
				}
				return addNewGamePathCommand;
			}
		}

		private CommandBase<object> applyCommand;
		public CommandBase<object> ApplyCommand
		{
			get
			{
				if (applyCommand == null)
				{
					applyCommand = new CommandBase<object>(
						obj =>
						{
							OptionsManager.Instance.UserInitializeCompleted();
							EventCenter.Boardcast<string>(EventType.PageNavigate, nameof(LauncherPage));
						}
					);
				}
				return applyCommand;
			}
		}
	}
}
