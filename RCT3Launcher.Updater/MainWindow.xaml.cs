using System;
using System.Net;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.IO.Compression;
using System.Diagnostics;

namespace RCT3Launcher.Updater
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private WebClient webClient = new WebClient();

		private Storyboard progressBarIncreaseStoryboard = new Storyboard();
		private DoubleAnimation progressBarIncreaseAnimation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.8));

		private bool isCancelUpdate = false;

		public MainWindow()
		{
			InitializeComponent();
			grid.MouseLeftButtonDown += (sender, e) => DragMove();

			Storyboard.SetTarget(progressBarIncreaseAnimation, progressBar);
			Storyboard.SetTargetProperty(progressBarIncreaseAnimation, new PropertyPath("Value"));
			progressBarIncreaseStoryboard.Children.Add(progressBarIncreaseAnimation);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			DownloadFiles();
		}

		private void WindowsCloseButton_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show(App.Current.Resources["Window_Closing_MessageBox_Text"].ToString(), App.Current.Resources["Window_Closing_MessageBox_Title"].ToString(), MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
			{
				if (webClient.IsBusy)
				{
					webClient.CancelAsync();
					isCancelUpdate = true;
					infoTextBox.Text = App.Current.Resources["Download_Canceling_Text"].ToString();
					windowCloseButton.IsEnabled = false;
					progressBarIncreaseStoryboard.Stop();
					progressBar.IsIndeterminate = true;
				}
				else
					CloseApplication();
			}
		}

		private void CloseApplication()
		{
			webClient.Dispose();
			FileInfo del = new FileInfo(@"temp");
			if (del.Exists)
				del.Delete();

			Process process = new Process();
			process.StartInfo.FileName = "RCT3Launcher.exe";
			process.Start();

			App.Current.Shutdown();
		}

		private async void DownloadFiles()
		{
			webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
			webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;

			try
			{
				await webClient.DownloadFileTaskAsync(App.downloadUrl, @"temp");
			}
			catch (TaskCanceledException)
			{

			}
		}

		private async void UpdateFiles()
		{
			progressBar.Value = 100;
			windowCloseButton.IsEnabled = false;
			infoTextBox.Text = App.Current.Resources["Updating_Text"].ToString();
			await Task.Run(() =>
			{
				using (ZipArchive zipFile = ZipFile.OpenRead("temp"))
					zipFile.ExtractToDirectory(@"tempfiles", true);

				if (App.launcherProcess != null)
				{
					App.launcherProcess.Kill();
					App.launcherProcess.Dispose();
					Thread.Sleep(1000);
				}

				DirectoryInfo tempFilesDir = new DirectoryInfo(@"tempfiles");
				foreach (FileInfo file in tempFilesDir.GetFiles())
					file.CopyTo(file.Name, true);
				tempFilesDir.Delete(true);

				App.Current.Dispatcher.Invoke(() => infoTextBox.Text = App.Current.Resources["Updated_Text"].ToString());
				Thread.Sleep(2000);
				App.Current.Dispatcher.Invoke(() => CloseApplication());
			});
		}

		private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			if (!isCancelUpdate)
			{
				App.Current.Dispatcher.Invoke(() => progressBarIncreaseStoryboard.Pause());
				(progressBarIncreaseStoryboard.Children[0] as DoubleAnimation).To = e.ProgressPercentage;
				App.Current.Dispatcher.Invoke(() => progressBarIncreaseStoryboard.Begin());
			}
		}

		private void WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			if (e.Cancelled || isCancelUpdate)
				CloseApplication();
			else
				UpdateFiles();
		}
	}
}
