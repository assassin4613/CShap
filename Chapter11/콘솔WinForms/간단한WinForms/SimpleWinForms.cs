using System;
using System.Windows.Forms;

namespace SimpleWinForms
{
    // MainApp이 System.Windows.Forms 네임스페이스의
    // Form 클래스로부터 상속받도록 선언.
    class MainApp : Form
    {
        static void Main(string[] args)
        {
            // Application.Run() 메소드에 MainApp의 인스턴스를
            // 매개 변수로 넘겨 호출한다.
            Application.Run(new MainApp());
        }
    }
}
