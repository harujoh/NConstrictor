using System;
using System.Runtime.InteropServices;

namespace NConstrictor
{
    public class PyNumber
    {
        [DllImport(@"Python3.dll", EntryPoint = "PyNumber_Add", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject Add(PyObject o1, PyObject o2);

        [DllImport(@"Python3.dll", EntryPoint = "PyNumber_Subtract", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject Subtract(PyObject o1, PyObject o2);

        [DllImport(@"Python3.dll", EntryPoint = "PyNumber_Multiply", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject Multiply(PyObject o1, PyObject o2);

        [DllImport(@"Python3.dll", EntryPoint = "PyNumber_TrueDivide", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject TrueDivide(PyObject o1, PyObject o2);
    }
}
