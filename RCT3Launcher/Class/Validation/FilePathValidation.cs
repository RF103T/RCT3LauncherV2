using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace RCT3Launcher.Validation
{
	class FilePathValidation : INotifyDataErrorInfo
	{
		private readonly IDictionary<string, IList<string>> m_errors = new Dictionary<string, IList<string>>();

		public bool HasErrors => throw new NotImplementedException();

		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

		public IEnumerable GetErrors(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
				return null;

			var errors = m_errors.SelectMany(e => e.Value);
			return errors;
		}

		public void Validate(Func<bool> valid, string error, [CallerMemberName] string prop = null)
		{
			if (valid())
				ClearError(prop);
			else
				SetError(new[] { error }, prop);

			//OnChanged(prop);
		}

		// 添加错误消息。
		public void SetError(IList<string> errors, string prop)
		{
			m_errors.Remove(prop);
			m_errors.Add(prop, errors);
			OnError(prop);
		}

		// 清除错误消息。
		public void ClearError(string prop)
		{
			m_errors.Remove(prop);
			OnError(prop);
		}

		// 验证属性时调用。
		private void OnError(string prop)
		{
			ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(prop));
		}
	}
}
