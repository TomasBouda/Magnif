using System.Windows;
using System.Windows.Input;
using TomLabs.MapLens.ViewModels;

namespace TomLabs.MapLens
{
	/// <summary>
	/// Interaction logic for SelectorWindow.xaml
	/// </summary>
	public partial class SelectorWindow : Window
	{
		public SelectorWindow(MainViewModel viewModel)
		{
			InitializeComponent();

			DataContext = viewModel;
		}

		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
				this.DragMove();
		}
	}
}
