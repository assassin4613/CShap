using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EchoServer
{
    class MainApp
    {
        static void Main(string[] args)
        {
            string bindIp = "127.0.0.1";
            const int bindPort = 5425;
            TcpListener server = null;
            try
            {
                // 서버의 주소체계를 설정한다.
                IPEndPoint localAddress = new IPEndPoint(IPAddress.Parse(bindIp), bindPort);

                // 요청받을 서버를 생성한다.
                server = new TcpListener(localAddress);

                // 서버를 연결 요청 받을 준비를 해둔다.
                server.Start();

                Console.WriteLine("메아리 서버 시작...");

                while(true)
                {
                    // 클라이언트의 연결 요청이 있기 전까지 AppceptTcpClient()메소드 코드블록에서 멈춰 있게 된다.
                    // 연결 받았다면 클라이언트의 정보를 출력 할 수 있게 된다.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("클라이언트 접속 : {0}", ((IPEndPoint)client.Client.RemoteEndPoint).ToString());

                    // 네트워크스트림을 통해 클라이언트의 받아서 클라이언트와 데이터를 주고 받을 준비를 한다.
                    NetworkStream stream = client.GetStream();

                    int length;
                    string data = null;
                    byte[] bytes = new byte[256];

                    // 현재 while 구문안에서 읽고 쓰기를 바로 수행한다. 즉, 클라이언트가 보낸 Data를 그대로 돌려 보내고 있다.
                    // 기본 형식이라고 참고 하길 바란다!!
                    // 연결이 끊기기 전까지 스트림을 읽을 수 있기 때문에 끊기지 않는다면 while 반복문을 계속 수행하게 된다.
                    while((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // byte형식의 data를 String형식으로 Encoding해서 String형식으로 나타낸다.
                        data = Encoding.Default.GetString(bytes, 0, length);
                        Console.WriteLine(String.Format("수신: {0}", data));

                        // Data를 Byte형식으로 다시 Encoding 한다.
                        byte[] msg = Encoding.Default.GetBytes(data);

                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine(String.Format("송신: {0}", data));
                    }

                    stream.Close();
                    client.Close();
                }
            }
            catch(SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                server.Stop();
            }

            Console.WriteLine("서버를 종료 합니다.");
            Console.ReadKey();
        }
    }
}
