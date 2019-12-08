using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Property
{
    class BirthdayInfo
    {
        //get, set선언
        //사용 가능한 get, set으로 선언한다.
        //기본 프로퍼티
        private string name;
        private DateTime birthday;

        //public string Name
        //{
        //    get { return name; }
        //    set { name = value; }
        //}

        //public DateTime Birthday
        //{
        //    get { return birthday; }
        //    set { birthday = value; }
        //}

        //자동프로퍼티
        public string Name
        {
            get; set;
        }

        public DateTime Birthday
        {
            get; set;
        }

        public int Age
        {
            get { return new DateTime(DateTime.Now.Subtract(Birthday).Ticks).Year; }
        }
    }

    class Property
    {
        static void Main(string[] args)
        {
            //기본, 자동 프로퍼티
            //BirthdayInfo birth = new BirthdayInfo();
            //birth.Name = "SoulSeek";
            //birth.Birthday = new DateTime(1982, 12, 07);

            //생성자에서 프로퍼티 사용
            BirthdayInfo birth = new BirthdayInfo()
            {
                Name = "SoulSeek",
                Birthday = new DateTime(1982, 12, 07)
            };

            Console.WriteLine("Name : {0}", birth.Name);
            Console.WriteLine("Birthday : {0}", birth.Birthday.ToShortDateString());
            Console.WriteLine("Age : {0}", birth.Age);

            Console.ReadLine();
            Console.ReadKey();
        }
    }
}
