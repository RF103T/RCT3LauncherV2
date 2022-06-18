using System;
using System.Diagnostics;
using System.Windows;

namespace RCT3Launcher.Updater
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //private static readonly string downloadUrlTemplate = "https://github.com/RF103T/RCT3LauncherV2/releases/download/{0}/RCT3Launcher.zip";
        //private static readonly string downloadUrlTemplate_with_environment = "https://github.com/RF103T/RCT3LauncherV2/releases/download/{0}/RCT3Launcher_with_environment_update.zip";

        public static string downloadUrl;

        public static Process launcherProcess = null;

        private void OnStartup(object sender, StartupEventArgs e)
        {
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                App.Current.Shutdown();
                return;
            }

            if (e.Args == null || e.Args.Length < 3)
            {
                MessageBox.Show("No valid operating parameters specified!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown();
                return;
            }

            try
            {
                launcherProcess = Process.GetProcessById(Convert.ToInt32(e.Args[0]));
                if (!launcherProcess.ProcessName.Contains("RCT3"))
                    launcherProcess = null;
            }
            catch (Exception)
            {
                launcherProcess = null;
            }

            ResourceDictionary dict = new ResourceDictionary();
            switch (e.Args[1])
            {
                case "zh_CN":
                    {
                        dict.Source = new Uri(@"pack://application:,,,/zh_CN.xaml", UriKind.Absolute);
                        break;
                    }
                default:
                    {
                        dict.Source = new Uri(@"pack://application:,,,/en_US.xaml", UriKind.Absolute);
                        break;
                    }
            }
            App.Current.Resources.MergedDictionaries[0] = dict;

#if WITH_ENVIRONMENT
			downloadUrl = string.Format("https://github.com/RF103T/RCT3LauncherV2/releases/download/{0}/RCT3Launcher_with_environment_update.zip", e.Args[2]);
#else
            downloadUrl = string.Format("https://github.com/RF103T/RCT3LauncherV2/releases/download/{0}/RCT3Launcher.zip", e.Args[2]);
#endif
        }
    }
}
