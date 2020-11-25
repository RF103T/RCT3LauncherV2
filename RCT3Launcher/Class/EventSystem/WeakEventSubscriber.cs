using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RCT3Launcher.EventSystem
{
	class WeakEventSubscriber : IWeakEventListener
	{
		public Action<object, EventArgs> ReceverAction
		{
			set
			{
				if (receverAction != value)
					receverAction = value;
			}
		}
		private Action<object, EventArgs> receverAction;

		public WeakEventSubscriber(Action<object, EventArgs> action)
		{
			ReceverAction = action;
		}

		public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
		{
			if (receverAction != null)
			{
				receverAction.Invoke(sender, e);
				return true;
			}
			return false;
		}
	}
}
