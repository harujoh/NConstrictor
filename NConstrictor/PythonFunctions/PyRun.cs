using System.Runtime.InteropServices;
using System.Security;

namespace NConstrictor
{
    class PyRun
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python37.dll", EntryPoint = "PyRun_SimpleString", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int SimpleString(string str);
    }
}
