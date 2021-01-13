using RCT3Launcher.EventSystem;
using RCT3Launcher.Models;
using RCT3Launcher.Option;
using RCT3Launcher.Option.EventArgs;
using RCT3Launcher.Option.LauncherOptions;
using RCT3Launcher.ViewModels.BaseClass;
using RCT3Launcher.Views.Pages;
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
				//new MainMenuItem("Mod_Manager_Page_Icon","Mod_Manager_MenuItem")
				//{
				//	NavigationPage = "Pages/ModManagerPage.xaml"
				//},
				new MainMenuItem("Save_Manager_Page_Icon","Save_Manager_MenuItem")
				{
					NavigationPage = "Pages/SaveManagerPage.xaml"
				},
				//new MainMenuItem("Music_Manager_Page_Icon","Music_Manager_MenuItem")
				//{
				//	NavigationPage = "Pages/MusicManagerPage.xaml"
				//},
				//new MainMenuItem("Game_Preference_Page_Icon","Game_Preference_MenuItem")
				//{
				//	NavigationPage = "Pages/GamePreferencePage.xaml"
				//},
				new MainMenuItem("Launcher_Preference_Page_Icon","Launcher_Preference_MenuItem")
				{
					NavigationPage = "Pages/LauncherPreferencePage.xaml"
				}
			};

			if (!OptionsManager.Instance.IsOptionsInitialized)
				EventCenter.Boardcast<string>(EventType.PageNavigate, nameof(GuidePage));
			else
				EventCenter.Boardcast<string>(EventType.PageNavigate, nameof(LauncherPage));
		}

		#region 菜单

		private ObservableCollection<MainMenuItem> mainMenuItems;
		public ObservableCollection<MainMenuItem> MainMenuItems
		{
			get
			{
				return mainMenuItems;
			}
			set
			{
				if (mainMenuItems != value)
				{
					mainMenuItems = value;
					RaisePropertyChanged(nameof(MainMenuItems));
				}
			}
		}

		private int selectedIndex = -1;
		public int SelectedIndex
		{
			get
			{
				return selectedIndex;
			}
			set
			{
				if (selectedIndex != value)
				{
					selectedIndex = value;
					NavigationPageSource = mainMenuItems[selectedIndex].NavigationPage;
				}
			}
		}

		private object selectedValue;
		public object SelectedValue
		{
			get
			{
				return selectedValue;
			}
			set
			{
				if (selectedValue != value)
				{
					selectedValue = value;
					if (selectedValue != null)
						SelectedIndex = mainMenuItems.IndexOf(selectedValue as MainMenuItem);
					RaisePropertyChanged(nameof(SelectedValue));
				}
			}
		}

		private bool isMainMenuEnabled;
		public bool IsMainMenuEnabled
		{
			get
			{
				return isMainMenuEnabled;
			}
			set
			{
				if (isMainMenuEnabled != value)
				{
					isMainMenuEnabled = value;
					RaisePropertyChanged(nameof(IsMainMenuEnabled));
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
						() =>
						{
							OptionsManager.Instance.SaveOptionFile();
							App.Current.Shutdown();
						}
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
							() => App.Current.MainWindow.WindowState = WindowState.Minimized
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
					IsMainMenuEnabled = true;
					SelectedValue = item;
					return;
				}
			}
			IsMainMenuEnabled = false;
			NavigationPageSource = navigationUrl;
		}
	}
}