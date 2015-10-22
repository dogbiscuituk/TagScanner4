using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TagScanner.Controllers
{
	public class CursorController
	{
		public CursorController(Control control)
		{
			_control = control;
		}

		private Control _control;
		private Stack<Cursor> _cursors = new Stack<Cursor>();

		public void BeginWait()
		{
			_cursors.Push(_control.Cursor);
			_control.Cursor = Cursors.WaitCursor;
		}

		public void EndWait()
		{
			if (_cursors.Any())
				_control.Cursor = _cursors.Pop();
		}
	}
}
