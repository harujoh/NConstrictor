using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NConstrictor
{
    public struct PyArray<T> : IDisposable
    {
        private readonly IntPtr _pyObject;

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
                    int size = Marshal.SizeOf(t) * array.Length;

                    IntPtr addr = NumPy.PyArrayGetPtr(this, indices);

                    GCHandle handle = GCHandle.Alloc(value, GCHandleType.Pinned);

                    Buffer.MemoryCopy((void*)handle.AddrOfPinnedObject(), (void*)addr, size, size);

                    handle.Free();
                }
                else
                {
                    IntPtr addr = NumPy.PyArrayGetPtr(this, indices);
                    Unsafe.Write((void*)(addr), value);
                }
            }
        }

        public static implicit operator PyObject(PyArray<T> i)
        {
            return Unsafe.As<PyArray<T>, PyObject>(ref i);
        }

        public static implicit operator PyArray<T>(PyObject i)
        {
            return Unsafe.As<PyObject, PyArray<T>>(ref i);
        }

        public void Dispose()
        {
            Py.DecRef(this);
        }
    }
}
