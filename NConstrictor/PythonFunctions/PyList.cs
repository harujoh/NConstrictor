using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace NConstrictor
{
    public class PyList : List<PyObject>
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

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyList_Size", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int PyListSize(PyObject list);


        private readonly PyObject _pyObject;

        //キャスト用
        private PyList(PyObject pyObject)
        {
            _pyObject = pyObject;
        }

        public PyList()
        {
            _pyObject = PyList.PyListNew(0);
        }

        public PyList(long len)
        {
            _pyObject = PyList.PyListNew(len);
        }

        public PyList(int len)
        {
            _pyObject = PyList.PyListNew(len);
        }

        public PyList(params PyObject[] items)
        {
            _pyObject = PyList.PyListNew(items.Length);

            for (int i = 0; i < items.Length; i++)
            {
                PyListSetItem(_pyObject, i, items[i]);
            }
        }

        public PyObject this[int index]
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

        public int Count => PyListSize(_pyObject);

        public void Insert(int index, PyObject item)
        {
            PyListInsert(_pyObject, index, item);
        }

        public void Add(PyObject item)
        {
            PyListAppend(_pyObject, item);
        }

        public PyObject AsTuple()
        {
            return PyListAsTuple(_pyObject);
        }

        public static implicit operator PyObject(PyList pyArray)
        {
            return pyArray._pyObject;
        }

        public static explicit operator PyList(PyObject pyObject)
        {
            return new PyList(pyObject);
        }

        public static implicit operator PyList(PyObject[] pyObjects)
        {
            return new PyList(pyObjects);
        }

        public static implicit operator PyList(int[] pyObjects)
        {
            PyList result = new PyList(pyObjects.Length);

            for (int i = 0; i < pyObjects.Length; i++)
            {
                PyListSetItem(result._pyObject, i, pyObjects[i]);
            }

            return result;
        }

        public static implicit operator PyList(float[] pyObjects)
        {
            PyList result = new PyList(pyObjects.Length);

            for (int i = 0; i < pyObjects.Length; i++)
            {
                PyListSetItem(result._pyObject, i, pyObjects[i]);
            }

            return result;
        }

        public static implicit operator PyList(double[] pyObjects)
        {
            PyList result = new PyList(pyObjects.Length);

            for (int i = 0; i < pyObjects.Length; i++)
            {
                PyListSetItem(result._pyObject, i, pyObjects[i]);
            }

            return result;
        }

        public void Dispose()
        {
            Py.Clear(_pyObject);
        }
    }
}
