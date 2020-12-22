using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RCT3Launcher.EventSystem
{
	public enum EventType
	{
		PageNavigate,
		SettingValueChanged,
		FileChanged,
		FileRenamed,
		MessageBoxShow,
		MessageBoxClose
	}

	public class EventCenter
	{
		private static Dictionary<EventType, Delegate> callBackActions = new Dictionary<EventType, Delegate>();

		static EventCenter()
		{

		}

		private static void OnListenerAdding(EventType type, Delegate callBack)
		{
			if (!callBackActions.ContainsKey(type))
			{
				callBackActions.Add(type, null);
				return;
			}
			Delegate d = callBackActions[type];
			if (d != null && d.GetType() != callBack.GetType())
				throw new Exception(string.Format("Cannot convert the delegate type {1} of event {0} to {2}.", type.ToString(), d.GetType(), callBack.GetType()));
		}

		private static void OnListenerRemoving(EventType type, Delegate callBack)
		{
			if (callBackActions.ContainsKey(type))
			{
				Delegate d = callBackActions[type];
				if (d != null)
				{
					if (d.GetType() != callBack.GetType())
						throw new Exception(string.Format("Unable to remove event listener, because the delegate type of event {0} is type {1}, not {2}.", type, d.GetType(), callBack.GetType()));
					else
						return;
				}
			}
			throw new Exception(string.Format("Unable to remove event listener, because event {0} is not bound to a delegate.", type));
		}

		private static void OnListenerRemoved(EventType type)
		{
			if (callBackActions[type] == null)
				callBackActions.Remove(type);
		}

		public static void AddListener(EventType type, Action callbackAction)
		{
			OnListenerAdding(type, callbackAction);
			callBackActions[type] = (callBackActions[type] as Action) + callbackAction;
		}
		public static void AddListener<T1>(EventType type, Action<T1> callbackAction)
		{
			OnListenerAdding(type, callbackAction);
			callBackActions[type] = (callBackActions[type] as Action<T1>) + callbackAction;
		}
		public static void AddListener<T1, T2>(EventType type, Action<T1, T2> callbackAction)
		{
			OnListenerAdding(type, callbackAction);
			callBackActions[type] = (callBackActions[type] as Action<T1, T2>) + callbackAction;
		}
		public static void AddListener<T1, T2, T3>(EventType type, Action<T1, T2, T3> callbackAction)
		{
			OnListenerAdding(type, callbackAction);
			callBackActions[type] = (callBackActions[type] as Action<T1, T2, T3>) + callbackAction;
		}
		public static void AddListener<T1, T2, T3, T4>(EventType type, Action<T1, T2, T3, T4> callbackAction)
		{
			OnListenerAdding(type, callbackAction);
			callBackActions[type] = (callBackActions[type] as Action<T1, T2, T3, T4>) + callbackAction;
		}
		public static void AddListener<T1, T2, T3, T4, T5>(EventType type, Action<T1, T2, T3, T4, T5> callbackAction)
		{
			OnListenerAdding(type, callbackAction);
			callBackActions[type] = (callBackActions[type] as Action<T1, T2, T3, T4, T5>) + callbackAction;
		}

		public static void RemoveListener(EventType type, Action callbackAction)
		{
			OnListenerRemoving(type, callbackAction);
			callBackActions[type] = (callBackActions[type] as Action) - callbackAction;
			OnListenerRemoved(type);
		}
		public static void RemoveListener<T1>(EventType type, Action<T1> callbackAction)
		{
			OnListenerRemoving(type, callbackAction);
			callBackActions[type] = (callBackActions[type] as Action<T1>) - callbackAction;
			OnListenerRemoved(type);
		}
		public static void RemoveListener<T1, T2>(EventType type, Action<T1, T2> callbackAction)
		{
			OnListenerRemoving(type, callbackAction);
			callBackActions[type] = (callBackActions[type] as Action<T1, T2>) - callbackAction;
			OnListenerRemoved(type);
		}
		public static void RemoveListener<T1, T2, T3>(EventType type, Action<T1, T2, T3> callbackAction)
		{
			OnListenerRemoving(type, callbackAction);
			callBackActions[type] = (callBackActions[type] as Action<T1, T2, T3>) - callbackAction;
			OnListenerRemoved(type);
		}
		public static void RemoveListener<T1, T2, T3, T4>(EventType type, Action<T1, T2, T3, T4> callbackAction)
		{
			OnListenerRemoving(type, callbackAction);
			callBackActions[type] = (callBackActions[type] as Action<T1, T2, T3, T4>) - callbackAction;
			OnListenerRemoved(type);
		}
		public static void RemoveListener<T1, T2, T3, T4, T5>(EventType type, Action<T1, T2, T3, T4, T5> callbackAction)
		{
			OnListenerRemoving(type, callbackAction);
			callBackActions[type] = (callBackActions[type] as Action<T1, T2, T3, T4, T5>) - callbackAction;
			OnListenerRemoved(type);
		}

		public static void Boardcast(EventType type)
		{
			if (callBackActions.TryGetValue(type, out Delegate d))
				(d as Action)?.Invoke();
		}
		public static void Boardcast<T1>(EventType type, T1 t1)
		{
			if (callBackActions.TryGetValue(type, out Delegate d))
				(d as Action<T1>)?.Invoke(t1);
		}
		public static void Boardcast<T1, T2>(EventType type, T1 t1, T2 t2)
		{
			if (callBackActions.TryGetValue(type, out Delegate d))
				(d as Action<T1, T2>)?.Invoke(t1, t2);
		}
		public static void Boardcast<T1, T2, T3>(EventType type, T1 t1, T2 t2, T3 t3)
		{
			if (callBackActions.TryGetValue(type, out Delegate d))
				(d as Action<T1, T2, T3>)?.Invoke(t1, t2, t3);
		}
		public static void Boardcast<T1, T2, T3, T4>(EventType type, T1 t1, T2 t2, T3 t3, T4 t4)
		{
			if (callBackActions.TryGetValue(type, out Delegate d))
				(d as Action<T1, T2, T3, T4>)?.Invoke(t1, t2, t3, t4);
		}
		public static void Boardcast<T1, T2, T3, T4, T5>(EventType type, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
		{
			if (callBackActions.TryGetValue(type, out Delegate d))
				(d as Action<T1, T2, T3, T4, T5>)?.Invoke(t1, t2, t3, t4, t5);
		}

		public static async void BoardcastAsync(EventType type)
		{
			if (callBackActions.TryGetValue(type, out Delegate d))
				await Task.Run(() => (d as Action)?.Invoke());
		}
		public static async void BoardcastAsync<T1>(EventType type, T1 t1)
		{
			if (callBackActions.TryGetValue(type, out Delegate d))
				await Task.Run(() => (d as Action<T1>)?.Invoke(t1));
		}
		public static async void BoardcastAsync<T1, T2>(EventType type, T1 t1, T2 t2)
		{
			if (callBackActions.TryGetValue(type, out Delegate d))
				await Task.Run(() => (d as Action<T1, T2>)?.Invoke(t1, t2));
		}
		public static async void BoardcastAsync<T1, T2, T3>(EventType type, T1 t1, T2 t2, T3 t3)
		{
			if (callBackActions.TryGetValue(type, out Delegate d))
				await Task.Run(() => (d as Action<T1, T2, T3>)?.Invoke(t1, t2, t3));
		}
		public static async void BoardcastAsync<T1, T2, T3, T4>(EventType type, T1 t1, T2 t2, T3 t3, T4 t4)
		{
			if (callBackActions.TryGetValue(type, out Delegate d))
				await Task.Run(() => (d as Action<T1, T2, T3, T4>)?.Invoke(t1, t2, t3, t4));
		}
		public static async void BoardcastAsync<T1, T2, T3, T4, T5>(EventType type, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
		{
			if (callBackActions.TryGetValue(type, out Delegate d))
				await Task.Run(() => (d as Action<T1, T2, T3, T4, T5>)?.Invoke(t1, t2, t3, t4, t5));
		}
	}
}
