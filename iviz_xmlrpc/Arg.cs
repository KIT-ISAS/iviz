//#define DEBUG__

using System;
using System.Collections.Generic;
using System.Linq;

namespace Iviz.XmlRpc
{
    public sealed class Arg
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

        public Arg(IEnumerable<Uri> f) : this(f?.Select(x => new Arg(x)))
        {
        }
        
        public Arg(in DateTime f)
        {
            value = $"<value><dateTime.iso8601>{f:yyyy-MM-ddTHH:mm:ssZ}</dateTime.iso8601></value>\n";
        }
        
        public Arg(string f)
        {
            value = $"<value>{f}</value>\n";
        }

        public Arg(IEnumerable<string> f) : this(f?.Select(x => new Arg(x)))
        {
        }

        public Arg(IEnumerable<string[]> f) : this(f?.Select(x => new Arg(x)))
        {
        }

        public Arg((string, string) f) : this(new Arg[] { f.Item1, f.Item2 })
        {
        }
        
        public Arg(IEnumerable<(string, string)> f) : this(f?.Select(x => new Arg(x)))
        {
        }

        public Arg(IEnumerable<Arg> f)
        {
            if (f is null)
            {
                throw new ArgumentNullException(nameof(f));
            }

            value = $"<value><array><data>{string.Join("", f)}</data></array></value>";
        }

        public Arg(IEnumerable<Arg[]> f) : this(f?.Select(x => new Arg(x)))
        {
        }
        
        public Arg(byte[] f)
        {
            if (f is null)
            {
                throw new ArgumentNullException(nameof(f));
            }

            value = $"<value><base64>{Convert.ToBase64String(f)}</base64></value>\n";
        }

        public Arg(in ArraySegment<byte> f)
        {
            if (f.Array is null)
            {
                throw new NullReferenceException(nameof(f.Array));
            }
            
            value = $"<value><base64>{Convert.ToBase64String(f.Array, f.Offset, f.Count)}</base64></value>\n";
        }
        
        public Arg(IEnumerable<(string name, Arg value)> f)
        {
            if (f is null)
            {
                throw new NullReferenceException(nameof(f));
            }
            
            value = $"<value><struct>" +
                    string.Join("", f.Select(tuple => $"<member><name>{tuple.name}</name>{tuple.value}</member>")) +
                    $"</struct></value>\n";
        }
        
        public Arg(IEnumerable<(string name, object value)> f)
        {
            if (f is null)
            {
                throw new NullReferenceException(nameof(f));
            }
            
            value = $"<value><struct>" +
                    string.Join("", f.Select(tuple => $"<member><name>{tuple.name}</name>{Create(tuple.value)}</member>")) +
                    $"</struct></value>\n";
        }

        public override string ToString()
        {
            return value;
        }

        public static implicit operator string(Arg arg) => arg?.ToString();

        public static implicit operator Arg(bool f) => new Arg(f);
        public static implicit operator Arg(double f) => new Arg(f);
        public static implicit operator Arg(int f) => new Arg(f);
        public static implicit operator Arg(Uri f) => new Arg(f);
        public static implicit operator Arg(Uri[] f) => new Arg(f);
        public static implicit operator Arg(string f) => new Arg(f);
        public static implicit operator Arg(string[] f) => new Arg(f);
        public static implicit operator Arg(string[][] f) => new Arg(f);
        public static implicit operator Arg((string, string) f) => new Arg(f);
        public static implicit operator Arg((string, string)[] f) => new Arg(f);
        public static implicit operator Arg(Arg[] f) => new Arg(f);
        public static implicit operator Arg(Arg[][] f) => new Arg(f);

        public static Arg Create(object o)
        {
            return o switch
            {
                bool i => new Arg(i),
                double i => new Arg(i),
                int i => new Arg(i),
                string i => new Arg(i),
                object[] i => new Arg(i.Select(Create)),
                byte[] i => new Arg(i),
                DateTime i => new Arg(i),
                List<(string, object)> i => new Arg(i),
                _ => null
            };
        }
    }
}
