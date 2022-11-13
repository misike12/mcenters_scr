using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

namespace <CrtImplementationDetails>
{
	// Token: 0x0200000B RID: 11
	internal class ModuleUninitializer : Stack
	{
		// Token: 0x0600007A RID: 122 RVA: 0x0000AA3C File Offset: 0x00009E3C
		[SecuritySafeCritical]
		internal void AddHandler(EventHandler handler)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				Monitor.Enter(ModuleUninitializer.@lock, ref flag);
				RuntimeHelpers.PrepareDelegate(handler);
				this.Push(handler);
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(ModuleUninitializer.@lock);
				}
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000B110 File Offset: 0x0000A510
		[SecuritySafeCritical]
		private ModuleUninitializer()
		{
			EventHandler value = new EventHandler(this.SingletonDomainUnload);
			AppDomain.CurrentDomain.DomainUnload += value;
			AppDomain.CurrentDomain.ProcessExit += value;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000AA9C File Offset: 0x00009E9C
		[PrePrepareMethod]
		[SecurityCritical]
		private void SingletonDomainUnload(object source, EventArgs arguments)
		{
			IEnumerator enumerator = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				Monitor.Enter(ModuleUninitializer.@lock, ref flag);
				foreach (EventHandler eventHandler in this)
				{
					eventHandler(source, arguments);
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(ModuleUninitializer.@lock);
				}
			}
		}

		// Token: 0x04000088 RID: 136
		private static object @lock = new object();

		// Token: 0x04000089 RID: 137
		internal static ModuleUninitializer _ModuleUninitializer = new ModuleUninitializer();
	}
}
