using System.Runtime.InteropServices;

namespace NConstrictor
{
    class PyRun
    {
        [DllImport(@"Python37.dll", EntryPoint = "PyRun_SimpleString", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int SimpleString(string str);
    }
}
