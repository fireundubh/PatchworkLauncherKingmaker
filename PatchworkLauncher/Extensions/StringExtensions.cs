using System;
using System.Configuration;
using System.IO;
using System.Text;
using PatchworkLauncher.Properties;

namespace PatchworkLauncher
{
	public static class StringExtensions
	{
		public static bool EndsWithIgnoreCase(this string instance, string value)
		{
			return instance.EndsWith(value, StringComparison.InvariantCultureIgnoreCase);
		}

		public static string Quote(this string value, char enclosure = '"')
		{
			return string.Format("{0}{1}{0}", enclosure, value);
		}

		public static StringBuilder AppendLineFormat(this StringBuilder sb, bool conditional, string format, params object[] args)
		{
			return !conditional ? sb : sb.AppendFormat(format, args).AppendLine();
		}
	}
}
