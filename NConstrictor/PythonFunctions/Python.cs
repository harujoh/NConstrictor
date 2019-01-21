using System;

namespace NConstrictor
{
    public class Python : IDisposable
    {
        public bool IsPrintLog;
        private PyObject _main;

        public Python(bool isPrintLog = false)
        {
            IsPrintLog = isPrintLog;
            Py.Initialize();
            NumPy.Initialize();

            _main = PyImport.AddModule("__main__");
        }

        public PyObject this[string name]
        {
            get
            {
                Py.IncRef(_main);
                return _main[name];
            }

            set
            {
                Py.IncRef(_main);
                _main[name] = value;
            }
        }

        public void Print(string name)
        {
            if (IsPrintLog) Console.WriteLine(">>> print(" + name + ")");
            PyRun.SimpleString("print(" + name + ")");
        }

        public void Run(string code)
        {
            if (IsPrintLog) Console.WriteLine(">>> " + code);
            PyRun.SimpleString(code);
        }

        public void Dispose()
        {
            Py.Finalize();
        }

        public static implicit operator PyObject(Python i)
        {
            return i._main;
        }
    }
}
