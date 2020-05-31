//#define DEBUG__

using System;
using System.Linq;

namespace Iviz.RoslibSharp.XmlRpc
{
    public class Arg
    {
        readonly string value;

        public Arg(bool f)
        {
            value = f ?
                "<value><boolean>1</boolean></value>\n" :
                "<value><boolean>0</boolean></value>\n";
        }

        public Arg(double f)
        {
            value = $"<value><double>{f}</double></value>\n";
        }

        public Arg(int f)
        {
            value = $"<value><i4>{f}</i4></value>\n";
        }

        public Arg(Uri f) : this(f?.ToString() ?? "(unknown)")
        {
        }

        public Arg(string f)
        {
            value = $"<value>{f}</value>\n";
        }

        public Arg(string[] f) : this(f.Select(x => new Arg(x)).ToArray())
        {
        }

        public Arg(string[][] f) : this(f.Select(x => new Arg(x)).ToArray())
        {
        }

        public Arg(Arg[] f)
        {
            value = $"<value><array><data>{string.Join<Arg>("", f)}</data></array></value>";
        }

        public Arg(Arg[][] f) : this(f.Select(x => new Arg(x)).ToArray())
        {
        }

        public override string ToString()
        {
            return value;
        }

        public static implicit operator string(Arg arg) => arg?.ToString();
    }
}
