using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.WpfPerformance.Data;

namespace Microsoft.Windows.Media
{
	// Token: 0x02000060 RID: 96
	public class MediaControl
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x0000F791 File Offset: 0x0000E791
		private MediaControl()
		{
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000F79C File Offset: 0x0000E79C
		private MediaControl(int processId, ClrVersion clrVersion)
		{
			string sectionName = "MilCore-" + processId.ToString(CultureInfo.InvariantCulture);
			string sectionName2 = "wpfgfx_v0400-" + processId.ToString(CultureInfo.InvariantCulture);
			switch (clrVersion)
			{
			case ClrVersion.AnyClr:
				if (MediaControl.CanAttachInternal(sectionName2))
				{
					this.ConnectSharedMemorySection(sectionName2);
					return;
				}
				this.ConnectSharedMemorySection(sectionName);
				return;
			case ClrVersion.V2:
				this.ConnectSharedMemorySection(sectionName);
				return;
			case ClrVersion.V4:
				this.ConnectSharedMemorySection(sectionName2);
				return;
			default:
				return;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000F81C File Offset: 0x0000E81C
		public static bool CanControl
		{
			get
			{
				if (MediaControl.canControl == null)
				{
					MediaControl.canControl = new bool?(true);
					try
					{
						MediaControl.CanAttach(new SelectProcessArgs(Process.GetCurrentProcess(), ClrVersion.AnyClr));
					}
					catch (DllNotFoundException ex)
					{
						MediaControl.errorMessage = ex.Message;
						MediaControl.canControl = new bool?(false);
					}
					catch (Exception)
					{
					}
				}
				return MediaControl.canControl == true;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000F8A4 File Offset: 0x0000E8A4
		public static string ErrorMessage
		{
			get
			{
				return MediaControl.errorMessage;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000F8AC File Offset: 0x0000E8AC
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x0000F8D4 File Offset: 0x0000E8D4
		public unsafe bool ShowDirtyRegionOverlay
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return ptr->ShowDirtyRegionOverlay != 0U;
			}
			set
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				ptr->ShowDirtyRegionOverlay = (value ? 1U : 0U);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000F8FC File Offset: 0x0000E8FC
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x0000F924 File Offset: 0x0000E924
		public unsafe bool ClearBackBufferBeforeRendering
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return ptr->ClearBackBufferBeforeRendering != 0U;
			}
			set
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				ptr->ClearBackBufferBeforeRendering = (value ? 1U : 0U);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000F94C File Offset: 0x0000E94C
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x0000F974 File Offset: 0x0000E974
		public unsafe bool DisableDirtyRegionSupport
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return ptr->DisableDirtyRegionSupport != 0U;
			}
			set
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				ptr->DisableDirtyRegionSupport = (value ? 1U : 0U);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000F99C File Offset: 0x0000E99C
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x0000F9C4 File Offset: 0x0000E9C4
		public unsafe bool EnableTranslucentRendering
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return ptr->EnableTranslucentRendering != 0U;
			}
			set
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				ptr->EnableTranslucentRendering = (value ? 1U : 0U);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000F9EC File Offset: 0x0000E9EC
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x0000FA14 File Offset: 0x0000EA14
		public unsafe bool AlphaEffectsDisabled
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return ptr->AlphaEffectsDisabled != 0U;
			}
			set
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				ptr->AlphaEffectsDisabled = (value ? 1U : 0U);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0000FA3C File Offset: 0x0000EA3C
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x0000FA64 File Offset: 0x0000EA64
		public unsafe bool PrimitiveSoftwareFallbackDisabled
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return ptr->PrimitiveSoftwareFallbackDisabled != 0U;
			}
			set
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				ptr->PrimitiveSoftwareFallbackDisabled = (value ? 1U : 0U);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000FA8C File Offset: 0x0000EA8C
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x0000FAB4 File Offset: 0x0000EAB4
		public unsafe bool PurpleSoftwareFallback
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return ptr->PurpleSoftwareFallback != 0U;
			}
			set
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				ptr->PurpleSoftwareFallback = (value ? 1U : 0U);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000FADC File Offset: 0x0000EADC
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x0000FB04 File Offset: 0x0000EB04
		public unsafe bool FantScalerDisabled
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return ptr->FantScalerDisabled != 0U;
			}
			set
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				ptr->FantScalerDisabled = (value ? 1U : 0U);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000FB2C File Offset: 0x0000EB2C
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x0000FB54 File Offset: 0x0000EB54
		public unsafe bool Draw3DDisabled
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return ptr->Draw3DDisabled != 0U;
			}
			set
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				ptr->Draw3DDisabled = (value ? 1U : 0U);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000FB7C File Offset: 0x0000EB7C
		public unsafe int FrameRate
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return (int)ptr->FrameRate;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000FB9C File Offset: 0x0000EB9C
		public unsafe int DirtyRectAddRate
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return (int)ptr->DirtyRectAddRate;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0000FBBC File Offset: 0x0000EBBC
		public unsafe int PercentElapsedTimeForComposition
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return (int)ptr->PercentElapsedTimeForComposition;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000FBDC File Offset: 0x0000EBDC
		public unsafe int TrianglesPerFrame
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return (int)ptr->TrianglesPerFrame;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000FBFC File Offset: 0x0000EBFC
		public unsafe int TrianglesPerFrameMax
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return (int)ptr->TrianglesPerFrameMax;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000FC1C File Offset: 0x0000EC1C
		public unsafe int TrianglesPerFrameCumulative
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return (int)ptr->TrianglesPerFrameCumulative;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000FC3C File Offset: 0x0000EC3C
		public unsafe int PixelsFilledPerFrame
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return (int)ptr->PixelsFilledPerFrame;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000FC5C File Offset: 0x0000EC5C
		public unsafe int PixelsFilledPerFrameMax
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return (int)ptr->PixelsFilledPerFrameMax;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000FC7C File Offset: 0x0000EC7C
		public unsafe int PixelsFilledPerFrameCumulative
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return (int)ptr->PixelsFilledPerFrameCumulative;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0000FC9C File Offset: 0x0000EC9C
		public unsafe int TextureUpdatesPerFrame
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return (int)ptr->TextureUpdatesPerFrame;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000FCBC File Offset: 0x0000ECBC
		public unsafe int TextureUpdatesPerFrameMax
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return (int)ptr->TextureUpdatesPerFrameMax;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0000FCDC File Offset: 0x0000ECDC
		public unsafe int TextureUpdatesPerFrameCumulative
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return (int)ptr->TextureUpdatesPerFrameCumulative;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000FCFC File Offset: 0x0000ECFC
		public unsafe int VideoMemoryUsage
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return (int)ptr->VideoMemoryUsage;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0000FD1C File Offset: 0x0000ED1C
		public unsafe int VideoMemoryUsageMax
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return (int)ptr->VideoMemoryUsageMax;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000FD3C File Offset: 0x0000ED3C
		public unsafe int VideoMemoryUsageMin
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return (int)ptr->VideoMemoryUsageMin;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0000FD5C File Offset: 0x0000ED5C
		public unsafe int SoftwareRenderTargets
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return (int)ptr->NumSoftwareRenderTargets;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000FD7C File Offset: 0x0000ED7C
		public unsafe int HardwareRenderTargets
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return (int)ptr->NumHardwareRenderTargets;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000FD9C File Offset: 0x0000ED9C
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x0000FDBC File Offset: 0x0000EDBC
		public unsafe int HardwareIntermediateRenderTargetsMax
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return (int)ptr->NumHardwareIntermediateRenderTargetsMax;
			}
			set
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				ptr->NumHardwareIntermediateRenderTargetsMax = (uint)value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000FDDC File Offset: 0x0000EDDC
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x0000FDFC File Offset: 0x0000EDFC
		public unsafe int SoftwareIntermediateRenderTargetsMax
		{
			get
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				return (int)ptr->NumSoftwareIntermediateRenderTargetsMax;
			}
			set
			{
				MediaControl.MediaControlFile* ptr = (MediaControl.MediaControlFile*)((void*)this.filePointer);
				ptr->NumSoftwareIntermediateRenderTargetsMax = (uint)value;
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000FE1C File Offset: 0x0000EE1C
		public static MediaControl Attach(int processId, ClrVersion clrVersion)
		{
			return new MediaControl(processId, clrVersion);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000FE28 File Offset: 0x0000EE28
		public static bool CanAttach(SelectProcessArgs args)
		{
			bool result = false;
			if (MediaControl.CanControl)
			{
				string sectionName = "MilCore-" + args.Process.Id.ToString(CultureInfo.InvariantCulture);
				string sectionName2 = "wpfgfx_v0400-" + args.Process.Id.ToString(CultureInfo.InvariantCulture);
				switch (args.ClrVersion)
				{
				case ClrVersion.V4:
					result = MediaControl.CanAttachInternal(sectionName2);
					break;
				}
			}
			return result;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000FEC9 File Offset: 0x0000EEC9
		private static void IFT(int hr)
		{
			if (hr >= 0)
			{
				return;
			}
			Marshal.ThrowExceptionForHR(hr);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000FED8 File Offset: 0x0000EED8
		private void ConnectSharedMemorySection(string sectionName)
		{
			if (IntPtr.Size == 8)
			{
				MediaControl.IFT(MediaControl.Imports_x64.Attach(sectionName, out this.debugControl));
				MediaControl.IFT(MediaControl.Imports_x64.GetDataPtr(this.debugControl, out this.filePointer));
				return;
			}
			MediaControl.IFT(MediaControl.Imports_x86.Attach(sectionName, out this.debugControl));
			MediaControl.IFT(MediaControl.Imports_x86.GetDataPtr(this.debugControl, out this.filePointer));
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000FF3C File Offset: 0x0000EF3C
		private static bool CanAttachInternal(string sectionName)
		{
			bool result = false;
			if (IntPtr.Size == 8)
			{
				MediaControl.IFT(MediaControl.Imports_x64.CanAttach(sectionName, out result));
			}
			else
			{
				MediaControl.IFT(MediaControl.Imports_x86.CanAttach(sectionName, out result));
			}
			return result;
		}

		// Token: 0x0400022D RID: 557
		private const string MilCtrlFilePreV4 = "MilCore-";

		// Token: 0x0400022E RID: 558
		private const string MilCtrlFileV4 = "wpfgfx_v0400-";

		// Token: 0x0400022F RID: 559
		private static bool? canControl;

		// Token: 0x04000230 RID: 560
		private static string errorMessage;

		// Token: 0x04000231 RID: 561
		private IntPtr filePointer;

		// Token: 0x04000232 RID: 562
		private MediaControl.MediaControlHandle debugControl;

		// Token: 0x02000061 RID: 97
		private struct MediaControlFile
		{
			// Token: 0x04000233 RID: 563
			public uint ShowDirtyRegionOverlay;

			// Token: 0x04000234 RID: 564
			public uint ClearBackBufferBeforeRendering;

			// Token: 0x04000235 RID: 565
			public uint DisableDirtyRegionSupport;

			// Token: 0x04000236 RID: 566
			public uint EnableTranslucentRendering;

			// Token: 0x04000237 RID: 567
			public uint FrameRate;

			// Token: 0x04000238 RID: 568
			public uint DirtyRectAddRate;

			// Token: 0x04000239 RID: 569
			public uint PercentElapsedTimeForComposition;

			// Token: 0x0400023A RID: 570
			public uint TrianglesPerFrame;

			// Token: 0x0400023B RID: 571
			public uint TrianglesPerFrameMax;

			// Token: 0x0400023C RID: 572
			public uint TrianglesPerFrameCumulative;

			// Token: 0x0400023D RID: 573
			public uint PixelsFilledPerFrame;

			// Token: 0x0400023E RID: 574
			public uint PixelsFilledPerFrameMax;

			// Token: 0x0400023F RID: 575
			public uint PixelsFilledPerFrameCumulative;

			// Token: 0x04000240 RID: 576
			public uint TextureUpdatesPerFrame;

			// Token: 0x04000241 RID: 577
			public uint TextureUpdatesPerFrameMax;

			// Token: 0x04000242 RID: 578
			public uint TextureUpdatesPerFrameCumulative;

			// Token: 0x04000243 RID: 579
			public uint VideoMemoryUsage;

			// Token: 0x04000244 RID: 580
			public uint VideoMemoryUsageMin;

			// Token: 0x04000245 RID: 581
			public uint VideoMemoryUsageMax;

			// Token: 0x04000246 RID: 582
			public uint NumSoftwareRenderTargets;

			// Token: 0x04000247 RID: 583
			public uint NumHardwareRenderTargets;

			// Token: 0x04000248 RID: 584
			public uint NumHardwareIntermediateRenderTargets;

			// Token: 0x04000249 RID: 585
			public uint NumHardwareIntermediateRenderTargetsMax;

			// Token: 0x0400024A RID: 586
			public uint NumSoftwareIntermediateRenderTargets;

			// Token: 0x0400024B RID: 587
			public uint NumSoftwareIntermediateRenderTargetsMax;

			// Token: 0x0400024C RID: 588
			public uint AlphaEffectsDisabled;

			// Token: 0x0400024D RID: 589
			public uint PrimitiveSoftwareFallbackDisabled;

			// Token: 0x0400024E RID: 590
			public uint PurpleSoftwareFallback;

			// Token: 0x0400024F RID: 591
			public uint FantScalerDisabled;

			// Token: 0x04000250 RID: 592
			public uint Draw3DDisabled;
		}

		// Token: 0x02000062 RID: 98
		private static class Imports_x86
		{
			// Token: 0x06000438 RID: 1080
			[DllImport("milctrl_v0300_x86.dll", EntryPoint = "MediaControl_CanAttach")]
			internal static extern int CanAttach([MarshalAs(UnmanagedType.LPWStr)] string sectionName, out bool canAccess);

			// Token: 0x06000439 RID: 1081
			[DllImport("milctrl_v0300_x86.dll", EntryPoint = "MediaControl_Attach")]
			internal static extern int Attach([MarshalAs(UnmanagedType.LPWStr)] string sectionName, out MediaControl.MediaControlHandle debugControl);

			// Token: 0x0600043A RID: 1082
			[DllImport("milctrl_v0300_x86.dll", EntryPoint = "MediaControl_Release")]
			internal static extern void Release(IntPtr debugControl);

			// Token: 0x0600043B RID: 1083
			[DllImport("milctrl_v0300_x86.dll", EntryPoint = "MediaControl_GetDataPtr")]
			internal static extern int GetDataPtr(MediaControl.MediaControlHandle debugControl, out IntPtr mediaControlPointer);

			// Token: 0x04000251 RID: 593
			private const string MilCtrlDll = "milctrl_v0300_x86.dll";
		}

		// Token: 0x02000063 RID: 99
		private static class Imports_x64
		{
			// Token: 0x0600043C RID: 1084
			[DllImport("milctrl_v0300_x64.dll", EntryPoint = "MediaControl_CanAttach")]
			internal static extern int CanAttach([MarshalAs(UnmanagedType.LPWStr)] string sectionName, out bool canAccess);

			// Token: 0x0600043D RID: 1085
			[DllImport("milctrl_v0300_x64.dll", EntryPoint = "MediaControl_Attach")]
			internal static extern int Attach([MarshalAs(UnmanagedType.LPWStr)] string sectionName, out MediaControl.MediaControlHandle debugControl);

			// Token: 0x0600043E RID: 1086
			[DllImport("milctrl_v0300_x64.dll", EntryPoint = "MediaControl_Release")]
			internal static extern void Release(IntPtr debugControl);

			// Token: 0x0600043F RID: 1087
			[DllImport("milctrl_v0300_x64.dll", EntryPoint = "MediaControl_GetDataPtr")]
			internal static extern int GetDataPtr(MediaControl.MediaControlHandle debugControl, out IntPtr mediaControlPointer);

			// Token: 0x04000252 RID: 594
			private const string MilCtrlDll = "milctrl_v0300_x64.dll";
		}

		// Token: 0x02000064 RID: 100
		private sealed class MediaControlHandle : SafeHandle
		{
			// Token: 0x06000440 RID: 1088 RVA: 0x0000FF70 File Offset: 0x0000EF70
			internal MediaControlHandle() : base(IntPtr.Zero, true)
			{
			}

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000FF7E File Offset: 0x0000EF7E
			public override bool IsInvalid
			{
				get
				{
					return this.handle == IntPtr.Zero;
				}
			}

			// Token: 0x06000442 RID: 1090 RVA: 0x0000FF90 File Offset: 0x0000EF90
			protected override bool ReleaseHandle()
			{
				if (IntPtr.Size == 8)
				{
					MediaControl.Imports_x64.Release(this.handle);
				}
				else
				{
					MediaControl.Imports_x86.Release(this.handle);
				}
				return true;
			}
		}
	}
}
