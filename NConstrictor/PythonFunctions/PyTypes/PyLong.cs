using System.Runtime.InteropServices;
using System.Security;

namespace NConstrictor
{
    public class PyLong
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyLong_FromLong", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject FromLong(long v);


        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyLong_AsLong", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern long AsLong(PyObject obj);
    }
}
