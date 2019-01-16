using System.Runtime.InteropServices;

namespace NConstrictor
{
    public class PyImport
    {
        [DllImport(@"Python3.dll", EntryPoint = "PyImport_AddModule", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject AddModule(string name);

        [DllImport(@"Python3.dll", EntryPoint = "PyImport_Import", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject Import(PyObject name);

        [DllImport(@"Python3.dll", EntryPoint = "PyImport_ImportModule", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject ImportModule(string name);

        public static PyObject Import(string name)
        {
            PyObject arg = PyUnicode.DecodeFSDefault(name);
            PyObject result = Import(arg);

            return result;
        }
    }
}
