﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PatchworkLauncher.Extensions
{
	public static class User32Extensions
	{
		#region Constants

		private const int HT_CAPTION = 0x2;
		private const int WM_NCLBUTTONDOWN = 0xA1;

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Releases the mouse capture from a window in the current thread and restores normal mouse input processing
		/// </summary>
		public static void ReleaseCapture(this Form form)
		{
			// TODO: see if there's something else we can do if this is a problem
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
					return;
			}

			ReleaseCapture();
			SendMessage(form.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
		}

		#endregion

		#region Methods

		[DllImport("user32.dll", EntryPoint = "ReleaseCapture", CallingConvention = CallingConvention.StdCall)]
		private static extern bool ReleaseCapture();

		[DllImport("user32.dll", EntryPoint = "SendMessage", CallingConvention = CallingConvention.StdCall)]
		private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

		#endregion
	}
}
