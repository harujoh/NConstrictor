using System;
using System.Runtime.InteropServices;

namespace NConstrictor
{
    // メモリ上のN次元配列の形を示す
    // shape[0] * ... * shape[ndim-1] * itemsize は len と等しくなければなりません。
    [StructLayout(LayoutKind.Sequential)]
    public struct PyBuffer
    {
        public IntPtr BufPtr; //生データのポインタ
        public IntPtr Obj;//null
        public long Len;//要素*バイト数
        public long ItemSize;//バイト数

        public int ReadOnly;
        public int Ndim;

        [MarshalAs(UnmanagedType.LPStr)]
        public string Format;

        public IntPtr Shape; //int[] ReadOnly 
        public IntPtr Strides; //int[] ReadOnly 一要素あたりのItemSizeが配列で入ってる
        public IntPtr SubOffsets; //int[] ReadOnly PIL(Python Imaging Library)用で基本null

        public IntPtr Internal; //ReadOnly 基本null
    }
}
