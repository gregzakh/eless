using System;
using System.Runtime.InteropServices;

namespace ELess {
  internal static partial class NativeMethods {
    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern Boolean CreateProcessWithToken(
       IntPtr hToken,
       UInt32 dwLogonFlags,
       [MarshalAs(UnmanagedType.LPWStr)]String lpApplicationName,
       [MarshalAs(UnmanagedType.LPWStr)]String lpCommandLine,
       UInt32 dwCreationFlags,
       IntPtr lpEnvironment,
       [MarshalAs(UnmanagedType.LPWStr)]String lpCurrentDirectory,
       out STARTUPINFO lpStartupInfo,
       out PROCESS_INFORMATION lpProcessInformation
    );

    // api-ms-win-core-processthreads-l1-1-0
    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern IntPtr OpenProcess(
       UInt32 dwDesiredAccess,
       [MarshalAs(UnmanagedType.Bool)]Boolean bInheritHandle,
       UInt32 dwProcessId
    );

    [DllImport("advapi32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern Boolean OpenProcessToken(
       IntPtr hProcessHandle,
       UInt32 dwDesiredAccess,
       ref IntPtr hTokenHandle
    );

    // api-ms-win-core-handle-l1-1-0
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern Boolean CloseHandle(
       IntPtr handle
    );

    // api-ms-win-security-base-l1-1-0
    [DllImport("advapi32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern Boolean DuplicateTokenEx(
       IntPtr hExistingToken,
       UInt32 dwDesiredAccess,
       IntPtr lpTokenAttributes,
       SECURITY_IMPERSONATION_LEVEL dwImpersonationLevel,
       TOKEN_TYPE dwtokenType,
       out IntPtr hNewToken
    );

    // api-ms-win-ntuser-window-l1-1-0
    [DllImport("user32.dll")]
    internal static extern IntPtr GetShellWindow();

    [DllImport("user32.dll")]
    internal static extern UInt32 GetWindowThreadProcessId(
       IntPtr hWnd,
       out UInt32 lpdwProcess
    );
  } // NativeMethods
}
