using RCT3Launcher.Models;
using RCT3Launcher.Option;
using RCT3Launcher.Option.EventArgs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace RCT3Launcher.Option.LauncherOptions
{
	public class GameInstallationsOption : OptionBase<ObservableCollection<GameInstallation>>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="defaultValue">设置项的默认值。</param>
		public GameInstallationsOption(ObservableCollection<GameInstallation> defaultValue) : base("GameInstallations", defaultValue)
		{

		}

		public override XmlElement OptionValueToXmlElement(XmlDocument document)
		{
			XmlElement optionRootNode = base.OptionValueToXmlElement(document);
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<GameInstallation>));
			string xmlText;
			using (MemoryStream xmlTextStream = new MemoryStream())
			{
				xmlSerializer.Serialize(xmlTextStream, value);
				xmlTextStream.Position = 0;
				using BinaryReader binaryReader = new BinaryReader(xmlTextStream);
				xmlText = Encoding.UTF8.GetString(binaryReader.ReadBytes((int)xmlTextStream.Length));
			}
			XmlDocument temp = new XmlDocument();
			temp.LoadXml(xmlText);
			foreach (XmlNode node in temp.DocumentElement.ChildNodes)
			{
				XmlNode importNode = optionRootNode.OwnerDocument.ImportNode(node, true);
				optionRootNode.AppendChild(importNode);
			}
			return optionRootNode;
		}

		public override void UpdateOptionValueInXmlElement(ref XmlElement optionElement)
		{
			optionElement.RemoveAll();
			XmlNode root = OptionValueToXmlElement(optionElement.OwnerDocument);
			foreach (XmlNode node in root.ChildNodes)
			{
				XmlNode importNode = optionElement.OwnerDocument.ImportNode(node, true);
				optionElement.AppendChild(importNode);
			}
		}

		public override void XmlElementToOptionValue(XmlElement optionElement)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameInstallation));
			foreach (XmlNode node in optionElement.ChildNodes)
			{
				using StringReader textReader = new StringReader(node.OuterXml);
				if (xmlSerializer.Deserialize(textReader) is GameInstallation installation)
					value.Add(installation);
			}
		}
	}
}
