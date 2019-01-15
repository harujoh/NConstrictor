using System;

namespace NConstrictor
{
    public class PyFunc : IDisposable
    {
        private readonly IntPtr _functionPointer;

        public PyFunc(IntPtr functionPointer)
        {
            _functionPointer = functionPointer;
            Py.IncRef(_functionPointer);
        }

        public IntPtr Call(params string[] argNames)
        {
            IntPtr[] param = new IntPtr[argNames.Length];

            for (int i = 0; i < param.Length; i++)
            {
                param[i] = PyObject.GetAttr(argNames[i]);
            }

            IntPtr args = PyTuple.Pack(param);
            IntPtr result = PyObject.CallObject(_functionPointer, args);
            Py.DecRef(args);
            Py.DecRef(_functionPointer);

            return result;
        }

        public void Dispose()
        {
            Py.DecRef(_functionPointer);
        }
    }
}
