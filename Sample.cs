using System;
using NConstrictor;

namespace NConstrictorSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Python.Initialize();

            PyArray<float> x = new[,]
            {
                {0.0f,  1.0f,  2.0f,  3.0f},
                {4.0f,  5.0f,  6.0f,  7.0f},
                {8.0f,  9.0f, 10.0f, 11.0f}
            };
            Python.Print(x);

            x[2, 1] += 50.0f;
            Python.Print(x);

            x += 50.0f;
            Python.Print(x);

            x += new[] { 100.0f, 200.0f, 300.0f, 400.0f };
            Python.Print(x);

            PyArray<float[]> pyArrayBuffer = new PyArray<float[]>(x);
            pyArrayBuffer[1] = new[] { 10.0f, 20.0f, 30.0f, 40.0f };
            Python.Print(x);

            Console.Read();
        }
    }
}
