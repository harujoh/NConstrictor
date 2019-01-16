using System;

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

        public PyValue GetPyValue()
        {
            return new PyValue(Main);
        }

        public void Dispose()
        {
            Py.Finalize();
        }
    }
}
