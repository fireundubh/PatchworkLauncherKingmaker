using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows.Forms;
using Patchwork.Utility.Binding;

namespace PatchworkLauncher.Extensions
{
	public static class GuiExtensions
	{
		#region Public Methods and Operators

		public static IList<Control> CastList(this Control.ControlCollection collection)
		{
			return collection.CastList<Control>();
		}

		public static IList<Control> CastList(this Form.ControlCollection collection)
		{
			return collection.CastList<Control>();
		}

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

		#endregion
	}

	public static class GuiBindings
	{
		#region Public Methods and Operators

		public static IBindable<TValue> Bind<TControl, TValue>(this TControl control, Expression<Func<TControl, TValue>> memberAccess, string refreshEvent = null) where TControl : Control
		{
			EventRaised notification = refreshEvent == null ? null : new EventRaised(refreshEvent);

			return control.Bind(memberAccess, notification).WithDispatcher(control.Dispatch);
		}

		#endregion

		#region Methods

		private static void Dispatch<TControl>(this TControl control, Action action) where TControl : Control
		{
			control?.InvokeIfRequired(() => action());
		}

		#endregion
	}
}
