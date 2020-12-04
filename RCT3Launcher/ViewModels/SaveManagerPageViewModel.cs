using RCT3Launcher.Models;
using RCT3Launcher.ViewModels.BaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCT3Launcher.ViewModels
{
	class SaveManagerPageViewModel : ViewModelBase
	{
		public SaveManagerPageViewModel()
		{
			GameSaves = new ObservableCollection<GameSave>()
			{
				new GameSave()
				{
					ID = 1,
					Name = "存档1124245278278272872727827"
				},
				new GameSave()
				{
					ID = 2,
					Name = "存档2"
				},
				new GameSave()
				{
					ID = 3,
					Name = "存档3"
				},
				new GameSave()
				{
					ID = 4,
					Name = "存档4"
				},
				new GameSave()
				{
					ID = 1,
					Name = "存档1"
				},
				new GameSave()
				{
					ID = 2,
					Name = "存档2"
				},
				new GameSave()
				{
					ID = 3,
					Name = "存档3"
				},
				new GameSave()
				{
					ID = 4,
					Name = "存档4"
				}
			};
		}

		private ObservableCollection<GameSave> gameSaves;
		public ObservableCollection<GameSave> GameSaves
		{
			get
			{
				return gameSaves;
			}
			set
			{
				if (gameSaves != value)
				{
					gameSaves = value;
					RaisePropertyChanged("GameSaves");
				}
			}
		}
	}
}
