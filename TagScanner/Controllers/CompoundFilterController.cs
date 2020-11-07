namespace TagScanner.Controllers
{
	using System;
	using System.Windows.Forms;
	using TagScanner.Models;
	using TagScanner.Views;

	public class CompoundFilterController : FilterController
	{
		#region Lifetime Management

		public CompoundFilterController(FilterDialog view) : base(view) { }

		#endregion

		#region View

		protected override FilterDialog View
		{
			get => base.View;
			set
			{
				base.View = value;
				var items = QuantifierBox.Items;
				items.Clear();
				items.AddRange(Metadata.QuantifierStrings);
				QuantifierBox.SelectedValueChanged += QuantifierBox_ValueChanged;
			}
		}

		private ComboBox QuantifierBox => View.QuantifierBox;

		public override bool Visible
		{
			get => QuantifierBox.Visible;
			set => QuantifierBox.Visible = value;
		}

		#endregion

		#region Text

		public override string Text
		{
			get => QuantifierBox.Text;
			set => QuantifierBox.Text = value;
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
