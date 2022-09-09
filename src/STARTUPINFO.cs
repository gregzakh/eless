using System;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace ELess {
  internal static partial class NativeMethods {
    [StructLayout(LayoutKind.Sequential)]
    internal class STARTUPINFO : IDisposable {
      internal UInt32 cb;
      internal IntPtr lpReserved = IntPtr.Zero;
      internal IntPtr lpDesktop  = IntPtr.Zero;
      internal IntPtr lpTitle    = IntPtr.Zero;
      internal UInt32 dwX;
      internal UInt32 dwY;
      internal UInt32 dwXSize;
      internal UInt32 dwYSize;
      internal UInt32 dwXCountChars;
      internal UInt32 dwYCountChars;
      internal UInt32 dwFillAttributes;
      internal UInt32 dwFlags;
      internal UInt16 wShowWindow;
      internal UInt16 cbReserved2;
      internal IntPtr lpReserved2 = IntPtr.Zero;
      internal SafeFileHandle hStdInput  = new SafeFileHandle(IntPtr.Zero, ownsHandle: false);
      internal SafeFileHandle hStdOutput = new SafeFileHandle(IntPtr.Zero, ownsHandle: false);
      internal SafeFileHandle hStdError  = new SafeFileHandle(IntPtr.Zero, ownsHandle: false);

      internal STARTUPINFO() {
        cb = (UInt32)Marshal.SizeOf((Object)this);
      }

      protected virtual void Dispose(Boolean disposing) {
        if (disposing) {
          if (null != hStdInput && !hStdInput.IsInvalid) {
            hStdInput.Close();
            hStdInput = null;
          }

          if (null != hStdOutput && !hStdOutput.IsInvalid) {
            hStdOutput.Close();
            hStdOutput = null;
          }

          if (null != hStdError && !hStdError.IsInvalid) {
            hStdError.Close();
            hStdError = null;
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
