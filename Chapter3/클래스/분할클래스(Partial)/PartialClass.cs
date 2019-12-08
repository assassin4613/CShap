using System;

namespace Consructor
{
    partial class PartialClass
    {
        int age;
        string gender;
        string name;
        string phone;
    }

    partial class PartialClass
    {
        public void SetProfile(int _age, string _gender, string _name, string _phone)
        {
            age = _age;
            gender = _gender;
            name = _name;
            phone = _phone;
        }

        public string GetProfile()
        {
            return string.Format("나이 : {0}, 성별 : {1}, 이름 : {2}, 연락처 : {3}", age, gender, name, phone);
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            PartialClass obj = new PartialClass();

            obj.SetProfile(25, "남", "SoulSeek", "01091271594");

            Console.WriteLine(obj.GetProfile());

            Console.ReadKey();
        }
    }
}
