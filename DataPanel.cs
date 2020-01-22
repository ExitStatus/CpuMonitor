using CpuMonitor.Statistics;
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

		private readonly Brush _backgroundBrush = new SolidBrush(CpuMonitor.Properties.Settings.Default.BackgroundColour);

		private Rectangle _resizeGripRect;
		private bool _resizeActive = false;
		private readonly List<CpuControl> _cpuCores = new List<CpuControl>();


		public void Shutdown()
		{
			_cpuCores.ForEach(z => z.Shutdown());
		}

		protected override async void OnCreateControl()
		{
			base.OnCreateControl();

			DoubleBuffered = true;

			_resizeGripRect = new Rectangle(Width - 12, Height - 12, 11, 11);

			int y = CpuMonitor.Properties.Settings.Default.BorderSize;


			int nCores = await CpuCore.GetCpuCores();

			for (int i = 0; i < nCores; i++)
			{
				var coreControl = new CpuControl(i);

				_cpuCores.Add(coreControl);
				Controls.Add(coreControl);

				coreControl.Top = y;
				coreControl.Left = CpuMonitor.Properties.Settings.Default.BorderSize;
				coreControl.Width = Width - (CpuMonitor.Properties.Settings.Default.BorderSize * 2);

				y += coreControl.Height;

			}
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			e.Graphics.FillRectangle(_backgroundBrush, 0, 0, Width, Height);
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, Width, Height, Border3DStyle.SunkenInner);
			ControlPaint.DrawSizeGrip(e.Graphics, CpuMonitor.Properties.Settings.Default.BackgroundColour, _resizeGripRect);

		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);

			_resizeGripRect = new Rectangle(Width - 12, Height - 12, 11, 11);
			_cpuCores.ForEach(z => z.Width = Width - (CpuMonitor.Properties.Settings.Default.BorderSize * 2));
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
