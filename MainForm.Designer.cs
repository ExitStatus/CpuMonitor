namespace CpuMonitor
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.MainControl = new CpuMonitor.MainPanel();
			this.DataControl = new CpuMonitor.DataPanel();
			this.TitleControl = new CpuMonitor.TitleBar();
			this.MainControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainControl
			// 
			this.MainControl.Controls.Add(this.DataControl);
			this.MainControl.Controls.Add(this.TitleControl);
			this.MainControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainControl.Location = new System.Drawing.Point(0, 0);
			this.MainControl.Name = "MainControl";
			this.MainControl.Size = new System.Drawing.Size(200, 150);
			this.MainControl.TabIndex = 0;
			// 
			// DataControl
			// 
			this.DataControl.Location = new System.Drawing.Point(24, 40);
			this.DataControl.Name = "DataControl";
			this.DataControl.Size = new System.Drawing.Size(145, 100);
			this.DataControl.TabIndex = 1;
			// 
			// TitleControl
			// 
			this.TitleControl.Location = new System.Drawing.Point(12, 12);
			this.TitleControl.Name = "TitleControl";
			this.TitleControl.Size = new System.Drawing.Size(176, 21);
			this.TitleControl.TabIndex = 0;
			this.TitleControl.Title = "CPU Monitor";
			this.TitleControl.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(200, 150);
			this.Controls.Add(this.MainControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Form1";
			this.MainControl.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private MainPanel MainControl;
		private TitleBar TitleControl;
		private DataPanel DataControl;
	}
}

