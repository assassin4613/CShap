using System;
using System.IO;

namespace Touch
{
    class MainApp
    {
        static void OnWrongPathType(string type)
        {
            Console.WriteLine("{0} is wrong type", type);
            return;
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage : Touch.exe <Path> [Type:File/Directory]");
                return;
            }

            //사용자 매개 변수로 1개의 값을 받으면 첫번째 매개변수는 경로로 지정하고 두번째는 File타입으로 고정한다.
            //사용자 매개 변수로 2개의 값을 받으면 첫번째 매개변수는 경로로 지정하고 두번째는 입력된 타입으로 고정한다.
            string path = args[0];
            string type = "File";

            if (args.Length > 1)
                type = args[1];

            //파일과 디렉토리가 존재한다면 생성시간을 갱신한다.
            if (File.Exists(path) || Directory.Exists(path))
            {
                if (type == "File")
                    File.SetLastWriteTime(path, DateTime.Now);
                else if (type == "Directory")
                    Directory.SetLastWriteTime(path, DateTime.Now);
                else
                {
                    OnWrongPathType(path);
                    return;
                }

                Console.WriteLine("Updated {0} {1}", path, type);
                Console.ReadKey();
            }
            //파일과 디렉토리가 존재하지 않는다면 생성한다.
            else
            {
                if (type == "File")
                    File.Create(path).Close();
                else if (type == "Directory")
                    Directory.CreateDirectory(path);
                else
                {
                    OnWrongPathType(path);
                    return;
                }

                Console.WriteLine("Created {0} {1}", path, type);
                Console.ReadKey();
            } 
        }
    }
}
