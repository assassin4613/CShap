using System;
using System.Windows.Forms;

namespace UsingApplication
{
    class MainApp : Form
    {
        static void Main(string[] args)
        {
            MainApp form = new MainApp();

            //윈도우 영역(프레임과 캡션바를 제외한 영역)를 클릭하면 이벤트를 받게 된다.
            form.Click += new EventHandler((sender, eventArgs) =>
            {
                Console.WriteLine("Closing Window...");
                Application.Exit();
            });

            Console.WriteLine("Starting Window Application..");
            Application.Run(form);

            Console.WriteLine("Exiting Window Application..");

            Console.ReadKey();
        }
    }
}
