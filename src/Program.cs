using System;
using System.Globalization;
using System.Security.Principal;

namespace ELess {
  internal sealed class Program {
    internal static String StrMsg(String msg, params Object[] list) {
      return String.Format(CultureInfo.InvariantCulture, msg, list);
    }

    static void Main() {
      Boolean elevated = false;
      String   comspec = null;
      using (WindowsIdentity wi = WindowsIdentity.GetCurrent()) {
        elevated = new WindowsPrincipal(wi).IsInRole(WindowsBuiltInRole.Administrator);
      }

      if (!elevated) {
        Console.WriteLine(StrMsg("You should be an administrator to take effect."));
        return;
      }

      comspec = Environment.GetEnvironmentVariable("comspec");
      if (String.IsNullOrEmpty(comspec)) {
        Console.WriteLine(StrMsg("It seems environment variable has not been set."));
        return;
      }

      NativeMethods.StartProcess(comspec);
    }
  } // Program
}
