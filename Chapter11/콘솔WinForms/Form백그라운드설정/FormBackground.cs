using System;
using System.IO;
using System.Drawing; // Color을 쓸려면 필요, 참조추가도 해야 사용가능하다.
using System.Windows.Forms;

namespace FormBackground
{
    class MainApp : Form
    {
        Random rand;

        public MainApp()
        {
            rand = new Random();

            this.MouseWheel += new MouseEventHandler(MouseWheelEvent);
            this.MouseDown += new MouseEventHandler(MouseDownEvent);
        }

        void MouseDownEvent(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                Color oldColor = this.BackColor;
                this.BackColor = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
            }
            else if(e.Button == MouseButtons.Right)
            {
                if(this.BackgroundImage != null)
                {
                    this.BackgroundImage = null;
                    return;
                }

                string file = "sample.jpg";

                if (File.Exists(file) == false)
                    MessageBox.Show("이미지 파일이 없다.");
                else
                    this.BackgroundImage = Image.FromFile(file);
            }
        }

        void MouseWheelEvent(object sender, MouseEventArgs e)
        {
            this.Opacity = this.Opacity + (e.Delta > 0 ? 0.1 : -0.1);
            Console.WriteLine("Opacity: {0}", this.Opacity);
        }

        static void Main(string[] args)
        {
            Application.Run(new MainApp());

            Console.ReadKey();
        }
    }
}
