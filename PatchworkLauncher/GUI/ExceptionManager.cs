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

		public static DialogResult Show(this ILogger logger, PatchingExceptionMessage message)
		{
			string exceptionMessage = message.ToString();
			logger.Error(exceptionMessage);

			return MessageBox.Show(exceptionMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public static DialogResult Show(this ILogger logger, PatchingProcessException outerException)
		{
			Exception exception = outerException.InnerException ?? outerException;

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

			var exceptionMessage = new PatchingExceptionMessage
			{
				Exception = exception,
				FailedObjects = failedObjects,
				Hint = outerException.GetHint(),
				Message = exception.Message,
				Operation = outerException.Step.GetEnumValueText() ?? "Patch a file",
			};

			logger.Error(exceptionMessage.ToString());

			return logger.Show(exceptionMessage);
		}

		#endregion
	}
}
