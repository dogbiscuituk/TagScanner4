using System;
using System.Windows.Forms;
using TagScanner.Models;
using TagScanner.Views;

namespace TagScanner.Controllers
{
	public class CompoundFilterEditController : FilterEditController
	{
		#region Lifetime Management

		public CompoundFilterEditController(FilterDialog view) : base(view) { }

		#endregion

		#region View

		protected override FilterDialog View
		{
			get
			{
				return base.View;
			}
			set
			{
				base.View = value;
				var items = QuantifierBox.Items;
				items.Clear();
				items.AddRange(Metadata.QuantifierStrings);
				QuantifierBox.SelectedValueChanged += QuantifierBox_ValueChanged;
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
