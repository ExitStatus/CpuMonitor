using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpuMonitor
{
	public class TitleBar : Panel
	{
		public event EventHandler OnWindowDrag;
		public event EventHandler OnCloseRequested;

		public string Title { get; set; }
		public Font TitleFont { get; set; } = new Font(FontFamily.GenericSansSerif, 8.0f, FontStyle.Regular);
		public Brush TitleColor { get; set; } = new SolidBrush(Color.White);

		private Rectangle _closeRect;
		private readonly Size _closeSize = new Size(13, 12);

		protected override void OnCreateControl()
		{
			base.OnCreateControl();

			_closeRect = new Rectangle(Width - (_closeSize.Width + 2), (Height/2)-(_closeSize.Height/2), _closeSize.Width, _closeSize.Height);
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			using (LinearGradientBrush linGrBrush = new LinearGradientBrush(
				new Point(0, 0), new Point(Width, 0),
				Color.FromArgb(255, 128, 0, 0),   // Opaque red
				Color.FromArgb(255, 0, 0, 0)))
			{
				e.Graphics.FillRectangle(linGrBrush, 0, 0, Width, Height);
			};

			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, Width, Height, Border3DStyle.SunkenInner);
			ControlPaint.DrawCaptionButton(e.Graphics, _closeRect, CaptionButton.Close, ButtonState.Flat );
			e.Graphics.DrawString(Title, TitleFont, TitleColor, 4, (Height / 2) - (TitleFont.Height / 2));
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (e.Button == MouseButtons.Left && !_closeRect.Contains(e.Location))
				OnWindowDrag?.Invoke(this, EventArgs.Empty);
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);

			if (_closeRect.Contains(e.Location))
				OnCloseRequested?.Invoke(this, EventArgs.Empty);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
		
			_closeRect = new Rectangle(Width - (_closeSize.Width + 2), (Height / 2) - (_closeSize.Height / 2), _closeSize.Width, _closeSize.Height);

		}
	}
}