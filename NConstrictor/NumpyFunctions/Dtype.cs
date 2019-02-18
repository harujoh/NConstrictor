using System;

namespace NConstrictor
{
    public static class Dtype
    {
        public static IntPtr Bool;// = 0;
        public static IntPtr Int8;// = 1;
        public static IntPtr Uint8;// = 2;
        public static IntPtr Int16;// = 3;
        public static IntPtr Uint16;// = 4;
        public static IntPtr Int32;//  = 5 or 7;
        public static IntPtr Uint32;// = 6 or 8;
        public static IntPtr Int64;// = 9;
        public static IntPtr Uint64;// = 10;
        public static IntPtr Float32;// = 11;
        public static IntPtr Float64;// = 12 or 13;
        public static IntPtr Complex64;// = 14;
        public static IntPtr Complex128;// = 15 or 16;
        public static IntPtr Object;// = 17;
        public static IntPtr S0;// = 18;
        public static IntPtr U0;// = 19;
        public static IntPtr V0;// = 20;
        public static IntPtr Datetime64;// = 21;
        public static IntPtr Timedelta64;// = 22;

        static Dtype()
        {
            Bool = NumPy.PyArrayDescrFromType(0);
            Int8 = NumPy.PyArrayDescrFromType(1);
            Uint8 = NumPy.PyArrayDescrFromType(2);
            Int16 = NumPy.PyArrayDescrFromType(3);
            Uint16 = NumPy.PyArrayDescrFromType(4);
            Int32 = NumPy.PyArrayDescrFromType(5);
            Uint32 = NumPy.PyArrayDescrFromType(6);
            Int64 = NumPy.PyArrayDescrFromType(9);
            Uint64 = NumPy.PyArrayDescrFromType(10);
            Float32 = NumPy.PyArrayDescrFromType(11);
            Float64 = NumPy.PyArrayDescrFromType(12);
            Complex64 = NumPy.PyArrayDescrFromType(14);
            Complex128 = NumPy.PyArrayDescrFromType(15);
            Object = NumPy.PyArrayDescrFromType(17);
            S0 = NumPy.PyArrayDescrFromType(18);
            U0 = NumPy.PyArrayDescrFromType(19);
            V0 = NumPy.PyArrayDescrFromType(20);
            Datetime64 = NumPy.PyArrayDescrFromType(21);
            Timedelta64 = NumPy.PyArrayDescrFromType(22);
        }

        public static IntPtr GetDtype<T>()
        {
            if (typeof(T) == typeof(Int32))
            {
                return Dtype.Int32;
            }
            else if (typeof(T) == typeof(UInt32))
            {
                return Dtype.Uint32;
            }
            else if (typeof(T) == typeof(Int16))
            {
                return Dtype.Int16;
            }
            else if (typeof(T) == typeof(UInt16))
            {
                return Dtype.Uint16;
            }
            else if (typeof(T) == typeof(Int64))
            {
                return Dtype.Int64;
            }
            else if (typeof(T) == typeof(UInt64))
            {
                return Dtype.Uint64;
            }
            else if (typeof(T) == typeof(Single))
            {
                return Dtype.Float32;
            }
            else if (typeof(T) == typeof(Double))
            {
                return Dtype.Float64;
            }
            else if (typeof(T) == typeof(Boolean))
            {
                return Dtype.Bool;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public static IntPtr GetDtype(Type t)
        {
            if (t == typeof(Int32))
            {
                return Dtype.Int32;
            }
            else if (t == typeof(UInt32))
            {
                return Dtype.Uint32;
            }
            else if (t == typeof(Int16))
            {
                return Dtype.Int16;
            }
            else if (t == typeof(UInt16))
            {
                return Dtype.Uint16;
            }
            else if (t == typeof(Int64))
            {
                return Dtype.Int64;
            }
            else if (t == typeof(UInt64))
            {
                return Dtype.Uint64;
            }
            else if (t == typeof(Single))
            {
                return Dtype.Float32;
            }
            else if (t == typeof(Double))
            {
                return Dtype.Float64;
            }
            else if (t == typeof(Boolean))
            {
                return Dtype.Bool;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
