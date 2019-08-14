using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace TomLabs.MapLens.Helpers
{
	internal class WindowAspectRatio
	{
		private double _ratio;

		private WindowAspectRatio(Window window)
		{
			_ratio = window.Width / window.Height;
			((HwndSource)HwndSource.FromVisual(window)).AddHook(DragHook);
		}

		public static WindowAspectRatio Register(Window window)
		{
			return new WindowAspectRatio(window);
		}

		internal enum WM
		{
			WINDOWPOSCHANGING = 0x0046,
		}

		[Flags()]
		public enum SWP
		{
			NoMove = 0x2,
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct WINDOWPOS
		{
			public IntPtr hwnd;
			public IntPtr hwndInsertAfter;
			public int x;
			public int y;
			public int cx;
			public int cy;
			public int flags;
		}

		private IntPtr DragHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handeled)
		{
			if ((WM)msg == WM.WINDOWPOSCHANGING)
			{
				WINDOWPOS position = (WINDOWPOS)Marshal.PtrToStructure(lParam, typeof(WINDOWPOS));

				if ((position.flags & (int)SWP.NoMove) != 0 ||
					HwndSource.FromHwnd(hwnd).RootVisual == null)
				{
					return IntPtr.Zero;
				}

				position.cx = (int)(position.cy * _ratio);

				Marshal.StructureToPtr(position, lParam, true);
				handeled = true;
			}

			return IntPtr.Zero;
		}
	}
}
