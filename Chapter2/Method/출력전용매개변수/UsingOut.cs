using System;

namespace Method
{
    class UsingOut
    {
        static int Divide(int a, int b, out int quotient, out int remainder)
        {
            quotient = a / b;
            remainder = a % b;

            return a + b;
        }

        static void Main(string[] args)
        {
            int a = 20;
            int b = 3;
            int Quotient;
            int Remainder;
            int Sum;

            Sum = Divide(a, b, out Quotient, out Remainder);

            Console.WriteLine("a: {0}, b: {1}\nSum: {2}\na / b: {3}\na % b: {4}", a, b, Sum, Quotient, Remainder);

            Console.ReadKey();
        }
    }
}
