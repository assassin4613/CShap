using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpNetwork
{
    class CNetworkService
    {
        //클라이언트의 접속을 받아들이기 위한 객체
        CListener client_listener;

        //메시지 수신, 전송시 필요한 오브젝트
        //GC가 작동되어서 알아서 해제하겠지만 서버환경에서는 자원소모를 최소화하는 것이
        //좋기 때문에 풀링형식을 사용한다.
        SocketAsyncEventArgsPool receive_event_args_pool;
        SocketAsyncEventArgsPool send_event_args_pool;

        //메시지 수신, 전송시, .Net 비동기 소켓에서 사용할 버퍼를 관리하는 객체
        BufferManager buffer_manager;

        //클라이언트의 접속이 이루어졌을 때 호출되는 델리게이트
        public delegate void SessionHandler(CUserToken token);
        public SessionHandler session_created_callback { get; set; }

        int max_connections;

        public void listen(string host, int port, int backlog)
        {
            this.client_listener = new CListener();
            this.client_listener.callback_on_newclient += on_new_client;
            this.client_listener.Start(host, port, backlog);

            this.receive_event_args_pool = new SocketAsyncEventArgsPool(this.max_connections);
            this.send_event_args_pool = new SocketAsyncEventArgsPool(this.max_connections);

            SocketAsyncEventArgs arg;
            for (int i = 0; i < this.max_connections; i++)
            {
                CUserToken token = new CUserToken();

                //receive pool
                {
                    arg = new SocketAsyncEventArgs();
                    arg.Completed += new EventHandler<SocketAsyncEventArgs>(receive_completed);
                    arg.UserToken = token;

                    this.buffer_manager.SetBuffer(arg);
                    this.receive_event_args_pool.Push(arg);
                }

                //send pool
                {
                    arg = new SocketAsyncEventArgs();
                    arg.Completed += new EventHandler<SocketAsyncEventArgs>(send_completed);
                    arg.UserToken = token;

                    this.buffer_manager.SetBuffer(arg);
                    this.send_event_args_pool.Push(arg);
                }
            }
        }

        void on_new_client(Socket client_socket, object token)
        {
            SocketAsyncEventArgs receive_args = this.receive_event_args_pool.Pop();
            SocketAsyncEventArgs send_args = this.send_event_args_pool.Pop();

            if (this.session_created_callback != null)
            {
                CUserToken user_token = receive_args.UserToken as CUserToken;
                this.session_created_callback(user_token);
            }

            begin_receive(client_socket, receive_args, send_args);
        }

        void begin_receive(Socket socket, SocketAsyncEventArgs receive_args, SocketAsyncEventArgs send_args)
        {
            //receive_args, send_args 아무곳에서나 꺼내와도 됨. 둘 다 동일한 CUserToken을 물고 있다.
            CUserToken token = receive_args.UserToken as CUserToken;
            token.set_event_args(receive_args, send_args);

            //생성된 클라이언트 소켓을 보관해 놓고 통신할 때 사용함.
            token.socket = socket;

            //데이터를 받을 수 있도록 소켓 메소드를 호출해준다.
            //비동기로 수신할 경우 Worker Thread에서 대기중으로 있다가 Completed에 설정해놓은 메소드가 호출된다.
            bool pending = socket.ReceiveAsync(receive_args);
            if (!pending)
            {
                process_receive(receive_args);
            }
        }

        void receive_completed(object sender, SocketAsyncEventArgs e)
        {
            if(e.LastOperation == SocketAsyncOperation.Receive)
            {
                process_receive(e);
                return;
            }

            throw new ArgumentException("The last operation completed on the socket was not a receive.");
        }

        private void process_receive(SocketAsyncEventArgs e)
        {
            CUserToken token = e.UserToken as CUserToken;

            if(e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                //이후의 작업은 CUserToken에 맡긴다.
                token.on_receive(e.Buffer, e.Offset, e.BytesTransferred);

                //다음 메시지 수신을 위해서 다시 ReceiveAsync 메소드를 호출한다.
                bool pending = token.socket.ReceiveAsync(e);
                if(!pending)
                {
                    process_receive(e);
                }
            }
            else
            {
                Console.WriteLine(string.Format("error {0}, transferred {1}", e.SocketError, e.BytesTransferred));
                close_clientsocket(token);
            }
        }
    }
}
