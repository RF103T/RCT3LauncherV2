using RCT3Launcher.Views.MessageBoxPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using RCT3Launcher.Classes.MessageBox;

namespace RCT3Launcher
{
	public enum MessageBoxButton
	{
		OK = 0,
		OKCancel = 1,
		YesNoCancel = 3,
		YesNo = 4,
		YesNoToAll = 6
	}

	public enum MessageBoxResult
	{
		None = 0,
		OK = 1,
		Cancel = 2,
		Yes = 6,
		No = 7,
		YesToAll = 9,
	}

	public class MessageBox
	{
		public static void Show<TContent>(TContent content, string caption = "", MessageBoxButton button = MessageBoxButton.OK) where TContent : MessageBoxPage, new()
		{
			EventSystem.EventCenter.Boardcast<Page, string, MessageBoxButton>(EventSystem.EventType.MessageBoxShow, content as Page, caption, button);
		}

		public static void Show<TContent>(Action<MessageBoxResult> callback, TContent content, string caption = "", MessageBoxButton button = MessageBoxButton.OK) where TContent : MessageBoxPage, new()
		{
			Action<MessageBoxResult> messageBoxClose = null;
			messageBoxClose = res =>
			{
				EventSystem.EventCenter.RemoveListener<MessageBoxResult>(EventSystem.EventType.MessageBoxClose, messageBoxClose);
				callback?.Invoke(res);
			};
			EventSystem.EventCenter.AddListener<MessageBoxResult>(EventSystem.EventType.MessageBoxClose, messageBoxClose);
			EventSystem.EventCenter.Boardcast<Page, string, MessageBoxButton>(EventSystem.EventType.MessageBoxShow, content as Page, caption, button);
		}

		public static void Show<TContent>(Action<MessageBoxResult, TContent> callback, TContent content, string caption = "", MessageBoxButton button = MessageBoxButton.OK) where TContent : MessageBoxPage, new()
		{
			Action<MessageBoxResult, object> messageBoxClose = null;
			messageBoxClose = (res, sender) =>
			{
				EventSystem.EventCenter.RemoveListener<MessageBoxResult, object>(EventSystem.EventType.MessageBoxClose, messageBoxClose);
				callback?.Invoke(res, (TContent)sender);
			};
			EventSystem.EventCenter.AddListener<MessageBoxResult, object>(EventSystem.EventType.MessageBoxClose, messageBoxClose);
			EventSystem.EventCenter.Boardcast<Page, string, MessageBoxButton>(EventSystem.EventType.MessageBoxShow, content as Page, caption, button);
		}
	}
}