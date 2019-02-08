using System.Runtime.InteropServices;
using System.Security;

namespace NConstrictor
{
    class PyFloat
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyFloat_FromDouble", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject FromDouble(double v);
    }
}
