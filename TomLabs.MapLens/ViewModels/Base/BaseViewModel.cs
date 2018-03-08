using System.ComponentModel;

namespace KeypadX.ViewModels.Base
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		/// <summary>
		/// The event that is fired when any child property changes its value
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

	}
}
