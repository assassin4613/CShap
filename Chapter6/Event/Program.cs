using System;
using System.Collections.Generic;

namespace Event
{
    //delegate 선언
    delegate void myDelegate(int a);

    //이벤트매니저 클래스
    class EventManager
    {
        //이벤트 타입으로 선언
        public event myDelegate eventCall;

        public void NumberCheck(int num)
        {
            //콜백 호출
            if (num % 2 == 0)
                eventCall(num);
        }
    }

    class Event
    {
        //이벤트에 등록이 될 함수.
        static void EventNumber(int num)
        {
            Console.WriteLine("{0}는 짝수", num);
        }

        static void Main(string[] args)
        {
            //매니저 생성
            EventManager eventManger = new EventManager();

            //이벤트에 delegate를 체인한다.
            eventManger.eventCall += new myDelegate(EventNumber);

            for (int i = 1; i < 10; i++)
                eventManger.NumberCheck(i);



            Console.ReadKey();
            Console.ReadLine();
        }
    }
}
