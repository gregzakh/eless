using System;

namespace ELess {
  internal static partial class NativeMethods {
    internal enum TOKEN_TYPE : uint {
      TokenPrimary = 1,
      TokenImpersonation
    }
  } // NativeMethods
}
