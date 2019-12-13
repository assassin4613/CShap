using System;
using System.Windows;
using System.Windows.Forms;

namespace MessageFilter
{
    class MessageFilter : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m)
        {
            //WM_PAINT(0x0F), WM_MOUSEMOVE(0x200), WM_TIMER(0x113), WM_NCMOUSEMOVE(0xA0)
            if (m.Msg == 0x0F || m.Msg == 0xA0 || m.Msg == 0x200 || m.Msg == 0x113)
                return false;

            Console.WriteLine("{0} : {1}", m.ToString(), m.Msg);

            //WM_LBUTTIONDOWN(0x201)메세지가 도착하면 Application.Exit() 실행.
            if (m.Msg == 0x201)
                Application.Exit();

            return true;
        }
    }

    class MainApp : Form
    {
        static void Main(string[] args)
        {
            Application.AddMessageFilter(new MessageFilter());
            Application.Run(new MainApp());

            Console.ReadKey();
        }
    }
}
