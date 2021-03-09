namespace RCT3Launcher.Classes.MessageBox
{
	public interface IValueMessageBoxPage<out T>
	{
		public T GetReturnValue();
	}
}
