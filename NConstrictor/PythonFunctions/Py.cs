using System.Runtime.InteropServices;
using System.Security;

namespace NConstrictor
{
    public class Py
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "Py_Initialize", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void Initialize();

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "Py_Finalize", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void Finalize();

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "Py_IncRef", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void IncRef(PyObject o);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "Py_DecRef", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void DecRef(PyObject o);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "Py_BuildValue", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject BuildValue(string format);

        public static readonly PyObject None;

        static Py()
        {
            None = Py.BuildValue("");
        }

        public static void Clear(PyObject o)
        {
            while (PyObject.Size(o) != -1)
            {
                Py.DecRef(o);
            }
        }
    }
}
