using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PatchworkLauncher
{
	public static class ThreadHelper
	{
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
	}
}
