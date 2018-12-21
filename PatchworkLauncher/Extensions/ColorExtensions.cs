using System;
using System.Drawing;
using PatchworkLauncher.Enums;

namespace PatchworkLauncher.Extensions
{
	public static class ColorExtensions
	{
		#region Public Methods and Operators

		/// <exception cref="T:System.Exception">The Transform delegate callback throws an exception.</exception>
		public static Color Lighten(this Color color, int increase)
		{
			return color.Transform((chan, val) => chan == ColorChannel.A ? val : (byte) Math.Min(255, val + increase));
		}

		/// <exception cref="T:System.Exception">The <paramref name="selector"/> delegate callback throws an exception.</exception>
		public static Color Transform(this Color color, Func<ColorChannel, byte, byte> selector)
		{
			return Color.FromArgb(selector(ColorChannel.A, color.A), selector(ColorChannel.R, color.R), selector(ColorChannel.G, color.G), selector(ColorChannel.B, color.B));
		}

		#endregion
	}
}
