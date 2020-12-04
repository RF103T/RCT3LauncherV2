using RCT3Launcher.Models;
using RCT3Launcher.Option.LauncherOptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RCT3Launcher.Option
{
	public class OptionsManager
	{
		/// <summary>
		/// 指示设置是否已经初始化。
		/// </summary>
		public static bool IsOptionsInitialized
		{
			private set
			{
				if (isOptionsInitialized != value)
					isOptionsInitialized = value;
			}
			get
			{
				return isOptionsInitialized;
			}
		}
		private static bool isOptionsInitialized;

		public enum OptionType
		{
			GameInstallation, Language
		}

		private static readonly string optionsXmlFilePath = "options.xml";
		private static readonly XmlDocument optionsXmlDocument;

		private static readonly string optionsXmlXPathRoot = "/Options/";

		public static readonly Dictionary<OptionType, IOption> optionMap = new Dictionary<OptionType, IOption>()
		{
			{OptionType.Language,new LanguageOption(LanguageOption.LanguageParameter.zh_CN)},
			{OptionType.GameInstallation,new GameInstallationsOption(
				new ObservableCollection<GameInstallation>
				{
					new GameInstallation()
					{
						ID = 1,
						Name = "配置1",
						FullNamePath = "",
						IconIndex = 0
					}
				})
			}
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
			foreach (OptionType type in optionMap.Keys)
				WriteOptionValueToFile(type);
			optionsXmlDocument.Save(optionsXmlFilePath);
		}

		/// <summary>
		/// 获取指定设置项。
		/// </summary>
		/// <typeparam name="TOption">设置项的类型。</typeparam>
		/// <param name="optionType">指定的设置项。</param>
		/// <returns>指定设置项的值。</returns>
		public static TOption GetOptionObject<TOption>(OptionType optionType)
		{
			IOption option = optionMap[optionType];
			if (option is TOption)
				return (TOption)option;
			throw new InvalidCastException();
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
		/// 将指定的设置项加入Xml文档。
		/// </summary>
		/// <param name="optionType">指定的设置项。</param>
		public static void AddOptionToDocument(OptionType optionType)
		{
			IOption option = optionMap[optionType];
			XmlElement rootNode = optionsXmlDocument.DocumentElement;
			XmlElement optionNote = option.OptionValueToXmlElement(optionsXmlDocument);
			rootNode.AppendChild(optionNote);
		}

		/// <summary>
		/// 从Xml文档中读取指定设置项的值。
		/// </summary>
		/// <param name="optionType">指定的设置项。</param>
		/// <returns>指示指定设置项是否存在。</returns>
		public static bool ReadOptionValueFromFile(OptionType optionType)
		{
			IOption option = optionMap[optionType];
			XmlElement rootNode = optionsXmlDocument.DocumentElement;
			XmlElement optionNode = rootNode.SelectSingleNode(optionsXmlXPathRoot + option.OptionName) as XmlElement;
			if (optionNode != null)
			{
				option.XmlElementToOptionValue(optionNode);
				return true;
			}
			return false;
		}

		/// <summary>
		/// 把指定设置项的值写入Xml文档。
		/// </summary>
		/// <param name="optionType">指定的设置项。</param>
		public static void WriteOptionValueToFile(OptionType optionType)
		{
			IOption option = optionMap[optionType];
			XmlElement rootNode = optionsXmlDocument.DocumentElement;
			XmlElement optionNode = rootNode.SelectSingleNode(optionsXmlXPathRoot + option.OptionName) as XmlElement;
			option.UpdateOptionValueInXmlElement(ref optionNode);
		}

		private static void InitializationOptionsXmlFile()
		{
			XmlElement rootNode = optionsXmlDocument.CreateElement("Options");
			optionsXmlDocument.AppendChild(rootNode);

			foreach (KeyValuePair<OptionType, IOption> pair in optionMap)
				AddOptionToDocument(pair.Key);

			optionsXmlDocument.Save(optionsXmlFilePath);
		}

		private static void InitializationOptions()
		{
			IsOptionsInitialized = true;

			optionsXmlDocument.Load(optionsXmlFilePath);

			foreach (KeyValuePair<OptionType, IOption> pair in optionMap)
			{
				if (!ReadOptionValueFromFile(pair.Key))
					AddOptionToDocument(pair.Key);
			}
		}
	}
}
