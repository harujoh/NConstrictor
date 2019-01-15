using System;
using System.Runtime.InteropServices;

namespace NConstrictor
{
    public class PyTuple
    {
        [DllImport(@"Python3.dll", EntryPoint = "PyTuple_New", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr New(long n);

        [DllImport(@"Python3.dll", EntryPoint = "PyTuple_SetItem", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr SetItem(IntPtr p, long pos, IntPtr o);

        [DllImport(@"Python3.dll", EntryPoint = "PyTuple_Size", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern long Size(IntPtr p);

        [DllImport(@"Python3.dll", EntryPoint = "PyTuple_GetItem", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr GetItem(IntPtr p, long pos);

        public static IntPtr[] UnPack(IntPtr p)
        {
            IntPtr[] result = new IntPtr[Size(p)];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = GetItem(p, i);
            }

            return result;
        }

        public static IntPtr Pack(params IntPtr[] args)
        {
            IntPtr result = New(args.Length);

            for (int i = 0; i < args.Length; i++)
            {
                SetItem(result, i, args[i]);
            }

            return result;
        }
    }
}
