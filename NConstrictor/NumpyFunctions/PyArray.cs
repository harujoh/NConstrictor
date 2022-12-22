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
            _pyObject = pyObject.Copy();
        }

        public PyObject DType => _pyObject["dtype"];
        public PyTuple Shape => _pyObject["shape"];

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

        public PyArray<T> Copy()
        {
            return (PyArray<T>)_pyObject["copy"].Call();
        }

        public PyArray<T> Reshape(params int[] shape)
        {
            return (PyArray<T>)_pyObject["reshape"].Call((PyList)shape);
        }

        public static PyArray<T> operator +(PyArray<T> x, PyArray<T> y)
        {
            return (PyArray<T>)PyNumber.Add(x, y);
        }

        public static PyArray<T> operator -(PyArray<T> x, PyArray<T> y)
        {
            return (PyArray<T>)PyNumber.Subtract(x, y);
        }

        public static PyArray<T> operator *(PyArray<T> x, PyArray<T> y)
        {
            return (PyArray<T>)PyNumber.Multiply(x, y);
        }

        public static PyArray<T> operator /(PyArray<T> x, PyArray<T> y)
        {
            return (PyArray<T>)PyNumber.TrueDivide(x, y);
        }

        // PyObject : PyArray
        public static PyArray<T> operator +(PyObject x, PyArray<T> y)
        {
            return (PyArray<T>)PyNumber.Add(x, y);
        }

        public static PyArray<T> operator -(PyObject x, PyArray<T> y)
        {
            return (PyArray<T>)PyNumber.Subtract(x, y);
        }

        public static PyArray<T> operator *(PyObject x, PyArray<T> y)
        {
            return (PyArray<T>)PyNumber.Multiply(x, y);
        }

        public static PyArray<T> operator /(PyObject x, PyArray<T> y)
        {
            return (PyArray<T>)PyNumber.TrueDivide(x, y);
        }

        // PyArray : PyObject
        public static PyArray<T> operator +(PyArray<T> x, PyObject y)
        {
            return (PyArray<T>)PyNumber.Add(x, y);
        }

        public static PyArray<T> operator -(PyArray<T> x, PyObject y)
        {
            return (PyArray<T>)PyNumber.Subtract(x, y);
        }

        public static PyArray<T> operator *(PyArray<T> x, PyObject y)
        {
            return (PyArray<T>)PyNumber.Multiply(x, y);
        }

        public static PyArray<T> operator /(PyArray<T> x, PyObject y)
        {
            return (PyArray<T>)PyNumber.TrueDivide(x, y);
        }

        // PyArray : float
        public static PyArray<T> operator +(PyArray<T> x, float y)
        {
            return (PyArray<T>)PyNumber.Add(x, y);
        }

        public static PyArray<T> operator -(PyArray<T> x, float y)
        {
            return (PyArray<T>)PyNumber.Subtract(x, y);
        }

        public static PyArray<T> operator *(PyArray<T> x, float y)
        {
            return (PyArray<T>)PyNumber.Multiply(x, y);
        }

        public static PyArray<T> operator /(PyArray<T> x, float y)
        {
            return (PyArray<T>)PyNumber.TrueDivide(x, y);
        }

        // PyArray : double
        public static PyArray<T> operator +(PyArray<T> x, double y)
        {
            return (PyArray<T>)PyNumber.Add(x, y);
        }

        public static PyArray<T> operator -(PyArray<T> x, double y)
        {
            return (PyArray<T>)PyNumber.Subtract(x, y);
        }

        public static PyArray<T> operator *(PyArray<T> x, double y)
        {
            return (PyArray<T>)PyNumber.Multiply(x, y);
        }

        public static PyArray<T> operator /(PyArray<T> x, double y)
        {
            return (PyArray<T>)PyNumber.TrueDivide(x, y);
        }

        // PyArray : int
        public static PyArray<T> operator +(PyArray<T> x, int y)
        {
            return (PyArray<T>)PyNumber.Add(x, y);
        }

        public static PyArray<T> operator -(PyArray<T> x, int y)
        {
            return (PyArray<T>)PyNumber.Subtract(x, y);
        }

        public static PyArray<T> operator *(PyArray<T> x, int y)
        {
            return (PyArray<T>)PyNumber.Multiply(x, y);
        }

        public static PyArray<T> operator /(PyArray<T> x, int y)
        {
            return (PyArray<T>)PyNumber.TrueDivide(x, y);
        }

        // PyArray : long
        public static PyArray<T> operator +(PyArray<T> x, long y)
        {
            return (PyArray<T>)PyNumber.Add(x, y);
        }

        public static PyArray<T> operator -(PyArray<T> x, long y)
        {
            return (PyArray<T>)PyNumber.Subtract(x, y);
        }

        public static PyArray<T> operator *(PyArray<T> x, long y)
        {
            return (PyArray<T>)PyNumber.Multiply(x, y);
        }

        public static PyArray<T> operator /(PyArray<T> x, long y)
        {
            return (PyArray<T>)PyNumber.TrueDivide(x, y);
        }

        public static implicit operator T[](PyArray<T> pyArray)
        {
            return (T[])new PyBuffer<T>(pyArray).GetArray();
        }

        public static implicit operator T[,](PyArray<T> pyArray)
        {
            return (T[,])new PyBuffer<T>(pyArray).GetArray();
        }

        public static implicit operator T[,,](PyArray<T> pyArray)
        {
            return (T[,,])new PyBuffer<T>(pyArray).GetArray();
        }

        public static implicit operator T[,,,](PyArray<T> pyArray)
        {
            return (T[,,,])new PyBuffer<T>(pyArray).GetArray();
        }

        public static implicit operator T[,,,,](PyArray<T> pyArray)
        {
            return (T[,,,,])new PyBuffer<T>(pyArray).GetArray();
        }

        public static implicit operator Array(PyArray<T> pyArray)
        {
            return new PyBuffer<T>(pyArray).GetArray();
        }

        public static implicit operator PyArray<T>(Array array)
        {
            long[] dims = new long[array.Rank];

            for (int i = 0; i < dims.Length; i++)
            {
                dims[i] = array.GetLength(i);
            }

            GCHandle handle = GCHandle.Alloc(array, GCHandleType.Pinned);
            PyObject result = NumPy.PyArrayNewFromDescr(NumPy.PyArrayType, Dtype.GetDtype(array.GetType().GetElementType()), array.Rank, dims, null, handle.AddrOfPinnedObject(), NpConsts.NPY_ARRAY_CARRAY, IntPtr.Zero);
            handle.Free();

            return Unsafe.As<PyObject, PyArray<T>>(ref result);
        }

        public static implicit operator PyArray<T>(PyObject[] array)
        {
            long[] dims = new long[array.Rank];

            for (int i = 0; i < dims.Length; i++)
            {
                dims[i] = array.GetLength(i);
            }

            GCHandle handle = GCHandle.Alloc(array, GCHandleType.Pinned);
            PyObject result = NumPy.PyArrayNewFromDescr(NumPy.PyArrayType, Dtype.GetDtype(array.GetType().GetElementType()), array.Rank, dims, null, handle.AddrOfPinnedObject(), NpConsts.NPY_ARRAY_CARRAY, IntPtr.Zero);
            handle.Free();

            return Unsafe.As<PyObject, PyArray<T>>(ref result);
        }

        public static implicit operator PyObject(PyArray<T> pyArray)
        {
            return Unsafe.As<PyArray<T>, PyObject>(ref pyArray);
        }

        public static explicit operator PyArray<T>(PyObject pyObject)
        {
            return Unsafe.As<PyObject, PyArray<T>>(ref pyObject);
        }

        public static explicit operator PyArray<T[]>(PyArray<T> pyObject)
        {
            return Unsafe.As<PyArray<T>, PyArray<T[]>>(ref pyObject);
        }

        public void Dispose()
        {
            Py.Clear(_pyObject);
        }
    }
}
