using System;
using NConstrictor;

namespace NConstrictorSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Python python = new Python(true);
            dynamic py = new PyDynamic(python);

            float[,] array =
            {
                {0.0f,  1.0f,  2.0f,  3.0f},
                {4.0f,  5.0f,  6.0f,  7.0f},
                {8.0f,  9.0f, 10.0f, 11.0f}
            };
            py.x = array;
            python.Print("x");


            PyArray<float> pyBuffer = py.x;
            pyBuffer[2, 1] += 100.0f;
            python.Print("x");


            py.x += 5000;
            python.Print("x");


            py.x += new [] { 10000.0f, 20000.0f, 30000.0f, 40000.0f };
            python.Print("x");


            PyArray<float[]> pyArrayBuffer = py.x;
            pyArrayBuffer[1] = new []{ 1111.0f, 2222.0f, 3333.0f, 4444.0f };
            python.Print("x");


            Console.Read();
        }
    }
}
