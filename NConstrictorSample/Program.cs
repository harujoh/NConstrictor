﻿using System;
using NConstrictor;
//using TestType = System.Int32;
using TestType = System.Single;
//using TestType = System.Double;

namespace NConstrictorSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Python py = new Python(true);

            TestType[,] array =
            {
                {0,  1,  2,  3},
                {4,  5,  6,  7},
                {8,  9, 10, 11}
            };

            //arrayの内容をxとしてPythonに送信
            py.Send<TestType>("x", array);

            //pythonで受信したxを表示する
            py.WriteLine("print(x)");

            //xのすべての値に10を加算する
            py.WriteLine("x = x + 10");

            //xをｙに転送
            py.WriteLine("y = x");

            //加算したxを表示する
            py.WriteLine("print(x)");

            //test.pyを読み込む
            py.WriteLine("import pytest");

            //test内の関数calcを呼び出す
            py.WriteLine("x = pytest.calc(x)");

            //関数の結果を表示する
            py.WriteLine("print(x)");

            Console.WriteLine("\n> pyBufferGen[0] += 10 From C#");

            //Pythonの値をC#から変更するクラスを作成
            PyBufferGen<TestType> pyBufferGen = new PyBufferGen<TestType>("x");
            pyBufferGen[0] += 10;

            //後片付けが必要
            pyBufferGen.Dispose();

            //c#から変更した結果を表示する
            py.WriteLine("print(x)");

            //計算したxをC#で取得
            TestType[] resultX = py.Get<TestType>("x");

            //Pythonで宣言したyをC#で取得
            TestType[] resultY = py.Get<TestType>("y");

            //取得した値を転送する
            TestType[,] destArrayX = new TestType[3, 4];
            Buffer.BlockCopy(resultX, 0, destArrayX, 0, sizeof(TestType) * resultX.Length);

            //取得した値を転送する
            TestType[,] destArrayY = new TestType[3, 4];
            Buffer.BlockCopy(resultY, 0, destArrayY, 0, sizeof(TestType) * resultY.Length);

            Console.WriteLine("\n> Console.WriteLine(x[i,j]) from C#");
            //取得したXの中身を表示
            for (int i = 0; i < destArrayX.GetLength(0); i++)
            {
                for (int j = 0; j < destArrayX.GetLength(1); j++)
                {
                    Console.WriteLine(i * destArrayX.GetLength(1) + j + " : " + destArrayX[i, j]);
                }
            }

            Console.WriteLine("\n> Console.WriteLine(y[i,j]) from C#");
            //取得したYの中身を表示
            for (int i = 0; i < destArrayY.GetLength(0); i++)
            {
                for (int j = 0; j < destArrayY.GetLength(1); j++)
                {
                    Console.WriteLine(i * destArrayY.GetLength(1) + j + " : " + destArrayY[i, j]);
                }
            }

            Console.Read();
        }
    }
}