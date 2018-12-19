using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PatchworkLauncher
{
	public static class ButtonExtensions
	{
		public static void ClearClickEvents(this Button button, params EventHandler[] eventHandlers)
		{
			foreach (EventHandler eventHandler in eventHandlers)
			{
				button.Click -= eventHandler;
			}
		}

		public static void ClearClickEvents(this Button button, IEnumerable<EventHandler> eventHandlers)
		{
			foreach (EventHandler eventHandler in eventHandlers)
			{
				button.Click -= eventHandler;
			}
		}
	}
}
