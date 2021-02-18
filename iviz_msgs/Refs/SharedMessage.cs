using System;
using System.Threading;

namespace Iviz.Msgs
{
    /// <summary>
    /// A class representing shared ownership of a message.
    /// Similar to <see cref="SharedRef{T}"/> but for messages.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class SharedMessage<T> : IDisposable where T : IMessage
    {
        public T Message { get; }
        readonly CountdownEvent cd;
        bool disposed;

        public SharedMessage(T t)
        {
            Message = t;
            cd = new CountdownEvent(1);
        }

        SharedMessage(SharedMessage<T> other)
        {
            Message = other.Message;
            cd = other.cd;
            cd.AddCount();
        }

        public SharedMessage<T> Share()
        {
            return new(this);
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            if (!cd.Signal())
            {
                return;
            }

            Message.Dispose();
            cd.Dispose();
        }

        public override string ToString()
        {
            return $"[SharedMessage Type={typeof(T).Name} Count={cd.CurrentCount.ToString()}]";
        }
    }
}