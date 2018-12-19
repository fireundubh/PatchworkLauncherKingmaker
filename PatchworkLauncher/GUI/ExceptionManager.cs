using System;
using System.IO;
using System.Windows.Forms;
using Patchwork.Engine;
using Patchwork.Engine.Utility;
using Serilog;

namespace PatchworkLauncher
{
	public static class ExceptionManager
	{
		#region Public Methods and Operators

		public static string GetHint(this Exception exception)
		{
			if (exception is PatchException)
			{
				return "A patch was invalid, incompatible, or caused an error.";
			}

			if (exception is IOException)
			{
				return "Related to reading/writing files.";
			}

			if (exception is ApplicationException)
			{
				return "An application error.";
			}

			if (exception != null)
			{
				return "A system error or some sort of bug.";
			}

			return string.Empty;
		}

		public static void ShowMessageBox(this Exception exception, CustomExceptionMessage message, ILogger logger)
		{
			string exceptionMessage = message.ToString();
			logger.Error(exceptionMessage);
			MessageBox.Show(exceptionMessage, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public static void ShowMessageBox(this PatchingProcessException outerException, ILogger logger)
		{
			string targetFile = outerException.TargetFile;
			string instructionName = outerException.AssociatedInstruction?.Name;

			string failedObjects = string.Empty;

			if (targetFile != null)
			{
				if (instructionName == null)
				{
					failedObjects = targetFile;
				}
				else
				{
					failedObjects = string.Format("{0} -> {1}", instructionName, targetFile);
				}
			}

			Exception exception = outerException.InnerException ?? outerException;

			var exceptionMessage = new CustomExceptionMessage
			{
				Exception = exception,
				FailedObjects = failedObjects,
				Hint = outerException.GetHint(),
				Message = exception.Message,
				Operation = outerException.Step.GetEnumValueText() ?? "Patch a file",
			};

			exception.ShowMessageBox(exceptionMessage, logger);
		}

		#endregion
	}
}
