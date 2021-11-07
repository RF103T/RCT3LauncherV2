using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RCT3Launcher
{
	public enum GameSaveType
	{
		Park, Scenario, Start_New_Scenario
	}

	public class GameSaveTypeHelper
	{
		private readonly static Dictionary<GameSaveType, string> saveTypeFullPath = new Dictionary<GameSaveType, string>()
		{
			{GameSaveType.Park,Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RCT3\Parks"},
			{GameSaveType.Scenario,Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RCT3\Scenarios"},
			{GameSaveType.Start_New_Scenario,Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RCT3\Start New Scenarios"}
		};

		public static string GetGameSaveTypeFullPath(GameSaveType type)
		{
			return saveTypeFullPath[type];
		}

		public static string GetGameSaveTypeSafeFullPath(GameSaveType type)
		{
			DirectoryInfo dirInfo = new DirectoryInfo(saveTypeFullPath[type]);
			if (!dirInfo.Exists)
				dirInfo.Create();
			return saveTypeFullPath[type];
		}

		public static string GetGameSaveTypeFormattedText(GameSaveType type)
		{
			return type switch
			{
				GameSaveType.Park => App.Current.Resources["SaveType_Park_FormattedText"].ToString(),
				GameSaveType.Scenario => App.Current.Resources["SaveType_Scenario_FormattedText"].ToString(),
				GameSaveType.Start_New_Scenario => App.Current.Resources["SaveType_Start_New_Scenario_FormattedText"].ToString(),
				_ => throw new NotImplementedException(),
			};
		}
	}
}
