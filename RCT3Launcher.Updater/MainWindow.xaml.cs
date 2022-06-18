using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.IO.Compression;
using System.Diagnostics;
using System.Net.Http;

namespace RCT3Launcher.Updater
{
    /// <summary>
    /// 更新主界面
    /// 简单实现Launcher的更新工作
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly HttpClient httpClient = new HttpClient();

        private Storyboard progressBarIncreaseStoryboard = new Storyboard();
        private DoubleAnimation progressBarIncreaseAnimation = new DoubleAnimation(0, TimeSpan.FromMilliseconds(50));

        private bool isCancelUpdate = false;

        private const int DownloadBufferLen = 8192;

        static MainWindow()
        {
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/38.0.2125.122 Safari/537.36 SE 2.X MetaSr 1.0");
        }

        public MainWindow()
        {
            InitializeComponent();
            grid.MouseLeftButtonDown += (sender, e) => DragMove();

            Storyboard.SetTarget(progressBarIncreaseAnimation, progressBar);
            Storyboard.SetTargetProperty(progressBarIncreaseAnimation, new PropertyPath("Value"));
            progressBarIncreaseStoryboard.Children.Add(progressBarIncreaseAnimation);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await DownloadFiles();
            await UpdateFiles();
        }

        private void WindowsCloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(App.Current.Resources["Window_Closing_MessageBox_Text"].ToString(), App.Current.Resources["Window_Closing_MessageBox_Title"].ToString(), MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                CloseApplication();
        }

        private void CloseApplication()
        {
            isCancelUpdate = true;
            httpClient.CancelPendingRequests();
            httpClient.Dispose();

            FileInfo del = new FileInfo(@"temp");
            if (del.Exists)
                del.Delete();

            if (!isCancelUpdate)
            {
                if (File.Exists("RCT3Launcher.exe"))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo()
                    {
                        FileName = "RCT3Launcher.exe"
                    };
                    Process.Start(startInfo);
                }
            }

            App.Current.Shutdown();
        }

        /// <summary>
        /// 下载更新文件
        /// </summary>
        private async Task DownloadFiles()
        {
            UpdateProgressBar(0);

            await Task.Run(async () =>
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(App.downloadUrl);

                    response.EnsureSuccessStatusCode();

                    long contentByteLen = response.Content.Headers.ContentLength.Value;
                    long currReadByteLen = 0;
                    using Stream download = await response.Content.ReadAsStreamAsync();

                    byte[] buffer = new byte[DownloadBufferLen];
                    using FileStream fileStream = File.Create(@"temp", DownloadBufferLen);
                    while (download.Position != download.Length)
                    {
                        int readByteLen = await download.ReadAsync(buffer, 0, DownloadBufferLen);
                        await fileStream.WriteAsync(buffer, 0, readByteLen);

                        currReadByteLen += readByteLen;
                        UpdateProgressBar((int)(currReadByteLen * 100 / contentByteLen));
                    }
                }
                catch (HttpRequestException)
                {
                    if (MessageBox.Show(App.Current.Resources["Connect_Timeout_Error"].ToString(), App.Current.Resources["Window_Error_MessageBox_Title"].ToString(), MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.Yes)
                        CloseApplication();
                }
            });

            UpdateProgressBar(100);
        }

        /// <summary>
        /// 更新启动器文件
        /// </summary>
        private async Task UpdateFiles()
        {
            // 更新界面基础状态
            UpdateProgressBar(0);
            windowCloseButton.IsEnabled = false;
            infoTextBox.Text = App.Current.Resources["Updating_Text"].ToString();

            await Task.Run(() =>
            {
                using ZipArchive zipFile = ZipFile.OpenRead("temp");
                zipFile.ExtractToDirectory(@"tempfiles", true);
                UpdateProgressBar(40);

                if (App.launcherProcess != null)
                {
                    App.launcherProcess.Kill();
                    App.launcherProcess.Dispose();
                }

                DirectoryInfo tempFilesDir = new DirectoryInfo(@"tempfiles");
                FileInfo[] fileInfos = tempFilesDir.GetFiles();

                int fileCount = fileInfos.Length;
                int currFileCount = 0;
                foreach (FileInfo file in fileInfos)
                {
                    try
                    {
                        file.CopyTo(file.Name, true);
                        currFileCount++;
                        UpdateProgressBar(40 + (60 * currFileCount / fileCount));
                    }
                    catch (IOException)
                    {
                        if (MessageBox.Show(App.Current.Resources["Copy_File_Error"].ToString(), App.Current.Resources["Window_Error_MessageBox_Title"].ToString(), MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.Yes)
                            CloseApplication();
                    }
                }
                tempFilesDir.Delete(true);
                zipFile.Dispose();
                UpdateProgressBar(100);

                App.Current.Dispatcher.Invoke(() => infoTextBox.Text = App.Current.Resources["Updated_Text"].ToString());
                Thread.Sleep(1000);
                App.Current.Dispatcher.Invoke(() => CloseApplication());
            });
        }

        /// <summary>
        /// 更新进度条
        /// </summary>
        /// <param name="percent">进度条进度，范围[0，100]</param>
        private void UpdateProgressBar(int percent)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                progressBarIncreaseStoryboard.Pause();
                (progressBarIncreaseStoryboard.Children[0] as DoubleAnimation).To = percent;
                progressBarIncreaseStoryboard.Begin();
            });
        }
    }
}
