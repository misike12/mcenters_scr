using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x0200000C RID: 12
	[Serializable]
	internal class OpenMPWithMultipleAppdomainsException : Exception
	{
		// Token: 0x0600007E RID: 126 RVA: 0x0000ADB0 File Offset: 0x0000A1B0
		protected OpenMPWithMultipleAppdomainsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000AD9C File Offset: 0x0000A19C
		public OpenMPWithMultipleAppdomainsException()
		{
		}
	}
}
