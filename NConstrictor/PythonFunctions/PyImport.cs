using System;
using System.Runtime.InteropServices;

namespace NConstrictor
{
    public class PyImport
    {
        [DllImport(@"Python3.dll", EntryPoint = "PyImport_AddModule", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr AddModule(string name);

        [DllImport(@"Python3.dll", EntryPoint = "PyImport_Import", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr Import(IntPtr name);

        [DllImport(@"Python3.dll", EntryPoint = "PyImport_ImportModule", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr ImportModule(string name);

        public static IntPtr Import(string name)
        {
            IntPtr arg = PyUnicode.DecodeFSDefault(name);
            IntPtr result = Import(arg);
            Py.DecRef(arg);

            return result;
        }
    }
}
