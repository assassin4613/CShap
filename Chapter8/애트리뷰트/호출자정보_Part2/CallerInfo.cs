using System;
using System.Runtime.CompilerServices;

namespace CallerInfo
{
    public static class Trace
    {
        public static void WriteLine(string message,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            Console.WriteLine(
                "{0}(Line:{1}) {2}: {3}", file, line, member, message);
        }

        public static void ReadKey(
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            Console.WriteLine(
                "{0}(Line:{1}) {2}", file, line, member);

            Console.Write("아무키나 누르면 종료합니다...");
            Console.ReadKey();
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            Trace.WriteLine("즐거운 프로그래밍!");

            Trace.ReadKey();
        }
    }
}
