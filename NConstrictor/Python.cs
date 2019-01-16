using System;
using System.Runtime.InteropServices;

namespace NConstrictor
{
    public class Python : IDisposable
    {
        public bool IsPrintLog;
        public PyObject Main;

        public Python(bool isPrintLog = false)
        {
            IsPrintLog = isPrintLog;
            Py.Initialize();
            NumPy.Initialize();

            Main = PyImport.AddModule("__main__");
        }

        public PyObject this[string name]
        {
            get
            {
                Py.IncRef(Main);
                return Main[name];
            }

            set
            {
                Py.IncRef(Main);
                Main[name] = value;
            }
        }


        public void Send<T>(string name, Array array)
        {
            long[] dims = new long[array.Rank];

            for (int i = 0; i < dims.Length; i++)
            {
                dims[i] = array.GetLength(i);
            }

            GCHandle handle = GCHandle.Alloc(array, GCHandleType.Pinned);

            PyObject npArray = NumPy.PyArrayNewFromDescr(NumPy.PyArrayType, GetDtype<T>(), array.Rank, dims, null, handle.AddrOfPinnedObject(), NpConsts.NPY_ARRAY_CARRAY, PyObject.Zero);
            Main[name] = npArray;

            handle.Free();
        }

        public Array GetArray<T>(PyObject o)
        {
            return new PyBuffer<T>(o).GetArray();
        }

        public void Run(string code)
        {
            if (IsPrintLog) Console.WriteLine(">>> " + code);
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

        public void Dispose()
        {
            Py.Finalize();
        }
    }
}
