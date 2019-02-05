using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NConstrictor
{
    public struct PyArray<T> : IDisposable
    {
        private readonly PyObject _pyObject;

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

        public static implicit operator PyArray<T>(Array array)
        {
            long[] dims = new long[array.Rank];

            for (int i = 0; i < dims.Length; i++)
            {
                dims[i] = array.GetLength(i);
            }

            GCHandle handle;
            if (array.GetType().GetElementType() == typeof(T))
            {
                handle = GCHandle.Alloc(array, GCHandleType.Pinned);
            }
            else
            {
                Array tmp = Array.CreateInstance(typeof(T), dims);
                Array.Copy(array, tmp, array.Length);
                handle = GCHandle.Alloc(tmp, GCHandleType.Pinned);
            }

            PyObject result = NumPy.PyArrayNewFromDescr(NumPy.PyArrayType, PyObject.GetDtype(typeof(T)), array.Rank, dims, null, handle.AddrOfPinnedObject(), NpConsts.NPY_ARRAY_CARRAY, IntPtr.Zero);

            handle.Free();

            return Unsafe.As<PyObject, PyArray<T>>(ref result);
        }

        public static implicit operator PyObject(PyArray<T> pyArray)
        {
            return Unsafe.As<PyArray<T>, PyObject>(ref pyArray);
        }

        public static implicit operator PyArray<T>(PyObject pyObject)
        {
            return Unsafe.As<PyObject, PyArray<T>>(ref pyObject);
        }

        public void Dispose()
        {
            Py.DecRef(_pyObject);
        }
    }
}
