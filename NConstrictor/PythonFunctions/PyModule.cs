using System;

namespace NConstrictor
{
    public class PyModule : IDisposable
    {
        private readonly IntPtr _modulePointer;

        public PyModule(string moduleName)
        {
            _modulePointer = PyImport.Import(moduleName);
            Py.IncRef(_modulePointer);
        }

        public PyFunc this[string functionName]
        {
            get
            {
                PyFunc result = new PyFunc(PyObject.GetAttrString(_modulePointer, functionName));
                Py.DecRef(_modulePointer);
                return result;
            }
        }

        public void Dispose()
        {
            Py.DecRef(_modulePointer);
        }
    }
}
