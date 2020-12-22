using RCT3Launcher.EventSystem;
using RCT3Launcher.Models;
using RCT3Launcher.Option;
using RCT3Launcher.Option.EventArgs;
using RCT3Launcher.Option.LauncherOptions;
using RCT3Launcher.ViewModels.BaseClass;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace RCT3Launcher.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel()
		{
			OptionsManager.Instance.GetOptionObject<LanguageOption>(OptionType.Language).ValueChanged += OnLanguageChanged;
			EventCenter.AddListener<string>(EventType.PageNavigate, OnPageNavigate);

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

			//if (!OptionsManager.Instance.IsOptionsInitialized)
			NavigationPageSource = "Pages/GuidePage.xaml";
			//else
			//	NavigationPageSource = MainMenuItems[0].NavigationPage;
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
					RaisePropertyChanged(nameof(MainMenuItems));
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
					RaisePropertyChanged(nameof(SelectedValue));
				}
			}
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
					RaisePropertyChanged(nameof(NavigationPageSource));
				}
			}
		}
		#endregion

		#region 按钮
		private CommandBase closeWindowButtonClickCommand;
		public CommandBase CloseWindowButtonClickCommand
		{
			get
			{
				if (closeWindowButtonClickCommand == null)
				{
					closeWindowButtonClickCommand = new CommandBase(
						new Action(() =>
							{
								OptionsManager.Instance.SaveOptionFile();
								Application.Current.Shutdown();
							}
							)
						);
				}
				return closeWindowButtonClickCommand;
			}
		}

		private CommandBase minimizeWindowButtonClickCommand;
		public CommandBase MinimizeWindowButtonClickCommand
		{
			get
			{
				if (minimizeWindowButtonClickCommand == null)
				{
					minimizeWindowButtonClickCommand = new CommandBase(
						new Action(
							() => Application.Current.MainWindow.WindowState = WindowState.Minimized
							)
						);
				}
				return minimizeWindowButtonClickCommand;
			}
		}
		#endregion

		public void OnLanguageChanged(object sender, ValueChangedEventArgs<LanguageOption.LanguageParameter> e)
		{
			foreach (MainMenuItem item in MainMenuItems)
				item.UpdateResource();
		}

		private void OnPageNavigate(string pageName)
		{
			string navigationUrl = string.Format("Pages/{0}.xaml", pageName);
			foreach (MainMenuItem item in MainMenuItems)
			{
				if (item.NavigationPage == navigationUrl)
				{
					SelectedValue = item;
					return;
				}
			}
			NavigationPageSource = navigationUrl;
		}
	}
}