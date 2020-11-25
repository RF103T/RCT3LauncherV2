using System;
using System.Collections.Generic;
using System.Text;

namespace RCT3Launcher.Option
{
	public abstract class OptionBase<TValue> : IOption
	{
		#region 参数

		/// <summary>
		/// 设置项的默认值。
		/// </summary>
		public TValue DefaultValue { get; set; }

		/// <summary>
		/// 指示设置项是否被设置过。
		/// </summary>
		public bool IsInitialization { get; set; }

		/// <summary>
		/// 设置项的名称。
		/// </summary>
		public string OptionName { get; set; }

		protected TValue _value;
		/// <summary>
		/// 设置项的值。
		/// </summary>
		public TValue Value
		{
			get
			{
				if (!IsInitialization)
					return DefaultValue;
				return _value;
			}
			set
			{
				if (!_value.Equals(value))
				{
					ValueChanged?.Invoke(this, new RCT3Launcher.Option.EventArgs.ValueChangedEventArgs<TValue>(Value, value));
					_value = value;
				}
			}
		}

		#endregion

		#region 事件

		/// <summary>
		/// 设置项的值被改变时引发。
		/// </summary>
		public event EventHandler<RCT3Launcher.Option.EventArgs.ValueChangedEventArgs<TValue>> ValueChanged;

		#endregion

		#region 方法

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name">设置项的名称。</param>
		/// <param name="defaultValue">设置项的默认值。</param>
		public OptionBase(string optionName, TValue defaultValue)
		{
			DefaultValue = defaultValue;
			OptionName = optionName;

			ValueChanged += OnValueChanged;
		}

		public void SetRawValue(object value)
		{
			Value = (TValue)value;
		}

		public virtual void SetRawValue(string value)
		{
			
		}

		/// <summary>
		/// 通知设置项的值更新。
		/// </summary>
		/// <param name="newValue"></param>
		public void Notify(TValue newValue)
		{
			ValueChanged?.Invoke(this, new RCT3Launcher.Option.EventArgs.ValueChangedEventArgs<TValue>(Value, newValue));
		}

		/// <summary>
		/// 设置值改变后引发的默认操作。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void OnValueChanged(object sender, RCT3Launcher.Option.EventArgs.ValueChangedEventArgs<TValue> e)
		{
			
		}

		#endregion
	}
}
