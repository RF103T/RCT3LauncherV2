using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RCT3Launcher.ViewModels.BaseClass
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// 属性发生改变时，调用该方法发出通知
		/// </summary>
		/// <param name="propertyName">属性名称</param>
		public void RaisePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
