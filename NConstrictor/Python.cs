using System;
using System.Runtime.InteropServices;

namespace NConstrictor
{
    public class Python
    {
        public bool IsPrintLog;

        public Python(bool isPrintLog = false)
        {
            IsPrintLog = isPrintLog;
            Py.Initialize();
            NumPy.Initialize();
        }

        public void Send<T>(string name, Array array)
        {
            long[] dims = new long[array.Rank];

            for (int i = 0; i < dims.Length; i++)
            {
                dims[i] = array.GetLength(i);
            }

            GCHandle handle = GCHandle.Alloc(array, GCHandleType.Pinned);

            IntPtr npArray = NumPy.PyArrayNewFromDescr(NumPy.PyArrayType, GetDtype<T>(), array.Rank, dims, null, handle.AddrOfPinnedObject(), NpConsts.NPY_ARRAY_CARRAY, IntPtr.Zero);
            PyObject.SetAttr(name, npArray);

            handle.Free();
        }

        public T[] Get<T>(string name)
        {
            PyBuffer<T> pyBuffer = new PyBuffer<T>(name);
            return pyBuffer.Data.ToArray();
        }

        public void WriteLine(string code)
        {
            if(IsPrintLog)Console.WriteLine(">>> " + code);
            PyRun.SimpleString(code);
        }

        IntPtr GetDtype<T>()
        {
            if (typeof(T) == typeof(int))
            {
                return Dtype.Int32;
            }
            else if (typeof(T) == typeof(float))
            {
                return Dtype.Float32;
            }
            else if (typeof(T) == typeof(double))
            {
                return Dtype.Float64;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

    }
}
