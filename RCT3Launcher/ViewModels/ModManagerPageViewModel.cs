using RCT3Launcher.Models;
using RCT3Launcher.Validation;
using RCT3Launcher.ViewModels.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace RCT3Launcher.ViewModels
{
	public class ModManagerPageViewModel : ValidatableViewModelBase
	{
		private GameInstallation _password = new GameInstallation();
		//[Required]
		//[StringLength(10)]
		//[CustomValidation(typeof(ModManagerPageViewModel), "PasswordValidate")]
		public string Password
		{
			get
			{
				return _password.Name;
			}
			set
			{
				if(_password.Name != value)
				{
					_password.Name = value;
					RaisePropertyChanged(nameof(Password));
				}
			}
		}

		public static ValidationResult PasswordValidate(object obj, ValidationContext context)
		{
			var user = (ModManagerPageViewModel)context.ObjectInstance;
			if (Regex.IsMatch(user.Password,"4"))
			{
				return new ValidationResult("不能有4", new List<string> {"Password"});
			}
			return ValidationResult.Success;
		}
	}
}
