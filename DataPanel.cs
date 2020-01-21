using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpuMonitor
{
	public class DataPanel : Panel
	{
		public event EventHandler OnResizeStart;
		public event EventHandler OnResizeMove;
		public event EventHandler OnResizeEnd;

		public Brush BackgroundColor { get; set; } = new SolidBrush(Color.Black);

		private Rectangle _resizeGripRect;
		private bool _resizeActive = false;


		protected override void OnCreateControl()
		{
			base.OnCreateControl();

			_resizeGripRect = new Rectangle(Width - 12, Height - 12, 11, 11);
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			e.Graphics.FillRectangle(BackgroundColor, 0, 0, Width, Height);
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, Width, Height, Border3DStyle.SunkenInner);
			ControlPaint.DrawSizeGrip(e.Graphics, Color.Black, _resizeGripRect);

		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);

			_resizeGripRect = new Rectangle(Width - 12, Height - 12, 11, 11);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (e.Button != MouseButtons.Left)
				return;

			if (_resizeGripRect.Contains(e.Location))
			{
				OnResizeStart?.Invoke(this, EventArgs.Empty);
				_resizeActive = true;
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (_resizeActive)
				OnResizeMove?.Invoke(this, EventArgs.Empty);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if (_resizeActive)
			{
				OnResizeEnd?.Invoke(this, EventArgs.Empty);
				_resizeActive = false;
			}
		}
	}
}
