using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

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
        internal static extern PyObject CallObject(PyObject callableObject, PyObject args);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyObject_Call", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern PyObject Call(PyObject callableObject, PyObject args, PyObject kw);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyObject_RichCompareBool", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int RichCompareBool(PyObject o1, PyObject o2, int opid);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyObject_Size", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern long Size(PyObject o);//Py_ssize_t PyObject_Size(PyObject *o)

        private readonly IntPtr _pyObject;

        public PyObject this[string name]
        {
            get
            {
                var result = GetAttrString(_pyObject, name);
                if (result == IntPtr.Zero) throw new Exception(name+"は存在しない名称です");
                return result;
            }

            set
            {
                SetAttrString(_pyObject, name, value);
            }
        }

        public PyObject Call(params PyObject[] args)
        {
            return CallObject(_pyObject, PyTuple.Pack(args));
        }

        public PyObject Call(PyDict kw)
        {
            return Call(_pyObject, PyTuple.Pack(), kw);
        }

        public PyObject Call(PyTuple args, PyDict kw)
        {
            return Call(_pyObject, args, kw);
        }

        public PyObject Call(PyObject args, PyDict kw)
        {
            return Call(_pyObject, PyTuple.Pack(args), kw);
        }

        public PyObject Call(PyObject[] args, PyDict kw)
        {
            return Call(_pyObject, PyTuple.Pack(args), kw);
        }

        public PyObject Copy()
        {
            return this["copy"].Call();
        }

        public static implicit operator PyObject(IntPtr i)
        {
            return Unsafe.As<IntPtr, PyObject>(ref i);
        }

        public static explicit operator long(PyObject o)
        {
            return PyLong.AsLong(o);
        }

        public static explicit operator double(PyObject o)
        {
            return PyFloat.AsDouble(o);
        }

        public static explicit operator string(PyObject o)
        {
            var target = o;
            try
            {
                var b = PyUnicode.EncodeFSDefault(o);

                if (b != IntPtr.Zero)
                {
                    target = b;
                }
                else
                {
                    //Intptr.ZeroなのでUnicodeではないため、そのまま使いたいけどそうもいかない…
                    return "Non Unicode String Value";
                }

                long len = PyBytes.Size(target);
                byte[] c = new byte[len];
                var cPtr = PyBytes.AsString(target);
                Marshal.Copy(cPtr, c, 0, c.Length);

                return Encoding.UTF8.GetString(c);
            }
            catch (Exception e)
            {
                return e.Message + ":Non Unicode String Value";
            }
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

        public override string ToString()
        {
            return (string)this;
        }
    }
}
