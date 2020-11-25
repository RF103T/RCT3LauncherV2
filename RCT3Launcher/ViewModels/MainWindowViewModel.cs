using RCT3Launcher.Models;
using RCT3Launcher.Option;
using RCT3Launcher.Option.EventArgs;
using RCT3Launcher.Option.LauncherOptions;
using RCT3Launcher.ViewModels.BaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace RCT3Launcher.ViewModels
{
	class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel()
		{
			MainMenuItems = new ObservableCollection<MainMenuItem>
			{
				new MainMenuItem("Launcher_Page_Icon","Launcher_MenuItem")
				{
					NavigationPage = "Pages/LauncherPage.xaml"
				},
				new MainMenuItem("Mod_Manager_Page_Icon","Mod_Manager_MenuItem")
				{   
					NavigationPage = "Pages/ModManagerPage.xaml"
				},
				new MainMenuItem("Save_Manager_Page_Icon","Save_Manager_MenuItem")
				{
					NavigationPage = "Pages/SaveManagerPage.xaml"
				},
				new MainMenuItem("Music_Manager_Page_Icon","Music_Manager_MenuItem")
				{
					NavigationPage = "Pages/MusicManagerPage.xaml"
				},
				new MainMenuItem("Game_Settings_Page_Icon","Game_Settings_MenuItem")
				{
					NavigationPage = "Pages/GameSettingsPage.xaml"
				},
				new MainMenuItem("Launcher_Settings_Page_Icon","Launcher_Settings_MenuItem")
				{
					NavigationPage = "Pages/LauncherSettingsPage.xaml"
				}
			};
			NavigationPageSource = "Pages/GuidePage.xaml";

			(OptionsManager.optionMap[OptionsManager.OptionType.Language] as LanguageOption).ValueChanged += OnLanguageChanged;
		}

		#region 菜单

		private ObservableCollection<MainMenuItem> _mainMenuItems;
		public ObservableCollection<MainMenuItem> MainMenuItems
		{
			get
			{
				return _mainMenuItems;
			}
			set
			{
				if (_mainMenuItems != value)
				{
					_mainMenuItems = value;
					RaisePropertyChanged("MainMenuItems");
				}
			}
		}

		private int _selectedIndex = -1;
		public int SelectedIndex
		{
			get
			{
				return _selectedIndex;
			}
			set
			{
				if (_selectedIndex != value)
				{
					_selectedIndex = value;
					NavigationPageSource = _mainMenuItems[_selectedIndex].NavigationPage;
				}
			}
		}

		private object _selectedValue;
		public object SelectedValue
		{
			get
			{
				return _selectedValue;
			}
			set
			{
				if (_selectedValue != value)
				{
					_selectedValue = value;
					if (_selectedValue != null)
						SelectedIndex = _mainMenuItems.IndexOf(_selectedValue as MainMenuItem);
					RaisePropertyChanged("SelectedValue");
				}
			}
		}

		public void OnLanguageChanged(object sender, ValueChangedEventArgs<LanguageOption.LanguageParameter> e)
		{
			foreach (MainMenuItem item in MainMenuItems)
				item.UpdateResource();
		}

		#endregion

		#region 页
		private string _navigationPageSource;
		public string NavigationPageSource
		{
			get
			{
				return _navigationPageSource;
			}
			set
			{
				if (_navigationPageSource != value)
				{
					_navigationPageSource = value;
					RaisePropertyChanged("NavigationPageSource");
				}
			}
		}
		#endregion

		#region 按钮
		private CommandBase<RoutedEventArgs> _closeWindowButtonClickCommand;
		public CommandBase<RoutedEventArgs> CloseWindowButtonClickCommand
		{
			get
			{
				if (_closeWindowButtonClickCommand == null)
				{
					_closeWindowButtonClickCommand = new CommandBase<RoutedEventArgs>(
						new Action<RoutedEventArgs>(
							args =>
							{
								OptionsManager.SaveOptionFile();
								Application.Current.Shutdown();
							}
							)
						);
				}
				return _closeWindowButtonClickCommand;
			}
		}

		private CommandBase<RoutedEventArgs> _minimizeWindowButtonClickCommand;
		public CommandBase<RoutedEventArgs> MinimizeWindowButtonClickCommand
		{
			get
			{
				if (_minimizeWindowButtonClickCommand == null)
				{
					_minimizeWindowButtonClickCommand = new CommandBase<RoutedEventArgs>(
						new Action<RoutedEventArgs>(
							args => App.Current.MainWindow.WindowState = WindowState.Minimized
							)
						);
				}
				return _minimizeWindowButtonClickCommand;
			}
		}
		#endregion
	}
}