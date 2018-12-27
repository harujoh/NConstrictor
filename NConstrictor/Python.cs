using System;

namespace NConstrictor
{
    public unsafe class Python
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

            if (typeof(T) == typeof(int))
            {
                //一次元の長さの配列を用意
                int[] intArray = new int[array.Length];
                //一次元化
                Buffer.BlockCopy(array, 0, intArray, 0, sizeof(int) * intArray.Length);

                fixed (void* address = intArray)
                {
                    IntPtr npArray = NumPy.PyArrayNewFromDescr(NumPy.PyArrayType, Dtype.Int32, array.Rank, dims, null, address, NpConsts.NPY_ARRAY_CARRAY, IntPtr.Zero);
                    PyObject.SetAttr(name, npArray);
                }
            }
            else if(typeof(T) == typeof(float))
            {
                //一次元の長さの配列を用意
                float[] intArray = new float[array.Length];
                //一次元化
                Buffer.BlockCopy(array, 0, intArray, 0, sizeof(float) * intArray.Length);

                fixed (void* address = intArray)
                {
                    IntPtr npAarray = NumPy.PyArrayNewFromDescr(NumPy.PyArrayType, Dtype.Float32, array.Rank, dims, null, address, NpConsts.NPY_ARRAY_CARRAY, IntPtr.Zero);
                    PyObject.SetAttr(name, npAarray);
                }
            }
            else if (typeof(T) == typeof(double))
            {
                //一次元の長さの配列を用意
                double[] intArray = new double[array.Length];
                //一次元化
                Buffer.BlockCopy(array, 0, intArray, 0, sizeof(double) * intArray.Length);

                fixed (void* address = intArray)
                {
                    IntPtr npArray = NumPy.PyArrayNewFromDescr(NumPy.PyArrayType, Dtype.Float64, array.Rank, dims, null, address, NpConsts.NPY_ARRAY_CARRAY, IntPtr.Zero);
                    PyObject.SetAttr(name, npArray);
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public T[] Get<T>(string name)
        {
            PyBuffer<T> pyBuffer = new PyBuffer<T>(name);
            T[] result = pyBuffer.Data.ToArray();
            pyBuffer.Dispose();
            return result;
        }

        public void WriteLine(string code)
        {
            if(IsPrintLog)Console.WriteLine(">>> " + code);
            PyRun.SimpleString(code);
        }
    }
}
