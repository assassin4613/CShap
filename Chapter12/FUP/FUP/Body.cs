using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace FUP
{
    //파일 전송 요청 메시지(0x01)에 사용할 본문 클래스
    //FILESIZE와 FILENAME 프로퍼티를 가진다.
    public class BodyRequest : ISerializable
    {
        public long FILESIZE;
        public byte[] FILENAME;

        public BodyRequest() { }
        public BodyRequest(byte[] bytes)
        {
            //bytes 0번부터 8바이트 크기만큼 읽는다.
            FILESIZE = BitConverter.ToInt64(bytes, 0);
            FILENAME = new byte[bytes.Length - sizeof(long)];
            Array.Copy(bytes, sizeof(long), FILENAME, 0, FILENAME.Length);
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[GetSize()];
            byte[] temp = BitConverter.GetBytes(FILESIZE);
            Array.Copy(temp, 0, bytes, 0, temp.Length);
            Array.Copy(FILENAME, 0, bytes, temp.Length, FILENAME.Length);

            return bytes;
        }

        public int GetSize()
        {
            return sizeof(long) + FILENAME.Length;
        }
    }

    public class BodyResponse : ISerializable
    {
        public uint MSGID;
        public byte RESPONSE;
        public BodyResponse() { }
        public BodyResponse(byte[] bytes)
        {
            //bytes 0번부터 4바이트 크기만큼 읽는다.
            MSGID = BitConverter.ToUInt32(bytes, 0);
            RESPONSE = bytes[4];
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[GetSize()];
            byte[] temp = BitConverter.GetBytes(MSGID);
            Array.Copy(temp, 0, bytes, 0, temp.Length);
            bytes[temp.Length] = RESPONSE;

            return bytes;
        }

        public int GetSize()
        {
            return sizeof(uint) + sizeof(byte);
        }
    }

    //실제 파일을 전송하는 메시지(0x03)에 사용할 본문 클래스
    //DATA 필드만 존재한다.
    public class BodyData : ISerializable
    {
        public byte[] DATA;

        public BodyData(byte[] bytes)
        {
            DATA = new byte[bytes.Length];
            bytes.CopyTo(DATA, 0);
        }

        public byte[] GetBytes()
        {
            return DATA;
        }

        public int GetSize()
        {
            return DATA.Length;
        }
    }

    //파일 전송 결과 메시지(0x04)에 사용할 본문 클래스
    //요청 메시지의 MSGID와 성공 여부를 나타내는 RESULT 프로퍼티를 가진다.
    public class BodyResult : ISerializable
    {
        public uint MSGID;
        public byte RESULT;

        public BodyResult() { }
        public BodyResult(byte[] bytes)
        {
            //bytes 0번부터 4바이트
            MSGID = BitConverter.ToUInt32(bytes, 0);
            RESULT = bytes[4];
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[GetSize()];
            byte[] temp = BitConverter.GetBytes(MSGID);
            Array.Copy(temp, 0, bytes, 0, temp.Length);
            bytes[temp.Length] = RESULT;

            return bytes;
        }

        public int GetSize()
        {
            return sizeof(uint) + sizeof(byte);
        }
    }
}
