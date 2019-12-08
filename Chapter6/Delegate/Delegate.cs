using System;
using System.Collections.Generic;

namespace Delegate
{
    //Delegate 선언 - 클래스 밖에 선언되어 있다.
    //기본 형태, CallBack형태
    delegate int myDelegate(int a, int b);

    //<T> 형태
    //delegate T myDelegate<T>(T a, T b);

    //체인
    //delegate void myDelegate();

    class Delegate
    {
        //사용할 함수들
        public static int plus(int a, int b)
        {
            return a + b;
        }

        public static int minus(int a, int b)
        {
            return a - b;
        }

        public static int multiply(int a, int b)
        {
            return a * b;
        }

        //계산기 - 기본형태
        public static void Calculator(int a, int b, myDelegate dele)
        {
            Console.WriteLine(dele(a, b));
        }

        //계산기 - <T>
        //public static int plus(int a, int b)
        //{
        //    return a + b;
        //}

        //public static float plus(float a, float b)
        //{
        //    return a + b;
        //}

        //public static double plus(double a, double b)
        //{
        //    return a + b;
        //}

        //public static void Calculator<T>(T a, T b, myDelegate<T> dele)
        //{
        //    Console.WriteLine(dele(a, b));
        //}

        //체인
        //public static void func0()
        //{
        //    Console.Write("첫번째");
        //}

        //public static void func1()
        //{
        //    Console.Write("두번째");
        //}

        //public static void func2()
        //{
        //    Console.Write("세번째");
        //}

        static void Main(string[] args)
        {
            //기본 형태
            //delegate 변수 선언
            myDelegate caculate;

            ////함수를 delegate에 선언
            caculate = new myDelegate(plus);
            int sum = caculate(11, 22);
            Console.WriteLine("11 + 22 = {0}", sum);

            ////함수를 delegate에 선언
            //caculate = new myDelegate(minus);
            //Console.WriteLine("22 - 11 = {0}", caculate(22, 11));

            //Callback 메서드
            //myDelegate Plus = new myDelegate(plus);
            //myDelegate Minus = new myDelegate(minus);
            //myDelegate Multiply = new myDelegate(multiply);

            //Calculator(11, 22, Plus);
            //Calculator(33, 22, Minus);
            //Calculator(11, 22, Multiply);

            //<T>
            //myDelegate<int> Plus_int = new myDelegate<int>(plus);
            //myDelegate<float> Plus_float = new myDelegate<float>(plus);
            //myDelegate<double> Plus_double = new myDelegate<double>(plus);

            //Calculator(11, 22, Plus_int);
            //Calculator(3.3f, 4.4f, Plus_float);
            //Calculator(5.5, 6.6, Plus_double);

            //체인
            //myDelegate dele;
            //dele = new myDelegate(func0);
            //dele += func1;
            //dele += func2;

            //dele();

            //Console.WriteLine();

            //dele -= func0;
            //dele -= func2;

            //dele();

            Console.WriteLine();

            Console.ReadKey();
            Console.ReadLine();
        }
    }
}
