using System;
using System.Windows.Forms;
using TagScanner.Models;
using TagScanner.Views;

namespace TagScanner.Controllers
{
	public class CompoundFilterEditController : FilterEditController
	{
		#region Lifetime Management

		public CompoundFilterEditController(FilterEditor view) : base(view) { }

		#endregion

		#region View

		protected override FilterEditor View
		{
			get
			{
				return base.View;
			}
			set
			{
				if (View != null)
				{
					QuantifierBox.SelectedValueChanged -= QuantifierBox_ValueChanged;
				}
				base.View = value;
				if (View != null)
				{
					var items = QuantifierBox.Items;
					items.Clear();
					items.AddRange(CompoundCondition.Quantifiers);
					QuantifierBox.SelectedValueChanged += QuantifierBox_ValueChanged;
				}
			}
		}

		private ComboBox QuantifierBox { get { return View.QuantifierBox; } }

		public override bool Visible
		{
			get
			{
				return QuantifierBox.Visible;
			}
			set
			{
				QuantifierBox.Visible = value;
			}
		}

		#endregion

		#region Text

		public override string Text
		{
			get
			{
				return QuantifierBox.Text;
			}
			set
			{
				QuantifierBox.Text = value;
			}
		}

		#endregion

		#region Control Events

		private void QuantifierBox_ValueChanged(object sender, EventArgs e)
		{
			OnValueChanged();
		}

		#endregion
	}
}
