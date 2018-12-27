using System.Runtime.InteropServices;

namespace NConstrictor
{
    class PyEval
    {
        [DllImport(@"Python3.dll", EntryPoint = "PyEval_InitThreads", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void InitThreads();
    }
}
