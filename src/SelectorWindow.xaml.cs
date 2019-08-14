using System;
using System.Windows;
using System.Windows.Input;
using TomLabs.MapLens.Helpers;
using TomLabs.MapLens.ViewModels;

namespace TomLabs.MapLens
{
	/// <summary>
	/// Interaction logic for SelectorWindow.xaml
	/// </summary>
	public partial class SelectorWindow : Window
	{
		private int _aspectRatio = 1;

		private SharedViewModel VM { get; set; }

		public SelectorWindow(SharedViewModel viewModel)
		{
			InitializeComponent();

			VM = viewModel;
			DataContext = VM;
		}

		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				this.DragMove();
			}
		}

		private void Window_SourceInitialized(object sender, EventArgs e)
		{
			WindowAspectRatio.Register((Window)sender);
		}
	}
}
