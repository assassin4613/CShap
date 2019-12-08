using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectEx
{
    class ObjectEx
    {
        

        static void Main(string[] args)
        {
            //문자열
            //string a = "크리스마스에 머하세요?";
            //string b = "집에 있어요!!";

            //string c = a + b;

            //Console.WriteLine(a);
            //Console.WriteLine(b);

            //Console.WriteLine(c);

            //object
            //object a = 1;
            //object b = 1.1f;
            //object c = 'a';
            //object d = "안녕하세요";
            //object e = true;

            //Console.WriteLine(a);
            //Console.WriteLine(b);
            //Console.WriteLine(c);
            //Console.WriteLine(d);
            //Console.WriteLine(e);

            //Object 형식이 GetType이라는 것을 호출하여 형식을 알아오는 것이다
            //ToString() 모든 데이터를 String형식으로 바꿔달라 즉, 타입이라는 형식자체를 문자열로 표기한다.
            //ToString()은 차후에 로그관련에 많이 사용될 수 있다(경험)
            //Console.WriteLine("type:{0} ", e.GetType().ToString());

            //boxing & unboxing
            //int a = 123;

            //object b = a; // a에 담긴값을 박싱해서 힙에 저장
            ////object b = a; //직접 선언하지 않아도 암묵적으로 이루어진다.

            //int c = (int)b;// b에 담긴값을 언박싱해서 스택에 저장.

            //Console.WriteLine(a);
            //Console.WriteLine(b);
            //Console.WriteLine(c);

            //Console.ReadKey();
        }
    }
}
