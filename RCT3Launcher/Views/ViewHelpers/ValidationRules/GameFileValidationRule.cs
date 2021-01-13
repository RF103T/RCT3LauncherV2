using PeNet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RCT3Launcher.Views.ViewHelpers.ValidationRules
{
	public class GameFileValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			DirectoryInfo gameDirectory = new DirectoryInfo(value.ToString());
			if (gameDirectory.Exists)
			{
				FileInfo[] subFiles = gameDirectory.GetFiles();
				foreach (FileInfo info in subFiles)
				{
					if (info.Extension.ToLower() == ".exe" && info.Name.Contains("RCT3"))
					{
						using FileStream peFileStream = new FileStream(info.FullName, FileMode.Open);
						PeFile peFile = new PeFile(peFileStream);
						if (GameVersionHelper.ValidTimeDateStamps.Keys.Contains(peFile.ImageNtHeaders.FileHeader.TimeDateStamp))
							return ValidationResult.ValidResult;
					}
				}
			}
			return new ValidationResult(false, App.Current.Resources["ValidationRule_GameFileError"]);
		}

		public static bool _validate(object value)
		{
			DirectoryInfo gameDirectory = new DirectoryInfo(value.ToString());
			if (gameDirectory.Exists)
			{
				FileInfo[] subFiles = gameDirectory.GetFiles();
				foreach (FileInfo info in subFiles)
				{
					if (info.Extension.ToLower() == ".exe" && info.Name.Contains("RCT3"))
					{
						using FileStream peFileStream = new FileStream(info.FullName, FileMode.Open);
						PeFile peFile = new PeFile(peFileStream);
						if (GameVersionHelper.ValidTimeDateStamps.Keys.Contains(peFile.ImageNtHeaders.FileHeader.TimeDateStamp))
							return true;
					}
				}
			}
			return false;
		}
	}
}
