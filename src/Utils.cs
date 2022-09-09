using System;
using System.IO;
using System.ComponentModel;

namespace ELess {
  internal static partial class NativeMethods {
    internal const UInt32 PROCESS_QUERY_INFORMATION = 0x400;
    internal const UInt32 TOKEN_DUPLICATE = 0x2;

    internal static void _CloseHandle(String msg, IntPtr hndl) {
      if (!CloseHandle(hndl)) Console.WriteLine(Program.StrMsg("[!] {0} in not closed.", msg));
    }

    internal static void StartProcess(String path) {
      IntPtr shell = IntPtr.Zero,
             shndl = IntPtr.Zero,
             token = IntPtr.Zero,
             duple = IntPtr.Zero;
      STARTUPINFO si = null;
      PROCESS_INFORMATION pi = null;

      try {
        if (IntPtr.Zero == (shell = GetShellWindow()))
          throw new Win32Exception("Cannot get shell's desktop window.");
        UInt32 pid = 0;
        if (0 == GetWindowThreadProcessId(shell, out pid))
          throw new Win32Exception("Cannot get ID of the shell process.");
        if (IntPtr.Zero == (shndl = OpenProcess(PROCESS_QUERY_INFORMATION, false, pid)))
          throw new Win32Exception();
        if (!OpenProcessToken(shndl, TOKEN_DUPLICATE, ref token))
          throw new Win32Exception();
        if (!DuplicateTokenEx(token, 0x18B, IntPtr.Zero,
            SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation, TOKEN_TYPE.TokenPrimary, out duple))
          throw new Win32Exception();
        si = new STARTUPINFO();
        pi = new PROCESS_INFORMATION();
        if (!CreateProcessWithToken(duple, 0, path, null, 0,
                                          IntPtr.Zero, Path.GetDirectoryName(path), out si, out pi))
          throw new Win32Exception();
      }
      catch (Win32Exception e) {
        Console.WriteLine(e.Message);
      }
      finally {
        if (null != pi) pi.Dispose();
        if (null != si) si.Dispose();

        if (IntPtr.Zero != duple) _CloseHandle("Duple handle", duple);
        if (IntPtr.Zero != token) _CloseHandle("Token handle", token);
        if (IntPtr.Zero != shndl) _CloseHandle("Process handle", shndl);
      }
    }
  } // NativeMethods
}
