using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consructor
{
    class Global
    {
        public static int Count = 0;

        public static int Caculator(int a, int b)
        {
            return a + b;
        }
    }

    class ClassA
    {
        public int a = 11;

        public ClassA()
        {
            Global.Count++;
        }
    }

    class ClassB
    {
        public int b = 22;

        public ClassB()
        {
            Global.Count++;
        }
    }

    class StaticField
    {
        static void Main()
        {
            Console.WriteLine("Global.Count : {0}", Global.Count);

            new ClassA();
            new ClassA();
            new ClassB();
            new ClassB();

            Console.WriteLine("Global.Count : {0}", Global.Count);

            int Sum = 0;
            ClassA classA = new ClassA();
            ClassB classB = new ClassB();

            Sum = Global.Caculator(classA.a, classB.b);

            Console.WriteLine("{0} + {1} = {2}", classA.a, classB.b, Sum);

            Console.ReadKey();
        }
    }
}
