using System;

namespace ActionTest
{
    class MainApp
    {
        static void Main(string[] args)
        {
            Action act1 = () => Console.WriteLine("Action()");
            act1();

            int result = 0;
            Action<int> act2 = (x) => result = x * x;

            act2(3);
            Console.WriteLine("result : {0}", result);

            Action<double, double> act3 = (x, y) =>
            {
                double pi = x / y;
                Console.WriteLine("Action<T1, T2>({0}, {1}) : {2}", x, y, pi);
            };

            act3(22.0, 7.0);

            //람다는 컴파일 상황에서 결정되는 것이기 때문에 컴파일 이전에 결정되어진 것이 없다!!!
            //Activited(act3(22.0, 7.0));

            Console.ReadKey();
        }

        //void Activited(Action<double, double> action)
        //{
        //    Console.WriteLine("{0}", action);
        //}
    }
}
