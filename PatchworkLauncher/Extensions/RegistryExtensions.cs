using Microsoft.Win32;

namespace PatchworkLauncher.Extensions
{
	public static class RegistryExtensions
	{
		#region Public Methods and Operators

		/// <exception cref="T:System.IO.IOException">The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value has been marked for deletion.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read from the registry key.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		public static bool RegQueryStringValue(this RegistryView registryView, string path, string name, out string value)
		{
			string result;

			using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
			{
				using (RegistryKey key = baseKey.OpenSubKey(path))
				{
					result = key?.GetValue(name).ToString();
				}
			}

			if (result != null)
			{
				value = result;
				return true;
			}

			value = string.Empty;
			return false;
		}

		#endregion
	}
}
