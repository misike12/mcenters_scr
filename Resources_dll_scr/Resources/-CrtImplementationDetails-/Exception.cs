using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	internal class Exception : Exception
	{
		// Token: 0x0600006E RID: 110 RVA: 0x0000A850 File Offset: 0x00009C50
		protected Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000A838 File Offset: 0x00009C38
		public Exception(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000A824 File Offset: 0x00009C24
		public Exception(string message) : base(message)
		{
		}
	}
}
