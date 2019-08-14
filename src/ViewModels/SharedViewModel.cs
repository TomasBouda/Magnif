using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace TomLabs.MapLens.ViewModels
{
	public class SharedViewModel : INotifyPropertyChanged
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
			}
		}

		public int SelectorTop { get; set; }
		public int SelectorLeft { get; set; }

		private int selectorWidth;
		public int SelectorWidth
		{
			get => selectorWidth;
			set
			{
				selectorWidth = value;
				MainWidth = selectorWidth * 2;
			}
		}
		public int MainWidth { get; set; }

		private int selectorHeight;
		public int SelectorHeight
		{
			get => selectorWidth;
			set
			{
				selectorHeight = value;
				MainHeight = selectorHeight * 2;
			}
		}
		public int MainHeight { get; set; }

		public int BorderThickness { get; set; } = 2;

		public bool SelectorCanClose { get; set; }

		private DispatcherTimer _dispatchTimer = new DispatcherTimer()
		{
			Interval = TimeSpan.FromMilliseconds(250)
		};

		public SharedViewModel()
		{
			_dispatchTimer.Tick += _dispatchTimer_Tick;
			_dispatchTimer.Start();
		}

		private void _dispatchTimer_Tick(object sender, EventArgs e)
		{
			CopyScreen();
		}

		private void CopyScreen()
		{
			var size = new System.Drawing.Size(SelectorWidth - BorderThickness * 2, SelectorHeight - BorderThickness * 2);
			using (var screenBmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb))
			{
				using (var bmpGraphics = Graphics.FromImage(screenBmp))
				{
					bmpGraphics.CopyFromScreen(SelectorLeft + BorderThickness, SelectorTop + BorderThickness, 0, 0, size);
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

		public void Stop()
		{
			_dispatchTimer.Stop();
		}
	}
}
