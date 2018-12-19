using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace PatchworkLauncher
{
	internal static class MiscExtensions
	{
		/// <exception cref="T:System.ArgumentException">Invalid type</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		public static T Deserialize<T>(this XmlSerializer serializer, string path, T defaultValue)
		{
			if (!File.Exists(path))
			{
				return defaultValue;
			}

			using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				var reader = new XmlTextReader(stream);

				if (!serializer.CanDeserialize(reader))
				{
					return defaultValue;
				}

				object result = serializer.Deserialize(reader);
				result = result ?? defaultValue;

				if (result is T)
				{
					return (T) result;
				}

				throw new ArgumentException("Invalid type.");
			}
		}

		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		public static void Serialize(this XmlSerializer serializer, object o, string path)
		{
			using (FileStream stream = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.Read))
			{
				serializer.Serialize(stream, o);
			}
		}
	}
}
