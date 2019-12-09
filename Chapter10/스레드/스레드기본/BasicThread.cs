using System;
using System.Threading;

namespace BasicThread
{
    class MainApp
    {
        static void DoSomething()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("DoSomething : {0}", i);

                // Sleep() 메소드는 다른 스레드도 CPU를 사용할 수 있도록 CPU 점유를 내려놓는다.
                // 매개 변수는 밀리초 단위이다.
                Thread.Sleep(10);
            }
        }

        static void Main(string[] args)
        {
            Thread t1 = new Thread(new ThreadStart(DoSomething));

            Console.WriteLine("Starting thread...");
            t1.Start();

            // t1 스레드의 DoSomthing() 매소드가 실행되는 동시에 
            // 메인 스레드의 이 반복문도 실행된다.
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Main : {0}", i);        
                Thread.Sleep(10);
            }

            Console.WriteLine("Wating until thread stops...");
            t1.Join();

            Console.WriteLine("Finished");

            Console.ReadKey();
        }
    }
}
