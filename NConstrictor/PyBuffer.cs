using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NConstrictor
{
    public unsafe struct PyBuffer<T> : IDisposable
    {
        private PyBufferRaw _pyBuffer;

        internal Span<T> Data
        {
            get { return new Span<T>((void*)_pyBuffer.BufPtr, (int)(_pyBuffer.Len / _pyBuffer.ItemSize)); }
        }

        public T this[int i]
        {
            set { Unsafe.Write((void*)(_pyBuffer.BufPtr + i * Unsafe.SizeOf<T>()), value); }
            get { return Unsafe.Read<T>((void*)(_pyBuffer.BufPtr + i * Unsafe.SizeOf<T>())); }
        }

        public int[] Shape;

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

            Shape = new int[_pyBuffer.Ndim];
            Marshal.Copy(_pyBuffer.Shape, Shape, 0, Shape.Length);
        }

        public void Dispose()
        {
            PyObject.ReleaseBuffer(_view);
            Marshal.FreeCoTaskMem(_view);
        }
    }
}
