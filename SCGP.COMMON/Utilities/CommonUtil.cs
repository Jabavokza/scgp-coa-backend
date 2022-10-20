using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SCGP.COA.COMMON.Utilities
{
    public static class CommonUtil
    {
        public static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        public static bool IsLinux => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    }
}
