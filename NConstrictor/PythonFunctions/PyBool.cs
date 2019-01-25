using System.Runtime.InteropServices;

namespace NConstrictor
{
    class PyBool
    {
        [DllImport(@"Python3.dll", EntryPoint = "PyBool_FromLong", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject FromLong(long v);
    }
}
