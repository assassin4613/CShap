using System;
using System.IO;

namespace FUP
{
    public class MessageUtil
    {
        public static void Send(Stream writer, Message msg)
        {
            writer.Write(msg.GetBytes(), 0, msg.GetSize());
        }

        public static Message Receive(Stream reader)
        {
            int totalRecv = 0;
            int sizeToRead = CONSTANTS.MAXSIZE;
            byte[] hBuffer = new byte[sizeToRead];

            while(sizeToRead > 0)
            {
                //버퍼를 할당하고
                byte[] buffer = new byte[sizeToRead];
                //스트림에서 sizeToRead 크기만큼 읽어서 recv에 전달한다.
                int recv = reader.Read(buffer, 0, sizeToRead);

                if (recv == 0)
                    return null;

                //hBuffer에 buffer의 0번부터 모두 복사 한다.
                buffer.CopyTo(hBuffer, totalRecv);

                //totalRecv에 recv를 추가하고 sizeToRead에서 recv를 뺀다.
                //메시지를 읽었으니 총량을 증가하고 읽은양은 줄어든다.
                totalRecv += recv;
                sizeToRead -= recv;
            }

            Header header = new Header(hBuffer);

            totalRecv = 0;
            byte[] bBuffer = new byte[header.BODYLEN];
            sizeToRead = (int)header.BODYLEN;

            while(sizeToRead > 0)
            {
                byte[] buffer = new byte[sizeToRead];
                int recv = reader.Read(buffer, 0, sizeToRead);

                if (recv == 0)
                    return null;

                buffer.CopyTo(bBuffer, totalRecv);
                totalRecv += recv;
                sizeToRead -= recv;
            }

            //Header의 MSGTYPE 프로퍼티를 통해 어떤Body 클래스의 생성자를 호출할지를 결정한다.
            ISerializable body = null;
            switch(header.MSGTYPE)
            {
                case CONSTANTS.REQ_FILE_SEND:
                    body = new BodyRequest(bBuffer);
                    break;
                case CONSTANTS.REP_FILE_SEND:
                    body = new BodyResponse(bBuffer);
                    break;
                case CONSTANTS.FILE_SEND_DATA:
                    body = new BodyData(bBuffer);
                    break;
                case CONSTANTS.FILE_SEND_RES:
                    body = new BodyResult(bBuffer);
                    break;
                default:
                    throw new Exception(String.Format("Unknown MSGTYPE : {0}", header.MSGTYPE));
            }

            return new Message() { Header = header, Body = body };
        }
    }
}
