using System;
using System.Windows;
using System.Windows.Input;
using TomLabs.MapLens.Helpers;
using TomLabs.MapLens.Properties;
using TomLabs.MapLens.ViewModels;

namespace TomLabs.MapLens
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public SharedViewModel VM { get; set; }
		private SelectorWindow SelectorWindow { get; set; }

		public MainWindow()
		{
			InitializeComponent();

			VM = new SharedViewModel();
			DataContext = VM;

			SelectorWindow = new SelectorWindow(VM);
			SelectorWindow.Show();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			VM.Stop();

			Settings.Default.MainWidth = Width;
			Settings.Default.MainHeight = Height;
			Settings.Default.MainTop = Top;
			Settings.Default.MainLeft = Left;

			Settings.Default.SelectorWidth = SelectorWindow.Width;
			Settings.Default.SelectorHeight = SelectorWindow.Height;
			Settings.Default.SelectorTop = SelectorWindow.Top;
			Settings.Default.SelectorLeft = SelectorWindow.Left;
			Settings.Default.Save();

			SelectorWindow.Close();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Width = Settings.Default.MainWidth;
			Height = Settings.Default.MainHeight;
			Top = Settings.Default.MainTop;
			Left = Settings.Default.MainLeft;

			SelectorWindow.Width = Settings.Default.SelectorWidth;
			SelectorWindow.Height = Settings.Default.SelectorHeight;
			SelectorWindow.Top = Settings.Default.SelectorTop;
			SelectorWindow.Left = Settings.Default.SelectorLeft;
		}

		private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
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
