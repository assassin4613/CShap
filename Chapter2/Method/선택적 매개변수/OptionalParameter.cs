using System;

namespace Method
{
    class OptionalParameter
    {
        static void PrintProfile(string name, string phone = "")
        {
            Console.WriteLine("Name:{0}, Phone:{1}", name, phone);
        }

        static void Main(string[] args)
        {
            PrintProfile("나연");
            PrintProfile("다현", "010-123-1234");
            PrintProfile("모모");
            PrintProfile("사나", "010-789-7890");

            Console.ReadKey();
        }
    }
}
