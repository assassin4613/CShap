using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EchoClient
{
    class MainApp
    {
        static void Main(string[] args)
        {
            string[] IPdata = {"127.0.0.1", "8080", "127.0.0.1", "절망적인 상황이란 없다. 절망하는 사람만 있을뿐!" };

            string      bindIp      = IPdata[0];
            int         bindPort    = Convert.ToInt32(IPdata[1]);
            string      serverIp    = IPdata[2];
            const int   serverPort  = 5425;
            string      message     = IPdata[3];

            // 클라이언트에 전송 부분에서 while문이 없는 것은 연결해서 데이터를 한번 보내보는 것으로 하기 때문.
            // 지속적인 주고 받는 테스트를 원한다면 Console.Read()로 입력된 Data를 서버롤 거쳐서 돌아오는 것을 체크 해보길 바란다.
            try
            {
                // 클라이언트 주소 체계를 설정한다.
                IPEndPoint clientAddress = new IPEndPoint(IPAddress.Parse(bindIp), bindPort);

                // 연결할 서버의 주소 체계를 설정한다.
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);

                Console.WriteLine("클라이언트: {0}, 서버: {1}", clientAddress.ToString(), serverAddress.ToString());

                // 서버에 연결 요청할 준비를 한다.
                TcpClient client = new TcpClient(clientAddress);

                // 서버에 연결을 요청한다.
                client.Connect(serverAddress);

                // 전송할 Data를 byte로 Encoding 한다.
                byte[] data = Encoding.Default.GetBytes(message);

                // 전송 스트림을 생성한다.
                NetworkStream stream = client.GetStream();

                // 스트림에 data를 기록한다.
                stream.Write(data, 0, data.Length);

                Console.WriteLine("송신: {0}", message);

                data = new byte[256];

                string responseData = "";

                int bytes = stream.Read(data, 0, data.Length);
                responseData = Encoding.Default.GetString(data, 0, bytes);
                Console.WriteLine("수신: {0}", responseData);

                stream.Close();
                client.Close();
            }
            catch(SocketException e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("클라이언트를 종료합니다.");
            Console.ReadKey();
        }
    }
}
