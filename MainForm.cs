using CpuMonitor.Statistics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpuMonitor
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();

			TitleControl.Left = Properties.Settings.Default.BorderSize;
			TitleControl.Top = Properties.Settings.Default.BorderSize;
			TitleControl.Width = Width - (Properties.Settings.Default.BorderSize * 2);
			TitleControl.Height = TitleControl.TitleFont.Height + Properties.Settings.Default.BorderSize;

			DataControl.Left = Properties.Settings.Default.BorderSize;
			DataControl.Top = TitleControl.Bottom + Properties.Settings.Default.BorderSize;
			DataControl.Width = Width - (Properties.Settings.Default.BorderSize * 2);
			DataControl.Height = Height - (DataControl.Top + Properties.Settings.Default.BorderSize);

			TitleControl.OnWindowDrag += (sender, args) => DragWindow();
			DataControl.OnResizeStart += (sender, args) => DragResizeStart();
			DataControl.OnResizeMove += (sender, args) => DragResizeMove();
			DataControl.OnResizeEnd += (sender, args) => DragResizeEnd();
		}

		public void Shutdown()
		{
			DataControl.Shutdown();
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);

			TitleControl.Width = Width - (Properties.Settings.Default.BorderSize * 2);
			DataControl.Width = Width - (Properties.Settings.Default.BorderSize * 2);
			DataControl.Height = Height - (DataControl.Top + Properties.Settings.Default.BorderSize);
		}



		#region Handle Window Dragging

		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 0x2;

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		private void DragWindow()
		{
			ReleaseCapture();
			SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
		}

		#endregion

		#region Handle Window Resizing

		private Rectangle? _drag = null;

		private void DragResizeStart()
		{
			_drag = new Rectangle(MousePosition.X, MousePosition.Y, Width, Height);
		}

		private void DragResizeMove()
		{
			if (_drag == null) 
				return;

			Width = MousePosition.X - ((Rectangle)_drag).X + ((Rectangle)_drag).Width;
			Height = MousePosition.Y - ((Rectangle)_drag).Y + ((Rectangle)_drag).Height;

			Invalidate(true);
		}

		private void DragResizeEnd()
		{
			_drag = null;
		}

		#endregion
	}
}
