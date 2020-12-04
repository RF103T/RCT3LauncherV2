using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

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
		/// 将设置项的值转换为Xml元素。
		/// </summary>
		/// <returns>设置项的Xml元素。</returns>
		public XmlElement OptionValueToXmlElement(XmlDocument document);

		/// <summary>
		/// 将设置项的Xml元素转换为值。
		/// </summary>
		/// <param name="optionElement">设置项的Xml元素。</param>
		public void XmlElementToOptionValue(XmlElement optionElement);

		/// <summary>
		/// 更新设置项的Xml元素。
		/// </summary>
		/// <param name="optionElement">设置项的现有Xml元素。</param>
		public void UpdateOptionValueInXmlElement(ref XmlElement optionElement);

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
