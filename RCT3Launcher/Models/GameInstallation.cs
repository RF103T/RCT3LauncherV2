using PeNet;
using RCT3Launcher.Class.Validation;
using RCT3Launcher.ViewModels;
using RCT3Launcher.Views.ViewHelpers.ValidationRules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace RCT3Launcher.Models
{
	[Serializable]
	public class GameInstallation : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public GameInstallation()
		{

		}

		~GameInstallation()
		{
			if (gameProcess != null)
			{
				gameProcess.Dispose();
				gameProcess = null;
			}
		}

		/// <summary>
		/// 配置索引
		/// </summary>
		public int ID
		{
			get
			{
				return id;
			}
			set
			{
				if (id != value)
				{
					id = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID)));
				}
			}
		}
		private int id;

		/// <summary>
		/// 指示该项是否可以删除
		/// </summary>
		public bool IsItemCanDelete
		{
			get
			{
				return ID != 1;
			}
		}

		/// <summary>
		/// 配置名称
		/// </summary>
		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				if (name != value)
				{
					name = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
				}
			}
		}
		private string name;

		/// <summary>
		/// 游戏目录
		/// </summary>
		public string GameDirectory
		{
			get
			{
				return gameDirectory;
			}
			set
			{
				if (gameDirectory != value)
				{
					if (value != string.Empty)
					{
						DirectoryInfo gameDirectory = new DirectoryInfo(value.ToString());
						FileInfo[] subFiles = gameDirectory.GetFiles();
						foreach (FileInfo info in subFiles)
						{
							if (info.Extension.ToLower() == ".exe" && info.Name.Contains("RCT3"))
							{
								using FileStream peFileStream = new FileStream(info.FullName, FileMode.Open);
								PeFile peFile = new PeFile(peFileStream);
								if (GameVersionHelper.ValidTimeDateStamps.Keys.Contains(peFile.ImageNtHeaders.FileHeader.TimeDateStamp))
								{
									GameFileFullName = info.FullName;
									GameVersion = GameVersionHelper.ValidTimeDateStamps[peFile.ImageNtHeaders.FileHeader.TimeDateStamp];
									break;
								}
							}
						}
					}
					gameDirectory = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GameDirectory)));
				}
			}
		}
		private string gameDirectory;

		/// <summary>
		/// 游戏启动文件路径
		/// </summary>
		[XmlIgnore]
		public string GameFileFullName
		{
			get
			{
				return gameFileFullName;
			}
			set
			{
				if (gameFileFullName != value)
				{
					gameFileFullName = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GameFileFullName)));
				}
			}
		}
		[NonSerialized]
		private string gameFileFullName;

		/// <summary>
		/// 游戏版本名
		/// </summary>
		[XmlIgnore]
		public string GameVersionName
		{
			get
			{
				return GameVersionHelper.GetGameVersionName(GameVersion);
			}
		}

		/// <summary>
		/// 游戏版本
		/// </summary>
		[XmlIgnore]
		public GameVersion GameVersion
		{
			get
			{
				return gameVersion;
			}
			set
			{
				if (gameVersion != value)
				{
					gameVersion = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GameVersion)));
				}
			}
		}
		[NonSerialized]
		private GameVersion gameVersion;

		/// <summary>
		/// 图标索引
		/// </summary>
		public int IconIndex
		{
			get
			{
				return iconIndex;
			}
			set
			{
				if (iconIndex != value)
				{
					iconIndex = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IconIndex)));
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LightIcon)));
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DarkIcon)));
				}
			}
		}
		private int iconIndex;

		/// <summary>
		/// 亮色图标
		/// </summary>
		[XmlIgnore]
		public DrawingImage LightIcon
		{
			get
			{
				return GameInstallationIconHelper.LightIconDrawingImages[IconIndex];
			}
		}

		/// <summary>
		/// 暗色图标
		/// </summary>
		[XmlIgnore]
		public DrawingImage DarkIcon
		{
			get
			{
				return GameInstallationIconHelper.DarkIconDrawingImages[IconIndex];
			}
		}

		/// <summary>
		/// 游戏进程
		/// </summary>
		[XmlIgnore]
		public Process GameProcess
		{
			get
			{
				return gameProcess;
			}
			set
			{
				if (gameProcess != value)
				{
					if (gameProcess != null && gameProcessExitedHandler != null)
					{
						gameProcess.Exited -= gameProcessExitedHandler;
						gameProcess = null;
						gameProcessExitedHandler = null;
					}
					gameProcess = value;
					gameProcess.EnableRaisingEvents = true;
					gameProcessExitedHandler = new EventHandler((obj, args) =>
					{
						PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsGameProcessExited)));
					});
					gameProcess.Exited += gameProcessExitedHandler;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsGameProcessExited)));
				}
			}
		}
		[NonSerialized]
		private Process gameProcess;
		[NonSerialized]
		private EventHandler gameProcessExitedHandler;

		/// <summary>
		/// 指示游戏进程是否退出
		/// </summary>
		[XmlIgnore]
		public bool IsGameProcessExited
		{
			get
			{
				return gameProcess == null || gameProcess.HasExited;
			}
		}
	}
}
