using System;
using System.Linq;
using System.Windows.Forms;
using TagScanner.Models;
using TagScanner.Views;

namespace TagScanner.Controllers
{
	public class SimpleFilterEditController : FilterEditController
	{
		#region Lifetime Management

		public SimpleFilterEditController(FilterEditor view) : base(view) { }

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
					PropertyBox.SelectedValueChanged -= PropertyBox_ValueChanged;
					OperatorBox.SelectedValueChanged -= OperatorBox_ValueChanged;
				}
				base.View = value;
				if (View != null)
				{
					var items = PropertyBox.Items;
					items.Clear();
					items.AddRange(SimpleCondition.SortableColumnNames);
					PropertyBox.SelectedValueChanged += PropertyBox_ValueChanged;
					OperatorBox.SelectedValueChanged += OperatorBox_ValueChanged;
				}
			}
		}

		private ComboBox OperatorBox { get { return View.OperatorBox; } }

		private ComboBox PropertyBox { get { return View.PropertyBox; } }

		private Control ValueBox { get { return View.ValueBox; } }

		private Control.ControlCollection ValueEdit { get { return ValueBox.Controls; } }

		public override bool Visible
		{
			get
			{
				return PropertyBox.Visible;
			}
			set
			{
				PropertyBox.Visible = OperatorBox.Visible = ValueBox.Visible = value;
            }
		}

		#region Value Edits

		private Control GetValueEdit(string propertyTypeName)
		{
			switch (propertyTypeName)
			{
				case "String":
					return ValueEditText;
				case "Int32":
					return ValueEditInt;
				case "Int64":
					return ValueEditLong;
				case "TimeSpan":
					return ValueEditTime;
				case "Logical":
					return ValueEditBool;
			}
			return null;
		}

		private ComboBox _valueEditBool;
		private ComboBox ValueEditBool
		{
			get
			{
				if (_valueEditBool == null)
				{
					_valueEditBool = new ComboBox
					{
						Dock = DockStyle.Bottom,
						DropDownStyle = ComboBoxStyle.DropDownList
					};
					_valueEditBool.Items.AddRange(new[] { "true", "false" });
					_valueEditBool.SelectedIndex = 0;
					_valueEditBool.SelectedValueChanged += ValueBox_ValueChanged;
				}
				return _valueEditBool;
			}
		}

		private NumericUpDown _valueEditInt;
		private NumericUpDown ValueEditInt
		{
			get
			{
				if (_valueEditInt == null)
				{
					_valueEditInt = new NumericUpDown
					{
						Dock = DockStyle.Bottom,
						Maximum = int.MaxValue,
						Minimum = int.MinValue
					};
					_valueEditInt.ValueChanged += ValueBox_ValueChanged;
				}
				return _valueEditInt;
			}
		}

		private NumericUpDown _valueEditLong;
		private NumericUpDown ValueEditLong
		{
			get
			{
				if (_valueEditLong == null)
				{
					_valueEditLong = new NumericUpDown
					{
						Dock = DockStyle.Bottom,
						Maximum = int.MaxValue,
						Minimum = int.MinValue
					};
					_valueEditLong.ValueChanged += ValueBox_ValueChanged;
				}
				return _valueEditLong;
			}
		}

		private ComboBox _valueEditText;
		private ComboBox ValueEditText
		{
			get
			{
				if (_valueEditText == null)
				{
					_valueEditText = new ComboBox
					{
						Dock = DockStyle.Bottom
					};
					_valueEditText.TextChanged += ValueBox_ValueChanged;
				}
				return _valueEditText;
			}
		}

		private DateTimePicker _valueEditTime;
		private DateTimePicker ValueEditTime
		{
			get
			{
				if (_valueEditTime == null)
				{
					_valueEditTime = new DateTimePicker
					{
						Dock = DockStyle.Bottom,
						Format = DateTimePickerFormat.Time,
						ShowUpDown = true,
						Value = DateTime.Today + TimeSpan.Zero
					};
					_valueEditTime.ValueChanged += ValueBox_ValueChanged;
				}
				return _valueEditTime;
			}
		}

		#endregion

		#endregion

		#region Text

		public override string Text
		{
			get
			{
				var valueBox = ValueEdit[0];
				return string.Format(
					"{0} {1} {2}",
					PropertyBox.Text,
					OperatorBox.Text,
					valueBox is NumericUpDown
						? ((NumericUpDown)valueBox).Value.ToString()
						: valueBox.Text);
			}
			set
			{
				Updating = true;
				var simpleCondition = new SimpleCondition(value);
				InitPropertyBox(simpleCondition.PropertyName);
				PropertyBox.SelectedItem = simpleCondition.PropertyName;
				OperatorBox.SelectedItem = simpleCondition.Operation;
				ValueEdit[0].Text = simpleCondition.ValueString;
				Updating = false;
			}
		}

		private void InitPropertyBox(string propertyName)
		{
			PropertyTypeName = SimpleCondition.GetPropertyTypeName(propertyName);
        }

		private string _propertyTypeName;
		private string PropertyTypeName
		{
			get
			{
				return _propertyTypeName;
			}
			set
			{
				if (PropertyTypeName == value)
					return;
				var ops = SimpleCondition.GetOperatorsForType(value);
				var items = OperatorBox.Items;
                if (!items.Cast<string>().SequenceEqual(ops))
				{
					var op = OperatorBox.SelectedItem;
					items.Clear();
					items.AddRange(ops.ToArray());
					if (ops.Contains(op))
						OperatorBox.SelectedItem = op;
					else
						OperatorBox.SelectedIndex = 0;
                }
				ValueEdit.Clear();
				ValueEdit.Add(GetValueEdit(value));
				_propertyTypeName = value;
			}
		}

		#endregion

		#region Control Events

		private void OperatorBox_ValueChanged(object sender, EventArgs e)
		{
			OnValueChanged();
		}

		private void PropertyBox_ValueChanged(object sender, EventArgs e)
		{
			var propertyName = PropertyBox.Text;
			InitPropertyBox(propertyName);
			OnValueChanged();
		}

		private void ValueBox_ValueChanged(object sender, EventArgs e)
		{
			OnValueChanged();
		}

		#endregion
	}
}
