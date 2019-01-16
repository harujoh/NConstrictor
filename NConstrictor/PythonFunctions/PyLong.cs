using System.Runtime.InteropServices;

namespace NConstrictor
{
    class PyLong
    {
        [DllImport(@"Python3.dll", EntryPoint = "PyLong_FromLong", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject FromLong(long v);
    }
}
