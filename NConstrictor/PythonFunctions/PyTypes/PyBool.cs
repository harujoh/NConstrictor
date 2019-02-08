using System.Runtime.InteropServices;
using System.Security;

namespace NConstrictor
{
    public class PyBool
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyBool_FromLong", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject FromLong(long v);
    }
}
