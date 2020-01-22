using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpuMonitor.Statistics
{
	public delegate void CoreChangedEventHandler(int coreNumber, int load);

	public class CpuCore
	{

		public event CoreChangedEventHandler OnCoreChanged;

		public int CoreNumber { get; private set; }
		public string ErrorMessage { get; private set; }

		public int Load
		{
			get => _load;
			set
			{
				if (_load != value)
				{
					_load = value;
					OnCoreChanged?.Invoke(CoreNumber, _load);
				}
			}
		}

		private int _load = 0;

		private readonly Thread _processThread;

		public CpuCore(int core)
		{
			CoreNumber = core;

			_processThread = new Thread(new ThreadStart(Scan))
			{
				Priority = ThreadPriority.BelowNormal,
				Name = $"Core_{core}"
			};

			_processThread.Start();
		}

		public void Shutdown()
		{
			_processThread.Abort();
			_processThread.Join();
		}

		private void Scan()
		{
			try
			{
				var perf = new PerformanceCounter("Processor", "% Processor Time", $"{CoreNumber}");
				bool finish = false;

				while (!finish)
				{
					ErrorMessage = null;

					try
					{
						Load = (int)perf.NextValue();

						Thread.Sleep(Properties.Settings.Default.UpdateFrequency);
					}
					catch (InvalidOperationException) { }
					catch (Win32Exception) { }
					catch (PlatformNotSupportedException) { }
					catch (UnauthorizedAccessException) { }
					catch (ThreadAbortException) { finish = true; }
					catch (Exception ex)
					{
						ErrorMessage = ex.Message;
					}
				}
			}
			catch (ThreadAbortException)
			{
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public static Task<int> GetCpuCores()
		{
			return Task<int>.Factory.StartNew(() =>
			{
				var cores = 0;

				foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get())
					cores += int.Parse(item["NumberOfCores"].ToString());

				return cores;

			});
		}
	}
}
