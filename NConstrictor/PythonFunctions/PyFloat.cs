using System.Runtime.InteropServices;

namespace NConstrictor
{
    class PyFloat
    {
        [DllImport(@"Python3.dll", EntryPoint = "PyFloat_FromDouble", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject FromDouble(double v);
    }
}
