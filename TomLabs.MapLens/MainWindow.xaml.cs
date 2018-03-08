using System.Windows;
using TomLabs.MapLens.Properties;
using TomLabs.MapLens.ViewModels;

namespace TomLabs.MapLens
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainViewModel VM { get; set; }
		private SelectorWindow SelectorWindow { get; set; }

		public MainWindow()
		{
			InitializeComponent();

			VM = new MainViewModel();
			DataContext = VM;

			SelectorWindow = new SelectorWindow(VM);
			SelectorWindow.Show();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
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
	}
}
