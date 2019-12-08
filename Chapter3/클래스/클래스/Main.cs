using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consructor
{
    class MainApp
    {
        static void Main(string[] args)
        {
            Cat kitty = new Cat("키티", "하얀색");
            kitty.SetKeyName("12323");
            kitty.Meow();
            Console.WriteLine("{0} : {1} : {2}", kitty.Name, kitty.Color, kitty.GetKeyName());

            Cat nero = new Cat("네로", "검은색");
            nero.SetKeyName("78468");
            nero.Meow();
            Console.WriteLine("{0} : {1} : {2}", nero.Name, nero.Color, nero.GetKeyName());

            //소멸자 실행을 위해 CTRL + F5로 창유지를 선택하자. 물론 옵션이나 설정에따라 빌드실행이 다를 수 있다.
        }
    }
}
