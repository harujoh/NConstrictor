using System;

namespace NConstrictorSample
{
    class Program
    {
        static void Main(string[] args)
        {
            //簡単に書ける版
            Console.WriteLine("【 Start Easy 】");
            Easy.Run();

            //高速動作版
            Console.WriteLine("【 Start Fast 】");
            Fast.Run();

            Console.WriteLine("Done.");
            Console.Read();
        }
    }
}
