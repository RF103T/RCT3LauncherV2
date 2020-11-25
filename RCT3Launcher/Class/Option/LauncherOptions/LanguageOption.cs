using RCT3Launcher.Option.EventArgs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

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

		public override void SetRawValue(string value)
		{
			Value = (LanguageParameter)Enum.Parse(typeof(LanguageParameter), value);
		}

		protected override void OnValueChanged(object sender, ValueChangedEventArgs<LanguageParameter> e)
		{
			base.OnValueChanged(sender, e);
			OptionsManager.WriteOptionValueToFile(OptionsManager.OptionType.Language);
			ResourceDictionary dict = new ResourceDictionary();
			switch (e.NewValue)
			{
				case LanguageParameter.zh_CN:
				{
					dict.Source = new Uri(@"/ResourcesDictionary/Languages/zh_CN.xaml", UriKind.Relative);
					break;
				}
				case LanguageParameter.en_US:
				{
					dict.Source = new Uri(@"/ResourcesDictionary/Languages/en_US.xaml", UriKind.Relative);
					break;
				}
			}
			Application.Current.Resources.MergedDictionaries[0] = dict;
		}
	}
}
