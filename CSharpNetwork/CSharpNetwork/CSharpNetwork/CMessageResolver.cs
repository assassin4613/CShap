using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpNetwork
{
    class Defines
    {
        public static readonly short HEADERSIZE = 2;
    }

    /// <summary>
    /// [hearder][body] 구조를 갖는 데이터를 파싱하는 클래스
    ///  - hearder :    데이터 사이즈. defines.HEADERSIZE에 정의된 타입만큼의 크기를 가짐
    ///                 2바이트일 경우 Int16, 4바이트는 Int32로 처리하면됨.
    ///                 본문의 크기가 Int16.Max 값을 넘지 않는다면 2바이트로 처리하는 것이 좋음.
    ///                 
    ///  - body :       메시지 본문.
    /// </summary>
    class CMessageResolver
    {
        public delegate void CompletedMessageCallback(Const<byte[]> buffer);
        int message_size;
        int message_buffer = new byte[1024];
    }
}
