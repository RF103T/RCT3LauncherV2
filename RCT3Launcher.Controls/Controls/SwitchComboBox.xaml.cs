using RCT3Launcher.Controls.InternalDataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace RCT3Launcher.Controls
{
	public partial class SwitchComboBox : UserControl
	{
		#region 参数

		#region Command

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}
		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.Register("Command", typeof(ICommand), typeof(SwitchComboBox), new PropertyMetadata(default(ICommand)));

		#endregion

		#region CommandParameter

		public object CommandParameter
		{
			get { return (object)GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}
		public static readonly DependencyProperty CommandParameterProperty =
			DependencyProperty.Register("CommandParameter", typeof(object), typeof(SwitchComboBox), new PropertyMetadata(default(object)));

		#endregion

		#region CommandTarget

		public IInputElement CommandTarget { get; set; }

		#endregion

		#region Items

		private ObservableCollection<SwitchComboBoxItemModel> items = new ObservableCollection<SwitchComboBoxItemModel>();

		public ObservableCollection<SwitchComboBoxItemModel> Items
		{
			get
			{
				return items;
			}
			set
			{
				if (items != value)
				{
					items.CollectionChanged -= OnItemsContentChanged;
					items = value;
					items.CollectionChanged += OnItemsContentChanged;
				}
			}
		}

		private void OnItemsContentChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			int len = Items.Count;
			if (grid.ColumnDefinitions.Count != len)
			{
				grid.ColumnDefinitions.Clear();
				for (int i = 0; i < len; i++)
				{
					grid.ColumnDefinitions.Add(new ColumnDefinition()
					{
						Width = new GridLength(1.0, GridUnitType.Star)
					});
				}
				Grid.SetColumnSpan(listBox, len);
				Grid.SetColumn(BackRect, 0);
			}
		}

		#endregion

		#region NegativeForeground

		public Brush NegativeForeground
		{
			get { return (Brush)GetValue(NegativeForegroundProperty); }
			set { SetValue(NegativeForegroundProperty, value); }
		}
		public static readonly DependencyProperty NegativeForegroundProperty =
			DependencyProperty.Register("NegativeForeground", typeof(Brush), typeof(SwitchComboBox), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 55, 71, 79))));

		#endregion

		#region SelectedIndex

		public int SelectedIndex
		{
			get { return (int)GetValue(SelectedIndexProperty); }
			set { SetValue(SelectedIndexProperty, value); }
		}
		public static readonly DependencyProperty SelectedIndexProperty =
			DependencyProperty.Register("SelectedIndex", typeof(int), typeof(SwitchComboBox), new PropertyMetadata(0, new PropertyChangedCallback(OnSelectedIndexChanged)));

		private static void OnSelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SwitchComboBox self = d as SwitchComboBox;
			TransformGroup transformGroup = self.BackRect.RenderTransform as TransformGroup;
			foreach (Transform transform in transformGroup.Children)
				if (transform is TranslateTransform translateTransform)
					translateTransform.X = (int)e.NewValue * self.BackRect.ActualWidth;
		}

		#endregion

		#endregion

		#region 事件

		public event SelectionChangedEventHandler SelectionChanged
		{
			add
			{
				this.AddHandler(SelectionChangedEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(SelectionChangedEvent, value);
			}
		}
		public static readonly RoutedEvent SelectionChangedEvent =
			EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Bubble, typeof(SelectionChangedEventHandler), typeof(SwitchComboBox));

		private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.Command != null)
			{
				this.Command.Execute(CommandParameter);

				e.RoutedEvent = SelectionChangedEvent;
				e.Source = this;
				this.RaiseEvent(e);
			}
		}

		#endregion

		private Storyboard mouseEnterBackRectMove, mouseLeaveBackRectMove, mouseDownBackRectScale, mouseUpBackRectScale;
		private DoubleAnimation backRectMoveToAnimation, backRectMoveBackAnimation, backRectScaleXToAnimation, backRectScaleYToAnimation, backRectScaleXBackAnimation, backRectScaleYBackAnimation;
		private PointAnimation backRectRenderTransformOriginToAnimation, backRectRenderTransformOriginBackAnimation;

		public SwitchComboBox()
		{
			InitializeComponent();

			InitializeAnimation();
			Items.CollectionChanged += OnItemsContentChanged;
		}

		~SwitchComboBox()
		{
			Items.CollectionChanged -= OnItemsContentChanged;
		}

		public void ListBoxItemMouseEnter(object sender, MouseEventArgs e)
		{
			ListBoxItem item = sender as ListBoxItem;
			int index = listBox.ItemContainerGenerator.IndexFromContainer(item);

			backRectMoveToAnimation.From = BackRect.RenderTransform.Value.OffsetX;
			backRectMoveToAnimation.To = BackRect.ActualWidth * index;
			backRectRenderTransformOriginToAnimation.From = BackRect.RenderTransformOrigin;
			backRectRenderTransformOriginToAnimation.To = new Point(0.5 + index, 0.5);
			mouseEnterBackRectMove.Children.Clear();
			mouseEnterBackRectMove.Children.Add(backRectMoveToAnimation);
			mouseEnterBackRectMove.Children.Add(backRectRenderTransformOriginToAnimation);
			mouseEnterBackRectMove.Begin();
		}

		public void ListBoxItemMouseLeave(object sender, MouseEventArgs e)
		{
			backRectMoveBackAnimation.From = BackRect.RenderTransform.Value.OffsetX;
			backRectMoveBackAnimation.To = BackRect.ActualWidth * SelectedIndex;
			backRectRenderTransformOriginBackAnimation.From = BackRect.RenderTransformOrigin;
			backRectRenderTransformOriginBackAnimation.To = new Point(0.5 + SelectedIndex, 0.5);
			mouseEnterBackRectMove.Children.Clear();
			mouseEnterBackRectMove.Children.Add(backRectMoveBackAnimation);
			mouseEnterBackRectMove.Children.Add(backRectRenderTransformOriginBackAnimation);
			mouseEnterBackRectMove.Begin();
		}

		public void ListBoxItemMouseDown(object sender, MouseButtonEventArgs e)
		{
			backRectScaleXToAnimation.From = BackRect.RenderTransform.Value.M11;
			backRectScaleYToAnimation.From = BackRect.RenderTransform.Value.M22;
			mouseDownBackRectScale.Children.Clear();
			mouseDownBackRectScale.Children.Add(backRectScaleXToAnimation);
			mouseDownBackRectScale.Children.Add(backRectScaleYToAnimation);
			mouseDownBackRectScale.Begin();
		}

		public void ListBoxItemMouseUp(object sender, MouseButtonEventArgs e)
		{
			backRectScaleXBackAnimation.From = BackRect.RenderTransform.Value.M11;
			backRectScaleYBackAnimation.From = BackRect.RenderTransform.Value.M22;
			mouseUpBackRectScale.Children.Clear();
			mouseUpBackRectScale.Children.Add(backRectScaleXBackAnimation);
			mouseUpBackRectScale.Children.Add(backRectScaleYBackAnimation);
			mouseUpBackRectScale.Begin();
		}

		private void InitializeAnimation()
		{
			mouseEnterBackRectMove = new Storyboard();
			backRectMoveToAnimation = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.2)))
			{
				EasingFunction = new CircleEase()
				{
					EasingMode = EasingMode.EaseInOut
				}
			};
			backRectRenderTransformOriginToAnimation = new PointAnimation(new Point(0.5, 0.5), new Duration(TimeSpan.FromSeconds(0.2)))
			{
				EasingFunction = new CircleEase()
				{
					EasingMode = EasingMode.EaseInOut
				}
			};
			Storyboard.SetTarget(backRectMoveToAnimation, BackRect);
			Storyboard.SetTargetProperty(backRectMoveToAnimation, new PropertyPath("RenderTransform.Children[0].X"));
			Storyboard.SetTarget(backRectRenderTransformOriginToAnimation, BackRect);
			Storyboard.SetTargetProperty(backRectRenderTransformOriginToAnimation, new PropertyPath("RenderTransformOrigin"));

			mouseLeaveBackRectMove = new Storyboard();
			backRectMoveBackAnimation = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.2)))
			{
				EasingFunction = new CircleEase()
				{
					EasingMode = EasingMode.EaseInOut
				}
			};
			backRectRenderTransformOriginBackAnimation = new PointAnimation(new Point(0.5, 0.5), new Duration(TimeSpan.FromSeconds(0.2)))
			{
				EasingFunction = new CircleEase()
				{
					EasingMode = EasingMode.EaseInOut
				}
			};
			Storyboard.SetTarget(backRectMoveBackAnimation, BackRect);
			Storyboard.SetTargetProperty(backRectMoveBackAnimation, new PropertyPath("RenderTransform.Children[0].X"));
			Storyboard.SetTarget(backRectRenderTransformOriginBackAnimation, BackRect);
			Storyboard.SetTargetProperty(backRectRenderTransformOriginBackAnimation, new PropertyPath("RenderTransformOrigin"));

			mouseDownBackRectScale = new Storyboard();
			backRectScaleXToAnimation = new DoubleAnimation(0.9, new Duration(TimeSpan.FromSeconds(0.1)))
			{
				EasingFunction = new CircleEase()
				{
					EasingMode = EasingMode.EaseInOut
				}
			};
			backRectScaleYToAnimation = new DoubleAnimation(0.9, new Duration(TimeSpan.FromSeconds(0.1)))
			{
				EasingFunction = new CircleEase()
				{
					EasingMode = EasingMode.EaseInOut
				}
			};
			Storyboard.SetTarget(backRectScaleXToAnimation, BackRect);
			Storyboard.SetTargetProperty(backRectScaleXToAnimation, new PropertyPath("RenderTransform.Children[1].ScaleX"));
			Storyboard.SetTarget(backRectScaleYToAnimation, BackRect);
			Storyboard.SetTargetProperty(backRectScaleYToAnimation, new PropertyPath("RenderTransform.Children[1].ScaleY"));

			mouseUpBackRectScale = new Storyboard();
			backRectScaleXBackAnimation = new DoubleAnimation(1.0, new Duration(TimeSpan.FromSeconds(0.8)))
			{
				EasingFunction = new ElasticEase()
			};
			backRectScaleYBackAnimation = new DoubleAnimation(1.0, new Duration(TimeSpan.FromSeconds(0.8)))
			{
				EasingFunction = new ElasticEase()
			};
			Storyboard.SetTarget(backRectScaleXBackAnimation, BackRect);
			Storyboard.SetTargetProperty(backRectScaleXBackAnimation, new PropertyPath("RenderTransform.Children[1].ScaleX"));
			Storyboard.SetTarget(backRectScaleYBackAnimation, BackRect);
			Storyboard.SetTargetProperty(backRectScaleYBackAnimation, new PropertyPath("RenderTransform.Children[1].ScaleY"));
		}
	}
}