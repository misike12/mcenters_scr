using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using <CppImplementationDetails>;
using <CrtImplementationDetails>;
using mcenters;

// Token: 0x02000001 RID: 1
internal class <Module>
{
	// Token: 0x06000001 RID: 1 RVA: 0x00006950 File Offset: 0x00005D50
	internal unsafe static void AddLog(char* value)
	{
		Logger.AddLog(new string((char*)value));
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00006968 File Offset: 0x00005D68
	internal unsafe static void AddError(char* value)
	{
		Logger.AddError(new string((char*)value));
	}

	// Token: 0x06000003 RID: 3 RVA: 0x0000B0F4 File Offset: 0x0000A4F4
	internal static void <CrtImplementationDetails>.ThrowNestedModuleLoadException(System.Exception innerException, System.Exception nestedException)
	{
		throw new ModuleLoadExceptionHandlerException("A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n", innerException, nestedException);
	}

	// Token: 0x06000004 RID: 4 RVA: 0x0000AA10 File Offset: 0x00009E10
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage)
	{
		throw new ModuleLoadException(errorMessage);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x0000AA24 File Offset: 0x00009E24
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage, System.Exception innerException)
	{
		throw new ModuleLoadException(errorMessage, innerException);
	}

	// Token: 0x06000006 RID: 6 RVA: 0x0000AB4C File Offset: 0x00009F4C
	internal static void <CrtImplementationDetails>.RegisterModuleUninitializer(EventHandler handler)
	{
		ModuleUninitializer._ModuleUninitializer.AddHandler(handler);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000AB64 File Offset: 0x00009F64
	[SecuritySafeCritical]
	internal unsafe static Guid <CrtImplementationDetails>.FromGUID(_GUID* guid)
	{
		Guid result = new Guid((uint)(*guid), *(guid + 4L), *(guid + 6L), *(guid + 8L + 1L * 0L), *(guid + 9L), *(guid + 8L + 1L * 2L), *(guid + 8L + 1L * 3L), *(guid + 8L + 1L * 4L), *(guid + 8L + 1L * 5L), *(guid + 8L + 1L * 6L), *(guid + 8L + 1L * 7L));
		return result;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000ABDC File Offset: 0x00009FDC
	[SecurityCritical]
	internal unsafe static int __get_default_appdomain(IUnknown** ppUnk)
	{
		int num = 0;
		ICorRuntimeHost* ptr = null;
		try
		{
			Guid riid = <Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e);
			ptr = (ICorRuntimeHost*)RuntimeEnvironment.GetRuntimeInterfaceAsIntPtr(<Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e), riid).ToPointer();
		}
		catch (System.Exception e)
		{
			num = Marshal.GetHRForException(e);
		}
		if (num >= 0)
		{
			long num2 = *(*(long*)ptr + 104L);
			num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown**), ptr, ppUnk, num2);
			ICorRuntimeHost* ptr2 = ptr;
			object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
		}
		return num;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x0000AC68 File Offset: 0x0000A068
	internal unsafe static void __release_appdomain(IUnknown* ppUnk)
	{
		object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ppUnk, *(*(long*)ppUnk + 16L));
	}

	// Token: 0x0600000A RID: 10 RVA: 0x0000AC84 File Offset: 0x0000A084
	[SecurityCritical]
	internal unsafe static AppDomain <CrtImplementationDetails>.GetDefaultDomain()
	{
		IUnknown* ptr = null;
		int num = <Module>.__get_default_appdomain(&ptr);
		if (num >= 0)
		{
			try
			{
				IntPtr pUnk = new IntPtr((void*)ptr);
				object objectForIUnknown = Marshal.GetObjectForIUnknown(pUnk);
				AppDomain appDomain = (AppDomain)objectForIUnknown;
				string message = "Expecting default appdomain";
				byte condition = appDomain.IsDefaultAppDomain() ? 1 : 0;
				Debug.Assert(condition != 0, message);
				return appDomain;
			}
			finally
			{
				<Module>.__release_appdomain(ptr);
			}
		}
		Marshal.ThrowExceptionForHR(num);
		return null;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x0000AD08 File Offset: 0x0000A108
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.DoCallBackInDefaultDomain(method function, void* cookie)
	{
		ICLRRuntimeHost* ptr = null;
		Guid riid = <Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02);
		ptr = (ICLRRuntimeHost*)RuntimeEnvironment.GetRuntimeInterfaceAsIntPtr(<Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02), riid).ToPointer();
		try
		{
			AppDomain appDomain = <Module>.<CrtImplementationDetails>.GetDefaultDomain();
			long num = *(*(long*)ptr + 64L);
			uint id = (uint)appDomain.Id;
			int num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl) (System.Void*),System.Void*), ptr, id, function, cookie, num);
			if (num2 < 0)
			{
				Marshal.ThrowExceptionForHR(num2);
			}
		}
		finally
		{
			ICLRRuntimeHost* ptr2 = ptr;
			object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
		}
	}

	// Token: 0x0600000C RID: 12 RVA: 0x0000ADC8 File Offset: 0x0000A1C8
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __scrt_is_safe_for_managed_code()
	{
		uint _scrt_native_dllmain_reason = <Module>.__scrt_native_dllmain_reason;
		return _scrt_native_dllmain_reason != 0U && _scrt_native_dllmain_reason != 1U;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x0000AE04 File Offset: 0x0000A204
	[SecuritySafeCritical]
	internal unsafe static int <CrtImplementationDetails>.DefaultDomain.DoNothing(void* cookie)
	{
		GC.KeepAlive(int.MaxValue);
		return 0;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x0000AE24 File Offset: 0x0000A224
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool <CrtImplementationDetails>.DefaultDomain.HasPerProcess()
	{
		bool result;
		if (<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)2)
		{
			for (void** ptr = (void**)(&<Module>.__xc_mp_a); ptr < (void**)(&<Module>.__xc_mp_z); ptr += 8L / (long)sizeof(void*))
			{
				if (*(long*)ptr != 0L)
				{
					<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)(-1);
					return 1;
				}
			}
			<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)0;
			result = false;
		}
		else
		{
			result = (<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)(-1));
		}
		return result;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x0000AE78 File Offset: 0x0000A278
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool <CrtImplementationDetails>.DefaultDomain.HasNative()
	{
		bool result;
		if (<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)2)
		{
			for (void** ptr = (void**)(&<Module>.__xi_a); ptr < (void**)(&<Module>.__xi_z); ptr += 8L / (long)sizeof(void*))
			{
				if (*(long*)ptr != 0L)
				{
					<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)(-1);
					return 1;
				}
			}
			for (void** ptr = (void**)(&<Module>.__xc_a); ptr < (void**)(&<Module>.__xc_z); ptr += 8L / (long)sizeof(void*))
			{
				if (*(long*)ptr != 0L)
				{
					<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)(-1);
					return 1;
				}
			}
			<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)0;
			result = false;
		}
		else
		{
			result = (<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)(-1));
		}
		return result;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x0000AEF0 File Offset: 0x0000A2F0
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsInitialization()
	{
		int result;
		if ((<Module>.<CrtImplementationDetails>.DefaultDomain.HasPerProcess() == null || <Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA) && (<Module>.<CrtImplementationDetails>.DefaultDomain.HasNative() == null || <Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA || <Module>.__scrt_current_native_startup_state != (__scrt_native_startup_state)0))
		{
			result = 0;
		}
		else
		{
			result = 1;
		}
		return result;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x0000AF2C File Offset: 0x0000A32C
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsUninitialization()
	{
		return <Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x0000AF40 File Offset: 0x0000A340
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.DefaultDomain.Initialize()
	{
		<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
	}

	// Token: 0x06000013 RID: 19 RVA: 0x000014EC File Offset: 0x000008EC
	internal static void ??__E?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00001500 File Offset: 0x00000900
	internal static void ??__E?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00001514 File Offset: 0x00000914
	internal static void ??__E?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA@@YMXXZ()
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = false;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00001528 File Offset: 0x00000928
	internal static void ??__E?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x0000153C File Offset: 0x0000093C
	internal static void ??__E?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00001550 File Offset: 0x00000950
	internal static void ??__E?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00001564 File Offset: 0x00000964
	internal static void ??__E?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x0000B148 File Offset: 0x0000A548
	[DebuggerStepThrough]
	[SecuritySafeCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeVtables(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during vtable initialization.\n");
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initterm_m((method*)(&<Module>.__xi_vt_a), (method*)(&<Module>.__xi_vt_z));
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x0000B17C File Offset: 0x0000A57C
	[SecuritySafeCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load while attempting to initialize the default appdomain.\n");
		<Module>.<CrtImplementationDetails>.DefaultDomain.Initialize();
	}

	// Token: 0x0600001C RID: 28 RVA: 0x0000B19C File Offset: 0x0000A59C
	[SecuritySafeCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeNative(LanguageSupport* A_0)
	{
		Debug.Assert(<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA, "Native globals must be initialized in the default domain");
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during native initialization.\n");
		<Module>.__security_init_cookie();
		<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
		if (<Module>.__scrt_is_safe_for_managed_code() == null)
		{
			<Module>.abort();
		}
		if (<Module>.__scrt_current_native_startup_state == (__scrt_native_startup_state)1)
		{
			<Module>.abort();
		}
		else if (<Module>.__scrt_current_native_startup_state == (__scrt_native_startup_state)0)
		{
			<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
			<Module>.__scrt_current_native_startup_state = (__scrt_native_startup_state)1;
			if (<Module>._initterm_e((method*)(&<Module>.__xi_a), (method*)(&<Module>.__xi_z)) != 0)
			{
				<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0));
			}
			<Module>._initterm((method*)(&<Module>.__xc_a), (method*)(&<Module>.__xc_z));
			<Module>.__scrt_current_native_startup_state = (__scrt_native_startup_state)2;
			<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
			<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
		}
	}

	// Token: 0x0600001D RID: 29 RVA: 0x0000B23C File Offset: 0x0000A63C
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerProcess(LanguageSupport* A_0)
	{
		Debug.Assert(<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA, "Per-process globals must be initialized in the default domain");
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during process initialization.\n");
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initatexit_m();
		<Module>._initterm_m((method*)(&<Module>.__xc_mp_a), (method*)(&<Module>.__xc_mp_z));
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
		<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x0000B28C File Offset: 0x0000A68C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during appdomain initialization.\n");
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initatexit_app_domain();
		<Module>._initterm_m((method*)(&<Module>.__xc_ma_a), (method*)(&<Module>.__xc_ma_z));
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x0000B2C8 File Offset: 0x0000A6C8
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during registration for the unload events.\n");
		EventHandler handler = new EventHandler(<Module>.<CrtImplementationDetails>.LanguageSupport.DomainUnload);
		<Module>.<CrtImplementationDetails>.RegisterModuleUninitializer(handler);
	}

	// Token: 0x06000020 RID: 32 RVA: 0x0000B2F8 File Offset: 0x0000A6F8
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport._Initialize(LanguageSupport* A_0)
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = AppDomain.CurrentDomain.IsDefaultAppDomain();
		if (<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA)
		{
			<Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
		}
		void* ptr = <Module>._getFiberPtrId();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			while (num2 == 0)
			{
				try
				{
				}
				finally
				{
					void* ptr2 = Interlocked.CompareExchange(ref <Module>.__scrt_native_startup_lock, ptr, 0L);
					if (ptr2 == null)
					{
						num2 = 1;
					}
					else if (ptr2 == ptr)
					{
						num = 1;
						num2 = 1;
					}
				}
				if (num2 == 0)
				{
					<Module>.Sleep(1000);
				}
			}
			<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeVtables(A_0);
			if (<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeNative(A_0);
				<Module>.<CrtImplementationDetails>.LanguageSupport.InitializePerProcess(A_0);
			}
			else if (<Module>.<CrtImplementationDetails>.DefaultDomain.NeedsInitialization() != null)
			{
				num3 = 1;
			}
		}
		finally
		{
			if (num == 0)
			{
				Interlocked.Exchange(ref <Module>.__scrt_native_startup_lock, 0L);
			}
		}
		if (num3 != 0)
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(A_0);
		}
		<Module>.<CrtImplementationDetails>.LanguageSupport.InitializePerAppDomain(A_0);
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 1;
		<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(A_0);
	}

	// Token: 0x06000021 RID: 33 RVA: 0x0000AF5C File Offset: 0x0000A35C
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain()
	{
		<Module>._app_exit_callback();
	}

	// Token: 0x06000022 RID: 34 RVA: 0x0000AF70 File Offset: 0x0000A370
	[SecurityCritical]
	internal unsafe static int <CrtImplementationDetails>.LanguageSupport._UninitializeDefaultDomain(void* cookie)
	{
		string message = "This function must be called in the default domain";
		byte condition = AppDomain.CurrentDomain.IsDefaultAppDomain() ? 1 : 0;
		Debug.Assert(condition != 0, message);
		<Module>._exit_callback();
		<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		if (<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA)
		{
			<Module>._cexit();
			<Module>.__scrt_current_native_startup_state = (__scrt_native_startup_state)0;
			<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		}
		<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		return 0;
	}

	// Token: 0x06000023 RID: 35 RVA: 0x0000AFC4 File Offset: 0x0000A3C4
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain()
	{
		if (<Module>.<CrtImplementationDetails>.DefaultDomain.NeedsUninitialization() != null)
		{
			if (AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport._UninitializeDefaultDomain(null);
			}
			else
			{
				<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
			}
		}
	}

	// Token: 0x06000024 RID: 36 RVA: 0x0000AFFC File Offset: 0x0000A3FC
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[PrePrepareMethod]
	internal static void <CrtImplementationDetails>.LanguageSupport.DomainUnload(object A_0, EventArgs A_1)
	{
		if (<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA != 0 && Interlocked.Exchange(ref <Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA, 1) == 0)
		{
			bool flag = Interlocked.Decrement(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA) == 0;
			<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain();
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain();
			}
		}
	}

	// Token: 0x06000025 RID: 37 RVA: 0x0000B3F0 File Offset: 0x0000A7F0
	[DebuggerStepThrough]
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.Cleanup(LanguageSupport* A_0, System.Exception innerException)
	{
		try
		{
			bool flag = Interlocked.Decrement(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA) == 0;
			<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain();
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain();
			}
		}
		catch (System.Exception nestedException)
		{
			<Module>.<CrtImplementationDetails>.ThrowNestedModuleLoadException(innerException, nestedException);
		}
		catch (object obj)
		{
			<Module>.<CrtImplementationDetails>.ThrowNestedModuleLoadException(innerException, null);
		}
	}

	// Token: 0x06000026 RID: 38 RVA: 0x0000B46C File Offset: 0x0000A86C
	[SecurityCritical]
	internal unsafe static LanguageSupport* <CrtImplementationDetails>.LanguageSupport.{ctor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{ctor}(A_0);
		return A_0;
	}

	// Token: 0x06000027 RID: 39 RVA: 0x0000B484 File Offset: 0x0000A884
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.{dtor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{dtor}(A_0);
	}

	// Token: 0x06000028 RID: 40 RVA: 0x0000B498 File Offset: 0x0000A898
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.Initialize(LanguageSupport* A_0)
	{
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load.\n");
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				Interlocked.Increment(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA);
				flag = true;
			}
			<Module>.<CrtImplementationDetails>.LanguageSupport._Initialize(A_0);
		}
		catch (System.Exception innerException)
		{
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.Cleanup(A_0, innerException);
			}
			<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0), innerException);
		}
		catch (object obj)
		{
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.Cleanup(A_0, null);
			}
			<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0), null);
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x0000B558 File Offset: 0x0000A958
	[DebuggerStepThrough]
	[SecurityCritical]
	static unsafe <Module>()
	{
		LanguageSupport languageSupport;
		<Module>.<CrtImplementationDetails>.LanguageSupport.{ctor}(ref languageSupport);
		try
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.Initialize(ref languageSupport);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(<CrtImplementationDetails>.LanguageSupport.{dtor}), (void*)(&languageSupport));
			throw;
		}
		<Module>.<CrtImplementationDetails>.LanguageSupport.{dtor}(ref languageSupport);
	}

	// Token: 0x0600002A RID: 42 RVA: 0x0000B03C File Offset: 0x0000A43C
	[SecuritySafeCritical]
	internal unsafe static string PE$AAVString@System@@(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr value = new IntPtr(*A_0);
		return ((GCHandle)value).Target;
	}

	// Token: 0x0600002B RID: 43 RVA: 0x0000B064 File Offset: 0x0000A464
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static gcroot<System::String\u0020^>* =(gcroot<System::String\u0020^>* A_0, string t)
	{
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Target = t;
		return A_0;
	}

	// Token: 0x0600002C RID: 44 RVA: 0x0000B08C File Offset: 0x0000A48C
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void {dtor}(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Free();
		*A_0 = 0L;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x0000B0B4 File Offset: 0x0000A4B4
	[SecuritySafeCritical]
	[DebuggerStepThrough]
	internal unsafe static gcroot<System::String\u0020^>* {ctor}(gcroot<System::String\u0020^>* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x0000B5E0 File Offset: 0x0000A9E0
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static ValueType <CrtImplementationDetails>.AtExitLock._handle()
	{
		ValueType result;
		if (<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA != null)
		{
			IntPtr value = new IntPtr(<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA);
			result = GCHandle.FromIntPtr(value);
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x0600002F RID: 47 RVA: 0x0000BB80 File Offset: 0x0000AF80
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Construct(object value)
	{
		<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		<Module>.<CrtImplementationDetails>.AtExitLock._lock_Set(value);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x0000B614 File Offset: 0x0000AA14
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Set(object value)
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType == null)
		{
			valueType = GCHandle.Alloc(value);
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = GCHandle.ToIntPtr((GCHandle)valueType).ToPointer();
		}
		else
		{
			((GCHandle)valueType).Target = value;
		}
	}

	// Token: 0x06000031 RID: 49 RVA: 0x0000B664 File Offset: 0x0000AA64
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static object <CrtImplementationDetails>.AtExitLock._lock_Get()
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		object result;
		if (valueType != null)
		{
			result = ((GCHandle)valueType).Target;
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x0000B690 File Offset: 0x0000AA90
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Destruct()
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType != null)
		{
			((GCHandle)valueType).Free();
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		}
	}

	// Token: 0x06000033 RID: 51 RVA: 0x0000B6BC File Offset: 0x0000AABC
	[DebuggerStepThrough]
	[SecurityCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.AtExitLock.IsInitialized()
	{
		return (<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() == null) ? 0 : 1;
	}

	// Token: 0x06000034 RID: 52 RVA: 0x0000BB9C File Offset: 0x0000AF9C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock.AddRef()
	{
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized() == null)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Construct(new object());
			<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA = 0;
		}
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA++;
	}

	// Token: 0x06000035 RID: 53 RVA: 0x0000B6D8 File Offset: 0x0000AAD8
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.RemoveRef()
	{
		string message = "Reference count must be greater than zero";
		byte condition;
		if (<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA > 0)
		{
			condition = 1;
		}
		else
		{
			condition = 0;
		}
		Debug.Assert(condition != 0, message);
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA--;
		if (<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA == 0)
		{
			string message2 = "Lock must be initialized";
			byte condition2 = <Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized();
			Debug.Assert(condition2 != 0, message2);
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Destruct();
		}
	}

	// Token: 0x06000036 RID: 54 RVA: 0x0000B72C File Offset: 0x0000AB2C
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.Enter()
	{
		Monitor.Enter(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
	}

	// Token: 0x06000037 RID: 55 RVA: 0x0000B744 File Offset: 0x0000AB44
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.Exit()
	{
		Monitor.Exit(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
	}

	// Token: 0x06000038 RID: 56 RVA: 0x0000B75C File Offset: 0x0000AB5C
	[SecurityCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __global_lock()
	{
		bool result = false;
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized() != null)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock.Enter();
			result = true;
		}
		return result;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x0000B77C File Offset: 0x0000AB7C
	[DebuggerStepThrough]
	[SecurityCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __global_unlock()
	{
		bool result = false;
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized() != null)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock.Exit();
			result = true;
		}
		return result;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x0000BBCC File Offset: 0x0000AFCC
	[SecurityCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __alloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.AddRef();
		return <Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized();
	}

	// Token: 0x0600003B RID: 59 RVA: 0x0000B79C File Offset: 0x0000AB9C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void __dealloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.RemoveRef();
	}

	// Token: 0x0600003C RID: 60 RVA: 0x0000B7B0 File Offset: 0x0000ABB0
	[SecurityCritical]
	internal unsafe static int _atexit_helper(method func, ulong* __pexit_list_size, method** __ponexitend_e, method** __ponexitbegin_e)
	{
		method system.Void_u0020() = 0L;
		int result;
		if (func == null)
		{
			result = -1;
		}
		else
		{
			if (<Module>.?A0xdf32f105.__global_lock() == 1)
			{
				try
				{
					method* ptr = (method*)<Module>.DecodePointer(*(long*)__ponexitbegin_e);
					method* ptr2 = (method*)<Module>.DecodePointer(*(long*)__ponexitend_e);
					if (*__pexit_list_size - 1UL < (ulong)((ptr2 - ptr) / 8))
					{
						try
						{
							ulong num;
							if (*__pexit_list_size * 8UL < 4096UL)
							{
								num = *__pexit_list_size * 8UL;
							}
							else
							{
								num = 4096UL;
							}
							IntPtr cb = new IntPtr((int)(*__pexit_list_size * 8UL + num));
							IntPtr pv = new IntPtr((void*)ptr);
							IntPtr intPtr = Marshal.ReAllocHGlobal(pv, cb);
							ptr2 = (byte*)intPtr.ToPointer() + ptr2 - ptr;
							ptr = (method*)intPtr.ToPointer();
							ulong num2;
							if (512UL < *__pexit_list_size)
							{
								num2 = 512UL;
							}
							else
							{
								num2 = *__pexit_list_size;
							}
							*__pexit_list_size += num2;
						}
						catch (OutOfMemoryException)
						{
							IntPtr cb2 = new IntPtr((int)(*__pexit_list_size * 8UL + 12UL));
							IntPtr pv2 = new IntPtr((void*)ptr);
							IntPtr intPtr2 = Marshal.ReAllocHGlobal(pv2, cb2);
							ptr2 = (byte*)intPtr2.ToPointer() + ptr2 - ptr;
							ptr = (method*)intPtr2.ToPointer();
							*__pexit_list_size += 4UL;
						}
					}
					*(long*)ptr2 = func;
					ptr2 += 8L / (long)sizeof(method);
					system.Void_u0020() = func;
					*(long*)__ponexitbegin_e = <Module>.EncodePointer((void*)ptr);
					*(long*)__ponexitend_e = <Module>.EncodePointer((void*)ptr2);
				}
				catch (OutOfMemoryException)
				{
				}
				finally
				{
					<Module>.?A0xdf32f105.__global_unlock();
				}
			}
			result = ((system.Void_u0020() != null) ? 0 : -1);
		}
		return result;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x0000B930 File Offset: 0x0000AD30
	[SecurityCritical]
	internal unsafe static void _exit_callback()
	{
		string message = "This function must be called in the default domain";
		byte condition = AppDomain.CurrentDomain.IsDefaultAppDomain() ? 1 : 0;
		Debug.Assert(condition != 0, message);
		if (<Module>.?A0xdf32f105.__exit_list_size != 0UL)
		{
			method* ptr = (method*)<Module>.DecodePointer((void*)<Module>.?A0xdf32f105.__onexitbegin_m);
			method* ptr2 = (method*)<Module>.DecodePointer((void*)<Module>.?A0xdf32f105.__onexitend_m);
			if (ptr != -1L && ptr != null && ptr2 != null)
			{
				method* ptr3 = ptr;
				method* ptr4 = ptr2;
				for (;;)
				{
					do
					{
						ptr2 -= 8L / (long)sizeof(method);
					}
					while (ptr2 >= ptr && *(long*)ptr2 == <Module>.EncodePointer(null));
					if (ptr2 < ptr)
					{
						break;
					}
					method system.Void_u0020() = <Module>.DecodePointer(*(long*)ptr2);
					*(long*)ptr2 = <Module>.EncodePointer(null);
					calli(System.Void(), system.Void_u0020());
					method* ptr5 = (method*)<Module>.DecodePointer((void*)<Module>.?A0xdf32f105.__onexitbegin_m);
					method* ptr6 = (method*)<Module>.DecodePointer((void*)<Module>.?A0xdf32f105.__onexitend_m);
					if (ptr3 != ptr5 || ptr4 != ptr6)
					{
						ptr3 = ptr5;
						ptr = ptr3;
						ptr4 = ptr6;
						ptr2 = ptr4;
					}
				}
				IntPtr hglobal = new IntPtr((void*)ptr);
				Marshal.FreeHGlobal(hglobal);
			}
			<Module>.?A0xdf32f105.__dealloc_global_lock();
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x0000BBE8 File Offset: 0x0000AFE8
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static int _initatexit_m()
	{
		string message = "This function must be called in the default domain";
		byte condition = AppDomain.CurrentDomain.IsDefaultAppDomain() ? 1 : 0;
		Debug.Assert(condition != 0, message);
		int result = 0;
		if (<Module>.?A0xdf32f105.__alloc_global_lock() == 1)
		{
			<Module>.?A0xdf32f105.__onexitbegin_m = (method*)<Module>.EncodePointer(Marshal.AllocHGlobal(256).ToPointer());
			<Module>.?A0xdf32f105.__onexitend_m = <Module>.?A0xdf32f105.__onexitbegin_m;
			<Module>.?A0xdf32f105.__exit_list_size = 32UL;
			result = 1;
		}
		return result;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x0000BC4C File Offset: 0x0000B04C
	internal static method _onexit_m(method _Function)
	{
		string message = "This function must be called in the default domain";
		byte condition = AppDomain.CurrentDomain.IsDefaultAppDomain() ? 1 : 0;
		Debug.Assert(condition != 0, message);
		return (<Module>._atexit_m(_Function) != -1) ? _Function : 0L;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x0000BA20 File Offset: 0x0000AE20
	[SecurityCritical]
	internal unsafe static int _atexit_m(method func)
	{
		string message = "This function must be called in the default domain";
		byte condition = AppDomain.CurrentDomain.IsDefaultAppDomain() ? 1 : 0;
		Debug.Assert(condition != 0, message);
		return <Module>._atexit_helper(<Module>.EncodePointer(func), &<Module>.?A0xdf32f105.__exit_list_size, &<Module>.?A0xdf32f105.__onexitend_m, &<Module>.?A0xdf32f105.__onexitbegin_m);
	}

	// Token: 0x06000041 RID: 65 RVA: 0x0000BC84 File Offset: 0x0000B084
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static int _initatexit_app_domain()
	{
		if (<Module>.?A0xdf32f105.__alloc_global_lock() == 1)
		{
			<Module>.__onexitbegin_app_domain = (method*)<Module>.EncodePointer(Marshal.AllocHGlobal(256).ToPointer());
			<Module>.__onexitend_app_domain = <Module>.__onexitbegin_app_domain;
			<Module>.__exit_list_size_app_domain = 32UL;
		}
		return 1;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x0000BA64 File Offset: 0x0000AE64
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
	internal unsafe static void _app_exit_callback()
	{
		if (<Module>.__exit_list_size_app_domain != 0UL)
		{
			method* ptr = (method*)<Module>.DecodePointer((void*)<Module>.__onexitbegin_app_domain);
			method* ptr2 = (method*)<Module>.DecodePointer((void*)<Module>.__onexitend_app_domain);
			try
			{
				if (ptr != -1L && ptr != null && ptr2 != null)
				{
					method* ptr3 = ptr;
					method* ptr4 = ptr2;
					for (;;)
					{
						do
						{
							ptr2 -= 8L / (long)sizeof(method);
						}
						while (ptr2 >= ptr && *(long*)ptr2 == <Module>.EncodePointer(null));
						if (ptr2 < ptr)
						{
							break;
						}
						method system.Void_u0020() = <Module>.DecodePointer(*(long*)ptr2);
						*(long*)ptr2 = <Module>.EncodePointer(null);
						calli(System.Void(), system.Void_u0020());
						method* ptr5 = (method*)<Module>.DecodePointer((void*)<Module>.__onexitbegin_app_domain);
						method* ptr6 = (method*)<Module>.DecodePointer((void*)<Module>.__onexitend_app_domain);
						if (ptr3 != ptr5 || ptr4 != ptr6)
						{
							ptr3 = ptr5;
							ptr = ptr3;
							ptr4 = ptr6;
							ptr2 = ptr4;
						}
					}
				}
			}
			finally
			{
				IntPtr hglobal = new IntPtr((void*)ptr);
				Marshal.FreeHGlobal(hglobal);
				<Module>.?A0xdf32f105.__dealloc_global_lock();
			}
		}
	}

	// Token: 0x06000043 RID: 67 RVA: 0x0000BCCC File Offset: 0x0000B0CC
	[SecurityCritical]
	internal static method _onexit_m_appdomain(method _Function)
	{
		return (<Module>._atexit_m_appdomain(_Function) != -1) ? _Function : 0L;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x0000BB54 File Offset: 0x0000AF54
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static int _atexit_m_appdomain(method func)
	{
		return <Module>._atexit_helper(<Module>.EncodePointer(func), &<Module>.__exit_list_size_app_domain, &<Module>.__onexitend_app_domain, &<Module>.__onexitbegin_app_domain);
	}

	// Token: 0x06000045 RID: 69
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* DecodePointer(void* _Ptr);

	// Token: 0x06000046 RID: 70
	[SuppressUnmanagedCodeSecurity]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* EncodePointer(void* _Ptr);

	// Token: 0x06000047 RID: 71 RVA: 0x0000BD00 File Offset: 0x0000B100
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static int _initterm_e(method* pfbegin, method* pfend)
	{
		int num = 0;
		while (pfbegin < pfend && num == 0)
		{
			if (*(long*)pfbegin != 0L)
			{
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(), *(long*)pfbegin);
			}
			pfbegin += 8L / (long)sizeof(method);
		}
		return num;
	}

	// Token: 0x06000048 RID: 72 RVA: 0x0000BD30 File Offset: 0x0000B130
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void _initterm(method* pfbegin, method* pfend)
	{
		while (pfbegin < pfend)
		{
			if (*(long*)pfbegin != 0L)
			{
				calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(), *(long*)pfbegin);
			}
			pfbegin += 8L / (long)sizeof(method);
		}
	}

	// Token: 0x06000049 RID: 73 RVA: 0x0000BD54 File Offset: 0x0000B154
	[DebuggerStepThrough]
	internal static ModuleHandle <CrtImplementationDetails>.ThisModule.Handle()
	{
		return typeof(ThisModule).Module.ModuleHandle;
	}

	// Token: 0x0600004A RID: 74 RVA: 0x0000BDA8 File Offset: 0x0000B1A8
	[SecurityCritical]
	[DebuggerStepThrough]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void _initterm_m(method* pfbegin, method* pfend)
	{
		while (pfbegin < pfend)
		{
			if (*(long*)pfbegin != 0L)
			{
				method system.Void_u0020modopt(System.Runtime.CompilerServices.IsConst)*_u0020() = <Module>.<CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(*(long*)pfbegin);
				object obj = calli(System.Void modopt(System.Runtime.CompilerServices.IsConst)*(), system.Void_u0020modopt(System.Runtime.CompilerServices.IsConst)*_u0020());
			}
			pfbegin += 8L / (long)sizeof(method);
		}
	}

	// Token: 0x0600004B RID: 75 RVA: 0x0000BD78 File Offset: 0x0000B178
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static method <CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(method methodToken)
	{
		return <Module>.<CrtImplementationDetails>.ThisModule.Handle().ResolveMethodHandle(methodToken).GetFunctionPointer().ToPointer();
	}

	// Token: 0x0600004C RID: 76 RVA: 0x0000BDE8 File Offset: 0x0000B1E8
	[HandleProcessCorruptedStateExceptions]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void ___CxxCallUnwindDtor(method pDtor, void* pThis)
	{
		try
		{
			calli(System.Void(System.Void*), pThis, pDtor);
		}
		catch when (endfilter(<Module>.__FrameUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x0600004D RID: 77 RVA: 0x0000BE2C File Offset: 0x0000B22C
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void ___CxxCallUnwindDelDtor(method pDtor, void* pThis)
	{
		try
		{
			calli(System.Void(System.Void*), pThis, pDtor);
		}
		catch when (endfilter(<Module>.__FrameUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x0600004E RID: 78 RVA: 0x0000BE70 File Offset: 0x0000B270
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void ___CxxCallUnwindVecDtor(method pVecDtor, void* ptr, ulong size, int count, method pDtor)
	{
		try
		{
			calli(System.Void(System.Void*,System.UInt64,System.Int32,System.Void (System.Void*)), ptr, size, count, pDtor, pVecDtor);
		}
		catch when (endfilter(<Module>.__FrameUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x0600004F RID: 79 RVA: 0x000060A0 File Offset: 0x000054A0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern int IsValidApp(int, void*);

	// Token: 0x06000050 RID: 80 RVA: 0x00006280 File Offset: 0x00005680
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern int ModifyApp(int, void*, [MarshalAs(UnmanagedType.U1)] bool, [MarshalAs(UnmanagedType.U1)] bool);

	// Token: 0x06000051 RID: 81 RVA: 0x00005E30 File Offset: 0x00005230
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal static extern int EnableDebugPriv();

	// Token: 0x06000052 RID: 82 RVA: 0x00006250 File Offset: 0x00005650
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal static extern int LaunchApp();

	// Token: 0x06000053 RID: 83 RVA: 0x0000ADF0 File Offset: 0x0000A1F0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void* _getFiberPtrId();

	// Token: 0x06000054 RID: 84 RVA: 0x0000BF90 File Offset: 0x0000B390
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void _cexit();

	// Token: 0x06000055 RID: 85 RVA: 0x0000C0D3 File Offset: 0x0000B4D3
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void Sleep(uint);

	// Token: 0x06000056 RID: 86 RVA: 0x0000C0DF File Offset: 0x0000B4DF
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void abort();

	// Token: 0x06000057 RID: 87 RVA: 0x000096E0 File Offset: 0x00008AE0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal static extern void __security_init_cookie();

	// Token: 0x06000058 RID: 88 RVA: 0x0000C0D9 File Offset: 0x0000B4D9
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern int __FrameUnwindFilter(_EXCEPTION_POINTERS*);

	// Token: 0x04000001 RID: 1 RVA: 0x0002684C File Offset: 0x0002484C
	internal static int __@@_PchSym_@00@UfhvihUlknxmUhlfixvUivklhUnxvmgvihUivhlfixvhUcGEUwvyftUkilxvhhOlyq@4B2008FD98C1DD4;

	// Token: 0x04000002 RID: 2 RVA: 0x00024028 File Offset: 0x00022028
	public static method __m2mep@?AddLog@@$$FYAXPEB_W@Z;

	// Token: 0x04000003 RID: 3 RVA: 0x00024038 File Offset: 0x00022038
	public static method __m2mep@?AddError@@$$FYAXPEB_W@Z;

	// Token: 0x04000004 RID: 4 RVA: 0x000157F8 File Offset: 0x000137F8
	internal static __s_GUID _GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x04000005 RID: 5 RVA: 0x000157E8 File Offset: 0x000137E8
	internal static __s_GUID _GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x04000006 RID: 6
	[FixedAddressValueType]
	internal static int ?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x04000007 RID: 7 RVA: 0x0000E4D8 File Offset: 0x0000C4D8
	internal static method ?Uninitialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000008 RID: 8
	[FixedAddressValueType]
	internal static Progress ?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000009 RID: 9 RVA: 0x0000E4F0 File Offset: 0x0000C4F0
	internal static method ?InitializedNative$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400000A RID: 10 RVA: 0x00015808 File Offset: 0x00013808
	internal static __s_GUID _GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x0400000B RID: 11 RVA: 0x00015818 File Offset: 0x00013818
	internal static __s_GUID _GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x0400000C RID: 12
	[FixedAddressValueType]
	internal static Progress ?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x0400000D RID: 13 RVA: 0x00026ECC File Offset: 0x00024ECC
	internal static bool ?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x0400000E RID: 14 RVA: 0x00024094 File Offset: 0x00022094
	internal static TriBool ?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A;

	// Token: 0x0400000F RID: 15 RVA: 0x00026ECF File Offset: 0x00024ECF
	internal static bool ?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000010 RID: 16 RVA: 0x00026EC8 File Offset: 0x00024EC8
	internal static int ?Count@AllDomains@<CrtImplementationDetails>@@2HA;

	// Token: 0x04000011 RID: 17
	[FixedAddressValueType]
	internal static int ?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x04000012 RID: 18 RVA: 0x00026ECE File Offset: 0x00024ECE
	internal static bool ?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000013 RID: 19
	[FixedAddressValueType]
	internal static bool ?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA;

	// Token: 0x04000014 RID: 20
	[FixedAddressValueType]
	internal static Progress ?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000015 RID: 21 RVA: 0x00026ECD File Offset: 0x00024ECD
	internal static bool ?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000016 RID: 22
	[FixedAddressValueType]
	internal static Progress ?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000017 RID: 23 RVA: 0x00024090 File Offset: 0x00022090
	internal static TriBool ?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A;

	// Token: 0x04000018 RID: 24 RVA: 0x0000E518 File Offset: 0x0000C518
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_z;

	// Token: 0x04000019 RID: 25 RVA: 0x0000E528 File Offset: 0x0000C528
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_z;

	// Token: 0x0400001A RID: 26 RVA: 0x0000E4F8 File Offset: 0x0000C4F8
	internal static method ?InitializedPerProcess$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400001B RID: 27 RVA: 0x0000E4C8 File Offset: 0x0000C4C8
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_a;

	// Token: 0x0400001C RID: 28 RVA: 0x0000E508 File Offset: 0x0000C508
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_z;

	// Token: 0x0400001D RID: 29 RVA: 0x0000E500 File Offset: 0x0000C500
	internal static method ?InitializedPerAppDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400001E RID: 30 RVA: 0x0000E520 File Offset: 0x0000C520
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_a;

	// Token: 0x0400001F RID: 31 RVA: 0x0000E4D0 File Offset: 0x0000C4D0
	internal static method ?Initialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000020 RID: 32 RVA: 0x0000E510 File Offset: 0x0000C510
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_a;

	// Token: 0x04000021 RID: 33 RVA: 0x0000E4E8 File Offset: 0x0000C4E8
	internal static method ?InitializedVtables$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000022 RID: 34 RVA: 0x0000E4E0 File Offset: 0x0000C4E0
	internal static method ?IsDefaultDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000023 RID: 35 RVA: 0x00024218 File Offset: 0x00022218
	public static method __m2mep@?ThrowNestedModuleLoadException@<CrtImplementationDetails>@@$$FYMXPE$AAVException@System@@0@Z;

	// Token: 0x04000024 RID: 36 RVA: 0x000240A8 File Offset: 0x000220A8
	public static method __m2mep@?ThrowModuleLoadException@<CrtImplementationDetails>@@$$FYMXPE$AAVString@System@@@Z;

	// Token: 0x04000025 RID: 37 RVA: 0x000240B8 File Offset: 0x000220B8
	public static method __m2mep@?ThrowModuleLoadException@<CrtImplementationDetails>@@$$FYMXPE$AAVString@System@@PE$AAVException@3@@Z;

	// Token: 0x04000026 RID: 38 RVA: 0x000240C8 File Offset: 0x000220C8
	public static method __m2mep@?RegisterModuleUninitializer@<CrtImplementationDetails>@@$$FYMXPE$AAVEventHandler@System@@@Z;

	// Token: 0x04000027 RID: 39 RVA: 0x000240D8 File Offset: 0x000220D8
	public static method __m2mep@?FromGUID@<CrtImplementationDetails>@@$$FYM?AVGuid@System@@AEBU_GUID@@@Z;

	// Token: 0x04000028 RID: 40 RVA: 0x000240E8 File Offset: 0x000220E8
	public static method __m2mep@?__get_default_appdomain@@$$FYAJPEAPEAUIUnknown@@@Z;

	// Token: 0x04000029 RID: 41 RVA: 0x000240F8 File Offset: 0x000220F8
	public static method __m2mep@?__release_appdomain@@$$FYAXPEAUIUnknown@@@Z;

	// Token: 0x0400002A RID: 42 RVA: 0x00024108 File Offset: 0x00022108
	public static method __m2mep@?GetDefaultDomain@<CrtImplementationDetails>@@$$FYMPE$AAVAppDomain@System@@XZ;

	// Token: 0x0400002B RID: 43 RVA: 0x00024118 File Offset: 0x00022118
	public static method __m2mep@?DoCallBackInDefaultDomain@<CrtImplementationDetails>@@$$FYAXP6AJPEAX@Z0@Z;

	// Token: 0x0400002C RID: 44 RVA: 0x00024128 File Offset: 0x00022128
	public static method __m2mep@?__scrt_is_safe_for_managed_code@@$$FYA_NXZ;

	// Token: 0x0400002D RID: 45 RVA: 0x00024138 File Offset: 0x00022138
	public static method __m2mep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x0400002E RID: 46 RVA: 0x00024148 File Offset: 0x00022148
	public static method __m2mep@?HasPerProcess@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x0400002F RID: 47 RVA: 0x00024158 File Offset: 0x00022158
	public static method __m2mep@?HasNative@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000030 RID: 48 RVA: 0x00024168 File Offset: 0x00022168
	public static method __m2mep@?NeedsInitialization@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000031 RID: 49 RVA: 0x00024178 File Offset: 0x00022178
	public static method __m2mep@?NeedsUninitialization@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000032 RID: 50 RVA: 0x00024188 File Offset: 0x00022188
	public static method __m2mep@?Initialize@DefaultDomain@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x04000033 RID: 51 RVA: 0x00024228 File Offset: 0x00022228
	public static method __m2mep@?InitializeVtables@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000034 RID: 52 RVA: 0x00024238 File Offset: 0x00022238
	public static method __m2mep@?InitializeDefaultAppDomain@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000035 RID: 53 RVA: 0x00024248 File Offset: 0x00022248
	public static method __m2mep@?InitializeNative@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000036 RID: 54 RVA: 0x00024258 File Offset: 0x00022258
	public static method __m2mep@?InitializePerProcess@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000037 RID: 55 RVA: 0x00024268 File Offset: 0x00022268
	public static method __m2mep@?InitializePerAppDomain@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000038 RID: 56 RVA: 0x00024278 File Offset: 0x00022278
	public static method __m2mep@?InitializeUninitializer@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000039 RID: 57 RVA: 0x00024288 File Offset: 0x00022288
	public static method __m2mep@?_Initialize@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x0400003A RID: 58 RVA: 0x00024198 File Offset: 0x00022198
	public static method __m2mep@?UninitializeAppDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAXXZ;

	// Token: 0x0400003B RID: 59 RVA: 0x000241A8 File Offset: 0x000221A8
	public static method __m2mep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x0400003C RID: 60 RVA: 0x000241B8 File Offset: 0x000221B8
	public static method __m2mep@?UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAXXZ;

	// Token: 0x0400003D RID: 61 RVA: 0x000241C8 File Offset: 0x000221C8
	public static method __m2mep@?DomainUnload@LanguageSupport@<CrtImplementationDetails>@@$$FCMXPE$AAVObject@System@@PE$AAVEventArgs@4@@Z;

	// Token: 0x0400003E RID: 62 RVA: 0x00024298 File Offset: 0x00022298
	public static method __m2mep@?Cleanup@LanguageSupport@<CrtImplementationDetails>@@$$FAEAMXPE$AAVException@System@@@Z;

	// Token: 0x0400003F RID: 63 RVA: 0x000242A8 File Offset: 0x000222A8
	public static method __m2mep@??0LanguageSupport@<CrtImplementationDetails>@@$$FQEAA@XZ;

	// Token: 0x04000040 RID: 64 RVA: 0x000242B8 File Offset: 0x000222B8
	public static method __m2mep@??1LanguageSupport@<CrtImplementationDetails>@@$$FQEAA@XZ;

	// Token: 0x04000041 RID: 65 RVA: 0x000242C8 File Offset: 0x000222C8
	public static method __m2mep@?Initialize@LanguageSupport@<CrtImplementationDetails>@@$$FQEAAXXZ;

	// Token: 0x04000042 RID: 66 RVA: 0x00024098 File Offset: 0x00022098
	public static method cctor@@$$FYMXXZ;

	// Token: 0x04000043 RID: 67 RVA: 0x000241D8 File Offset: 0x000221D8
	public static method __m2mep@??B?$gcroot@PE$AAVString@System@@@@$$FQEBMPE$AAVString@System@@XZ;

	// Token: 0x04000044 RID: 68 RVA: 0x000241E8 File Offset: 0x000221E8
	public static method __m2mep@??4?$gcroot@PE$AAVString@System@@@@$$FQEAMAEAU0@PE$AAVString@System@@@Z;

	// Token: 0x04000045 RID: 69 RVA: 0x000241F8 File Offset: 0x000221F8
	public static method __m2mep@??1?$gcroot@PE$AAVString@System@@@@$$FQEAA@XZ;

	// Token: 0x04000046 RID: 70 RVA: 0x00024208 File Offset: 0x00022208
	public static method __m2mep@??0?$gcroot@PE$AAVString@System@@@@$$FQEAA@XZ;

	// Token: 0x04000047 RID: 71 RVA: 0x00015828 File Offset: 0x00013828
	public unsafe static int** __unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000048 RID: 72 RVA: 0x00015830 File Offset: 0x00013830
	public unsafe static int** __unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000049 RID: 73 RVA: 0x00027058 File Offset: 0x00025058
	internal unsafe static method* __onexitbegin_m;

	// Token: 0x0400004A RID: 74 RVA: 0x00027050 File Offset: 0x00025050
	internal static ulong __exit_list_size;

	// Token: 0x0400004B RID: 75
	[FixedAddressValueType]
	internal unsafe static method* __onexitend_app_domain;

	// Token: 0x0400004C RID: 76
	[FixedAddressValueType]
	internal unsafe static void* ?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA;

	// Token: 0x0400004D RID: 77
	[FixedAddressValueType]
	internal static int ?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA;

	// Token: 0x0400004E RID: 78 RVA: 0x00027060 File Offset: 0x00025060
	internal unsafe static method* __onexitend_m;

	// Token: 0x0400004F RID: 79
	[FixedAddressValueType]
	internal static ulong __exit_list_size_app_domain;

	// Token: 0x04000050 RID: 80
	[FixedAddressValueType]
	internal unsafe static method* __onexitbegin_app_domain;

	// Token: 0x04000051 RID: 81 RVA: 0x000243A0 File Offset: 0x000223A0
	public static method __m2mep@?_handle@AtExitLock@<CrtImplementationDetails>@@$$FCMPE$AA__ZVGCHandle@InteropServices@Runtime@System@@XZ;

	// Token: 0x04000052 RID: 82 RVA: 0x00024450 File Offset: 0x00022450
	public static method __m2mep@?_lock_Construct@AtExitLock@<CrtImplementationDetails>@@$$FCMXPE$AAVObject@System@@@Z;

	// Token: 0x04000053 RID: 83 RVA: 0x000243B0 File Offset: 0x000223B0
	public static method __m2mep@?_lock_Set@AtExitLock@<CrtImplementationDetails>@@$$FCMXPE$AAVObject@System@@@Z;

	// Token: 0x04000054 RID: 84 RVA: 0x000243C0 File Offset: 0x000223C0
	public static method __m2mep@?_lock_Get@AtExitLock@<CrtImplementationDetails>@@$$FCMPE$AAVObject@System@@XZ;

	// Token: 0x04000055 RID: 85 RVA: 0x000243D0 File Offset: 0x000223D0
	public static method __m2mep@?_lock_Destruct@AtExitLock@<CrtImplementationDetails>@@$$FCAXXZ;

	// Token: 0x04000056 RID: 86 RVA: 0x000243E0 File Offset: 0x000223E0
	public static method __m2mep@?IsInitialized@AtExitLock@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000057 RID: 87 RVA: 0x00024460 File Offset: 0x00022460
	public static method __m2mep@?AddRef@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x04000058 RID: 88 RVA: 0x000243F0 File Offset: 0x000223F0
	public static method __m2mep@?RemoveRef@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x04000059 RID: 89 RVA: 0x00024400 File Offset: 0x00022400
	public static method __m2mep@?Enter@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x0400005A RID: 90 RVA: 0x00024410 File Offset: 0x00022410
	public static method __m2mep@?Exit@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x0400005B RID: 91 RVA: 0x00024420 File Offset: 0x00022420
	public static method __m2mep@?__global_lock@?A0xdf32f105@@$$FYA_NXZ;

	// Token: 0x0400005C RID: 92 RVA: 0x00024430 File Offset: 0x00022430
	public static method __m2mep@?__global_unlock@?A0xdf32f105@@$$FYA_NXZ;

	// Token: 0x0400005D RID: 93 RVA: 0x00024470 File Offset: 0x00022470
	public static method __m2mep@?__alloc_global_lock@?A0xdf32f105@@$$FYA_NXZ;

	// Token: 0x0400005E RID: 94 RVA: 0x00024440 File Offset: 0x00022440
	public static method __m2mep@?__dealloc_global_lock@?A0xdf32f105@@$$FYAXXZ;

	// Token: 0x0400005F RID: 95 RVA: 0x00024310 File Offset: 0x00022310
	public static method __m2mep@?_atexit_helper@@$$J0YMHP6MXXZPEA_KPEAPEAP6MXXZ2@Z;

	// Token: 0x04000060 RID: 96 RVA: 0x00024320 File Offset: 0x00022320
	public static method __m2mep@?_exit_callback@@$$J0YMXXZ;

	// Token: 0x04000061 RID: 97 RVA: 0x00024360 File Offset: 0x00022360
	public static method __m2mep@?_initatexit_m@@$$J0YMHXZ;

	// Token: 0x04000062 RID: 98 RVA: 0x00024370 File Offset: 0x00022370
	public static method __m2mep@?_onexit_m@@$$J0YMP6MHXZP6MHXZ@Z;

	// Token: 0x04000063 RID: 99 RVA: 0x00024330 File Offset: 0x00022330
	public static method __m2mep@?_atexit_m@@$$J0YMHP6MXXZ@Z;

	// Token: 0x04000064 RID: 100 RVA: 0x00024380 File Offset: 0x00022380
	public static method __m2mep@?_initatexit_app_domain@@$$J0YMHXZ;

	// Token: 0x04000065 RID: 101 RVA: 0x00024340 File Offset: 0x00022340
	public static method __m2mep@?_app_exit_callback@@$$J0YMXXZ;

	// Token: 0x04000066 RID: 102 RVA: 0x00024390 File Offset: 0x00022390
	public static method __m2mep@?_onexit_m_appdomain@@$$J0YMP6MHXZP6MHXZ@Z;

	// Token: 0x04000067 RID: 103 RVA: 0x00024350 File Offset: 0x00022350
	public static method __m2mep@?_atexit_m_appdomain@@$$J0YMHP6MXXZ@Z;

	// Token: 0x04000068 RID: 104 RVA: 0x00024480 File Offset: 0x00022480
	public static method __m2mep@?_initterm_e@@$$FYMHPEAP6AHXZ0@Z;

	// Token: 0x04000069 RID: 105 RVA: 0x00024490 File Offset: 0x00022490
	public static method __m2mep@?_initterm@@$$FYMXPEAP6AXXZ0@Z;

	// Token: 0x0400006A RID: 106 RVA: 0x000244B0 File Offset: 0x000224B0
	public static method __m2mep@?Handle@ThisModule@<CrtImplementationDetails>@@$$FCM?AVModuleHandle@System@@XZ;

	// Token: 0x0400006B RID: 107 RVA: 0x000244A0 File Offset: 0x000224A0
	public static method __m2mep@?_initterm_m@@$$FYMXPEBQ6MPEBXXZ0@Z;

	// Token: 0x0400006C RID: 108 RVA: 0x000244C0 File Offset: 0x000224C0
	public static method __m2mep@??$ResolveMethod@$$A6MPEBXXZ@ThisModule@<CrtImplementationDetails>@@$$FSMP6MPEBXXZP6MPEBXXZ@Z;

	// Token: 0x0400006D RID: 109 RVA: 0x000244D0 File Offset: 0x000224D0
	public static method __m2mep@?___CxxCallUnwindDtor@@$$J0YMXP6MXPEAX@Z0@Z;

	// Token: 0x0400006E RID: 110 RVA: 0x000244E0 File Offset: 0x000224E0
	public static method __m2mep@?___CxxCallUnwindDelDtor@@$$J0YMXP6MXPEAX@Z0@Z;

	// Token: 0x0400006F RID: 111 RVA: 0x000244F0 File Offset: 0x000224F0
	public static method __m2mep@?___CxxCallUnwindVecDtor@@$$J0YMXP6MXPEAX_KHP6MX0@Z@Z01H2@Z;

	// Token: 0x04000070 RID: 112 RVA: 0x0000E4A0 File Offset: 0x0000C4A0
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_z;

	// Token: 0x04000071 RID: 113 RVA: 0x00026858 File Offset: 0x00024858
	internal static __scrt_native_startup_state __scrt_current_native_startup_state;

	// Token: 0x04000072 RID: 114 RVA: 0x00026860 File Offset: 0x00024860
	internal unsafe static void* __scrt_native_startup_lock;

	// Token: 0x04000073 RID: 115 RVA: 0x0000E3B8 File Offset: 0x0000C3B8
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_a;

	// Token: 0x04000074 RID: 116 RVA: 0x0000E498 File Offset: 0x0000C498
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_a;

	// Token: 0x04000075 RID: 117 RVA: 0x00024048 File Offset: 0x00022048
	internal static uint __scrt_native_dllmain_reason;

	// Token: 0x04000076 RID: 118 RVA: 0x0000E490 File Offset: 0x0000C490
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_z;
}
