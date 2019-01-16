using System;

namespace NConstrictorSample
{
    class Program
    {
        static void Main(string[] args)
        {
            //簡単に書ける版
            Easy.Run();

            //高速動作版
            Fast.Run();

            Console.WriteLine("Done.");
            Console.Read();
        }
    }
}
