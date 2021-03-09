using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCT3Launcher.Classes.MessageBox
{
	public class MessageBoxPageFactory
	{
		private static Dictionary<Type, MessageBoxPage> instances = new Dictionary<Type, MessageBoxPage>();

		public static T GetInstance<T>() where T : MessageBoxPage, new()
		{
			if (!instances.TryGetValue(typeof(T), out MessageBoxPage page))
			{
				page = new T();
				instances.Add(typeof(T), page);
			}
			return page as T;
		}
	}
}
