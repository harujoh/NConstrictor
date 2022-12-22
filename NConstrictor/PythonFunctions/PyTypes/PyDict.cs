using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace NConstrictor
{
    public class   PyDict : Dictionary<PyObject, PyObject>
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyDict_New", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject PyDict_New();

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyDict_SetItem", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject SetItem(PyObject p, PyObject key, PyObject val);


        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyDict_GetItem", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject GetItem(PyObject p, PyObject key);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyDict_Size", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Size(PyObject p);

        private PyObject _pyDict;

        public PyDict()
        {
            _pyDict = PyDict_New();
        }

        public PyDict(string key, PyObject val)
        {
            _pyDict = PyDict_New();
            Add(key, val);
        }

        public PyDict(string[] keys, PyObject[] vals)
        {
            _pyDict = PyDict_New();

#if DEBUG
            if (keys.Length != vals.Length) throw new Exception();
#endif

            for (int i = 0; i < keys.Length; i++)
            {
                Add(keys[i], vals[i]);
            }
        }

        public PyDict(KeyValuePair<PyObject, PyObject> kvp)
        {
            _pyDict = PyDict_New();
            Add(kvp.Key, kvp.Value);
        }

        public PyDict(Dictionary<PyObject, PyObject> dict)
        {
            _pyDict = PyDict_New();

            foreach (var kvp in dict)
            {
                Add(kvp.Key, kvp.Value);
            }
        }

        public PyObject this[PyObject key]
        {
            get
            {
                return GetItem(_pyDict, key);
            }

            set
            {
                SetItem(_pyDict, key, value);
            }
        }

        public int Count => Size(_pyDict);

        public void Add(PyObject key, PyObject val)
        {
            SetItem(_pyDict, key, val);
        }

        public static implicit operator PyObject(PyDict variable)
        {
            return variable._pyDict;
        }

        public void Dispose()
        {
            _pyDict.Dispose();
        }
    }
}
