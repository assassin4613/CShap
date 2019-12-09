using System;
using System.Security.Permissions;
using System.Threading;

namespace InterruptingThread
{
    class SideTask
    {
        int count;

        public SideTask(int count)
        {
            this.count = count;
        }

        public void KeepAlive()
        {
            try
            {
                Console.WriteLine("Running thread isn't gonna be interrupted");

                // SpinWait() 메소드는 원래 별로 쓸일은 없다. 
                // 이 메소드는 Sleep()과 유사하게 스레드를 하지만 Sleep()과 
                // 달리 스레드가 Running 상태를 유지하게 한다.
                // 이 메소드를 사용한 것은 65행의 Interrupt() 메소드를 호출하는
                // 시점에서 Running상태를 유지하도록 하기 위함이다.
                // 65행에서 Interrupt() 메소드가 호출된 스레드는 36행의 Thread.Sleep(10)에 의해 
                // WaitSleepJoin상태로 들어가고, 이 때 인터럽트가 발생한다.
                Thread.SpinWait(1000000000);

                while (count > 0)
                {
                    Console.WriteLine("{0} left", count--);

                    Console.WriteLine("Entering into WaitJoinSleep State...");
                    Thread.Sleep(10);
                }
                Console.WriteLine("Count : 0");
            }
            catch (ThreadInterruptedException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("Clearing resource...");
            }
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            SideTask task = new SideTask(100);
            Thread t1 = new Thread(new ThreadStart(task.KeepAlive));
            t1.IsBackground = false;

            Console.WriteLine("Starting thread...");
            t1.Start();

            Thread.Sleep(100);

            Console.WriteLine("Interrupting thread...");
            t1.Interrupt();

            Console.WriteLine("Wating until thread stops...");
            t1.Join();

            Console.WriteLine("Finished");

            Console.ReadKey();
        }
    }
}
