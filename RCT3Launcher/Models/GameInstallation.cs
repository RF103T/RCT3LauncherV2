using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Media;

namespace RCT3Launcher.Models
{
	[Serializable]
	public class GameInstallation : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public GameInstallation()
		{

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
		public string FullNamePath
		{
			get
			{
				return fullNamePath;
			}
			set
			{
				if (fullNamePath != value)
				{
					fullNamePath = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FullNamePath)));
				}
			}
		}
		private string fullNamePath;

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
				}
			}
		}
		private int iconIndex;
	}
}
