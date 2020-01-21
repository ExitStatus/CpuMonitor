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

		public string Title { get; set; }
		public Font TitleFont { get; set; } = new Font(FontFamily.GenericSansSerif, 8.0f, FontStyle.Regular);
		public Brush TitleColor { get; set; } = new SolidBrush(Color.White);

		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			LinearGradientBrush linGrBrush = new LinearGradientBrush(
				new Point(0, 0),
				new Point(Width, 0),
				Color.FromArgb(255, 128, 0, 0),   // Opaque red
				Color.FromArgb(255, 0, 0, 0));  // Opaque blue

			e.Graphics.FillRectangle(linGrBrush, 0, 0, Width, Height);
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, Width, Height, Border3DStyle.SunkenInner);

			e.Graphics.DrawString(Title, TitleFont, TitleColor, 4, (Height / 2) - (TitleFont.Height / 2));
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (e.Button == MouseButtons.Left)
				OnWindowDrag?.Invoke(this, EventArgs.Empty);
		}
	}
}