using System;
using System.Collections.Generic;

namespace NConstrictor
{
    public class Python : IDisposable
    {
        public static PyObject Main;

        public static PyObject Os;
        public static PyObject Sys;
        public static PyObject Warnings;
        public static PyObject Logging;
        public static PyObject Builtins;

        private static Dictionary<PyObject, string> _names = new Dictionary<PyObject, string>();
        private static ulong _nameCounter = 0;

        public Python()
        {
            Initialize();
        }

        public static void Initialize(bool showVersion = false)
        {
            Py.Initialize();
            NumPy.Initialize();

            Main = PyImport.AddModule("__main__");

            Sys = PyImport.ImportModule("sys");
            Main["sys"] = Sys;

            Os = PyImport.ImportModule("os");
            Main["os"] = Os;
            Logging = PyImport.ImportModule("logging");
            Main["logging"] = Logging;
            Warnings = PyImport.ImportModule("warnings");
            Main["warnings"] = Warnings;

            Builtins = PyImport.ImportModule("builtins");
            Main["builtins"] = Builtins;

            Py.None = Builtins["None"];

            if(showVersion)
            {
                Console.WriteLine("Python version : " + Python.Sys["version"]);
                var vers = ((PyTuple)Python.Sys["version_info"]).UnPack();
                Console.WriteLine($"sys.version_info(major = {(long)vers[0]}, minor = {(long)vers[1]}, micro = {(long)vers[2]}, releaselevel = '{vers[3]}', serial = {(long)vers[4]})" + Environment.NewLine);
            }
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

            SetNamelessObject(pyObject);
            PyRun.SimpleString("print(" + _names[pyObject] + ")");
        }

        public static void Print(params PyObject[] pyObjects)
        {
            Console.WriteLine(">>> print()");
            SimplePrint(pyObjects);
        }

        public static void SimplePrint(params PyObject[] pyObjects)
        {
            SetNamelessObject(pyObjects[0]);
            string str = _names[pyObjects[0]];
            for (int i = 1; i < pyObjects.Length; i++)
            {
                SetNamelessObject(pyObjects[i]);
                str += "," + _names[pyObjects[i]];
            }

            PyRun.SimpleString("print(" + str + ")");
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

        static void SetNamelessObject(PyObject value)
        {
            if (!_names.ContainsKey(value))
            {
                string name = "Py" + _nameCounter;

                _names.Add(value, name);
                _nameCounter++;

                Main[name] = value;
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
