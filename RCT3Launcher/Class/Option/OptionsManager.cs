using RCT3Launcher.Option.LauncherOptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RCT3Launcher.Option
{
	public class OptionsManager
	{
		public enum OptionType
		{
			GamePath, Language
		}

		private static readonly string optionsXmlFilePath = "options.xml";
		private static readonly XmlDocument optionsXmlDocument;

		private static readonly string optionsXmlXPathRoot = "/Options/";

		public static Dictionary<OptionType, IOption> optionMap = new Dictionary<OptionType, IOption>()
		{
			{OptionType.Language,new LanguageOption(LanguageOption.LanguageParameter.zh_CN)}
		};

		static OptionsManager()
		{
			optionsXmlDocument = new XmlDocument();
			if (!File.Exists(optionsXmlFilePath))
				InitializationOptionsXmlFile();
			else
				InitializationOptions();
		}

		public static void SaveOptionFile()
		{
			optionsXmlDocument.Save(optionsXmlFilePath);
		}

		/// <summary>
		/// 获取指定设置项的值。
		/// </summary>
		/// <typeparam name="TValue">设置项的值类型。</typeparam>
		/// <param name="optionType">指定的设置项。</param>
		/// <returns>指定设置项的值。</returns>
		public static TValue GetOptionValue<TValue>(OptionType optionType)
		{
			return (optionMap[optionType] as OptionBase<TValue>).Value;
		}

		/// <summary>
		/// 设置指定设置项的值。
		/// </summary>
		/// <typeparam name="TValue">设置项的值类型。</typeparam>
		/// <param name="optionType">指定的设置项。</param>
		/// <param name="value">要设置的值。</param>
		public static void SetOptionValue<TValue>(OptionType optionType, TValue value)
		{
			(optionMap[optionType] as OptionBase<TValue>).Value = value;
		}

		/// <summary>
		/// 将指定的设置项加入设置文件。
		/// </summary>
		/// <param name="optionType">指定的设置项。</param>
		public static void AddOptionToFile(OptionType optionType)
		{
			IOption option = optionMap[optionType];
			XmlElement rootNode = optionsXmlDocument.DocumentElement;
			XmlElement subNote = optionsXmlDocument.CreateElement(option.OptionName);
			PropertyInfo propertyInfo = option.GetType().GetProperty("Value");
			subNote.InnerText = propertyInfo.GetValue(option).ToString();
			rootNode.AppendChild(subNote);
		}

		/// <summary>
		/// 从文件中读取指定设置项的值。
		/// </summary>
		/// <param name="optionType">指定的设置项。</param>
		/// <returns>指定设置项的值。</returns>
		public static string ReadOptionValueFromFile(OptionType optionType)
		{
			IOption option = optionMap[optionType];
			XmlElement rootNode = optionsXmlDocument.DocumentElement;
			XmlNode optionNode = rootNode.SelectSingleNode(optionsXmlXPathRoot + option.OptionName);
			return optionNode.InnerText;
		}

		/// <summary>
		/// 把指定设置项的值写入文件。
		/// </summary>
		/// <param name="optionType">指定的设置项。</param>
		public static void WriteOptionValueToFile(OptionType optionType)
		{
			IOption option = optionMap[optionType];
			XmlElement rootNode = optionsXmlDocument.DocumentElement;
			XmlNode optionNode = rootNode.SelectSingleNode(optionsXmlXPathRoot + option.OptionName);
			PropertyInfo propertyInfo = option.GetType().GetProperty("Value");
			optionNode.InnerText = propertyInfo.GetValue(option).ToString();
		}

		private static void InitializationOptionsXmlFile()
		{
			XmlElement rootNode = optionsXmlDocument.CreateElement("Options");
			optionsXmlDocument.AppendChild(rootNode);

			foreach (KeyValuePair<OptionType, IOption> pair in optionMap)
				AddOptionToFile(pair.Key);

			optionsXmlDocument.Save(optionsXmlFilePath);
		}

		private static void InitializationOptions()
		{
			optionsXmlDocument.Load(optionsXmlFilePath);

			XmlElement rootNode = optionsXmlDocument.DocumentElement;
			foreach (KeyValuePair<OptionType, IOption> pair in optionMap)
			{
				XmlNode optionNode = rootNode.SelectSingleNode(optionsXmlXPathRoot + pair.Value.OptionName);
				if (optionNode != null)
				{
					IOption option = pair.Value;
					option.IsInitialization = true;
					option.SetRawValue(optionNode.InnerText);
				}
				else
					AddOptionToFile(pair.Key);
			}
		}
	}
}
