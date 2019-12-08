using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parameter
{
    class Paremeter
    {   //ref, out
        static void Divide(int a, int b, out int quotient, out int remainder)
        {
            quotient = a / b;
            remainder = a % b;
        }

        //params Type[] args
        static int Plus(params int[] args)
        {
            int sum = 0;

            for (int i = 0; i < args.Length; i++)
            {
                sum += args[i];
            }

            return sum;
        }

        //선택적 매개변수
        static void Profile(string name = "", string num = "")
        {
            Console.WriteLine("name : {0}, num : {1}", name, num);
        }

        static void Main(string[] args)
        {

            //ref, out
            int a = 20;
            int b = 3;
            int c;
            int d;

            Divide(a, b, out c, out d);

            Console.WriteLine("a : {0}, b : {1}, c : {2}, d : {3}", a, b, c, d);

            //params Type[] args
            Console.WriteLine("Sum : {0}", Plus(1, 2, 3, 4, 5, 6, 7, 8, 9, 10));

            //선택적 매개변수
            Profile("Soul", "01091271594");
            Profile("Seek");
        }
    }
}
