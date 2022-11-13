using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	internal class ModuleLoadException : Exception
	{
		// Token: 0x06000071 RID: 113 RVA: 0x0000A894 File Offset: 0x00009C94
		protected ModuleLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000A87C File Offset: 0x00009C7C
		public ModuleLoadException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000A868 File Offset: 0x00009C68
		public ModuleLoadException(string message) : base(message)
		{
		}

		// Token: 0x04000085 RID: 133
		public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";
	}
}
