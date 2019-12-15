using System;

namespace FUP
{
    public class Header : ISerializable
    {
        public uint     MSGID     { get; set; }
        public uint     MSGTYPE   { get; set; }
        public uint     BODYLEN   { get; set; }
        public byte     FRAGMENTED  { get; set; }
        public byte     LASTMSG   { get; set; }
        public ushort   SEQ       { get; set; }

        public Header() { }
        public Header(byte[] bytes)
        {
            // btyes 0번부터 4바이트 크기만큼 읽는다.(0 ~ 3)
            MSGID   = BitConverter.ToUInt32(bytes, 0);
            // btyes 4번부터 4바이트 크기만큼 읽는다.(4 ~ 7)
            MSGTYPE = BitConverter.ToUInt32(bytes, 4);
            // btyes 8번부터 4바이트 크기만큼 읽는다.(8 ~ 11)
            BODYLEN = BitConverter.ToUInt32(bytes, 8);
            // btyes 12번부터 1바이트 크기만큼 읽는다.
            FRAGMENTED = bytes[12];
            // btyes 13번부터 1바이트 크기만큼 읽는다.
            LASTMSG = bytes[13];
            // btyes 14번부터 2바이트 크기만큼 읽는다.(14 ~ 15)
            SEQ = BitConverter.ToUInt16(bytes, 14);
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[CONSTANTS.MAXSIZE];

            byte[] temp = BitConverter.GetBytes(MSGID);
            Array.Copy(temp, 0, bytes, 0, temp.Length);

            temp = BitConverter.GetBytes(MSGTYPE);
            Array.Copy(temp, 0, bytes, 4, temp.Length);

            temp = BitConverter.GetBytes(BODYLEN);
            Array.Copy(temp, 0, bytes, 8, temp.Length);

            bytes[12] = FRAGMENTED;
            bytes[13] = LASTMSG;

            temp = BitConverter.GetBytes(SEQ);
            Array.Copy(temp, 0, bytes, 14, temp.Length);

            return bytes;
        }

        public int GetSize()
        {
            return CONSTANTS.MAXSIZE;
        }
    }
}
