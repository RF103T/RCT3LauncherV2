using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace RCT3Launcher.Controls.InternalDataModel
{
	public class SwitchComboBoxItemModel : INotifyPropertyChanged
	{
		private int id;
		public int ID
		{
			get { return id; }
			set
			{
				if (id != value)
				{
					id = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID)));
				}
			}
		}

		private string content;
		public string Content
		{
			get { return content; }
			set
			{
				if (content != value)
				{
					content = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Content)));
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
