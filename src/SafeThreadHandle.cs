using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace ELess {
  [SuppressUnmanagedCodeSecurity]
  internal sealed class SafeThreadHandle : SafeHandleZeroOrMinusOneIsInvalid {
    internal SafeThreadHandle() : base(ownsHandle: true) {}
    internal void InitialSetHandle(IntPtr h) { SetHandle(h); }

    protected override Boolean ReleaseHandle() {
      return NativeMethods.CloseHandle(handle);
    }
  } // SafeThreadHandle
}
