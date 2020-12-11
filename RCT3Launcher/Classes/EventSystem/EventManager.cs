using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RCT3Launcher.EventSystem
{

	class EventManager : WeakEventManager
	{
		public enum EventType
		{
			PageNavigate, SettingValueChanged
		}

		//事件类型对应的事件处理器
		private static Dictionary<EventType, EventHandler<EventArgs>> eventHandler = new Dictionary<EventType, EventHandler<EventArgs>>();

		public static void Boardcast(EventType type, object sender, EventArgs args)
		{
			if (!eventHandler.TryGetValue(type, out EventHandler<EventArgs> handler))
				return;
			handler?.Invoke(sender, args);
		}

		public static EventManager CurrentManager
		{
			get
			{
				var manager = GetCurrentManager(typeof(EventManager)) as EventManager;
				if (manager == null)
				{
					manager = new EventManager();
					SetCurrentManager(typeof(EventManager), manager);
				}
				return manager;
			}
		}

		public static void Listener(object source, IWeakEventListener listener)
		{
			CurrentManager.ProtectedAddListener(source, listener);
		}

		public static void StopListener(object source, IWeakEventListener listener)
		{
			CurrentManager.ProtectedRemoveListener(source, listener);
		}

		protected override void StartListening(object source)
		{
			//(source as WeakEventSubscriber).
		}

		protected override void StopListening(object source)
		{

		}
	}
}
