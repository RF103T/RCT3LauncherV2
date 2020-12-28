using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace RCT3Launcher.Views.ViewHelpers.ItemStyleSelectors
{
	class MainMenuItemStyleSelector : StyleSelector
	{
		public Style MainMenuItemStyle { get; set; }

		public Style MainMenuEndItemStyle { get; set; }

		public override Style SelectStyle(object item, DependencyObject container)
		{
			ListBox listBox = ItemsControl.ItemsControlFromItemContainer(container) as ListBox;
			int index = listBox.ItemContainerGenerator.IndexFromContainer(container);
			int len = listBox.ItemContainerGenerator.Items.Count;
			if (len == index + 1)
				return MainMenuEndItemStyle;
			else
				return MainMenuItemStyle;
		}
	}
}
