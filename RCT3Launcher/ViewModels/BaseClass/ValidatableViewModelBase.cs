using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCT3Launcher.ViewModels.BaseClass
{
	public class ValidatableViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo
	{
		private ConcurrentDictionary<string, List<string>> _errors = new ConcurrentDictionary<string, List<string>>();

		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// 属性发生改变时，调用该方法发出通知
		/// </summary>
		/// <param name="propertyName">属性名称</param>
		public void RaisePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public void OnErrorsChanged(string propertyName)
        {
            var handler = ErrorsChanged;
            if (handler != null)
                handler(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            List<string> errorsForName;
            _errors.TryGetValue(propertyName, out errorsForName);
            return errorsForName;
        }

        public bool HasErrors
        {
            get { return _errors.Any(kv => kv.Value != null && kv.Value.Count > 0); }
        }

        public Task ValidateAsync()
        {
            return Task.Run(() => Validate());
        }

        private object _lock = new object();
        public void Validate()
        {
            lock (_lock)
            {
                var validationContext = new ValidationContext(this, null, null);
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(this, validationContext, validationResults, true);

                foreach (var kv in _errors.ToList())
                {
                    if (validationResults.All(r => r.MemberNames.All(m => m != kv.Key)))
                    {
                        List<string> outLi;
                        _errors.TryRemove(kv.Key, out outLi);
                        OnErrorsChanged(kv.Key);
                    }
                }

                var q = from r in validationResults
                        from m in r.MemberNames
                        group r by m into g
                        select g;

                foreach (var prop in q)
                {
                    var messages = prop.Select(r => r.ErrorMessage).ToList();

                    if (_errors.ContainsKey(prop.Key))
                    {
                        List<string> outLi;
                        _errors.TryRemove(prop.Key, out outLi);
                    }
                    _errors.TryAdd(prop.Key, messages);
                    OnErrorsChanged(prop.Key);
                }
            }
        }


    }
}
