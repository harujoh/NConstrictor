using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace NConstrictor
{
    public struct PyObject : IDisposable
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python37.dll", EntryPoint = "PyObject_GetBuffer", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int GetBuffer(PyObject exporter, IntPtr view, int flags);
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python37.dll", EntryPoint = "PyBuffer_Release", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void ReleaseBuffer(IntPtr view);  //命名ルールで言えばPyBuffer内に記述すべきだがPyObject_GetBufferの対である為こちらに記述

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyObject_GetAttrString", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject GetAttrString(PyObject o, string attrName);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyObject_SetAttrString", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int SetAttrString(PyObject o, string attrName, PyObject v);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyObject_CallObject", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject CallObject(PyObject callableObject, PyObject args);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyObject_RichCompareBool", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int RichCompareBool(PyObject o1, PyObject o2, int opid);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyObject_Size", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern long Size(PyObject o);

        private readonly IntPtr _pyObject;

        public PyObject this[string name]
        {
            get
            {
                return GetAttrString(_pyObject, name);
            }

            set
            {
                SetAttrString(_pyObject, name, value);
            }
        }

        public PyObject Call(params PyObject[] argNames)
        {
            return CallObject(_pyObject, PyTuple.Pack(argNames));
        }

        public static implicit operator PyObject(IntPtr i)
        {
            return Unsafe.As<IntPtr, PyObject>(ref i);
        }

        public static implicit operator PyObject(string str)
        {
            return PyUnicode.DecodeFSDefault(str);
        }

        public static implicit operator PyObject(bool flg)
        {
            return PyBool.FromLong(flg ? 1 : 0);
        }

        public static implicit operator PyObject(double d)
        {
            return PyFloat.FromDouble(d);
        }

        public static implicit operator PyObject(long l)
        {
            return PyLong.FromLong(l);
        }

        public static PyObject operator +(PyObject x, PyObject y)
        {
            return PyNumber.Add(x, y);
        }

        public static PyObject operator -(PyObject x, PyObject y)
        {
            return PyNumber.Subtract(x, y);
        }

        public static PyObject operator *(PyObject x, PyObject y)
        {
            return PyNumber.Multiply(x, y);
        }

        public static PyObject operator /(PyObject x, PyObject y)
        {
            return PyNumber.TrueDivide(x, y);
        }

        public static bool operator ==(PyObject a, IntPtr b)
        {
            return a._pyObject == b;
        }

        public static bool operator !=(PyObject a, IntPtr b)
        {
            return !(a == b);
        }

        public static bool operator ==(PyObject a, PyObject b)
        {
            return RichCompareBool(a, b, PyConsts.PY_EQ) == 1;
        }

        public static bool operator !=(PyObject a, PyObject b)
        {
            return RichCompareBool(a, b, PyConsts.PY_NE) == 1;
        }

        public static bool operator <(PyObject a, PyObject b)
        {
            return RichCompareBool(a, b, PyConsts.PY_LT) == 1;
        }

        public static bool operator <=(PyObject a, PyObject b)
        {
            return RichCompareBool(a, b, PyConsts.PY_LE) == 1;
        }

        public static bool operator >(PyObject a, PyObject b)
        {
            return RichCompareBool(a, b, PyConsts.PY_GT) == 1;
        }

        public static bool operator >=(PyObject a, PyObject b)
        {
            return RichCompareBool(a, b, PyConsts.PY_GE) == 1;
        }

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

        public void Dispose()
        {
            Py.Clear(_pyObject);
        }
    }
}
