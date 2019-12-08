using System;
//Collection
using System.Collections;
//Generic
using System.Collections.Generic;

namespace Collection
{
    //Dictionary 예제
    public class Item
    {
        string itemName;
        int str;
        int dex;
        int hp;
    }

    public class User
    {
        string Name;
        int Level;
        int Hp;
        int Exp;

        public string name
        {
            get { return Name; }
            set { Name = value; }
        }

        public int level
        {
            get { return Level; }
            set { Level = value; }
        }

        public int hp
        {
            get { return Hp; }
            set { Hp = value; }
        }

        public int exp
        {
            get { return Exp; }
            set { Exp = value; }
        }

        public User(string name_, int level_, int hp_, int exp_, Item item_ = null)
        {
            this.name = name_;
            this.level = level_;
            this.hp = hp_;
            this.exp = exp_;

            if (item_ != null)
            {
                Item item = new Item();

                //item.Add();
            }
        }

    }

    public class CollectionEx
    {
        static void Main(string[] args)
        {
            //ArrayList
            //ArrayList arrayList = new ArrayList();

            ////데이터 추가
            //arrayList.Add(10);
            //arrayList.Add(20);
            //arrayList.Add(30);

            ////해당 인덱스의 데이터 삭제
            //arrayList.RemoveAt(1);

            ////해당 데이터값을 찾아서 삭제
            //arrayList.Remove(30);

            ////해당 인덱스의 데이터 삽입
            //arrayList.Insert(1, 2.3f);

            //arrayList.Add("ABC");
            //arrayList.Add("가나다");

            ////컬렉션의 모든 요소는 object 타입으로 저장된다.
            //foreach (object obj in arrayList)
            //{
            //    Console.WriteLine("{0}", obj);
            //}

            //List<T>
            //List<int> list = new List<int>();

            ////추가
            //list.Add(11);
            //list.Add(12);
            //list.Add(13);

            ////특정 인덱스의 데이터 삭제
            //list.RemoveAt(1);
            ////특정 데이터를 찾아서 삭제
            //list.Remove(11);

            ////특정인덱스에 데이터 삽입.
            //list.Insert(0, 44);

            //foreach (int element in list)
            //{
            //    Console.WriteLine("{0}", element);
            //}

            //Dictionary<T>
            //Dictionary<string, string> dt
            //    = new Dictionary<string, string>();

            ////이렇게 쓰여진다는 걸 보여주기 위한.
            //dt["Apple"] = "사과";
            //dt["Banana"] = "바나나";
            //dt["Orange"] = "오렌지";
            //dt["Strawberry"] = "딸기";
            //dt["Grape"] = "포도";

            //Console.WriteLine("Apple : " + dt["Apple"]);
            //Console.WriteLine("Apple : " + dt["Banana"]);
            //Console.WriteLine("Apple : " + dt["Orange"]);

            ////실제 쓰임.
            ////foreach
            //foreach (KeyValuePair<string, string> each in dt)
            //{
            //    Console.WriteLine("{0} : {1}", each.Key, each.Value);
            //}

            ////for(키값이 필요 없을때)
            //IEnumerator em = dt.GetEnumerator();

            //while (em.MoveNext())
            //{
            //    Console.WriteLine("{0}, {1}", em.Current);
            //}

            //직관적인 예제

            Dictionary<string, User> userDic = new Dictionary<string, User>();

            User user = new User("Soul", 1, 1, 1);
            userDic.Add(user.name, user);

            User user1 = new User("Seek", 2, 2, 2);
            userDic.Add(user1.name, user1);

            IEnumerator em = userDic.GetEnumerator();

            while (em.MoveNext())
            {
                KeyValuePair<string, User> pair = (KeyValuePair<string, User>)em.Current;

                User us = pair.Value;

                Console.WriteLine("Name : {0}, Level : {1}, HP : {2}, EXP : {3}"
                                , us.name, us.level, us.hp, us.exp);
            }

            Console.ReadKey();
            Console.ReadLine();
        }
    }
}
