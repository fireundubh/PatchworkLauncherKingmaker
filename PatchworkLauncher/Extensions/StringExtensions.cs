using System;
using System.Text;

namespace PatchworkLauncher.Extensions
{
	public static class StringExtensions
	{
		#region Public Methods and Operators

		public static bool EndsWithIgnoreCase(this string instance, string value)
		{
			return instance.EndsWith(value, StringComparison.InvariantCultureIgnoreCase);
		}

		public static bool StartsWithIgnoreCase(this string instance, string value)
		{
			return instance.StartsWith(value, StringComparison.InvariantCultureIgnoreCase);
		}

		public static string Quote(this string value, char enclosure = '"')
		{
			return string.Format("{0}{1}{0}", enclosure, value);
		}

		public static StringBuilder AppendLineFormat(this StringBuilder sb, bool conditional, string format, params object[] args)
		{
			return !conditional ? sb : sb.AppendFormat(format, args).AppendLine();
		}

		#endregion
	}
}
