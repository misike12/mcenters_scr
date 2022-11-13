using System;

namespace mcenters
{
	// Token: 0x02000005 RID: 5
	public enum PrepareResults
	{
		// Token: 0x0400007D RID: 125
		StartError,
		// Token: 0x0400007E RID: 126
		StartFailed,
		// Token: 0x0400007F RID: 127
		InvalidVersion,
		// Token: 0x04000080 RID: 128
		Success,
		// Token: 0x04000081 RID: 129
		MemoryReadError,
		// Token: 0x04000082 RID: 130
		MemoryWriteError,
		// Token: 0x04000083 RID: 131
		MemoryPatternError,
		// Token: 0x04000084 RID: 132
		UnknownError
	}
}
