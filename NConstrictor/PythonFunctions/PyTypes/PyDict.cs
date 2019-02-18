using System;
using System.Runtime.InteropServices;
using System.Security;

namespace NConstrictor
{
    public class PyDict : IDisposable
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyDict_New", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject New();

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyDict_SetItemString", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject SetItemString(PyObject p, string key, PyObject val);

        private PyObject _pyDict;

        public PyDict()
        {
            _pyDict = New();
        }

        public PyDict(string key, PyObject val)
        {
            _pyDict = New();
            Add(key, val);
        }

        public PyDict(string[] keys, PyObject[] vals)
        {
            _pyDict = New();

#if DEBUG
            if(keys.Length != vals.Length)throw new Exception();
#endif

            for (int i = 0; i < keys.Length; i++)
            {
                Add(keys[i], vals[i]);
            }
        }

        public void Add(string key, PyObject val)
        {
            SetItemString(_pyDict, key, val);
        }

        public static implicit operator PyObject(PyDict pyDict)
        {
            return pyDict._pyDict;
        }

        public void Dispose()
        {
            _pyDict.Dispose();
        }
    }
}
