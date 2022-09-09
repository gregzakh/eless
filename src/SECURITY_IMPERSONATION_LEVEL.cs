using System;

namespace ELess {
  internal static partial class NativeMethods {
    internal enum SECURITY_IMPERSONATION_LEVEL : uint {
      SecurityAnonymous,
      SecurityIdentification,
      SecurityImpersonation,
      SecurityDelegation
    }
  } // NativeMethods
}
