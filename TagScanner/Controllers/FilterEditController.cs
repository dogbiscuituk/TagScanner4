using System;
using TagScanner.Views;

namespace TagScanner.Controllers
{
	public abstract class FilterEditController
	{
		#region Lifetime Management

		protected FilterEditController(FilterEditor view)
		{
			View = view;
			Visible = false;
		}

		#endregion

		#region View

		protected FilterEditor _view;
		protected virtual FilterEditor View
		{
			get { return _view; }
			set { _view = value; }
		}

		public abstract bool Visible { get; set; }

		#endregion

		#region Text

		public abstract string Text { get; set; }

		#endregion

		#region Control Events

		protected bool Updating;

		public event EventHandler ValueChanged;

		protected virtual void OnValueChanged()
		{
			if (Updating)
				return;
			var valueChanged = ValueChanged;
			if (valueChanged != null)
				valueChanged(this, EventArgs.Empty);
		}

		#endregion
	}
}
