using System;
using System.Linq;
using System.IO;

namespace Dir
{
    class MainApp
    {
        static void Main(string[] args)
        {
            string directory;

            //사용자 매개변수에 경로가 기입되어 있으면 
            //그 경로를 사용하고 없으면 현재 경로를 사용한다.
            if (args.Length < 1)
                directory = ".";
            else
                directory = args[0];

            Console.WriteLine("{0} directory Info", directory);
            Console.WriteLine("- Directories :");

            var directories = (from dir in Directory.GetDirectories(directory) // 하위 디렉토리 목록 조회
                               let info = new DirectoryInfo(dir)
                               select new
                               {
                                   Name = info.Name,
                                   Attributes = info.Attributes
                               }).ToList();

            foreach (var d in directories)
                Console.WriteLine("{0} : {1}", d.Name, d.Attributes);

            Console.WriteLine("- Files :");

            var files = (from file in Directory.GetFiles(directory) // 하위 파일 목록 조회
                         let info = new FileInfo(file)              // let은 LINQ안에서 변수를 만든다. LINQ의 var이라고 생각하면 된다.
                         select new
                         {
                             Name = info.Name,
                             FileSize = info.Length,
                             Attributes = info.Attributes
                         }).ToList();

            foreach (var f in files)
                Console.WriteLine("{0} : {1}, {2}", f.Name, f.FileSize, f.Attributes);

            Console.ReadKey();
        }
    }
}

