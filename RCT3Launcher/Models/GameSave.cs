using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RCT3Launcher.Models
{
	[Serializable]
	public class GameSave : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public GameSave()
		{

		}

		public GameSave(string filePath)
		{
			SaveFileInfo = new FileInfo(filePath);
			GetSaveImage();
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
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID)));
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
					//if (SaveFileInfo != null && value != string.Empty && value != SaveFileInfo.Name)
					//{
					//	StringBuilder builder = new StringBuilder(SaveFileInfo.FullName.Substring(0, SaveFileInfo.FullName.Length - SaveFileInfo.Name.Length));
					//	builder.Append(value).Append(".dat");
					//	SaveFileInfo.MoveTo(builder.ToString(), true);
					//}
					name = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
				}
			}
		}

		private GameSaveType type;
		public GameSaveType Type
		{
			get { return type; }
			set
			{
				if (type != value)
				{
					type = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Type)));
				}
			}
		}

		private BitmapSource saveImage;
		public BitmapSource SaveImage
		{
			get { return saveImage; }
			set
			{
				if (saveImage != value)
				{
					saveImage = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SaveImage)));
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
					saveFileInfo = value;
					Name = saveFileInfo.Name.Substring(0, saveFileInfo.Name.Length - saveFileInfo.Extension.Length);
				}
			}
		}

		public void UpdateInfo()
		{
			GetSaveImage();
		}

		private async void GetSaveImage()
		{
			SaveImage = await Task.Run<BitmapSource>(async () =>
			{
				BitmapSource res = null;
				PixelFormat pixelFormat = PixelFormats.Bgra32;
				int width = 74;
				int height = 58;
				int rawStride = width * pixelFormat.BitsPerPixel / 8;
				byte[] rawImage = new byte[rawStride * height];

				using (FileStream file = File.Open(SaveFileInfo.FullName, FileMode.Open))
				{
					byte[] header = new byte[] { 0x50, 0x32, 0x00, 0x00, 0xC4, 0x10, 0x00, 0x00 }; //信息头
					byte[] src = new byte[(int)file.Length];
					_ = await file.ReadAsync(src.AsMemory(0, (int)file.Length));
					int index = Utils.ArrayUtils.IndexOf(src, header);
					if (index != -1)
					{
						index += 8;
						for (int h = height - 1; h >= 0; h--)
						{
							for (int w = 0; w < width; w++)
							{
								int offset = h * rawStride + w * 4;
								rawImage[offset] = src[index++]; //B
								rawImage[offset + 1] = src[index++]; //G
								rawImage[offset + 2] = src[index++]; //R
								rawImage[offset + 3] = 255; //A
							}
						}
						res = BitmapSource.Create(width, height, 96, 96, pixelFormat, null, rawImage, rawStride);
						res.Freeze();
					}
				}
				return res;
			});
		}
	}
}
