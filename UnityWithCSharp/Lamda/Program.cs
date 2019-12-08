using System;
using System.Collections.Generic;

namespace Lamda
{
    delegate void myDelegate(int a, int b);
    delegate int myDelegate1(int a, int b);
    delegate void myDelegate2();

    class Program
    {
        //원래 이런식으로 선언을 해왔었다.
        int add(int i, int j)
        {
            return i + j;
        }

        static void Main(string[] args)
        {
            //싱글라인.
            myDelegate1 add = (a, b) => a + b;
            myDelegate2 lamda = () => Console.WriteLine("람다식");

            Console.WriteLine("11 + 22 = {0}", add(11, 22));

            lamda();

            //다중라인.(delegate가 매개변수를 대입 받는 형식으로만, 값리턴 형식으로 X)
            myDelegate Compare = (a, b) =>
            {
                if (a > b)
                    Console.Write("{0}보다 {1}가 크다", b, a);
                else if (a < b)
                    Console.Write("{0}보다 {1}가 크다", a, b);
                else
                    Console.Write("{0}, {1}는 같다", a, b);
            };

            Compare(11, 22);

            Console.ReadLine();
            Console.ReadKey();
        }
    }
}
