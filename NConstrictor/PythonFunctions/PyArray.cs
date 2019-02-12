using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NConstrictor
{
    public struct PyArray<T> : IDisposable
    {
        private readonly PyObject _pyObject;

        public PyArray(PyObject pyObject)
        {
            _pyObject = Python.GetNamelessObject(pyObject);
        }

        public unsafe T this[params long[] indices]
        {
            get
            {
                if (typeof(T).IsArray)
                {
                    //todo 配列の取得は未対応
                    throw new NotImplementedException();
                }
                else
                {
                    IntPtr addr = NumPy.PyArrayGetPtr(this, indices);
                    return Unsafe.Read<T>((void*)addr);
                }
            }

            set
            {
                if (value is Array array)
                {
                    Type t = value.GetType().GetElementType();

                    GCHandle handle;

                    if (t == array.GetType().GetElementType())
                    {
                        handle = GCHandle.Alloc(array, GCHandleType.Pinned);
                    }
                    else
                    {
                        long[] dims = new long[array.Rank];

                        for (int i = 0; i < dims.Length; i++)
                        {
                            dims[i] = array.GetLength(i);
                        }

                        Array tmp = Array.CreateInstance(typeof(T), dims);
                        Array.Copy(array, tmp, array.Length);

                        handle = GCHandle.Alloc(tmp, GCHandleType.Pinned);
                    }

                    int size = Marshal.SizeOf(t) * array.Length;

                    IntPtr addr = NumPy.PyArrayGetPtr(this, indices);

                    Buffer.MemoryCopy((void*)handle.AddrOfPinnedObject(), (void*)addr, size, size);

                    handle.Free();
                }
                else
                {
                    IntPtr addr = NumPy.PyArrayGetPtr(this, indices);
                    Unsafe.Write((void*)addr, value);
                }
            }
        }

        public TArray ToNdArray<TArray>()
        {
            return new PyBuffer<T>(_pyObject).GetNdArray<TArray>();
        }

        public T[] ToArray()
        {
            return new PyBuffer<T>(_pyObject).GetArray();
        }

        public static PyObject operator +(PyArray<T> x, PyArray<T> y)
        {
            return PyNumber.Add(x, y);
        }

        public static PyObject operator -(PyArray<T> x, PyArray<T> y)
        {
            return PyNumber.Subtract(x, y);
        }

        public static PyObject operator *(PyArray<T> x, PyArray<T> y)
        {
            return PyNumber.Multiply(x, y);
        }

        public static PyObject operator /(PyArray<T> x, PyArray<T> y)
        {
            return PyNumber.TrueDivide(x, y);
        }

        // PyObject : PyArray
        public static PyObject operator +(PyObject x, PyArray<T> y)
        {
            return PyNumber.Add(x, y);
        }

        public static PyObject operator -(PyObject x, PyArray<T> y)
        {
            return PyNumber.Subtract(x, y);
        }

        public static PyObject operator *(PyObject x, PyArray<T> y)
        {
            return PyNumber.Multiply(x, y);
        }

        public static PyObject operator /(PyObject x, PyArray<T> y)
        {
            return PyNumber.TrueDivide(x, y);
        }

        // PyArray : PyObject
        public static PyObject operator +(PyArray<T> x, PyObject y)
        {
            return PyNumber.Add(x, y);
        }

        public static PyObject operator -(PyArray<T> x, PyObject y)
        {
            return PyNumber.Subtract(x, y);
        }

        public static PyObject operator *(PyArray<T> x, PyObject y)
        {
            return PyNumber.Multiply(x, y);
        }

        public static PyObject operator /(PyArray<T> x, PyObject y)
        {
            return PyNumber.TrueDivide(x, y);
        }

        public static implicit operator PyObject(PyArray<T> pyArray)
        {
            return Unsafe.As<PyArray<T>, PyObject>(ref pyArray);
        }

        public static implicit operator PyArray<T>(PyObject pyObject)
        {
            PyObject result = Python.GetNamelessObject(pyObject);
            return Unsafe.As<PyObject, PyArray<T>>(ref result);
        }

        public void Dispose()
        {
            Py.Clear(_pyObject);
        }
    }
}
