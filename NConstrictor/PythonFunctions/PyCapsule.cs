using System;
using System.Runtime.InteropServices;
using System.Security;

namespace NConstrictor
{
    class PyCapsule
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyCapsule_GetPointer", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr GetPointer(PyObject capsule, string name);
    }
}
