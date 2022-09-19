using System.Runtime.InteropServices;
using System.Security;

namespace NConstrictor
{
    public class PyFloat
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyFloat_FromDouble", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject FromDouble(double v);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyFloat_AsDouble", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern double AsDouble(PyObject pyFloat);
    }
}
