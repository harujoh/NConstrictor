using System.Runtime.InteropServices;
using System.Security;

namespace NConstrictor
{
    public class PyErr
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyErr_Print", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void Print();
    }
}
