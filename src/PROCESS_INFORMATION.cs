using System;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace ELess {
  internal static partial class NativeMethods {
    [StructLayout(LayoutKind.Sequential)]
    internal class PROCESS_INFORMATION : IDisposable {
      internal SafeProcessHandle hProcess = null;
      internal SafeThreadHandle  hThread  = null;
      internal UInt32 dwProcessId;
      internal UInt32 dwThreadId;

      protected virtual void Dispose(Boolean disposing) {
        if (disposing) {
          if (null != hProcess && !hProcess.IsInvalid) {
            hProcess.Close();
            hProcess = null;
          }

          if (null != hThread && !hThread.IsInvalid) {
            hThread.Close();
            hThread = null;
          }
        }
      }

      public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
      }
    }
  } // NativeMethods
}
