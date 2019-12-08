using System;

namespace Property
{
    class BirthdayInfo
    {
        private string name;
        public string Name
        {
            get { return name; }
        }

        private DateTime birthday;
        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        public int Age
        {
            get { return new DateTime(DateTime.Now.Subtract(birthday).Ticks).Year; }
        }

        public BirthdayInfo(string name)
        {
            this.name = name;
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            BirthdayInfo birth = new BirthdayInfo("나연");
            birth.Birthday = new DateTime(1995, 9, 22);

            Console.WriteLine("이름 : {0}", birth.Name);
            Console.WriteLine("생일 : {0}", birth.Birthday);
            Console.WriteLine("나이 : {0}", birth.Age);

            Console.ReadKey();
        }
    }
}
