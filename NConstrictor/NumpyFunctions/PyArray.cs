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

        public Array this[params Range[] indices]
        {
            get
            {
                int[] lengthArray = new int[indices.Length];
                long[] sourceIndex = new long[indices.Length];
                int[] destIndex = new int[indices.Length];
                int count = 1;

                for (int i = 0; i < lengthArray.Length; i++)
                {
                    lengthArray[i] = (indices[i].End.Value != 0 ? indices[i].End.Value : (int)Shape[i]) - indices[i].Start.Value;

                    count *= lengthArray[i];
                    sourceIndex[i] = indices[i].Start.Value;
                }

                Array result = Array.CreateInstance(typeof(T), lengthArray);

                for (int i = 0; i < count; i++)
                {
                    result.SetValue(this[sourceIndex], destIndex);

                    //最下次元をカウントアップ
                    sourceIndex[^1]++;
                    destIndex[^1]++;

                    //繰り上げ処理
                    for (int j = lengthArray.Length - 2; j >= 0; j--)
                    {
                        if (destIndex[j + 1] >= lengthArray[j + 1])
                        {
                            sourceIndex[j + 1] = indices[j + 1].Start.Value;
                            destIndex[j + 1] = 0;
                            destIndex[j]++;
                            sourceIndex[j]++;
                        }
                    }
                }

                return result;
            }

            set { }
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

        // PyArray : T
        public static PyArray<T> operator +(PyArray<T> x, T y)
        {
            return (PyArray<T>)PyNumber.Add(x, PyObject.GetPyObject(y));
        }

        public static PyArray<T> operator -(PyArray<T> x, T y)
        {
            return (PyArray<T>)PyNumber.Subtract(x, PyObject.GetPyObject(y));
        }

        public static PyArray<T> operator *(PyArray<T> x, T y)
        {
            return (PyArray<T>)PyNumber.Multiply(x, PyObject.GetPyObject(y));
        }

        public static PyArray<T> operator /(PyArray<T> x, T y)
        {
            return (PyArray<T>)PyNumber.TrueDivide(x, PyObject.GetPyObject(y));
        }

        // T : PyArray
        public static PyArray<T> operator +(T x, PyArray<T> y)
        {
            return (PyArray<T>)PyNumber.Add(PyObject.GetPyObject(x), y);
        }

        public static PyArray<T> operator -(T x, PyArray<T> y)
        {
            return (PyArray<T>)PyNumber.Subtract(PyObject.GetPyObject(x), y);
        }

        public static PyArray<T> operator *(T x, PyArray<T> y)
        {
            return (PyArray<T>)PyNumber.Multiply(PyObject.GetPyObject(x), y);
        }

        public static PyArray<T> operator /(T x, PyArray<T> y)
        {
            return (PyArray<T>)PyNumber.TrueDivide(PyObject.GetPyObject(x), y);
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
