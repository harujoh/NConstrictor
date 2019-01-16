using System.Runtime.InteropServices;

namespace NConstrictor
{
    public class Py
    {
        [DllImport(@"Python3.dll", EntryPoint = "Py_Initialize", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void Initialize();

        [DllImport(@"Python3.dll", EntryPoint = "Py_Finalize", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void Finalize();

        [DllImport(@"Python3.dll", EntryPoint = "Py_IncRef", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void IncRef(PyObject o);

        [DllImport(@"Python3.dll", EntryPoint = "Py_DecRef", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void DecRef(PyObject o);
    }
}
