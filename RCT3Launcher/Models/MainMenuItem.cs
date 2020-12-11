using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Media;

namespace RCT3Launcher.Models
{
	[Serializable]
	public class MainMenuItem : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// 图标
		/// </summary>
		public DrawingImage Icon
		{
			get
			{
				return icon;
			}
			set
			{
				if (icon != value)
				{
					icon = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Icon"));
				}
			}
		}
		private DrawingImage icon;
		private object iconResourceKey;


		/// <summary>
		/// 名称
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
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
				}
			}
		}
		private string name;
		private object nameResourceKey;

		/// <summary>
		/// 导航到的页面
		/// </summary>
		public string NavigationPage
		{
			get
			{
				return navigationPage;
			}
			set
			{
				if (navigationPage != value)
				{
					navigationPage = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NavigationPage"));
				}
			}
		}
		private string navigationPage;

		public MainMenuItem()
		{

		}

		public MainMenuItem(object iconResourceKey, object nameResourceKey)
		{
			this.iconResourceKey = iconResourceKey;
			this.nameResourceKey = nameResourceKey;
			UpdateResource();
		}

		/// <summary>
		/// 根据指定的ResourceKey更新数据。
		/// </summary>
		/// <returns>如果更新成功，返回true；否则返回false。</returns>
		public bool UpdateResource()
		{
			DrawingImage tempIcon = App.Current.TryFindResource(iconResourceKey) as DrawingImage;
			string tempName = App.Current.TryFindResource(nameResourceKey) as string;
			if (tempIcon == null || tempName == null)
				return false;
			Icon = tempIcon;
			Name = tempName;
			return true;
		}
	}
}
