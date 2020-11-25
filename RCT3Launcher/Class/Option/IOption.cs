using System;
using System.Collections.Generic;
using System.Text;

namespace RCT3Launcher.Option
{
	public interface IOption
	{
		/// <summary>
		/// 指示设置项是否被设置过。
		/// </summary>
		public bool IsInitialization { get; set; }

		/// <summary>
		/// 设置项的名称。
		/// </summary>
		public string OptionName { get; set; }

		/// <summary>
		/// 设置设置项的值（从Object转换）。
		/// </summary>
		/// <param name="value">要设置的值。</param>
		public void SetRawValue(object value);

		/// <summary>
		/// 设置设置项的值（从String转换）。
		/// </summary>
		/// <param name="value">要设置的值。</param>
		public void SetRawValue(string value);

	}
}
