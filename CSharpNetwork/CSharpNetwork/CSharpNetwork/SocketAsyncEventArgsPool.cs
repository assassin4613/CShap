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
    class SocketAsyncEventArgsPool
    {
        Stack<SocketAsyncEventArgs> m_pool;

        public SocketAsyncEventArgsPool(int capacity)
        {
            m_pool = new Stack<SocketAsyncEventArgs>(capacity);
        }

        public void Push(SocketAsyncEventArgs item)
        {
            if(item == null)
            {
                throw new ArgumentNullException("items added to a SocketAsyncEventArgsPool cannot be null");
            }

            lock(m_pool)
            {
                m_pool.Push(item);
            }
        }

        public SocketAsyncEventArgs Pop()
        {
            lock(m_pool)
            {
                if (m_pool == null)
                    return null;

                return m_pool.Pop();
            }
        }

        public int Count
        {
            get { return m_pool.Count; }
        }
    }
}
