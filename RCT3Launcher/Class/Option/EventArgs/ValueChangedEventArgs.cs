using RCT3Launcher.Option.LauncherOptions;

namespace RCT3Launcher.Option.EventArgs
{
	public class ValueChangedEventArgs<TValue> : System.EventArgs
	{
		public TValue OldValue { get; set; }

		public TValue NewValue { get; set; }

		public static new ValueChangedEventArgs<TValue> Empty
		{
			get
			{
				return new ValueChangedEventArgs<TValue>();
			}
		}

		private ValueChangedEventArgs()
		{

		}

		public ValueChangedEventArgs(TValue oldValue, TValue newValue)
		{
			OldValue = oldValue;
			NewValue = newValue;
		}
	}
}
