using System;

namespace BitFlagOperator
{
    public enum COLOR
    {
        NONE    = 0,
        WHITE   = 2,
        BLACK   = 4,
        RED     = 8,
        BLUE    = 16,
        GREEN   = 32,
    }

    class BitFlag
    {
        static void MyMethod(COLOR color)
        {
            if ((color & COLOR.WHITE) == COLOR.WHITE)
                Console.WriteLine("하얀색이 포함되어 있다\n");

            if ((color & COLOR.BLACK) == COLOR.BLACK)
                Console.WriteLine("검정색이 포함되어 있다\n");

            if ((color & COLOR.RED) == COLOR.RED)
                Console.WriteLine("빨색이 포함되어 있다\n");

            if ((color & COLOR.BLUE) == COLOR.BLUE)
                Console.WriteLine("파란색이 포함되어 있다\n");

            if ((color & COLOR.GREEN) == COLOR.GREEN)
                Console.WriteLine("녹색이 포함되어 있다\n");

            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            COLOR color = COLOR.RED | COLOR.BLUE | COLOR.GREEN;

            MyMethod(color);
        }
    }
}
