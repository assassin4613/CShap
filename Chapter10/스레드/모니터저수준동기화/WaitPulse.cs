using System;
using System.Threading;

namespace WaitPulse
{
    class Counter
    {
        const int LOOP_COUNT = 1000;

        // lockedCount와 count는 스레드가 블록될 조건을 검사하기 위해 사용된다.
        // lockedCount는 count 변수를 다른 스레드가 사용하고 있는지를 판별하기 위해
        // count는 각 스레드가 너무 오랫동안 count 변수를 혼자 사용하는 것을 막기 위해 사용.
        readonly object thisLock;
        bool lockedCount = false;

        private int count;
        public int Count
        {
            get { return count; }
        }

        public Counter()
        {
            thisLock = new object();
            count = 0;
        }

        public void Increase()
        {
            int loopCount = LOOP_COUNT;

            while (loopCount-- > 0)
            {
                lock (thisLock)
                {
                    // Count가 0보다 크거나 lockedCount가 다른 스레드에 의해 
                    // true로 바뀌어 있으면 현재 스레드를 블록시킨다.
                    // 다른 스레드가 Pulse() 메소드를 호출해줄 때까지는 WaitSleepJoin상태로 남는다.
                    while (count > 0 || lockedCount == true)
                        Monitor.Wait(thisLock);

                    lockedCount = true;
                    count++;
                    Console.WriteLine("Increase : {0}", count);
                    // lockedCount를 false로 만든 뒤에 다른 스레드를 깨운다.
                    // 깨어난 스레드들은 53행에 있는 while 문의 조건 검사를 통해 
                    // 다시 Wait()을 호출할지, 그 다음 코드를 실행할지를 결정한다. 
                    lockedCount = false;

                    Monitor.Pulse(thisLock);
                }
            }
        }

        public void Decrease()
        {
            int loopCount = LOOP_COUNT;

            while (loopCount-- > 0)
            {
                lock (thisLock)
                {
                    while (count < 0 || lockedCount == true)
                        Monitor.Wait(thisLock);

                    lockedCount = true;
                    count--;
                    Console.WriteLine("Decrease : {0}", count);
                    lockedCount = false;

                    Monitor.Pulse(thisLock);
                }
            }
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            Counter counter = new Counter();

            Thread incThread = new Thread( new ThreadStart(counter.Increase));
            Thread decThread = new Thread(new ThreadStart(counter.Decrease));

            incThread.Start();
            decThread.Start();

            incThread.Join();
            decThread.Join();

            Console.WriteLine("최종 결과 : {0}", counter.Count);

            Console.ReadKey();
        }
    }
}