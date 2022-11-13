using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using mcenters;

namespace M_Centers
{
	// Token: 0x02000003 RID: 3
	public partial class MainWindow : Window
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000208C File Offset: 0x0000028C
		public MainWindow()
		{
			this.InitializeComponent();
			API.EnableDebug();
			Logger.SetLogControl(this.outputBox);
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			bool flag = commandLineArgs != null;
			if (flag)
			{
				string text = null;
				bool flag2 = commandLineArgs.Length > 1;
				if (flag2)
				{
					text = commandLineArgs[1];
				}
				bool flag3 = !string.IsNullOrEmpty(text);
				if (flag3)
				{
					bool flag4 = text == "trial-enable";
					if (flag4)
					{
						this.trial = true;
						this.trialBox.IsChecked = new bool?(true);
						this.PerformHack();
					}
					bool flag5 = text == "trial-disable";
					if (flag5)
					{
						this.trial = false;
						this.trialBox.IsChecked = new bool?(false);
						this.PerformHack();
					}
				}
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000215B File Offset: 0x0000035B
		private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Environment.Exit(0);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002168 File Offset: 0x00000368
		private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
		{
			TextBlock textBlock = sender as TextBlock;
			textBlock.Background = null;
			textBlock.Foreground = new SolidColorBrush(Colors.Red);
			textBlock.FontWeight = FontWeights.ExtraBold;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021A4 File Offset: 0x000003A4
		private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
		{
			TextBlock textBlock = sender as TextBlock;
			textBlock.Foreground = new SolidColorBrush(Colors.Black);
			textBlock.Background = new SolidColorBrush(Colors.Red);
			textBlock.FontWeight = FontWeights.Bold;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021E8 File Offset: 0x000003E8
		private void PerformHack()
		{
			Logger.Clear();
			bool flag = this.trial;
			if (flag)
			{
				Logger.AddLog("Selected Minecraft Mode = Enable Trial");
			}
			else
			{
				Logger.AddLog("Selected Minecraft Mode = Enable Full Game");
			}
			API.PrepareProcess(this.trial);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000222A File Offset: 0x0000042A
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.PerformHack();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002234 File Offset: 0x00000434
		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			this.trial = true;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000223E File Offset: 0x0000043E
		private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			this.trial = false;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002248 File Offset: 0x00000448
		private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
		{
			bool flag = e.LeftButton == MouseButtonState.Pressed;
			if (flag)
			{
				base.DragMove();
			}
		}

		// Token: 0x04000001 RID: 1
		private bool trial = false;
	}
}
