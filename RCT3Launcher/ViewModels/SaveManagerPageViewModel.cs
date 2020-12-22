using Microsoft.Win32;
using RCT3Launcher.Models;
using RCT3Launcher.ViewModels.BaseClass;
using RCT3Launcher.Views.MessageBoxPages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RCT3Launcher.ViewModels
{
	class SaveManagerPageViewModel : ViewModelBase
	{
		public SaveManagerPageViewModel()
		{
			EventSystem.EventCenter.AddListener<FileSystemEventArgs>(EventSystem.EventType.FileChanged, OnSaveFileChanged);
			EventSystem.EventCenter.AddListener<RenamedEventArgs>(EventSystem.EventType.FileRenamed, OnSaveFileRenamed);
			GetSaveFile();
		}

		private ObservableCollection<GameSave> gameSaves;
		public ObservableCollection<GameSave> GameSaves
		{
			get
			{
				return gameSaves;
			}
			set
			{
				if (gameSaves != value)
				{
					gameSaves = value;
					RaisePropertyChanged(nameof(GameSaves));
				}
			}
		}

		private IList<GameSave> selectedSaveItems;
		public IList<GameSave> SelectedSaveItems
		{
			get
			{
				return selectedSaveItems;
			}
			set
			{
				if (selectedSaveItems != value)
				{
					selectedSaveItems = value;
					RaisePropertyChanged(nameof(SelectedSaveItems));
				}
			}
		}

		private CommandBase<object> deleteCommand;
		public CommandBase<object> DeleteCommand
		{
			get
			{
				if (deleteCommand == null)
				{
					deleteCommand = new CommandBase<object>(new Action<object>(data =>
						{
							string messageBoxText;
							List<GameSave> waitForDelSaves = new List<GameSave>();
							if(data is IList l)
							{
								IList<GameSave> i = l as IList<GameSave>;
							}
							waitForDelSaves.AddRange(
								data switch
								{
									GameSave save => new List<GameSave>() { save },
									IList list => list.Cast<GameSave>(),
									_ => throw new NotImplementedException()
								}
							);
							if (waitForDelSaves.Count > 0)
								if (waitForDelSaves.Count == 1)
									messageBoxText = String.Format(Application.Current.Resources["SaveManagerPage_Single_Select_Delete_Warning_MessageBoxText"].ToString(), waitForDelSaves[0].Name);
								else
									messageBoxText = String.Format(Application.Current.Resources["SaveManagerPage_Multi_Select_Delete_Warning_MessageBoxText"].ToString(), waitForDelSaves.Count);
							else
								return;
							MessageBox.Show<TextMessageBoxPage>(new Action<MessageBoxResult>(res =>
							{
								if (res == MessageBoxResult.Yes)
									foreach (GameSave save in waitForDelSaves)
										save.SaveFileInfo.Delete();
							}), new TextMessageBoxPage(messageBoxText), Application.Current.Resources["Text_Warning"].ToString(), MessageBoxButton.YesNo);
						}
					));
				}
				return deleteCommand;
			}
		}

		private CommandBase importCommand;
		public CommandBase ImportCommand
		{
			get
			{
				if (importCommand == null)
				{
					importCommand = new CommandBase(new Action(() =>
					{
						OpenFileDialog openFileDialog = new OpenFileDialog()
						{
							Title = Application.Current.Resources["SaveManagerPage_Import_FileDialog_Title"].ToString(),
							Filter = Application.Current.Resources["SaveManagerPage_Import_FileDialog_Filter"].ToString(),
							Multiselect = true,
							DefaultExt = "dat"
						};
						if (openFileDialog.ShowDialog() == false)
							return;
						foreach (string fileName in openFileDialog.FileNames)
						{
							FileInfo info = new FileInfo(fileName);
							switch (info.Extension)
							{
								case ".dat":
								{
									break;
								}
								case ".bak":
								{
									break;
								}
								case ".zip":
								{

									break;
								}
							}
						}
					}));
				}
				return importCommand;
			}
		}

		private void OnSaveFileChanged(FileSystemEventArgs e)
		{
			if (e.ChangeType == WatcherChangeTypes.Created)
			{
				App.Current.Dispatcher.Invoke(new Action(() => GameSaves.Add(new GameSave(e.FullPath)
				{
					ID = GameSaves.Count + 1
				})));
				return;
			}
			foreach (GameSave save in GameSaves)
			{
				if (save.SaveFileInfo.FullName == e.FullPath)
				{
					if (e.ChangeType == WatcherChangeTypes.Changed)
						save.UpdateInfo();
					else
					{
						foreach (GameSave s in GameSaves)
							if (s.ID > save.ID)
								s.ID--;
						App.Current.Dispatcher.Invoke(new Action(() => GameSaves.Remove(save)));
					}
					break;
				}
			}
		}

		private void OnSaveFileRenamed(RenamedEventArgs e)
		{
			foreach (GameSave save in GameSaves)
			{
				if (save.SaveFileInfo.FullName == e.OldFullPath)
				{
					App.Current.Dispatcher.Invoke(new Action(() =>
					{
						GameSaves.Remove(save);
						GameSaves.Add(new GameSave(e.FullPath)
						{
							ID = save.ID
						});
					}));
					break;
				}
			}
		}

		private async void GetSaveFile()
		{
			IEnumerable<GameSave> res = await Task.Run<IEnumerable<GameSave>>(() =>
			{
				List<GameSave> gameSaves = new List<GameSave>();
				DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RCT3\Start New Scenarios");
				int index = 1;
				foreach (FileInfo info in directoryInfo.EnumerateFiles("*.dat"))
				{
					gameSaves.Add(new GameSave(info.FullName)
					{
						ID = index++
					});
				}
				return gameSaves.AsEnumerable<GameSave>();
			});
			GameSaves = new ObservableCollection<GameSave>(res);
		}
	}
}
