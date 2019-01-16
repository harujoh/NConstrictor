using System;
using System.Runtime.InteropServices;

namespace NConstrictor
{
    public struct PyObject : IDisposable
    {
        public bool Equals(PyObject other)
        {
            return _pyObject.Equals(other._pyObject);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is PyObject other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _pyObject.GetHashCode();
        }

        public static PyObject Zero = new PyObject(IntPtr.Zero);

        [DllImport(@"Python37.dll", EntryPoint = "PyObject_GetBuffer", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int GetBuffer(PyObject exporter, IntPtr view, int flags);

        //命名ルールで言えばPyBuffer内に記述すべきだがPyObject_GetBufferの対である為こちらに記述
        [DllImport(@"Python37.dll", EntryPoint = "PyBuffer_Release", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void ReleaseBuffer(IntPtr view);

        [DllImport(@"Python3.dll", EntryPoint = "PyObject_GetAttrString", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject GetAttrString(PyObject o, string attrName);

        [DllImport(@"Python3.dll", EntryPoint = "PyObject_SetAttrString", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int SetAttrString(PyObject o, string attrName, PyObject v);

        [DllImport(@"Python3.dll", EntryPoint = "PyObject_CallObject", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject CallObject(PyObject callableObject, PyObject args);

        private readonly IntPtr _pyObject;

        public PyObject(IntPtr pyObject)
        {
            _pyObject = pyObject;
        }

        public PyObject this[string name]
        {
            get
            {
                PyObject result = PyObject.GetAttrString(this, name);
                Py.DecRef(this);

                return result;
            }

            set
            {
                PyObject.SetAttrString(this, name, value);
                Py.DecRef(value);
            }
        }

        public PyObject Call(params PyObject[] argNames)
        {
            PyObject args = PyTuple.Pack(argNames);
            PyObject result = PyObject.CallObject(this, args);

            return result;
        }

        public void Dispose()
        {
            Py.DecRef(this);
        }

        public static bool operator ==(PyObject x, IntPtr y)
        {
            return x._pyObject == y;
        }

        public static bool operator !=(PyObject a, IntPtr b)
        {
            return !(a == b);
        }
    }
}
