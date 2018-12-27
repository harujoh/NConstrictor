using System;
using System.Runtime.InteropServices;

namespace NConstrictor
{
    class PyModule
    {
        [DllImport(@"Python3.dll", EntryPoint = "PyModule_GetDict", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr GetDict(IntPtr module);
    }
}
