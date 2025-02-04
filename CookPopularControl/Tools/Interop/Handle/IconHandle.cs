﻿using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace CookPopularControl.Tools.Interop.Handle
{
    internal sealed class IconHandle : WpfSafeHandle
    {
        [SecurityCritical]
        private IconHandle() : base(true, CommonHandles.Icon)
        {
        }

        [SecurityCritical]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        protected override bool ReleaseHandle()
        {
            return InteropMethods.DestroyIcon(handle);
        }

        [SecurityCritical, SecuritySafeCritical]
        internal static IconHandle GetInvalidIcon()
        {
            return new IconHandle();
        }

        [SecurityCritical]
        internal IntPtr CriticalGetHandle()
        {
            return handle;
        }
    }
}
