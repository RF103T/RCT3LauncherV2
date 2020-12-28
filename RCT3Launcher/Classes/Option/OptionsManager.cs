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
	public enum OptionType
	{
		GameInstallation, Language
	}

	public class OptionsManager
	{
		private static readonly string optionsXmlFilePath = "options.xml";
		private static readonly string optionsXmlXPathRoot = "/Options/";

		/// <summary>
		/// 获取设置管理器的单例。
		/// </summary>
		public static OptionsManager Instance
		{
			get
			{
				return instance;
			}
		}
		private static readonly OptionsManager instance = new OptionsManager();

		static OptionsManager()
		{
		}

		/// <summary>
		/// 指示设置是否已经初始化。
		/// </summary>
		public bool IsOptionsInitialized
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
		private bool isOptionsInitialized;

		private readonly XmlDocument optionsXmlDocument = new XmlDocument();
		private readonly Dictionary<OptionType, IOption> optionMap = new Dictionary<OptionType, IOption>()
		{
			{OptionType.Language,new LanguageOption(LanguageOption.LanguageParameter.zh_CN)},
			{OptionType.GameInstallation,new GameInstallationsOption(
				new ObservableCollection<GameInstallation>
				{
					new GameInstallation()
					{
						ID = 1,
						Name = "配置1",
						GameDirectory = "",
						IconIndex = 0
					}
				})
			}
		};

		private OptionsManager()
		{
			try
			{
				optionsXmlDocument.Load(optionsXmlFilePath);
				if (!optionsXmlDocument.DocumentElement.HasAttribute("IsOptionsInitialized") ||
					!System.Convert.ToBoolean(optionsXmlDocument.DocumentElement.GetAttribute("IsOptionsInitialized")))
					throw new Exception();
			}
			catch (Exception)
			{
				optionsXmlDocument.RemoveAll();
				InitializeOptionsXmlFile();
				return;
			}
			InitializeOptions();
		}

		/// <summary>
		/// 保存设置到文件。
		/// </summary>
		public void SaveOptionFile()
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
		public TOption GetOptionObject<TOption>(OptionType optionType)
		{
			IOption option = optionMap[optionType];
			if (option is TOption)
				return (TOption)option;
			throw new InvalidCastException();
		}

		/// <summary>
		/// 将指定的设置项加入Xml文档。
		/// </summary>
		/// <param name="optionType">指定的设置项。</param>
		public void AddOptionToDocument(OptionType optionType)
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
		public bool ReadOptionValueFromFile(OptionType optionType)
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
		public void WriteOptionValueToFile(OptionType optionType)
		{
			IOption option = optionMap[optionType];
			XmlElement rootNode = optionsXmlDocument.DocumentElement;
			XmlElement optionNode = rootNode.SelectSingleNode(optionsXmlXPathRoot + option.OptionName) as XmlElement;
			option.UpdateOptionValueInXmlElement(ref optionNode);
		}

		public void UserInitializeCompleted()
		{
			IsOptionsInitialized = true;
			optionsXmlDocument.DocumentElement.SetAttribute("IsOptionsInitialized", System.Convert.ToString(IsOptionsInitialized));
		}

		private void InitializeOptionsXmlFile()
		{
			XmlElement rootNode = optionsXmlDocument.CreateElement("Options");
			XmlAttribute isInitializedAttribute = optionsXmlDocument.CreateAttribute("IsOptionsInitialized");
			isInitializedAttribute.Value = System.Convert.ToString(false);
			rootNode.Attributes.Append(isInitializedAttribute);
			optionsXmlDocument.AppendChild(rootNode);

			foreach (KeyValuePair<OptionType, IOption> pair in optionMap)
			{
				pair.Value.InitializeOption();
				AddOptionToDocument(pair.Key);
			}

			optionsXmlDocument.Save(optionsXmlFilePath);
		}

		private void InitializeOptions()
		{
			IsOptionsInitialized = true;

			foreach (KeyValuePair<OptionType, IOption> pair in optionMap)
			{
				if (!ReadOptionValueFromFile(pair.Key))
					AddOptionToDocument(pair.Key);
			}
		}
	}
}
