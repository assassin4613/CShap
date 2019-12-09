using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UsingTask
{
    class MainApp
    {
        static void Main(string[] args)
        {
            string srcFile = args[0];

            Action<object> FileCopyAction = (object state) =>
            {
                String[] paths = (String[])state;
                File.Copy(paths[0], paths[1]);

                Console.WriteLine("TaskID:{0}, ThreadID:{1}, {2} was copied to {3}", Task.CurrentId, Thread.CurrentThread.ManagedThreadId, paths[0], paths[1]);
            };

            // 두 번째 매개 변수는 FileCopyAction의 매개 변수로 사용된다.
            Task t1 = new Task( FileCopyAction, new string[] { srcFile, srcFile + ".copy1" });

            Task t2 = Task.Run(() =>
            {
                FileCopyAction(new string[] { srcFile, srcFile + ".copy2" });
            });

            t1.Start();

            Task t3 = new Task( FileCopyAction, new string[] { srcFile, srcFile + ".copy3" });

            // Task는 코드의 비동기 실행을 위한 Start() 메소드뿐 아니라 동기 실행을 위한
            // RunSynchronously() 메소드도 제공한다.
            // 이 메소드는 실행이 끝나야 반환되지만, 나쁜 습관을 방지하기 위해 Wait()는 꼬박꼬박 호출해주는 것이 좋다.
            t3.RunSynchronously();

            t1.Wait();
            t2.Wait();
            t3.Wait();

            var myTask = Task<List<int>>.Run( () =>
            {
                Thread.Sleep(1000);

                List<int> list = new List<int>();
                list.Add(3);
                list.Add(4);
                list.Add(5);

                return list;
            });

            myTask.Wait();

            Console.ReadKey();
        }
    }
}
