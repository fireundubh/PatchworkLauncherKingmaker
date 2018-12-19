using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows.Forms;
using Patchwork.Utility.Binding;

namespace PatchworkLauncher.Extensions
{
	public static class GuiExtensions
	{
		public static void ShowOrFocus(this Form form)
		{
			if (form.Visible)
			{
				form.Focus();
			}
			else
			{
				form.Show();
			}
		}

		public static IList<Control> CastList(this Control.ControlCollection collection)
		{
			return collection.CastList<Control>();
		}

		public static IList<Control> CastList(this Form.ControlCollection collection)
		{
			return collection.CastList<Control>();
		}
	}

	public static class GuiBindings
	{
		/// <exception cref="T:System.Exception">The <paramref name="act"/> delegate callback throws an exception.</exception>
		public static IBindable<TValue> Bind<TControl, TValue>(this TControl control, Expression<Func<TControl, TValue>> memberAccess, string refreshEvent = null) where TControl : Control
		{
			Action<Action> dispatcher = act =>
			                            {
				                            if (control?.InvokeRequired == true)
				                            {
					                            control.Invoke(act);
				                            }
				                            else
				                            {
					                            act();
				                            }
			                            };
			EventRaised notification = refreshEvent == null ? null : new EventRaised(refreshEvent);
			return control.Bind(memberAccess, notification).WithDispatcher(dispatcher);
		}
	}
}
