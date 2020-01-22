using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
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

			e.Graphics.DrawString($"{_load}%", CpuMonitor.Properties.Settings.Default.DataFont,
				_foregroundBrush, Width - _percentSize.Width, (Height / 2) - (CpuMonitor.Properties.Settings.Default.DataFont.Height / 2));

		}
	}
}
