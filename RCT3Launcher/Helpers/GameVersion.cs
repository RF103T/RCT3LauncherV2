using System.Collections.Generic;
using System.Windows;

namespace RCT3Launcher
{
	public enum GameVersion
	{
		RollerCoaster_Tycoon_3_Platinum,
		RollerCoaster_Tycoon_3_Complete_Edition
	}

	public class GameVersionHelper
	{
		private readonly static Dictionary<uint, GameVersion> validTimeDateStamps = new Dictionary<uint, GameVersion>()
		{
			{0x434E4089, GameVersion.RollerCoaster_Tycoon_3_Platinum},
			{0x5F316745, GameVersion.RollerCoaster_Tycoon_3_Complete_Edition},
		};

		public static Dictionary<uint, GameVersion> ValidTimeDateStamps
		{
			get
			{
				return validTimeDateStamps;
			}
		}

		public static string GetGameVersionName(GameVersion gameVersion)
		{
			switch (gameVersion)
			{
				case GameVersion.RollerCoaster_Tycoon_3_Complete_Edition:
					return Application.Current.Resources["RCT3_Complete_Edition_Name"].ToString();
				default:
					return Application.Current.Resources["RCT3_Platinum_Name"].ToString();
			}
		}
	}
}
