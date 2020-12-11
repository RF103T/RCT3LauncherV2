using RCT3Launcher.Controls;
using RCT3Launcher.Option;
using RCT3Launcher.Option.LauncherOptions;
using RCT3Launcher.ViewModels.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCT3Launcher.ViewModels
{
	public class LauncherSettingsPageViewModel : ViewModelBase
	{
		private CommandBase<SwitchComboBox> languageSwitchComboBoxCommand;
		public CommandBase<SwitchComboBox> LanguageSwitchComboBoxCommand
		{
			get
			{
				if (languageSwitchComboBoxCommand == null)
				{
					languageSwitchComboBoxCommand = new CommandBase<SwitchComboBox>(
						new Action<SwitchComboBox>(
							self =>
							{
								LanguageOption setting = OptionsManager.GetOptionObject<LanguageOption>(OptionsManager.OptionType.Language);
								switch (self.SelectedIndex)
								{
									case 0:
									{
										setting.Value = LanguageOption.LanguageParameter.zh_CN;
										break;
									}
									case 1:
									{
										setting.Value = LanguageOption.LanguageParameter.en_US;
										break;
									}
								}
							}
							)
						);
				}
				return languageSwitchComboBoxCommand;
			}
		}
	}
}
