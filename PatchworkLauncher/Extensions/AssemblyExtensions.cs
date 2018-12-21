using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Mono.Cecil;
using Patchwork.AutoPatching;
using Patchwork.Engine;
using Patchwork.Engine.Utility;
using Patchwork.Utility;

namespace PatchworkLauncher.Extensions
{
	[Obsolete]
	public static class AssemblyPatcherExtensions
	{
		#region Public Methods and Operators

		/// <exception cref="T:PatchworkLauncher.PatchingProcessException">Cannot write to backup to <paramref name="targetFilePath"/> during the patching process</exception>
		public static void TryBackup(this AssemblyPatcher patcher, string targetFilePath, PatchGroup associatedPatchGroup)
		{
			try
			{
				patcher.WriteTo(targetFilePath);
			}
			catch (Exception exception)
			{
				throw new PatchingProcessException(exception)
				{
					AssociatedInstruction = null,
					AssociatedPatchGroup = associatedPatchGroup,
					Step = PatchProcessingStep.WritingToFile
				};
			}
		}

		public static void TryCopyAttributesAssembly(this Assembly sourceAssembly, string targetAssemblyFileName)
		{
			if (!File.Exists(targetAssemblyFileName))
			{
				return;
			}

			AssemblyDefinition targetAssembly = AssemblyCache.Default.ReadAssembly(targetAssemblyFileName);

			string sourceMetadata = sourceAssembly.GetAssemblyMetadataString() ?? string.Empty;
			string targetMetadata = targetAssembly.GetAssemblyMetadataString() ?? string.Empty;

			if (!sourceMetadata.EqualsIgnoreCaseInvariant(targetMetadata))
			{
				File.Copy(sourceAssembly.Location, targetAssemblyFileName, true);
			}
		}

		/// <exception cref="T:PatchworkLauncher.PatchingProcessException">Cannot patch manifest during the patching process</exception>
		public static void TryPatchManifest(this AssemblyPatcher patcher, PatchInstruction patch, PatchGroup patchGroup, ProgressObject patchProgress)
		{
			try
			{
				patcher.PatchManifest(patch.Patch, patchProgress.ToMonitor());
			}
			catch (PatchException patchException)
			{
				throw new PatchingProcessException(patchException)
				{
					AssociatedInstruction = patch,
					AssociatedPatchGroup = patchGroup,
					Step = PatchProcessingStep.ApplyingSpecificPatch
				};
			}
		}

		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter contains invalid characters, is empty, or contains only white spaces.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.IO.IOException" />, instead.The <paramref name="path" /> parameter is longer than the system-defined maximum length.</exception>
		public static string TryRunPeVerify(this AssemblyPatcher patcher, AppInfo appInfo, string targetFile)
		{
			var peSettings = new PEVerifyInput
			{
				AssemblyResolutionFolder = Path.GetDirectoryName(targetFile),
				IgnoreErrors = appInfo.IgnorePEVerifyErrors.ToList()
			};

			PEVerifyOutput peOutput = patcher.RunPeVerify(peSettings);

			return peOutput.Output;
		}

		#endregion
	}
}
