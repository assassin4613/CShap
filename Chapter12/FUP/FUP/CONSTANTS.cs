namespace FUP
{
    public class CONSTANTS
    {
        //메시지 타입을 상수로 정의
        public const uint REQ_FILE_SEND = 0x01;
        public const uint REP_FILE_SEND = 0x02;

        public const uint FILE_SEND_DATA = 0x03;
        public const uint FILE_SEND_RES = 0x04;

        public const byte NOT_FRANGMENTED = 0x00;
        public const byte FRAGMENTED = 0x01;

        public const byte NOT_LASTMSG = 0x00;
        public const byte LASTMSG = 0x01;

        public const byte ACCEPTED = 0x00;
        public const byte DENIED = 0x01;

        public const byte FAIL = 0x00;
        public const byte SUCCESS = 0x01;

        public const int MAXSIZE = 16;
    }
}
