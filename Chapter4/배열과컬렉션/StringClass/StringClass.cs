using System;
//StringBuilder
using System.Text;

namespace StringClass
{
    class StringClass
    {
        static void Main(string[] args)
        {
            //선언
            //string s1 = "C# Programing";
            //s1 = "F#";

            //Console.WriteLine("String : {0}", s1);

            ////배열인덱스로 접근
            //for(int i = 0; i < s1.Length; i++)
            //{
            //    Console.WriteLine("{0} : {1}", i, s1[i]);
            //}

            //문자열 <-----> 문자배열 변환
            //문자열--->문자배열
            //string str1 = "Hello";
            //char[] charArray1 = str1.ToCharArray();

            //for(int i = 0; i < charArray1.Length; i++)
            //{
            //    Console.WriteLine(charArray1[i]);
            //}

            ////문자배열--->문자열
            //char[] charArray2 = { 'A', 'B', 'C', 'D' };
            //string str2 = new string(charArray2);

            //Console.WriteLine(str2);

            //Substring
            //string s1 = "C# Programing!!";
            ////(시작, 종료)
            //Console.WriteLine("1 : {0}", s1.Substring(1, 5));
            ////(시작)
            //Console.WriteLine("2 : {0}", s1.Substring(5));

            //Split
            //string s1 = "abc,d,f";
            //나눌 기준 문자.
            //char sp = ',';

            //Console.WriteLine(s1);

            //문자열을 나누어 담을 배열
            //string[] splString = s1.Split(sp);

            //문자열 출력
            //for(int i = 0; i < splString.Length; i++)
            //{
            //    Console.WriteLine(splString[i]);
            //}

            //IndexOf, LastIndexOf
            //string s1 = "This evening is chicken, chickenday!!";

            //IndexOf
            //Console.WriteLine("Index : {0}", s1.IndexOf("chicken"));
            //LastIndexOf
            //Console.WriteLine("Index : {0}", s1.LastIndexOf("chicken"));

            //Substring + IndexOf
            //string s1 = "abcdefg";
            //string searchText = "f";
            //찾는 문자열중 최초문자열의 위치를 알려주기 때문에 이것으로 인덱스를 알아와서 나누는 방법
            //Console.WriteLine(s1.Substring(0, s1.IndexOf(searchText)));

            //문자열--->숫자(변환)
            //string s1 = "123456";
            //int num1 = int.Parse(s1);
            //int num2 = System.Int32.Parse(s1);

            //Console.WriteLine("Num : {0}", s1);

            //숫자--->문자열(표현)
            //int Num = 123456;

            //Console.WriteLine("Num : {0}", Num.ToString());

            //Replace
            //string s1 = "t...t";
            //Console.WriteLine(s1.Replace("t", "ㅜ")); // (바꿀문자, 바뀔문자)

            //ToUpper, ToLower
            //string s1 = "ABCDEFG";
            //string s2 = "hijklmn";

            //Console.WriteLine(s1.ToLower());
            //Console.WriteLine(s2.ToUpper());

            //Trim
            //string s1 = "     T";
            //Console.WriteLine(s1);
            //Console.WriteLine(s1.Trim());

            //StringBuilder
            StringBuilder sb = new StringBuilder();

            for(int i = 1; i < 26; i++)
            {
                sb.Append(i.ToString());

                //줄바꾸기 문자
                sb.Append(System.Environment.NewLine);
            }

            string s = sb.ToString();

            Console.WriteLine(s);

            Console.ReadLine();
            Console.ReadKey();
        }
    }
}
