using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CpuMonitor.Statistics;

namespace CpuMonitor
{
	public class CpuControl : Panel
	{
		private CpuCore _core;
		private readonly Brush _backgroundBrush = new SolidBrush(CpuMonitor.Properties.Settings.Default.BackgroundColour);
		private readonly Brush _foregroundBrush = new SolidBrush(CpuMonitor.Properties.Settings.Default.ForegroundColour);
		private readonly Pen _barOutlineColor = new Pen(CpuMonitor.Properties.Settings.Default.ForegroundColour, 1.0f);

		private int _load = 0;
		private string _label;
		private Size _labelSize;
		private Size _percentSize;
		private readonly int _coreNum;

		public CpuControl(int coreNum) : base()
		{
			_coreNum = coreNum;
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();

			DoubleBuffered = true;

			_core = new CpuCore(_coreNum);

			_label = $"Core {(_coreNum + 1):00}";
			_labelSize = TextRenderer.MeasureText(_label, CpuMonitor.Properties.Settings.Default.DataFont);
			_percentSize = TextRenderer.MeasureText("100%", CpuMonitor.Properties.Settings.Default.DataFont);

			_core.OnCoreChanged += (number, load) =>
			{
				_load = load;
				Invalidate();
			};

			Height = _labelSize.Height;
		}

		public void Shutdown()
		{
			_core.Shutdown();
		}



		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			e.Graphics.FillRectangle(_backgroundBrush, 0,0,Width,Height);
			e.Graphics.DrawString(_label, CpuMonitor.Properties.Settings.Default.DataFont, 
				_foregroundBrush, 0, (Height / 2) - (CpuMonitor.Properties.Settings.Default.DataFont.Height / 2));

			int rhs = Width - _percentSize.Width;

			e.Graphics.DrawString($"{_load}%", CpuMonitor.Properties.Settings.Default.DataFont,
				_foregroundBrush, rhs, (Height / 2) - (CpuMonitor.Properties.Settings.Default.DataFont.Height / 2));

			int barWidth = rhs - (_labelSize.Width + 8);
			int loadWidth = (int)((_load / 100.0f) * barWidth);

			using (LinearGradientBrush gradBrush = new LinearGradientBrush(
				new Point(_labelSize.Width + 4, 0), new Point(_labelSize.Width + 4 + barWidth, 0),
				Color.FromArgb(255, 0, 255, 0),
				Color.FromArgb(255, 255, 0, 0)))
			{
				e.Graphics.FillRectangle(gradBrush, _labelSize.Width + 4, 2, loadWidth, Height-4);
			};

			
			e.Graphics.DrawRectangle(_barOutlineColor, _labelSize.Width + 4, 2, barWidth, Height-4);
		}
	}
}
