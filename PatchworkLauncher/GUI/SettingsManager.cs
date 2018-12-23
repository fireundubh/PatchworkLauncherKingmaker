using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Win32;
using NCode.ReparsePoints;
using Patchwork.Engine.Utility;
using PatchworkLauncher.Extensions;
using Serilog;

namespace PatchworkLauncher
{
	public static class SettingsManager
	{
		#region Constructors and Destructors

		static SettingsManager()
		{
			SerializerInstance = new XmlSerializer(typeof(XmlSettings));
			FullPath = Path.GetFullPath(XmlSettings.Launcher.Files.Settings);

			Logger = LogManager.CreateLogger("SettingsManager");
		}

		#endregion

		#region Public Properties

		public static string FullPath { get; }

		public static XmlSettings XmlSettings
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

		public static string GetGamePathFromRegistry()
		{
			RegistryView[] registryViews =
			{
				RegistryView.Registry32,
				RegistryView.Registry64
			};

			string value = string.Empty;

			if (registryViews.Any(registryView => registryView.RegQueryStringValue(XmlSettings.Launcher.Registry.Steam, "InstallLocation", out value)) && !string.IsNullOrEmpty(value))
			{
				return value;
			}

			if (registryViews.Any(registryView => registryView.RegQueryStringValue(XmlSettings.Launcher.Registry.Galaxy, "PATH", out value)) && !string.IsNullOrEmpty(value))
			{
				return value;
			}

			return value;
		}

		public static void Dispose()
		{
			((IDisposable) Logger)?.Dispose();
		}

		public static void Initialize()
		{
			InvalidateXmlData();
		}

		public static void InvalidateXmlData()
		{
			InvalidateClientPath();
			InvalidateGamePath();
			InvalidateInstructions();
		}

		#endregion

		#region Methods

		private static bool IsLocalMod(string patchLocation)
		{
			return patchLocation.StartsWithIgnoreCase(XmlData.ModsPath);
		}

		private static bool ValidateXmlPath(string xmlPath, bool isFile = false)
		{
			if (isFile)
			{
				if (File.Exists(xmlPath))
				{
					Logger.Debug("File path validated: {0}", xmlPath);
					return true;
				}
			}
			else
			{
				if (Directory.Exists(xmlPath))
				{
					Logger.Debug("Directory path validated: {0}", xmlPath);
					return true;
				}
			}

			// TODO: might fail on linux
			IReparsePointProvider provider = ReparsePointFactory.Provider;
			LinkType linkType = provider.GetLinkType(xmlPath);

			switch (linkType)
			{
				case LinkType.Junction:
				case LinkType.Symbolic:
					Logger.Debug("Directory path validated as junction point or symbolic link: {0}", xmlPath);
					return true;
				default:
					return false;
			}
		}

		private static string GetLocalPath(string patchLocation)
		{
			return patchLocation == null ? string.Empty : Path.Combine(XmlData.ModsPath, Path.GetFileName(patchLocation));
		}

		private static void InvalidateClientPath()
		{
			if (ValidateXmlPath(XmlData.ClientPath, true))
			{
				return;
			}

			Logger.Debug("Cannot validate client path. Setting client path to empty.");
			XmlData.ClientPath = string.Empty;
		}

		private static void InvalidateGamePath()
		{
			if (ValidateXmlPath(XmlData.GamePath, false))
			{
				return;
			}

			Logger.Debug("Cannot validate game path. Setting game path to empty.");
			XmlData.GamePath = string.Empty;
		}

		private static void InvalidateInstructions()
		{
			XmlSettings settings = XmlSettings;

			List<XmlInstruction> instructions = settings?.Instructions;

			if (instructions == null || instructions.Count == 0)
			{
				return;
			}

			Logger.Debug("Reserializing instructions, filtering duplicates and nonexistent files.");
			instructions.RemoveWhere(x => x != null && (!File.Exists(GetLocalPath(x.Location)) || !IsLocalMod(x.Location)));
			SerializerInstance.Serialize(settings, FullPath);
		}

		#endregion

		public static class XmlData
		{
			#region Public Properties

			public static List<XmlInstruction> Instructions
			{
				get
				{
					return XmlSettings.Instructions;
				}
				set
				{
					XmlSettings settings = XmlSettings;
					settings.Instructions = value;
					SerializerInstance.Serialize(settings, FullPath);
				}
			}

			public static string Arguments
			{
				get
				{
					return XmlSettings.Options.Arguments;
				}
				set
				{
					XmlSettings settings = XmlSettings;
					settings.Options.Arguments = value;
					SerializerInstance.Serialize(settings, FullPath);
				}
			}

			public static string ClientPath
			{
				get
				{
					return XmlSettings.Launcher.Files.Client;
				}
				set
				{
					XmlSettings settings = XmlSettings;
					settings.Launcher.Files.Client = value;
					SerializerInstance.Serialize(settings, FullPath);
				}
			}

			public static string GamePath
			{
				get
				{
					return XmlSettings.Launcher.Folders.Game;
				}
				set
				{
					XmlSettings settings = XmlSettings;
					settings.Launcher.Folders.Game = value;
					SerializerInstance.Serialize(settings, FullPath);
				}
			}

			public static string LogsPath
			{
				get
				{
					return XmlSettings.Launcher.Folders.Logs;
				}
				set
				{
					XmlSettings settings = XmlSettings;
					settings.Launcher.Folders.Logs = value;
					SerializerInstance.Serialize(settings, FullPath);
				}
			}

			public static string ModsPath
			{
				get
				{
					return XmlSettings.Launcher.Folders.Mods;
				}
				set
				{
					XmlSettings settings = XmlSettings;
					settings.Launcher.Folders.Mods = value;
					SerializerInstance.Serialize(settings, FullPath);
				}
			}

			#endregion
		}
	}
}
