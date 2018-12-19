using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchworkLauncher
{
	public enum PatchProcessingStep
	{
		[Description("Unknown")]
		Unknown,

		[Description("Group patches by target file")]
		Grouping,

		[Description("Apply a specific patch")]
		ApplyingSpecificPatch,

		[Description("Write resulting assembly")]
		WritingToFile,

		[Description("Switch file to modded version")]
		PerformingSwitch,
	}

	/// <inheritdoc />
	public class PatchingProcessException : Exception
	{
		private string _targetFile;

		public PatchInstruction AssociatedInstruction { get; set; }

		public PatchGroup AssociatedPatchGroup { get; set; }

		public string TargetFile
		{
			get { return _targetFile ?? AssociatedPatchGroup?.TargetPath; }
			set { _targetFile = value; }
		}

		public PatchProcessingStep Step { get; set; }

		/// <inheritdoc />
		public PatchingProcessException(Exception innerException) : base(null, innerException)
		{
		}

		/// <inheritdoc />
		public PatchingProcessException(string message) : base(message)
		{
		}

		/// <inheritdoc />
		public PatchingProcessException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
