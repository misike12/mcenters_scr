using System;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace mcenters
{
	// Token: 0x02000003 RID: 3
	public class Logger
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00006688 File Offset: 0x00005A88
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000669C File Offset: 0x00005A9C
		private static Paragraph Text
		{
			get
			{
				return Logger.<backing_store>Text;
			}
			set
			{
				Logger.<backing_store>Text = value;
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000066B0 File Offset: 0x00005AB0
		// (set) Token: 0x0600005D RID: 93 RVA: 0x000066C4 File Offset: 0x00005AC4
		private static RichTextBox logger
		{
			get
			{
				return Logger.<backing_store>logger;
			}
			set
			{
				Logger.<backing_store>logger = value;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000066EC File Offset: 0x00005AEC
		public static void SetLogControl(RichTextBox box)
		{
			Logger.logger = box;
			Logger.logger.Document.Blocks.Clear();
			if (Logger.Text == null)
			{
				Logger.Text = new Paragraph();
			}
			Logger.logger.Document.Blocks.Add(Logger.Text);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00006740 File Offset: 0x00005B40
		public static void Clear()
		{
			Logger.Text.Inlines.Clear();
			Logger.cleared = true;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000686C File Offset: 0x00005C6C
		public static void AddLog(string text, Color color)
		{
			if (Logger.logger != null)
			{
				Run run;
				if (!Logger.cleared)
				{
					run = new Run("\n" + text);
					run.Foreground = new SolidColorBrush(color);
				}
				else
				{
					run = new Run(text);
					run.Foreground = new SolidColorBrush(color);
					Logger.cleared = false;
				}
				Logger.Text.Inlines.Add(run);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000067FC File Offset: 0x00005BFC
		public static void AddLog(string text)
		{
			Color blue = Colors.Blue;
			if (Logger.logger != null)
			{
				Run run;
				if (!Logger.cleared)
				{
					run = new Run("\n" + text);
					run.Foreground = new SolidColorBrush(blue);
				}
				else
				{
					run = new Run(text);
					run.Foreground = new SolidColorBrush(blue);
					Logger.cleared = false;
				}
				Logger.Text.Inlines.Add(run);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000068D8 File Offset: 0x00005CD8
		public static void AddError(string text)
		{
			if (Logger.logger != null)
			{
				Run run;
				if (!Logger.cleared)
				{
					run = new Run("\n" + text);
					Color red = Colors.Red;
					run.Foreground = new SolidColorBrush(red);
				}
				else
				{
					run = new Run(text);
					Color red2 = Colors.Red;
					run.Foreground = new SolidColorBrush(red2);
					Logger.cleared = false;
				}
				Logger.Text.Inlines.Add(run);
			}
		}

		// Token: 0x04000077 RID: 119
		private static bool cleared = true;

		// Token: 0x04000078 RID: 120
		private static Paragraph <backing_store>Text;

		// Token: 0x04000079 RID: 121
		private static RichTextBox <backing_store>logger;
	}
}
