using RCT3Launcher.Views.MessageBoxPages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace RCT3Launcher
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			NavigationCommands.BrowseBack.InputGestures.Clear();
			InitializeComponent();

			EventSystem.EventCenter.AddListener<Page, string, MessageBoxButton>(EventSystem.EventType.MessageBoxShow, MessageBoxShow);
		}

		#region MessageBox

		private void MessageBoxShow(Page content, string title, MessageBoxButton button)
		{
			messageBoxTitle.Text = title;
			messageBoxContent.Navigate(content);
			messageBoxCloseButton.IsEnabled = true;
			messageBoxOKButton.Visibility = Visibility.Collapsed;
			messageBoxYesButton.Visibility = Visibility.Collapsed;
			messageBoxNoButton.Visibility = Visibility.Collapsed;
			messageBoxCancelButton.Visibility = Visibility.Collapsed;
			switch (button)
			{
				case MessageBoxButton.OK:
				{
					messageBoxOKButton.Visibility = Visibility.Visible;
					break;
				}
				case MessageBoxButton.OKCancel:
				{
					messageBoxOKButton.Visibility = Visibility.Visible;
					messageBoxCancelButton.Visibility = Visibility.Visible;
					break;
				}
				case MessageBoxButton.YesNoCancel:
				{
					messageBoxYesButton.Visibility = Visibility.Visible;
					messageBoxNoButton.Visibility = Visibility.Visible;
					messageBoxCancelButton.Visibility = Visibility.Visible;
					break;
				}
				case MessageBoxButton.YesNo:
				{
					messageBoxCloseButton.IsEnabled = false;
					messageBoxYesButton.Visibility = Visibility.Visible;
					messageBoxNoButton.Visibility = Visibility.Visible;
					break;
				}
			}
			OpenMessageBoxOverlay();
		}

		private void MessageBoxButton_Click(object sender, RoutedEventArgs e)
		{
			string tag = (sender as Button).Tag.ToString();
			switch (tag)
			{
				case "OK":
				{
					EventSystem.EventCenter.Boardcast<MessageBoxResult>(EventSystem.EventType.MessageBoxClose, MessageBoxResult.OK);
					EventSystem.EventCenter.Boardcast<MessageBoxResult, object>(EventSystem.EventType.MessageBoxClose, MessageBoxResult.OK, messageBoxContent.Content);
					break;
				}
				case "Yes":
				{
					EventSystem.EventCenter.Boardcast<MessageBoxResult>(EventSystem.EventType.MessageBoxClose, MessageBoxResult.Yes);
					EventSystem.EventCenter.Boardcast<MessageBoxResult, object>(EventSystem.EventType.MessageBoxClose, MessageBoxResult.Yes, messageBoxContent.Content);
					break;
				}
				case "No":
				{
					EventSystem.EventCenter.Boardcast<MessageBoxResult>(EventSystem.EventType.MessageBoxClose, MessageBoxResult.No);
					EventSystem.EventCenter.Boardcast<MessageBoxResult, object>(EventSystem.EventType.MessageBoxClose, MessageBoxResult.No, messageBoxContent.Content);
					break;
				}
				case "Cancel":
				{
					EventSystem.EventCenter.Boardcast<MessageBoxResult>(EventSystem.EventType.MessageBoxClose, MessageBoxResult.Cancel);
					EventSystem.EventCenter.Boardcast<MessageBoxResult, object>(EventSystem.EventType.MessageBoxClose, MessageBoxResult.Cancel, messageBoxContent.Content);
					break;
				}
			}
			CloseMessageBoxOverlay();
		}

		private void OpenMessageBoxOverlay()
		{
			messageBoxOverlay.Visibility = Visibility.Visible;
			Storyboard messageBoxOpen = this.Resources["MessageBox_Open"] as Storyboard;
			messageBoxOpen.Begin();
		}

		private void CloseMessageBoxOverlay()
		{
			Storyboard messageBoxClose = this.Resources["MessageBox_Close"] as Storyboard;
			EventHandler timeLineCompleted = null;
			timeLineCompleted = new EventHandler((obj, e) =>
			{
				messageBoxOverlay.Visibility = Visibility.Collapsed;
				messageBoxClose.Completed -= timeLineCompleted;
			});
			messageBoxClose.Completed += timeLineCompleted;
			messageBoxClose.Begin();
		}

		#endregion
	}
}
