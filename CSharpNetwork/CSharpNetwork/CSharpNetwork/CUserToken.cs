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
    class CUserToken
    {
        public void on_receive(byte[] buffer, int offset, int transffered)
        {
            this.message_resolver.on_receive(buffer, offset, transfered, on_message);
        }
    }
}
