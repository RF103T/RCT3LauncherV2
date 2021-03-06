﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RCT3Launcher.Views.ViewHelpers.ItemStyleSelectors
{
	class ListItemStyleSelector : StyleSelector
	{
		public Style EvenItemStyle { get; set; }

		public Style OddItemStyle { get; set; }

		public override Style SelectStyle(object item, DependencyObject container)
		{
			ListBox listBox = ItemsControl.ItemsControlFromItemContainer(container) as ListBox;
			if (listBox.Tag.ToString() == "List")
			{
				int index = listBox.ItemContainerGenerator.IndexFromContainer(container);
				if ((index & 1) == 1)
					return OddItemStyle;
				else
					return EvenItemStyle;
			}
			return App.Current.Resources["List_ImageItem2_Style"] as Style;
		}
	}
}
