using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NConstrictor
{
    public struct PyObject : IDisposable
    {
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

        [DllImport(@"Python3.dll", EntryPoint = "PyObject_RichCompareBool", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int RichCompareBool(PyObject o1, PyObject o2, int opid);

        private readonly IntPtr _pyObject;

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

        public static implicit operator PyObject(Array array)
        {
            long[] dims = new long[array.Rank];

            for (int i = 0; i < dims.Length; i++)
            {
                dims[i] = array.GetLength(i);
            }

            GCHandle handle = GCHandle.Alloc(array, GCHandleType.Pinned);
            Type t = array.GetType().GetElementType();
            PyObject result = NumPy.PyArrayNewFromDescr(NumPy.PyArrayType, GetDtype(t), array.Rank, dims, null, handle.AddrOfPinnedObject(), NpConsts.NPY_ARRAY_CARRAY, IntPtr.Zero);

            handle.Free();

            return result;
        }

        static IntPtr GetDtype(Type t)
        {
            if (t == typeof(int))
            {
                return Dtype.Int32;
            }
            else if (t == typeof(float))
            {
                return Dtype.Float32;
            }
            else if (t == typeof(double))
            {
                return Dtype.Float64;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public Array ToArray<T>()
        {
            return new PyBuffer<T>(this).GetArray();
        }

        public static implicit operator PyObject(IntPtr i)
        {
            return Unsafe.As<IntPtr, PyObject>(ref i);
        }

        public static implicit operator PyObject(string i)
        {
            return PyUnicode.DecodeFSDefault(i);
        }

        public static implicit operator PyObject(double i)
        {
            return PyFloat.FromDouble(i);
        }

        public static implicit operator PyObject(long i)
        {
            return PyLong.FromLong(i);
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
            Py.DecRef(this);
        }
    }
}
