using System;
using System.Runtime.InteropServices;

namespace NConstrictor
{
    public class NumPy
    {
        private static PyObject _numpy;
        private static PyObject _cApi;
        private static IntPtr _pyArrayAPI;
        private static IntPtr[] _pyArrayAPIs = new IntPtr[303];//multiarray_funcs_api.Length

        public delegate IntPtr PyArrayDescrFromTypeDelegate(int typenum);
        public static PyArrayDescrFromTypeDelegate PyArrayDescrFromType;

        public delegate PyObject PyArrayFromBufferDelegate(PyObject buf, IntPtr dtype, int count, int offset);
        public static PyArrayFromBufferDelegate PyArrayFromBuffer;

        public delegate PyObject PyArrayNewFromDescrDelegate(IntPtr subtype, IntPtr descr, int nd, long[] dims, long[] strides, IntPtr data, int flags, PyObject obj);
        public static PyArrayNewFromDescrDelegate PyArrayNewFromDescr;

        public delegate IntPtr PyArrayGetPtrDelegate(PyObject aobj, long[] ind);
        public static PyArrayGetPtrDelegate PyArrayGetPtr;

        public delegate IntPtr PyArrayTakeFromDelegate(PyObject selfArray, PyObject indices, int axis, PyObject retArray, NPY_CLIPMODE clipMode);
        public static PyArrayTakeFromDelegate PyArrayTakeFrom;

        public static IntPtr PyArrayType;

        public static void Initialize()
        {
            //PyObject* numpy = PyImport_ImportModule("numpy.core.multiarray");
            _numpy = PyImport.ImportModule("numpy.core.multiarray");

            if (_numpy == IntPtr.Zero)
            {
                //PyErr.SetString(PyExc.ImportError, "numpy.core.multiarray failed to import");
                throw new Exception("numpy.core.multiarray failed to import");
            }

            _cApi = _numpy["_ARRAY_API"];

            if (_cApi == IntPtr.Zero)
            {
                //PyErr_SetString(PyExc_AttributeError, "_ARRAY_API not found");
                throw new Exception("_ARRAY_API not found");
            }

            _pyArrayAPI = PyCapsule.GetPointer(_cApi, null);
            Py.DecRef(_cApi);

            if (_pyArrayAPI == IntPtr.Zero)
            {
                //PyErr_SetString(PyExc_RuntimeError, "_ARRAY_API is NULL pointer");
                throw new Exception("_ARRAY_API is NULL pointer");
            }

            Marshal.Copy(_pyArrayAPI, _pyArrayAPIs, 0, _pyArrayAPIs.Length);

            PyArrayDescrFromType = (PyArrayDescrFromTypeDelegate)Marshal.GetDelegateForFunctionPointer(_pyArrayAPIs[MultiarrayFuncsAPI.PyArray_DescrFromType], typeof(PyArrayDescrFromTypeDelegate));
            PyArrayFromBuffer = (PyArrayFromBufferDelegate)Marshal.GetDelegateForFunctionPointer(_pyArrayAPIs[MultiarrayFuncsAPI.PyArray_FromBuffer], typeof(PyArrayFromBufferDelegate));
            PyArrayNewFromDescr = (PyArrayNewFromDescrDelegate)Marshal.GetDelegateForFunctionPointer(_pyArrayAPIs[MultiarrayFuncsAPI.PyArray_NewFromDescr], typeof(PyArrayNewFromDescrDelegate));
            PyArrayGetPtr = (PyArrayGetPtrDelegate)Marshal.GetDelegateForFunctionPointer(_pyArrayAPIs[MultiarrayFuncsAPI.PyArray_GetPtr], typeof(PyArrayGetPtrDelegate));
            PyArrayTakeFrom = (PyArrayTakeFromDelegate)Marshal.GetDelegateForFunctionPointer(_pyArrayAPIs[MultiarrayFuncsAPI.PyArray_TakeFrom], typeof(PyArrayTakeFromDelegate));

            PyArrayType = _pyArrayAPIs[MultiarrayTypesAPI.PyArray_Type];
        }
    }
}
