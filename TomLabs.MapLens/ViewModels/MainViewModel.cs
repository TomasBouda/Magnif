using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using KeypadX.ViewModels.Base;

namespace TomLabs.MapLens.ViewModels
{
	public class MainViewModel : BaseViewModel, INotifyPropertyChanged
	{
		[System.Runtime.InteropServices.DllImport("gdi32.dll")]
		public static extern bool DeleteObject(IntPtr hObject);

		public event PropertyChangedEventHandler PropertyChanged;

		private BitmapSource _screen;
		public BitmapSource Screen
		{
			get => _screen;
			set
			{
				_screen = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Screen)));
			}
		}

		public int SelectorTop { get; set; }
		public int SelectorLeft { get; set; }
		public int SelectorWidth { get; set; }
		public int SelectorHeight { get; set; }

		private DispatcherTimer _dispatchTimer = new DispatcherTimer()
		{
			Interval = TimeSpan.FromMilliseconds(250),
			IsEnabled = true,
		};

		public MainViewModel()
		{
			_dispatchTimer.Tick += _dispatchTimer_Tick;
			_dispatchTimer.Start();
		}

		private void CopyScreen()
		{
			var size = new System.Drawing.Size(SelectorWidth, SelectorHeight);
			using (var screenBmp = new Bitmap(SelectorWidth, SelectorHeight, PixelFormat.Format32bppArgb))
			{
				using (var bmpGraphics = Graphics.FromImage(screenBmp))
				{
					bmpGraphics.CopyFromScreen(SelectorLeft, SelectorTop, 0, 0, size);
					var handle = screenBmp.GetHbitmap();
					Screen = Imaging.CreateBitmapSourceFromHBitmap(
						handle,
						IntPtr.Zero,
						Int32Rect.Empty,
						BitmapSizeOptions.FromEmptyOptions());
					DeleteObject(handle);
				}
			}
		}

		private void _dispatchTimer_Tick(object sender, EventArgs e)
		{
			CopyScreen();
		}
	}
}
