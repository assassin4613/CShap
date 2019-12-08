using System;

namespace BasicAttribute
{
    class MyClass
    {
        [Obsolete("OldMethod는 폐기되었습니다. NewMethod()를 이용하세요.")]
        public void OldMethod()
        {
            Console.WriteLine("I'm old");
        }

        public void NewMethod()
        {
            Console.WriteLine("I'm new");
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            MyClass obj = new MyClass();

            obj.OldMethod(); // 이 코드에 오류 경고가 뜨고 오류창이나 녹샐줄로 경고 표시를 해준다.
            obj.NewMethod();

            Console.ReadKey();
        }
    }
}
