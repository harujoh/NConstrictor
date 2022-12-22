using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace NConstrictor
{
    public struct PyTuple
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyTuple_New", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyTuple New(long n);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyTuple_SetItem", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject SetItem(PyObject p, long pos, PyObject o);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyTuple_Size", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern long Size(PyObject p);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyTuple_GetItem", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject GetItem(PyObject p, long pos);

        private readonly PyObject _pyObject;

        private long Length => Size(this);

        public PyObject this[int index]
        {
            get
            {
                return GetItem(_pyObject, index);
            }

            set
            {
                SetItem(_pyObject, index, value);
            }
        }

        public PyObject[] UnPack()
        {
            PyObject[] result = new PyObject[Size(this)];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = GetItem(this, i);
            }

            return result;
        }

        public static PyTuple Pack(params PyObject[] args)
        {
            PyTuple result = New(args.Length);

            for (int i = 0; i < args.Length; i++)
            {
                SetItem(result, i, args[i]);
            }

            return result;
        }

        public static implicit operator PyObject(PyTuple pyArray)
        {
            return Unsafe.As<PyTuple, PyObject>(ref pyArray);
        }

        public static implicit operator PyTuple(PyObject pyObject)
        {
            return Unsafe.As<PyObject, PyTuple>(ref pyObject);
        }

        public static implicit operator PyTuple(PyObject[] pyObjects)
        {
            return Pack(pyObjects);
        }

        public static explicit operator PyObject[](PyTuple pyTuple)
        {
            return pyTuple.UnPack();
        }

    }
}
