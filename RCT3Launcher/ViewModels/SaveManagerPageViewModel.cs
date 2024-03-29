﻿using Ionic.Zip;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using RCT3Launcher.Controls.InternalDataModel;
using RCT3Launcher.Models;
using RCT3Launcher.ViewModels.BaseClass;
using RCT3Launcher.Views.MessageBoxPages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
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
					deleteCommand = new CommandBase<object>(
						data =>
						{
							string messageBoxText;
							List<GameSave> waitForDelSaves = new List<GameSave>();
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
									messageBoxText = String.Format(App.Current.Resources["SaveManagerPage_Single_Select_Delete_Warning_MessageBoxText"].ToString(), waitForDelSaves[0].Name);
								else
									messageBoxText = String.Format(App.Current.Resources["SaveManagerPage_Multi_Select_Delete_Warning_MessageBoxText"].ToString(), waitForDelSaves.Count);
							else
								return;
							MessageBox.Show<TextMessageBoxPage>(res =>
							{
								if (res == MessageBoxResult.Yes)
									foreach (GameSave save in waitForDelSaves)
										save.SaveFileInfo.Delete();
							}, TextMessageBoxPage.Create(messageBoxText), App.Current.Resources["Text_Warning"].ToString(), MessageBoxButton.YesNo);
						}
					);
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
					importCommand = new CommandBase(
						() =>
						{
							OpenFileDialog openFileDialog = new OpenFileDialog()
							{
								Title = App.Current.Resources["SaveManagerPage_Import_FileDialog_Title"].ToString(),
								Filter = App.Current.Resources["SaveManagerPage_Import_FileDialog_Filter"].ToString(),
								Multiselect = true,
								DefaultExt = "dat"
							};
							if (openFileDialog.ShowDialog() == false)
								return;
							if (openFileDialog.FilterIndex == 1)
							{
								MessageBox.Show<SwitchComboBoxMessageBoxPage>((res, sender) =>
									 {
										 if (res == MessageBoxResult.OK)
											 ImportSaveFiles(openFileDialog.FileNames, GameSaveTypeHelper.GetGameSaveTypeFullPath((GameSaveType)sender.GetReturnValue()));
									 },
									 SwitchComboBoxMessageBoxPage.Create(App.Current.Resources["SaveManagerPage_Import_Type_Select_MessageBoxText"].ToString(),
										new List<SwitchComboBoxItemModel>()
										{
											new SwitchComboBoxItemModel(){ID = 0,Content=GameSaveTypeHelper.GetGameSaveTypeFormattedText(GameSaveType.Park)},
											new SwitchComboBoxItemModel(){ID = 1,Content=GameSaveTypeHelper.GetGameSaveTypeFormattedText(GameSaveType.Scenario)},
											new SwitchComboBoxItemModel(){ID = 2,Content=GameSaveTypeHelper.GetGameSaveTypeFormattedText(GameSaveType.Start_New_Scenario)}
										}),
									App.Current.Resources["Text_Information"].ToString(), MessageBoxButton.OK);
							}
							else
								ImportSaveFiles(openFileDialog.FileNames, null);
						}
					);
				}
				return importCommand;
			}
		}

		private CommandBase<object> exportCommand;
		public CommandBase<object> ExportCommand
		{
			get
			{
				if (exportCommand == null)
				{
					exportCommand = new CommandBase<object>(
						data =>
						{
							List<GameSave> waitForExportSaves = new List<GameSave>();
							waitForExportSaves.AddRange(
								data switch
								{
									GameSave save => new List<GameSave>() { save },
									IList list => list.Cast<GameSave>(),
									_ => throw new NotImplementedException()
								}
							);
							if (waitForExportSaves.Count <= 0)
								return;
							SaveFileDialog dialog = new SaveFileDialog()
							{
								Title = App.Current.Resources["SaveManagerPage_Export_FileDialog_Title"].ToString(),
								FileName = "saves",
								DefaultExt = ".zip",
								Filter = App.Current.Resources["SaveManagerPage_Export_FileDialog_Filter"].ToString()
							};
							if (dialog.ShowDialog().Value)
							{
								using ZipFile zip = new ZipFile()
								{
									AlternateEncoding = Encoding.UTF8,
									AlternateEncodingUsage = ZipOption.Always
								};
								foreach (GameSave save in waitForExportSaves)
									zip.AddFile(save.SaveFileInfo.FullName, save.SaveType.ToString());
								zip.Save(dialog.FileName);
							}
						}
					);
				}
				return exportCommand;
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
					App.Current.Dispatcher.Invoke(() =>
					{
						GameSaves.Remove(save);
						GameSaves.Add(new GameSave(e.FullPath)
						{
							ID = save.ID
						});
					});
					break;
				}
			}
		}

		private async void GetSaveFile()
		{
			IEnumerable<GameSave> res = await Task.Run<IEnumerable<GameSave>>(() =>
			{
				List<GameSave> gameSaves = new List<GameSave>();
				DirectoryInfo rootDir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RCT3");
				FileInfo[] files = rootDir.GetFiles("*.dat", SearchOption.AllDirectories);

				var validFiles = from file in files
								 where file.FullName.Contains(@"\Parks") || file.FullName.Contains(@"\Scenarios") || file.FullName.Contains(@"\Start New Scenarios")
								 select file;

				int index = 1;
				foreach (FileInfo info in validFiles)
				{
					gameSaves.Add(new GameSave(info)
					{
						ID = index++
					});
				}
				return gameSaves.AsEnumerable<GameSave>();
			});
			GameSaves = new ObservableCollection<GameSave>(res);
		}

		/// <summary>
		/// 导入存档文件。
		/// </summary>
		/// <param name="fileNames">要导入的文件，可以为存档文件或者启动器导出的zip文件。</param>
		/// <param name="importPath">导入路径，只有导入存档文件的时候需要指定，导入启动器zip文件时不需要。</param>
		private async void ImportSaveFiles(string[] fileNames, string importPath)
		{
			await Task.Run(() =>
			{
				DirectoryInfo tempDirectory = Directory.CreateDirectory("TEMP");
				Dictionary<string, FileInfo> waitForImportFiles = new Dictionary<string, FileInfo>();
				Dictionary<string, string> importPaths = new Dictionary<string, string>();
				if (importPath != null)
				{
					foreach (string name in fileNames)
					{
						FileInfo file = new FileInfo(name);
						waitForImportFiles.Add(file.Name, file);
						importPaths.Add(file.Name, importPath);
					}
				}
				else
				{
					foreach (string name in fileNames)
					{
						using (ZipFile zip = ZipFile.Read(name))
						{
							zip.ExtractAll("TEMP");
						}
						FileInfo[] files = tempDirectory.GetFiles("*.dat", SearchOption.AllDirectories);
						foreach (FileInfo file in files)
						{
							if (file.FullName.Contains(@"\Park"))
								importPaths.Add(file.Name, GameSaveTypeHelper.GetGameSaveTypeFullPath(GameSaveType.Park));
							else if (file.FullName.Contains(@"\Scenario"))
								importPaths.Add(file.Name, GameSaveTypeHelper.GetGameSaveTypeFullPath(GameSaveType.Scenario));
							else if (file.FullName.Contains(@"\Start_New_Scenario"))
								importPaths.Add(file.Name, GameSaveTypeHelper.GetGameSaveTypeFullPath(GameSaveType.Start_New_Scenario));
							else
								continue;
							waitForImportFiles.Add(file.Name, file);
						}
					}
				}
				foreach (GameSave save in GameSaves)
				{
					if (waitForImportFiles.TryGetValue(save.SaveFileInfo.Name, out FileInfo temp))
					{
						string pathTemp = importPaths[temp.Name];
						waitForImportFiles.Remove(temp.Name);
						importPaths.Remove(temp.Name);
						temp = temp.CopyTo(@"TEMP\" + temp.Name.Replace(".dat", "_import.dat"), true);
						waitForImportFiles.Add(temp.Name, temp);
						importPaths.Add(temp.Name, pathTemp);
					}
				}
				foreach (KeyValuePair<string, FileInfo> pair in waitForImportFiles)
					pair.Value.MoveTo(importPaths[pair.Key] + @"\" + pair.Value.Name, false);
				tempDirectory.Delete(true);
			});
		}
	}
}
