using System;
using System.Runtime.InteropServices;

namespace NConstrictor
{
    class PyImport
    {
        [DllImport(@"Python3.dll", EntryPoint = "PyImport_AddModule", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr AddModule(string name);

        [DllImport(@"Python3.dll", EntryPoint = "PyImport_ImportModule", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr ImportModule(string name);
    }
}
