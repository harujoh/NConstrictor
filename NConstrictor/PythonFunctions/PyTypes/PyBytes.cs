using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;
using System.Security;

namespace NConstrictor
{
    public class PyBytes
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyBytes_Size", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern long Size(PyObject o);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyBytes_AsString", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr AsString(PyObject o);
    }
}