using System.Runtime.InteropServices;
using System.Security;

namespace NConstrictor
{
    public class PyImport
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyImport_AddModule", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject AddModule(string name);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyImport_Import", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject Import(PyObject name);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyImport_ImportModule", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject ImportModule(string name);

        public static PyObject Import(string name)
        {
            return Import(PyUnicode.DecodeFSDefault(name));
        }
    }
}
