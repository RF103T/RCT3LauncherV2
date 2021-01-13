using RCT3Launcher.Option;
using RCT3Launcher.Option.LauncherOptions;
using RCT3Launcher.Views.MessageBoxPages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace RCT3Launcher
{
	public class UpdateChecker
	{
		struct ApiInfo
		{
			[JsonInclude]
			public string tag_name;
			[JsonInclude]
			public bool prerelease;
			[JsonInclude]
			public string body;

			[JsonInclude]
			public Version version;
		}

		public bool isCheckingUpdate = false;

		private readonly string checkUrl = "https://api.github.com/repos/RF103T/RCT3LauncherV2/releases";

		public static UpdateChecker Instance
		{
			get
			{
				return instance;
			}
		}
		private static readonly UpdateChecker instance = new UpdateChecker();

		private Version ApplicationVersion
		{
			get
			{
				return typeof(App).Assembly.GetName().Version;
			}
		}

		private async Task<ApiInfo> GetLatestReleaseVersion()
		{
			return await Task<ApiInfo>.Run(() =>
			{
				WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
				webHeaderCollection.Add("User-Agent", "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/38.0.2125.122 Safari/537.36 SE 2.X MetaSr 1.0");
				using WebClient webClient = new WebClient();
				webClient.Headers = webHeaderCollection;
				string json = webClient.DownloadString(checkUrl);
				List<ApiInfo> list = JsonSerializer.Deserialize<List<ApiInfo>>(json, new JsonSerializerOptions
				{
					IncludeFields = true
				});
				ApiInfo latestInfo = list[0];
				Regex regex = new Regex(@"(\d{1,}\.)+\d{1,}");
				string versionStr = regex.Match(latestInfo.tag_name).Value + ".0";
				latestInfo.version = new Version(versionStr);
				isCheckingUpdate = false;
				return latestInfo;
			});
		}

		public async void CheckUpdate()
		{
			if (!isCheckingUpdate)
			{
				isCheckingUpdate = true;
				ApiInfo latestReleaseInfo = await GetLatestReleaseVersion();
				if (latestReleaseInfo.version.CompareTo(ApplicationVersion) == 1)
					MessageBox.Show<MarkDownMessageBoxPage>((res) =>
					{
						if (res == MessageBoxResult.Yes)
						{
							Process process = new Process();
							process.StartInfo.FileName = "Updater.exe";
							process.StartInfo.Arguments = string.Format("{0} {1} {2}", Process.GetCurrentProcess().Id,
															OptionsManager.Instance.GetOptionObject<LanguageOption>(OptionType.Language).Value.ToString(),
															latestReleaseInfo.tag_name);
							process.Start();
						}
					}, new MarkDownMessageBoxPage(latestReleaseInfo.body), App.Current.Resources["Updater_New_Version_Detected_Title"].ToString(), MessageBoxButton.YesNo);
				else
					MessageBox.Show<TextMessageBoxPage>(new TextMessageBoxPage(App.Current.Resources["Updater_Latest_Version_Text"].ToString()), App.Current.Resources["Updater_Check_For_Update_Title"].ToString());
				isCheckingUpdate = false;
			}
		}
	}
}
