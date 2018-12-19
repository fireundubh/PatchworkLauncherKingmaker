using System;
using System.Text;
using Patchwork.Engine.Utility;

namespace PatchworkLauncher
{
	public class CustomExceptionMessage
	{
		public Exception Exception { get; set; }

		public string Hint { get; set; }

		public string Operation { get; set; }

		public string FailedObjects { get; set; }

		public string Message { get; set; }

		public override string ToString()
		{
			var errorMessage = new StringBuilder();

			errorMessage.AppendLine("An error has occurred.");
			errorMessage.AppendLineFormat(!this.Operation.IsNullOrWhitespace(), "While trying to: {0}", this.Operation);
			errorMessage.AppendLineFormat(!this.Hint.IsNullOrWhitespace(), "Error type: {0} ({1})", this.Hint, this.Exception?.GetType().Name);
			errorMessage.AppendLineFormat(this.Exception != null, "Internal message: {0}", this.Exception.Message);
			errorMessage.AppendLineFormat(!this.FailedObjects.IsNullOrWhitespace(), "Object(s) that failed: {0}", this.FailedObjects);
			errorMessage.AppendLineFormat(!this.Message.IsNullOrWhitespace(), "{0}", this.Message);

			return errorMessage.ToString();
		}
	}
}
