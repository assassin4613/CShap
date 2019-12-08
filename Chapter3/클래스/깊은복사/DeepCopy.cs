using System;

namespace Consructor
{
    class MyClass
    {
        public int MyField1;
        public int MyField2;

        public MyClass DeepCopy()
        {
            MyClass newCopy = new MyClass();
            newCopy.MyField1 = this.MyField1;
            newCopy.MyField2 = this.MyField2;

            return newCopy;
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            Console.WriteLine("얕은 복사");

            MyClass source = new MyClass();
            source.MyField1 = 10;
            source.MyField2 = 20;

            MyClass target = source;
            target.MyField2 = 30;

            Console.WriteLine("{0} {1}", source.MyField1, source.MyField2);
            Console.WriteLine("{0} {1}", target.MyField1, target.MyField2);

            Console.WriteLine("깊은 복사");

            MyClass source2 = new MyClass();
            source2.MyField1 = 10;
            source2.MyField2 = 20;

            MyClass target2 = source2.DeepCopy();
            target2.MyField2 = 30;

            Console.WriteLine("{0} {1}", source2.MyField1, source2.MyField2);
            Console.WriteLine("{0} {1}", target2.MyField1, target2.MyField2);

            Console.ReadKey();
        }
    }
}