using System;
using System.Collections.Generic;

namespace TryCatch
{
    class TryCatch
    {
        //finally
        static int Divide(int divisor, int dividend)
        {
            return divisor / dividend;
        }

        //throw
        //static void DoSomething(int arg)
        //{
        //    if (arg < 10)
        //        Console.WriteLine("arg : {0}", arg);
        //    else
        //        throw new Exception("arg가 10보다 큽니다.");
        //}

        static void Main(string[] args)
        {
            //Try~Catch
            //int[] arr = { 1, 2, 3 };

            //try
            //{
            //    for(int i = 0; i < 5; i++)
            //    {
            //        Console.WriteLine(arr[i]);
            //    }
            //}
            //catch(IndexOutOfRangeException e)
            //{
            //    Console.WriteLine("예외가 발생했습니다. : {0}", e.Message);
            //}
            ////catch (Exception e)
            ////{
            ////    Console.WriteLine("예외가 발생했습니다. : {0}", e.Message);
            ////}

            //throw
            //try
            //{
            //    DoSomething(1);
            //    DoSomething(3);
            //    DoSomething(5);
            //    DoSomething(7);
            //    DoSomething(11);
            //    DoSomething(13);
            //}
            //catch(Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            //Console.WriteLine("종료");

            //finally
            try
            {
                Console.Write("수를 입력하세요 : ");
                string temp = Console.ReadLine();
                int divisor = Convert.ToInt32(temp);

                Console.Write("수를 입력하세요 : ");
                temp = Console.ReadLine();
                int dividend = Convert.ToInt32(temp);

                Console.WriteLine("{0} / {1} = {2}", divisor, dividend, divisor / dividend);
            }
            catch(FormatException e)
            {
                Console.WriteLine("Error : " + e.Message);
                //Console.WriteLine("Error : " + e.StackTrace);
            }
            catch(DivideByZeroException e)
            {
                Console.WriteLine("Error : " + e.Message);
                //Console.WriteLine("Error : " + e.StackTrace);
            }
            finally
            {

            }

            Console.ReadKey();
            Console.ReadLine();
        }
    }
}
