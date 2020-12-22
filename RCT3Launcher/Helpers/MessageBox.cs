using RCT3Launcher.Views.MessageBoxPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RCT3Launcher
{
	public class MessageBox
	{
		public static void Show<T>(T content, string caption = "", System.Windows.MessageBoxButton button = System.Windows.MessageBoxButton.OK) where T : IMessageBoxPage
		{
			EventSystem.EventCenter.Boardcast<Page, string, System.Windows.MessageBoxButton>(EventSystem.EventType.MessageBoxShow, content as Page, caption, button);
		}

		public static void Show<T>(Action<System.Windows.MessageBoxResult> callback, T content, string caption = "", System.Windows.MessageBoxButton button = System.Windows.MessageBoxButton.OK) where T : IMessageBoxPage
		{
			Action<System.Windows.MessageBoxResult> messageBoxClose = null;
			messageBoxClose = new Action<System.Windows.MessageBoxResult>(
				res =>
				{
					EventSystem.EventCenter.RemoveListener<System.Windows.MessageBoxResult>(EventSystem.EventType.MessageBoxClose, messageBoxClose);
					callback?.Invoke(res);
				});
			EventSystem.EventCenter.AddListener<System.Windows.MessageBoxResult>(EventSystem.EventType.MessageBoxClose, messageBoxClose);
			EventSystem.EventCenter.Boardcast<Page, string, System.Windows.MessageBoxButton>(EventSystem.EventType.MessageBoxShow, content as Page, caption, button);
		}
	}
}