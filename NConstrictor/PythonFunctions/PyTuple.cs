using System.Runtime.InteropServices;

namespace NConstrictor
{
    public class PyTuple
    {
        [DllImport(@"Python3.dll", EntryPoint = "PyTuple_New", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject New(long n);

        [DllImport(@"Python3.dll", EntryPoint = "PyTuple_SetItem", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject SetItem(PyObject p, long pos, PyObject o);

        [DllImport(@"Python3.dll", EntryPoint = "PyTuple_Size", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern long Size(PyObject p);

        [DllImport(@"Python3.dll", EntryPoint = "PyTuple_GetItem", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject GetItem(PyObject p, long pos);

        public static PyObject[] UnPack(PyObject p)
        {
            PyObject[] result = new PyObject[Size(p)];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = GetItem(p, i);
            }

            return result;
        }

        public static PyObject Pack(params PyObject[] args)
        {
            PyObject result = New(args.Length);

            for (int i = 0; i < args.Length; i++)
            {
                SetItem(result, i, args[i]);
            }

            return result;
        }
    }
}
