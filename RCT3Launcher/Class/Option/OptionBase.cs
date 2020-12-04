using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

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

		protected TValue value;
		/// <summary>
		/// 设置项的值。
		/// </summary>
		public TValue Value
		{
			get
			{
				//if (!IsInitialization)
				//	return DefaultValue;
				return value;
			}
			set
			{
				if (!this.value.Equals(value))
				{
					this.value = value;
					valueChanged?.Invoke(this, new RCT3Launcher.Option.EventArgs.ValueChangedEventArgs<TValue>(Value, value));
				}
			}
		}

		#endregion

		#region 事件

		/// <summary>
		/// 设置项的值被改变时引发。
		/// </summary>
		public event EventHandler<RCT3Launcher.Option.EventArgs.ValueChangedEventArgs<TValue>> ValueChanged
		{
			add
			{
				valueChanged += value;
				Notify();
			}
			remove
			{
				valueChanged -= value;
			}
		}
		private EventHandler<RCT3Launcher.Option.EventArgs.ValueChangedEventArgs<TValue>> valueChanged;

		#endregion

		#region 方法

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name">设置项的名称。</param>
		/// <param name="defaultValue">设置项的默认值。</param>
		public OptionBase(string optionName, TValue defaultValue)
		{
			//DefaultValue = defaultValue;
			value = defaultValue;
			OptionName = optionName;

			ValueChanged += OnValueChanged;
		}

		public virtual XmlElement OptionValueToXmlElement(XmlDocument document)
		{
			XmlElement optionElement = document.CreateElement(this.OptionName);
			return optionElement;
		}

		public abstract void XmlElementToOptionValue(XmlElement optionElement);

		public abstract void UpdateOptionValueInXmlElement(ref XmlElement optionElement);


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
		public void Notify()
		{
			valueChanged?.Invoke(this, new RCT3Launcher.Option.EventArgs.ValueChangedEventArgs<TValue>(Value, Value));
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
