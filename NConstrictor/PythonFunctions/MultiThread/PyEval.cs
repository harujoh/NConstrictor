using System.Runtime.InteropServices;
using System.Security;

namespace NConstrictor
{
    public class PyEval
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyEval_InitThreads", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void InitThreads();
    }
}
