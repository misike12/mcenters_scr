using System;
using System.Runtime.Serialization;
using System.Security;

namespace <CrtImplementationDetails>
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	internal class ModuleLoadExceptionHandlerException : ModuleLoadException
	{
		// Token: 0x06000074 RID: 116 RVA: 0x0000A9D0 File Offset: 0x00009DD0
		protected ModuleLoadExceptionHandlerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			Type typeFromHandle = typeof(Exception);
			string name = "NestedException";
			this.NestedException = (Exception)info.GetValue(name, typeFromHandle);
			GC.KeepAlive(this);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000B0D8 File Offset: 0x0000A4D8
		public ModuleLoadExceptionHandlerException(string message, Exception innerException, Exception nestedException) : base(message, innerException)
		{
			this.NestedException = nestedException;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000076 RID: 118 RVA: 0x0000A8AC File Offset: 0x00009CAC
		// (set) Token: 0x06000077 RID: 119 RVA: 0x0000A8C4 File Offset: 0x00009CC4
		public Exception NestedException
		{
			get
			{
				return this.<backing_store>NestedException;
			}
			set
			{
				this.<backing_store>NestedException = value;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000A8D8 File Offset: 0x00009CD8
		public override string ToString()
		{
			string text = (this.InnerException == null) ? string.Empty : this.InnerException.ToString();
			string text2 = (this.NestedException == null) ? string.Empty : this.NestedException.ToString();
			object[] array = new object[4];
			Type type = this.GetType();
			array[0] = type;
			string text3;
			if (this.Message != null)
			{
				text3 = this.Message;
			}
			else
			{
				text3 = string.Empty;
			}
			array[1] = text3;
			string text4;
			if (text != null)
			{
				text4 = text;
			}
			else
			{
				text4 = string.Empty;
			}
			array[2] = text4;
			string text5;
			if (text2 != null)
			{
				text5 = text2;
			}
			else
			{
				text5 = string.Empty;
			}
			array[3] = text5;
			string result = string.Format("\n{0}: {1}\n--- Start of primary exception ---\n{2}\n--- End of primary exception ---\n\n--- Start of nested exception ---\n{3}\n--- End of nested exception ---\n", array);
			GC.KeepAlive(this);
			return result;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000A994 File Offset: 0x00009D94
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			Type typeFromHandle = typeof(Exception);
			Exception nestedException = this.NestedException;
			string name = "NestedException";
			info.AddValue(name, nestedException, typeFromHandle);
			GC.KeepAlive(this);
		}

		// Token: 0x04000086 RID: 134
		private const string formatString = "\n{0}: {1}\n--- Start of primary exception ---\n{2}\n--- End of primary exception ---\n\n--- Start of nested exception ---\n{3}\n--- End of nested exception ---\n";

		// Token: 0x04000087 RID: 135
		private Exception <backing_store>NestedException;
	}
}
