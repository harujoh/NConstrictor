using System;

namespace NConstrictor
{
    public unsafe class Python
    {
        public bool IsOutlog;

        public Python(bool isOutlog = false)
        {
            IsOutlog = isOutlog;
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
                    IntPtr npAarray = NumPy.PyArrayNewFromDescr(NumPy.PyArrayType, Dtype.Int32, array.Rank, dims, null, address, NpConsts.NPY_ARRAY_CARRAY, IntPtr.Zero);
                    PyObject.SetAttr(name, npAarray);
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
                    IntPtr npAarray = NumPy.PyArrayNewFromDescr(NumPy.PyArrayType, Dtype.Float64, array.Rank, dims, null, address, NpConsts.NPY_ARRAY_CARRAY, IntPtr.Zero);
                    PyObject.SetAttr(name, npAarray);
                }
            }
        }

        public T[] Get<T>(string name)
        {
            PyBufferGen<T> pyBufferGen = new PyBufferGen<T>(name);
            T[] resut = pyBufferGen.Data.ToArray();
            pyBufferGen.Dispose();
            return resut;
        }

        public void WriteLine(string code)
        {
            if(IsOutlog)Console.WriteLine(">>> " + code);
            PyRun.SimpleString(code);
        }
    }
}
