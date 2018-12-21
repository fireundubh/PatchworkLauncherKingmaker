using System;
using System.Text;
using Patchwork.Engine.Utility;
using PatchworkLauncher.Extensions;

namespace PatchworkLauncher
{
	public class PatchingExceptionMessage
	{
		#region Public Properties

		public Exception Exception { get; set; }

		public string FailedObjects { get; set; }

		public string Hint { get; set; }

		public string Message { get; set; }

		public string Operation { get; set; }

		#endregion

		#region Public Methods and Operators

		public override string ToString()
		{
			var message = new StringBuilder();

			message.AppendLine("An error has occurred.");
			message.AppendLineFormat(!this.Operation.IsNullOrWhitespace(), "While trying to: {0}", this.Operation);
			message.AppendLineFormat(!this.Hint.IsNullOrWhitespace(), "Error type: {0} ({1})", this.Hint, this.Exception?.GetType().Name);
			message.AppendLineFormat(this.Exception != null, "Internal message: {0}", this.Exception?.Message ?? "Unknown");
			message.AppendLineFormat(!this.FailedObjects.IsNullOrWhitespace(), "Object(s) that failed: {0}", this.FailedObjects);
			message.AppendLineFormat(!this.Message.IsNullOrWhitespace(), "{0}", this.Message);

			return message.ToString();
		}

		#endregion
	}
}
