using System;
using NConstrictor;

using TestType = System.Single;
//using TestType = System.Int32;
//using TestType = System.Double;

namespace NConstrictorSample
{
    class Fast
    {
        public static void Run()
        {
            Python py = new Python(true);

            TestType[,] array =
            {
                {0,  1,  2,  3},
                {4,  5,  6,  7},
                {8,  9, 10, 11}
            };

            //arrayの内容をxとしてPythonに送信
            py["x"] = array;

            //pythonで受信したxを表示する
            py.Run("print(x)");

            //xのすべての値に10を加算する
            py["x"] += 10;

            //xをｙに転送
            py["y"] = py["x"];

            //加算したxを表示する
            py.Run("print(x)");

            //pytest.pyを読み込む
            PyObject pytest = PyImport.Import("pytest");

            //test内の関数calcを呼び出す
            py["x"] = pytest["calc"].Call(py["x"]);

            //関数の結果を表示する
            py.Run("print(x)");

            Console.WriteLine("\n> pyBuffer[2, 1] += 10 From C#");

            //Pythonの値をC#から変更するクラスを作成
            PyBuffer<TestType> pyBuffer = new PyBuffer<TestType>(py["x"]);
            pyBuffer[2, 1] += 10;

            //c#から変更した結果を表示する
            py.Run("print(x)");

            //計算したxをC#で取得
            TestType[,] destArrayX = (TestType[,])py["x"].GetArray<TestType>();

            //Pythonで宣言したyをC#で取得
            TestType[,] destArrayY = (TestType[,])py["y"].GetArray<TestType>();

            //取得したXの中身を表示
            Console.WriteLine("\n> Console.WriteLine(x[i,j]) from C#");
            for (int i = 0; i < destArrayX.GetLength(0); i++)
            {
                for (int j = 0; j < destArrayX.GetLength(1); j++)
                {
                    Console.WriteLine(i * destArrayX.GetLength(1) + j + " : " + destArrayX[i, j]);
                }
            }

            //取得したYの中身を表示
            Console.WriteLine("\n> Console.WriteLine(y[i,j]) from C#");
            for (int i = 0; i < destArrayY.GetLength(0); i++)
            {
                for (int j = 0; j < destArrayY.GetLength(1); j++)
                {
                    Console.WriteLine(i * destArrayY.GetLength(1) + j + " : " + destArrayY[i, j]);
                }
            }
        }
    }
}
