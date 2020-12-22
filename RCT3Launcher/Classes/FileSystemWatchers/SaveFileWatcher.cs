using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RCT3Launcher.FileSystemWatchers
{
	public class SaveFileWatcher : IDisposable
	{
		private Timer fileChangedTimer = null;
		private Semaphore semaphore = new Semaphore(1, 1);
		private Dictionary<string, FileSystemEventArgs> waitForNotifyEvents = new Dictionary<string, FileSystemEventArgs>();

		private readonly Dictionary<GameSaveType, FileSystemWatcher> watchers = new Dictionary<GameSaveType, FileSystemWatcher>()
		{
			//{GameSaveType.Park,new FileSystemWatcher(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RCT3\Parks")
			//{
			//	Filter="*.dat"
			//}},
			//{GameSaveType.Scenario,new FileSystemWatcher(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RCT3\Scenarios")
			//{
			//	Filter="*.dat"
			//}},
			{GameSaveType.Start_New_Scenario,new FileSystemWatcher(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RCT3\Start New Scenarios")
			{
				Filter = "*.dat",
				NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size,
				EnableRaisingEvents = true
			}
			}
		};

		public SaveFileWatcher()
		{
			foreach (KeyValuePair<GameSaveType, FileSystemWatcher> pair in watchers)
			{
				pair.Value.Created += OnChanged;
				pair.Value.Changed += OnChanged;
				pair.Value.Deleted += OnChanged;
				pair.Value.Renamed += OnRenamed;
			}
			fileChangedTimer = new Timer(OnTimerOver, null, Timeout.Infinite, Timeout.Infinite);
		}

		~SaveFileWatcher()
		{
			Dispose();
		}

		private void OnChanged(object source, FileSystemEventArgs e)
		{
			foreach (KeyValuePair<GameSaveType, FileSystemWatcher> pair in watchers)
			{
				if (pair.Value == source)
				{
					if (e.ChangeType == WatcherChangeTypes.Deleted)
						EventSystem.EventCenter.BoardcastAsync<FileSystemEventArgs>(EventSystem.EventType.FileChanged, e);
					else
					{
						semaphore.WaitOne();
						if (!waitForNotifyEvents.ContainsKey(e.FullPath))
							waitForNotifyEvents.Add(e.FullPath, e);
						semaphore.Release();
						fileChangedTimer.Change(1000, Timeout.Infinite);
					}
				}
			}
		}

		private void OnTimerOver(object state)
		{
			List<FileSystemEventArgs> temp = new List<FileSystemEventArgs>();
			semaphore.WaitOne();
			temp.AddRange(waitForNotifyEvents.Values);
			waitForNotifyEvents.Clear();
			semaphore.Release();

			foreach (FileSystemEventArgs e in temp)
				EventSystem.EventCenter.BoardcastAsync<FileSystemEventArgs>(EventSystem.EventType.FileChanged, e);
		}

		private void OnRenamed(object source, RenamedEventArgs e)
		{
			foreach (KeyValuePair<GameSaveType, FileSystemWatcher> pair in watchers)
				if (pair.Value == source)
					EventSystem.EventCenter.BoardcastAsync<RenamedEventArgs>(EventSystem.EventType.FileRenamed, e);
		}

		public void Dispose()
		{
			foreach (KeyValuePair<GameSaveType, FileSystemWatcher> pair in watchers)
			{
				if (pair.Value != null)
				{
					pair.Value.Created -= OnChanged;
					pair.Value.Changed -= OnChanged;
					pair.Value.Deleted -= OnChanged;
					pair.Value.Renamed -= OnRenamed;
					pair.Value.Dispose();
					watchers.Remove(pair.Key);
				}
			}
		}
	}
}
