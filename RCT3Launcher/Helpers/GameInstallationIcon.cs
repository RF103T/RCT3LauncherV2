using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace RCT3Launcher
{
	public class GameInstallationIconHelper
	{
		public static List<DrawingImage> DarkIconDrawingImages
		{
			get
			{
				return darkIconDrawingImages;
			}
		}
		private readonly static List<DrawingImage> darkIconDrawingImages = new List<DrawingImage>()
		{
			App.Current.Resources["Dark_Steam_Logo"] as DrawingImage,
			App.Current.Resources["Dark_Epic_Logo"] as DrawingImage,
			App.Current.Resources["Dark_Gog_Logo"] as DrawingImage,
			App.Current.Resources["Dark_Computer_Logo"] as DrawingImage,
			App.Current.Resources["Dark_Game_Logo"] as DrawingImage,
		};

		public static List<DrawingImage> LightIconDrawingImages
		{
			get
			{
				return lightIconDrawingImages;
			}
		}
		private readonly static List<DrawingImage> lightIconDrawingImages = new List<DrawingImage>()
		{
			App.Current.Resources["Light_Steam_Logo"] as DrawingImage,
			App.Current.Resources["Light_Epic_Logo"] as DrawingImage,
			App.Current.Resources["Light_Gog_Logo"] as DrawingImage,
			App.Current.Resources["Light_Computer_Logo"] as DrawingImage,
			App.Current.Resources["Light_Game_Logo"] as DrawingImage,
		};
	}
}
