using System.Runtime.CompilerServices;

namespace NConstrictor
{
    public struct PyMain
    {
        private PyObject _mainModule;

        public PyObject this[string name]
        {
            get
            {
                Py.IncRef(_mainModule);
                return _mainModule[name];
            }

            set
            {
                Py.IncRef(_mainModule);
                _mainModule[name] = value;
            }
        }

        public static implicit operator PyMain(PyObject main)
        {
            return Unsafe.As<PyObject, PyMain>(ref main);
        }
    }
}
