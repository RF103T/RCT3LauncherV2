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
			GamePathItems = new ObservableCollection<GamePathItem>()
			{
				new GamePathItem()
				{
					ID = 1
				}
			};
		}

		private ObservableCollection<GamePathItem> _gamePathItems;
		public ObservableCollection<GamePathItem> GamePathItems
		{
			get
			{
				return _gamePathItems;
			}
			set
			{
				if (_gamePathItems != value)
				{
					_gamePathItems = value;
					RaisePropertyChanged("GamePathItems");
				}
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
								LanguageOption setting = OptionsManager.optionMap[OptionsManager.OptionType.Language] as LanguageOption;
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
							args => GamePathItems.Add(
								new GamePathItem()
								{
									ID = GamePathItems.Count + 1
								}
							)
						)
					);
				}
				return addNewGamePathCommand;
			}
		}

		private CommandBase<GamePathItem> deleteGamePathCommand;
		public CommandBase<GamePathItem> DeleteGamePathCommand
		{
			get
			{
				if (deleteGamePathCommand == null)
				{
					deleteGamePathCommand = new CommandBase<GamePathItem>(
						new Action<GamePathItem>(
							item =>
							{
								int id = item.ID;
								for (int i = id; i < GamePathItems.Count; i++)
									GamePathItems[i].ID--;
								GamePathItems.Remove(item);
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
