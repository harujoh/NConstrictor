using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace NConstrictor
{
    public struct PyList : IDisposable
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyList_New", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject PyListNew(long len);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyList_GetItem", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject PyListGetItem(PyObject list, long index);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyList_SetItem", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int PyListSetItem(PyObject list, long index, PyObject item);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyList_Insert", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int PyListInsert(PyObject list, long index, PyObject item);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyList_Append", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int PyListAppend(PyObject list, PyObject item);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyList_AsTuple", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject PyListAsTuple(PyObject list);


        private readonly PyObject _pyObject;

        public PyList(PyObject[] items)
        {
            _pyObject = PyList.PyListNew(items.Length);

            for (int i = 0; i < items.Length; i++)
            {
                PyListSetItem(_pyObject, i, items[i]);
            }
        }

        public PyList(long len)
        {
            _pyObject = PyList.PyListNew(len);
        }

        public PyObject this[long index]
        {
            get
            {
                return PyListGetItem(_pyObject, index);
            }

            set
            {
                PyListSetItem(_pyObject, index, value);
            }
        }

        public void Insert(long index, PyObject item)
        {
            PyListInsert(_pyObject, index, item);
        }

        public void Append(PyObject item)
        {
            PyListAppend(_pyObject, item);
        }

        public PyObject AsTuple()
        {
            return PyListAsTuple(_pyObject);
        }

        public static implicit operator PyList(PyObject[] pyObjects)
        {
            return new PyList(pyObjects);
        }


        public void Dispose()
        {
            Py.Clear(_pyObject);
        }
    }
}
