using System;
using System.Collections.Generic;

namespace NConstrictor
{
    public class Python : IDisposable
    {
        public static PyObject Main;

        public static PyObject Sys;
        public static PyObject Builtins;

        private static Dictionary<PyObject, string> _names = new Dictionary<PyObject, string>();
        private static ulong _nameCounter = 0;

        public Python()
        {
            Initialize();
        }

        public static void Initialize()
        {
            Py.Initialize();
            NumPy.Initialize();

            Main = PyImport.AddModule("__main__");

            Sys = PyImport.AddModule("sys");
            Builtins = PyImport.AddModule("builtins");

            Py.None = Builtins["None"];
        }

        public PyObject this[string name]
        {
            get
            {
                return Main[name];
            }

            set
            {
                Main[name] = value;
            }
        }

        public static void Print(string name, bool isPrintLog = true)
        {
            if (isPrintLog) Console.WriteLine(">>> print(" + name + ")");
            PyRun.SimpleString("print(" + name + ")");
        }

        public static void Print(PyObject pyObject, bool isPrintLog = true)
        {
            if (isPrintLog) Console.WriteLine(">>> print()");
            PyRun.SimpleString("print(" + _names[pyObject] + ")");
        }

        public static void Run(string code, bool isPrintLog = true)
        {
            if (isPrintLog) Console.WriteLine(">>> " + code);
            PyRun.SimpleString(code);
        }

        public static void RunOnly(string code)
        {
            PyRun.SimpleString(code);
        }

        public static PyObject GetNamelessObject(PyObject value)
        {
            if (!_names.ContainsKey(value))
            {
                string name = "Py" + _nameCounter;

                _names.Add(value, name);
                _nameCounter++;

                Main[name] = value;

                return Main[name];
            }
            else
            {
                return value;
            }
        }

        public static string GetName(PyObject value)
        {
            return _names[value];
        }

        public void Dispose()
        {
            Py.Finalize();
        }
    }
}
