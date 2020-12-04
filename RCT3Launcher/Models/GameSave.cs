using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace RCT3Launcher.Models
{
	[Serializable]
	class GameSave : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public GameSave()
		{

		}

		public GameSave(string filePath)
		{
			SaveFileInfo = new FileInfo(filePath);
		}

		private int id;
		public int ID
		{
			get { return id; }
			set
			{
				if (id != value)
				{
					id = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ID"));
				}
			}
		}

		private string name;
		public string Name
		{
			get { return name; }
			set
			{
				if (name != value)
				{
					//演示
					if (SaveFileInfo != null)
					{
						StringBuilder builder = new StringBuilder(SaveFileInfo.FullName.Substring(0, SaveFileInfo.FullName.Length - SaveFileInfo.Name.Length));
						builder.Append(value).Append(".dat");
						SaveFileInfo.MoveTo(builder.ToString(), true);
					}
					name = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
				}
			}
		}

		private BitmapImage saveImage;
		public BitmapImage SaveImage
		{
			get { return saveImage; }
			set
			{
				if (saveImage != value)
				{
					saveImage = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SaveImage"));
				}
			}
		}

		private FileInfo saveFileInfo;
		public FileInfo SaveFileInfo
		{
			get { return saveFileInfo; }
			private set
			{
				if (saveFileInfo != value)
				{
					Name = saveFileInfo.Name.Substring(saveFileInfo.Name.Length - saveFileInfo.Extension.Length);
					saveFileInfo = value;
				}
			}
		}
	}
}
