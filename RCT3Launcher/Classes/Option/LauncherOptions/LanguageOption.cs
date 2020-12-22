using RCT3Launcher.Option.EventArgs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Xml;

namespace RCT3Launcher.Option.LauncherOptions
{
	public class LanguageOption : OptionBase<LanguageOption.LanguageParameter>
	{
		public enum LanguageParameter
		{
			/// <summary>
			/// 简体中文
			/// </summary>
			zh_CN,
			/// <summary>
			/// 英语（美国）
			/// </summary>
			en_US
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="defaultValue">设置项的默认值。</param>
		public LanguageOption(LanguageParameter defaultValue) : base("Language", defaultValue)
		{

		}

		public override XmlElement OptionValueToXmlElement(XmlDocument document)
		{
			XmlElement optionRootElement = base.OptionValueToXmlElement(document);
			optionRootElement.InnerText = Value.ToString();
			return optionRootElement;
		}

		public override void XmlElementToOptionValue(XmlElement optionElement)
		{
			SetRawValue(optionElement.InnerText);
		}

		public override void UpdateOptionValueInXmlElement(ref XmlElement optionElement)
		{
			optionElement.InnerText = Value.ToString();
		}

		public override void SetRawValue(string value)
		{
			Value = (LanguageParameter)Enum.Parse(typeof(LanguageParameter), value);
		}

		protected override void OnValueChanged(object sender, ValueChangedEventArgs<LanguageParameter> e)
		{
			base.OnValueChanged(sender, e);
			ResourceDictionary dict = new ResourceDictionary();
			switch (e.NewValue)
			{
				case LanguageParameter.zh_CN:
				{
					dict.Source = new Uri(@"pack://application:,,,/ResourcesDictionary/Languages/zh_CN.xaml", UriKind.Absolute);
					break;
				}
				case LanguageParameter.en_US:
				{
					dict.Source = new Uri(@"pack://application:,,,/ResourcesDictionary/Languages/en_US.xaml", UriKind.Absolute);
					break;
				}
			}
			Application.Current.Resources.MergedDictionaries[0] = dict;
		}
	}
}
