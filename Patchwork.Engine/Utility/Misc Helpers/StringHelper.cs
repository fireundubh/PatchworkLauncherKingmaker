using System;
using System.Collections.Generic;
using System.Linq;

namespace Patchwork.Engine.Utility
{
	internal static class StringHelper
	{
		#region Static Fields

		private static readonly Random Rnd = new Random();

		private static readonly char[] WordChars = CharsBetween('a', 'z').Concat(CharsBetween('A', 'Z')).Concat(CharsBetween('0', '9')).ToArray();

		#endregion

		#region Public Methods and Operators

		public static bool EqualsIgnoreCase(this string a, string b)
		{
			return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
		}

		public static bool EqualsIgnoreCaseInvariant(this string a, string b)
		{
			return string.Equals(a, b, StringComparison.InvariantCultureIgnoreCase);
		}

		public static bool IsNullOrWhitespace(this string str)
		{
			return string.IsNullOrWhiteSpace(str);
		}

		public static char[] CharsBetween(char start, char end)
		{
			var ret = new List<char>();

			for (; start <= end; start++)
			{
				ret.Add(start);
			}

			return ret.ToArray();
		}

		public static string FindLongestCommonSubstring(string a, string b, bool caseInsensitive = false)
		{
			string commonString = string.Empty;

			for (var i = 0; i < Math.Min(a.Length, b.Length); i++)
			{
				StringComparison comparisonMode = caseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

				if (!string.Equals(a[i].ToString(), b[i].ToString(), comparisonMode))
				{
					return commonString;
				}

				commonString += a[i];
			}

			return commonString;
		}

		public static string Join(this IEnumerable<string> strs, string sep = "")
		{
			return string.Join(sep, strs);
		}

		public static string RandomWordString(int len)
		{
			string str = string.Empty;

			for (var i = 0; i < len; i++)
			{
				str += WordChars[Rnd.Next(0, WordChars.Length)];
			}

			return str;
		}

		public static string Replicate(this string str, int count)
		{
			return count == 0 ? string.Empty : Enumerable.Repeat(str, count).Aggregate(string.Concat);
		}

		#endregion
	}
}
