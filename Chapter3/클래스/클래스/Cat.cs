using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consructor
{
    public class Cat
    {
        public string Name;
        public string Color;

        string KeyName;

        public Cat()
        {
            Name = "";
            Color = "";
        }

        public Cat(string _Name, string _Color)
        {
            Name = _Name;
            Color = _Color;
        }

        public void Meow()
        {
            Console.WriteLine("{0} : 야옹", Name);
        }

        public void SetKeyName(string key)
        {
            KeyName = key;
        }

        public string GetKeyName()
        {
            return KeyName;
        }

        ~Cat()
        {
            Console.WriteLine("{0} : 잘가", Name);
        }
    }
}
