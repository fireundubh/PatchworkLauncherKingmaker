using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Win32;
using NCode.ReparsePoints;
using Patchwork.Engine.Utility;
using PatchworkLauncher.Extensions;
using PatchworkLauncher.Properties;
using Serilog;

namespace PatchworkLauncher
{
	public static class SettingsManager
	{
		#region Constructors and Destructors

		static SettingsManager()
		{
			SerializerInstance = new XmlSerializer(typeof(XmlSettings));
			FullPath = Path.GetFullPath(PathSettings.Default.Settings);

			Logger = LogManager.CreateLogger("SettingsManager");
		}

		#endregion

		#region Public Properties

		public static string BaseFolder
		{
			get
			{
				XmlSettings settings = Settings;
				return settings.BaseFolder;
			}
			set
			{
				XmlSettings settings = Settings;
				settings.BaseFolder = value;
				SerializerInstance.Serialize(settings, FullPath);
			}
		}

		public static string FullPath { get; }

		public static List<XmlInstruction> Instructions
		{
			get
			{
				XmlSettings settings = Settings;
				return settings.Instructions;
			}
			set
			{
				XmlSettings settings = Settings;
				settings.Instructions = value;
				SerializerInstance.Serialize(settings, FullPath);
			}
		}

		public static XmlSettings Settings
		{
			get
			{
				return SerializerInstance.Deserialize(FullPath, new XmlSettings());
			}
			set
			{
				SerializerInstance.Serialize(value, FullPath);
			}
		}

		#endregion

		#region Properties

		private static ILogger Logger { get; }

		private static XmlSerializer SerializerInstance { get; }

		#endregion

		#region Public Methods and Operators

		public static void Dispose()
		{
			((IDisposable)Logger).Dispose();
		}

		public static void Initialize()
		{
			CreateSettings();
		}

		public static void InvalidateBaseFolder()
		{
			if (Directory.Exists(BaseFolder))
			{
				return;
			}

			// TODO: might fail on linux
			IReparsePointProvider provider = ReparsePointFactory.Provider;
			LinkType linkType = provider.GetLinkType(BaseFolder);

			if (linkType == LinkType.Junction || linkType == LinkType.Symbolic)
			{
				return;
			}

			BaseFolder = string.Empty;
		}

		public static void InvalidateInstructions()
		{
			XmlSettings settings = Settings;

			List<XmlInstruction> instructions = settings?.Instructions;

			if (instructions == null || instructions.Count == 0)
			{
				return;
			}

			instructions.RemoveWhere(instruction => instruction != null && !File.Exists(instruction.PatchLocation));
			SerializerInstance.Serialize(settings, FullPath);
		}

		public static void SetGamePathFallback()
		{
			string message = LaunchManager.GetGameFolderWarning();

			if (message == null)
			{
				return;
			}

			if (!LaunchManager.ShowGameDialog(message))
			{
				LaunchManager.ExitApplication();
			}
		}

		public static string SetGamePathFromRegistry()
		{
			string value = string.Empty;

			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				return value;
			}

			if (RegistryView.Registry32.RegQueryStringValue(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 640820", "InstallLocation", out value))
			{
				BaseFolder = value;
			}

			if (RegistryView.Registry32.RegQueryStringValue(@"SOFTWARE\Wow6432Node\GOG.com\Games\1982293831", "PATH", out value))
			{
				BaseFolder = value;
			}

			if (RegistryView.Registry64.RegQueryStringValue(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 640820", "InstallLocation", out value))
			{
				BaseFolder = value;
			}

			if (RegistryView.Registry64.RegQueryStringValue(@"SOFTWARE\Wow6432Node\GOG.com\Games\1982293831", "PATH", out value))
			{
				BaseFolder = value;
			}

			return value;
		}

		#endregion

		#region Methods

		private static void CreateSettings()
		{
			if (!File.Exists(FullPath))
			{
				Instructions = new List<XmlInstruction>();
				BaseFolder = string.Empty;
			}
		}

		#endregion
	}
}
