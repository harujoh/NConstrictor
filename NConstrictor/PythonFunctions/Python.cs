using System;

namespace NConstrictor
{
    public struct Python : IDisposable
    {
        public static PyObject Main;

        static Python()
        {
            Py.Initialize();
            NumPy.Initialize();

            Main = PyImport.AddModule("__main__");
        }

        public static void Print(string name, bool isPrintLog = true)
        {
            if (isPrintLog) Console.WriteLine(">>> print(" + name + ")");
            PyRun.SimpleString("print(" + name + ")");
        }

        public static void PrintOnly(string name)
        {
            PyRun.SimpleString("print(" + name + ")");
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

        public void Dispose()
        {
            Py.Finalize();
        }
    }
}
