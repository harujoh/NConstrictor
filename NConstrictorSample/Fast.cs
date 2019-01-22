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
            py.Print("x");

            //xのすべての値に10を加算する
            py["x"] += 10;

            //xをｙに転送
            py["y"] = py["x"];

            //加算したxを表示する
            py.Print("x");

            //pytest.pyを読み込む
            PyObject pytest = PyImport.Import("pytest");

            //test内の関数calcを呼び出す
            py["x"] = pytest["calc"].Call(py["x"]);

            //関数の結果を表示する
            py.Print("x");

            Console.WriteLine("\n> pyBuffer[2, 1] += 1000 From C#");

            //Pythonの値をC#から変更するクラスを作成
            PyArray<TestType> pyBuffer = py["x"];
            pyBuffer[2, 1] += 1000;

            //c#から変更した結果を表示する
            py.Print("x");

            //加算用の配列を作る
            TestType[] addArray = { 10000, 20000, 30000, 40000 };

            //xのすべての値にaddArrayを加算する
            py["x"] += addArray;

            //加算したxを表示する
            py.Print("x");

            //セット用の配列を作る
            TestType[] setArray = { 1111, 2222, 3333, 4444 };

            //Pythonの値をC#から変更するクラスを作成
            PyArray<TestType[]> pyArrayBuffer = py["x"];

            //x[1]にsetArrayを設定する
            pyArrayBuffer[1] = setArray;

            //加算したxを表示する
            py.Print("x");

            //計算したxをC#で取得
            TestType[,] destArrayX = (TestType[,])py["x"].ToArray<TestType>();

            //Pythonで宣言したyをC#で取得
            TestType[,] destArrayY = (TestType[,])py["y"].ToArray<TestType>();

            //取得したXの中身を表示
            Console.WriteLine("\n> Console.WriteLine(x[i, j]) from C#");
            for (int i = 0; i < destArrayX.GetLength(0); i++)
            {
                for (int j = 0; j < destArrayX.GetLength(1); j++)
                {
                    Console.WriteLine(i * destArrayX.GetLength(1) + j + " : " + destArrayX[i, j]);
                }
            }

            //取得したYの中身を表示
            Console.WriteLine("\n> Console.WriteLine(y[i, j]) from C#");
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
