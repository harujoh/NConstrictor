using System;
using System.Runtime.InteropServices;

namespace NConstrictor
{
    class PyCapsule
    {
        [DllImport(@"Python3.dll", EntryPoint = "PyCapsule_GetPointer", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr GetPointer(IntPtr capsule, string name);
    }
}
