using System.Collections.Concurrent;
using System.Text;

namespace Iviz.Tools
{
    public static class BuilderPool
    {
        static readonly ConcurrentBag<StringBuilder> Pool = new();

        public static StringBuilder Rent()
        {
            return Pool.TryTake(out StringBuilder? result) ? result : new StringBuilder(100);
        }

        public static void Return(StringBuilder str)
        {
            str.Clear();
            Pool.Add(str);
        }
    }
}