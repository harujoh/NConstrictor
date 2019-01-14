using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NConstrictor
{
    public unsafe struct PyBuffer<T> : IDisposable
    {
        private PyBufferRaw _pyBuffer;

        public T this[params int[] i]
        {
            set { Unsafe.Write((void*)(_pyBuffer.BufPtr + GetLocalIndex(i) * Unsafe.SizeOf<T>()), value); }
            get { return Unsafe.Read<T>((void*)(_pyBuffer.BufPtr + GetLocalIndex(i) * Unsafe.SizeOf<T>())); }
        }

        public long[] Shape;

        private readonly IntPtr _view;

        public PyBuffer(string name)
        {
            IntPtr targetObj = PyObject.GetAttr(name);

            if (targetObj == IntPtr.Zero)
            {
                throw new Exception("対象の変数" + name + "が見つかりませんでした");
            }

            _pyBuffer = new PyBufferRaw();

            _view = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(PyBufferRaw)));

            if (PyObject.GetBuffer(targetObj, _view, PyConsts.BUF_C_CONTIGUOUS | PyConsts.BUF_FORMAT) != 0)
            {
                throw new Exception();
            }

            _pyBuffer = (PyBufferRaw)Marshal.PtrToStructure(_view, typeof(PyBufferRaw));

            Shape = new long[_pyBuffer.Ndim];
            Marshal.Copy(_pyBuffer.Shape, Shape, 0, Shape.Length);
        }

        int GetLocalIndex(int[] indices)
        {
#if DEBUG
            if(indices.Length != Shape.Length) throw new Exception("指定された次元数が異なります");
#endif
            long result = 0;
            long rankOffset = 1;

            for (int i = indices.Length - 1; i >= 0; i--)
            {
                result += indices[i] * rankOffset;
                rankOffset *= Shape[i];
            }

            return (int)result;
        }

        public Array GetArray()
        {
            int[] shape = new int[Shape.Length];
            for (int i = 0; i < Shape.Length; i++)
            {
                shape[i] = (int)Shape[i];
            }

            Array result = Array.CreateInstance(typeof(T), shape);

            GCHandle handle = GCHandle.Alloc(result, GCHandleType.Pinned);
            Buffer.MemoryCopy((void*)_pyBuffer.BufPtr, (void*)handle.AddrOfPinnedObject(), _pyBuffer.Len, _pyBuffer.Len);
            handle.Free();

            return result;
        }

        public void Dispose()
        {
            PyObject.ReleaseBuffer(_view);
            Marshal.FreeCoTaskMem(_view);
        }
    }
}
