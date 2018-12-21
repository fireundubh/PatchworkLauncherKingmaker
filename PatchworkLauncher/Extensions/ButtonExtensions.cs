using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PatchworkLauncher.Extensions
{
	public static class ButtonExtensions
	{
		#region Public Methods and Operators

		[Obsolete]
		public static void ClearClickEvents(this Button button, params EventHandler[] eventHandlers)
		{
			foreach (EventHandler eventHandler in eventHandlers)
			{
				button.Click -= eventHandler;
			}
		}

		[Obsolete]
		public static void ClearClickEvents(this Button button, IEnumerable<EventHandler> eventHandlers)
		{
			foreach (EventHandler eventHandler in eventHandlers)
			{
				button.Click -= eventHandler;
			}
		}

		#endregion
	}
}
