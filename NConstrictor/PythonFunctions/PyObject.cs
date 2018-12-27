using System;
using System.Runtime.InteropServices;

namespace NConstrictor
{
    public class PyObject
    {
        [DllImport(@"Python37.dll", EntryPoint = "PyObject_GetBuffer", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int GetBuffer(IntPtr exporter, IntPtr view, int flags);

        //命名ルールで言えばPyBuffer内に記述すべきだがPyObject_GetBufferの対である為こちらに記述
        [DllImport(@"Python37.dll", EntryPoint = "PyBuffer_Release", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void ReleaseBuffer(IntPtr view);

        [DllImport(@"Python3.dll", EntryPoint = "PyObject_GetAttrString", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr GetAttrString(IntPtr o, string attrName);

        [DllImport(@"Python3.dll", EntryPoint = "PyObject_SetAttrString", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int SetAttrString(IntPtr o, string attrName, IntPtr v);


        public static IntPtr GetAttr(string attr)
        {
            return GetAttrString(PyImport.AddModule("__main__"), attr);
        }

        public static void SetAttr(string attr, IntPtr value)
        {
            SetAttrString(PyImport.AddModule("__main__"), attr, value);
        }
    }
}
