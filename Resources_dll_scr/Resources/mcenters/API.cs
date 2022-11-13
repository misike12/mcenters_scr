using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Media;

namespace mcenters
{
	// Token: 0x02000004 RID: 4
	public class API
	{
		// Token: 0x06000065 RID: 101 RVA: 0x0000678C File Offset: 0x00005B8C
		public static int Test()
		{
			return 1;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000679C File Offset: 0x00005B9C
		public static int EnableDebug()
		{
			return <Module>.EnableDebugPriv();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000069D0 File Offset: 0x00005DD0
		public static PrepareResults PrepareProcess([MarshalAs(UnmanagedType.U1)] bool trial)
		{
			Process process = null;
			process = null;
			bool flag = false;
			PrepareResults result;
			try
			{
				int num = <Module>.LaunchApp();
				process = ((num == 0) ? process : Process.GetProcessById(num));
				if (API.appId == 0)
				{
					API.appId = num;
					process.EnableRaisingEvents = true;
					process.Exited += API.OnExited;
				}
				if (process != null)
				{
					flag = true;
				}
			}
			catch (Exception ex)
			{
				Logger.AddError("An Error Occured while starting Minecraft");
				result = PrepareResults.StartError;
				goto IL_5B;
			}
			if (!flag)
			{
				Logger.AddError("Minecraft Start Failed. Is Minecraft Installed?");
				return PrepareResults.StartFailed;
			}
			if (!API.IsValidVersion(process.MainModule.FileVersionInfo))
			{
				Logger.AddError("Unsupported Minecraft Version");
				return PrepareResults.InvalidVersion;
			}
			IntPtr baseAddress = process.MainModule.BaseAddress;
			int num2 = <Module>.ModifyApp(process.Id, baseAddress.ToPointer(), trial, API.ignoreVerification);
			if (num2 == -4 || num2 == -3)
			{
				Logger.AddError("Error Occured While Writing Memory");
				return PrepareResults.MemoryWriteError;
			}
			if (num2 == -2)
			{
				Logger.AddError("Error Occured While Reading Memory");
				return PrepareResults.MemoryReadError;
			}
			if (num2 == 0)
			{
				Logger.AddError("Unknown Memory Patterns\nYou may be using unsupported version\nor using other mods");
				return PrepareResults.MemoryPatternError;
			}
			if (num2 != 1)
			{
				Logger.AddError("Unknown Error Occured");
				return PrepareResults.UnknownError;
			}
			API.ignoreVerification = true;
			Color green = Colors.Green;
			Logger.AddLog("Mode Injection Successful", green);
			return PrepareResults.Success;
			IL_5B:
			return result;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00006B28 File Offset: 0x00005F28
		public static int GetProcess()
		{
			Process[] processesByName = Process.GetProcessesByName("Minecraft.Windows");
			int result;
			if (processesByName == null)
			{
				result = -1;
			}
			else
			{
				List<Process> list = new List<Process>();
				foreach (Process process in processesByName)
				{
					FileVersionInfo fileVersionInfo = process.MainModule.FileVersionInfo;
					if (API.IsValidVersion(fileVersionInfo))
					{
						list.Add(process);
					}
				}
				if (list.Count == 0)
				{
					result = -1;
				}
				else
				{
					int num = 0;
					foreach (Process process2 in list)
					{
						if (!process2.HasExited)
						{
							IntPtr baseAddress = process2.MainModule.BaseAddress;
							num = <Module>.IsValidApp(process2.Id, baseAddress.ToPointer());
							if (num != 0)
							{
								int num2 = num;
								if (num2 == -2)
								{
									return -3;
								}
								if (num2 == -1)
								{
									return -2;
								}
								if (num2 == 1)
								{
									process2.Kill();
									return 1;
								}
							}
						}
					}
					int num3 = num;
					if (num3 != -2)
					{
						if (num3 != -1)
						{
							if (num3 != 0)
							{
								if (num3 == 1)
								{
									result = 1;
								}
							}
							else
							{
								result = 0;
							}
						}
						else
						{
							result = -2;
						}
					}
					else
					{
						result = -3;
					}
				}
			}
			return result;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00006980 File Offset: 0x00005D80
		[return: MarshalAs(UnmanagedType.U1)]
		private static bool IsValidVersion(FileVersionInfo info)
		{
			Logger.AddLog("Detected Version: " + info.ProductVersion);
			int result;
			if (info.ProductName == "Minecraft" && info.ProductVersion == "1.19.41.01")
			{
				result = 1;
			}
			else
			{
				result = 0;
			}
			return result != 0;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000067E0 File Offset: 0x00005BE0
		private static void OnExited(object sender, EventArgs e)
		{
			API.ignoreVerification = false;
			API.appId = 0;
		}

		// Token: 0x0400007A RID: 122
		private static bool ignoreVerification = false;

		// Token: 0x0400007B RID: 123
		private static int appId = 0;
	}
}
