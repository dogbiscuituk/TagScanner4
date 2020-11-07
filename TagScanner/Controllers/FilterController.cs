namespace TagScanner.Controllers
{
    using System;
    using TagScanner.Views;

    public abstract class FilterController
    {
        #region Lifetime Management

        protected FilterController(FilterDialog view)
        {
            View = view;
            Visible = false;
        }

        #endregion

        #region View

        protected FilterDialog _view;
        protected virtual FilterDialog View
        {
            get => _view;
            set => _view = value;
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
            if (!Updating)
                ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
