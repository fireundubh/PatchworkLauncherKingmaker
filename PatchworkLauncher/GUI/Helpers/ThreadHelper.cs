using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PatchworkLauncher
{
	public static class ThreadHelper
	{
		#region Public Methods and Operators

		public static DialogResult InvokeDialogIfRequired(this Form owner, Form messageBox)
		{
			if (owner?.InvokeRequired == true)
			{
				return (DialogResult) owner.Invoke(new Func<DialogResult>(() => messageBox.ShowDialog(owner)));
			}

			return messageBox.ShowDialog(owner);
		}

		/// <exception cref="T:System.Exception">A delegate callback throws an exception.</exception>
		public static void InvokeIfRequired(this ISynchronizeInvoke obj, MethodInvoker method)
		{
			if (obj?.InvokeRequired == true)
			{
				object[] args = Array.Empty<object>();
				obj.Invoke(method, args);
			}
			else
			{
				method();
			}
		}

		#endregion
	}
}
