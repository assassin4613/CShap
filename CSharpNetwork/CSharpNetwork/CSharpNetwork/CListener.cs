using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpNetwork
{
    class CListener
    {
        //bind -> listen -> accept

        //비동기 Accept를 위한 EventArgs
        SocketAsyncEventArgs accept_args;

        //클라이언트의 접속을 처리할 변수
        Socket listen_socket;

        //Accept 처리의 순서를 제어하기 위한 이벤트 변수
        AutoResetEvent flow_control_event;

        //새로운 클라이언트가 접속했을 때 호출되는 콜백
        public delegate void NewclientHandler(Socket client_socket, object token);
        public NewclientHandler callback_on_newclient;

        public CListener()
        {
            this.callback_on_newclient = null;
        }

        public void Start(string host, int port, int backlog)
        {
            //소켓 생성
            this.listen_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //소켓 주소 준비
            IPAddress address;
            if(host == "0.0.0.0")
            {
                address = IPAddress.Any;
            }
            else
            {
                address = IPAddress.Parse(host);
            }

            //소켓에 추가할 주소와 포트 생성
            //클라이언트가 접속하기위하 접근하는 최종지접이기 때문에 Endpoint. 즉, 서버측 IPAddress
            IPEndPoint endpoint = new IPEndPoint(address, port);

            try
            {
                //소켓에 host정보를 바인딩 시킨 뒤 Listen 메소드를 호출하여 준비
                listen_socket.Bind(endpoint);
                listen_socket.Listen(backlog);

                this.accept_args = new SocketAsyncEventArgs();
                this.accept_args.Completed += new EventHandler<SocketAsyncEventArgs>(on_accept_completed);

                //클라이언트가 들어오기를 기다림.
                //비동기 메소드이므로 블로킹 되지 않고 바로 리턴
                //콜백 메소드를 통해서 접속 통보를 처리
                //this.listen_socket.AcceptAsync(this.accept_args);
                
                //특정 OS 버전에서 콘솔입력이 대기중일때 accept 처리가 안되는 버그가 있을 수 있으니 그럴환경일 경우를 대비해서 교체
                Thread listen_thread = new Thread(do_listen);
                listen_thread.Start();
            }
            catch(Exception e)
            {
                //Console.WriteLine(e.Message);
            }
        }

        void do_listen()
        {
            // accept처리 제어를 위해 이벤트 객체
            this.flow_control_event = new AutoResetEvent(false);

            while(true)
            {
                // SocketAsyncEventArgs를 재사용하기 위해서 null로 만듦
                this.accept_args.AcceptSocket = null;
                bool pending = true;

                try
                {
                    //비동기 accept를 호출하여 클라이언트의 접속을 받아들임
                    //비동기 메소드이지만 동기적으로 수행이 완료될 경우도 있으니
                    //리턴 값을 확인하여 분기시켜야함.
                    pending = listen_socket.AcceptAsync(this.accept_args);
                }
                catch(Exception e)
                {
                    //Console.WriteLine(e.Message);
                    continue;
                }

                //즉시 완료되면 이벤트가 발생하지 않으므로 리턴 값이 false일 경우 콜백 메소드를 직접 호출
                //pending상태라면 비동기 요청이 들어간 상태이므로 콜백 메소드를 기다림
                // http://msdn.microsoft.com/ko-kr/library/system.net.sockets.socket.acceptasync%28v=vs.110%29.aspx
                if (!pending)
                {
                    on_accept_completed(null, this.accept_args);
                }

                //클라이언트 접속 처리가 완료되면 이벤트 객체의 신호를 전달받아 다시 루프를 수행하도록함.
                this.flow_control_event.WaitOne();
            }
        }

        void on_accept_completed(object sender, SocketAsyncEventArgs e)
        {
            if(e.SocketError == SocketError.Success)
            {
                Socket client_socket = e.AcceptSocket;

                this.flow_control_event.Set();

                //accept까지의 역할만 수행하고 클라이언트의 접속 이후의 처리는
                //외부로 넘기기 위해서 콜백 메소드를 호출해주도록 함
                //이유는 소켓 처리부와 컨텐츠 구현부를 분리하기 위함임
                //컨텐츠 구현부분은 자주 바뀔 가능성은 있지만, 소켓 Accept 부분은 상대적으로 변경이 적은 부분이기 때문에
                //양쪽을 분리시켜 주는 것이 좋음.
                //또한 클래스 설계 방침에 따라 Listen에 관련된 코드만 존재하도록 하기 위한 이유도 있다.
                if(this.callback_on_newclient != null)
                {
                    this.callback_on_newclient(client_socket, e.UserToken);
                }

                return;
            }
            else
            {
                //todo : Accept 실패 처리
                //Console.WriteLine("Failed to accept client.");
            }

            //다음 연결을 받아들임
            this.flow_control_event.Set();
        }
    }
}
