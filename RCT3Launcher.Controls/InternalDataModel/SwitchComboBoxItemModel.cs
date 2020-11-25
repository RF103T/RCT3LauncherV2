using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace RCT3Launcher.Controls.InternalDataModel
{
	public class SwitchComboBoxItemModel : FrameworkElement, INotifyPropertyChanged
	{
		public int ID
		{
			get { return (int)GetValue(IDProperty); }
			set
			{
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ID"));
				SetValue(IDProperty, value);
			}
		}
		public static readonly DependencyProperty IDProperty =
			DependencyProperty.RegisterAttached("ID", typeof(int), typeof(SwitchComboBoxItemModel), new PropertyMetadata(0));



		public string Content
		{
			get { return (string)GetValue(ContentProperty); }
			set
			{
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content"));
				SetValue(ContentProperty, value);
			}
		}
		public static readonly DependencyProperty ContentProperty =
			DependencyProperty.RegisterAttached("Content", typeof(string), typeof(SwitchComboBoxItemModel), new PropertyMetadata(""));


		public event PropertyChangedEventHandler PropertyChanged;
	}
}
