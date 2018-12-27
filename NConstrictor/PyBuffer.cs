using System;
using System.Runtime.InteropServices;

namespace NConstrictor
{
    public unsafe ref struct PyBuffer<T>
    {
        public PyBufferRaw Buf;
        public Span<T> Data;

        public T this[int i]
        {
            set { this.Data[i] = value; }
            get { return this.Data[i]; }
        }

        public int[] Shape;
        public int[] Strides;
        public int[] SubOffsets;

        private IntPtr view;
        public IntPtr BufPtr
        {
            get { return Buf.BufPtr; }
        }

        public PyBuffer(string name)
        {
            IntPtr targetObj = PyObject.GetAttr(name);

            if (targetObj == IntPtr.Zero)
            {
                throw new Exception("対象の変数" + name + "が見つかりませんでした");
            }

            view = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(PyBufferRaw)));

            if (PyObject.GetBuffer(targetObj, view, PyConsts.BUF_C_CONTIGUOUS | PyConsts.BUF_FORMAT) != 0)
            {
                throw new Exception();
            }

            Buf = (PyBufferRaw)Marshal.PtrToStructure(view, typeof(PyBufferRaw));

            Shape = new int[Buf.Ndim];
            Marshal.Copy(Buf.Shape, Shape, 0, Shape.Length);

            Strides = new int[Buf.Ndim];
            Marshal.Copy(Buf.Strides, Strides, 0, Strides.Length);

            if (Buf.SubOffsets != IntPtr.Zero)
            {
                SubOffsets = new int[Buf.Ndim];
                Marshal.Copy(Buf.SubOffsets, SubOffsets, 0, SubOffsets.Length);
            }
            else
            {
                SubOffsets = new int[] { };
            }

            Data = new Span<T>(Buf.BufPtr.ToPointer(), (int)(Buf.Len / Buf.ItemSize));
        }

        //ref structなので名ばかりのなんちゃってDispose
        public void Dispose()
        {
            PyObject.ReleaseBuffer(view);
            Marshal.FreeCoTaskMem(view);
        }
    }
}
