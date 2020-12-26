using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCT3Launcher.Views.MessageBoxPages
{
	public interface IValueMessageBoxPage<out T> : IMessageBoxPage
	{
		public T GetReturnValue();
	}
}
