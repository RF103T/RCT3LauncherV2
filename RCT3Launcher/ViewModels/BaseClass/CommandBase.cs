using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace RCT3Launcher.ViewModels.BaseClass
{
	public class CommandBase<T> : ICommand
	{
		private Action<T> _execute;

		private Func<T, bool> _canExecute;

		public event EventHandler CanExecuteChanged;

		public CommandBase(Action<T> execute) : this(execute, null)
		{

		}

		public CommandBase(Action<T> execute, Func<T, bool> canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			if (_canExecute == null)
				return true;
			return _canExecute((T)parameter);
		}

		public void Execute(object parameter)
		{
			if (CanExecute(parameter))
				_execute?.Invoke((T)parameter);
		}
	}

	public class CommandBase : ICommand
	{
		private Action _execute;

		public event EventHandler CanExecuteChanged;

		public CommandBase(Action execute)
		{
			_execute = execute;
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			if (CanExecute(parameter))
				_execute?.Invoke();
		}
	}
}
