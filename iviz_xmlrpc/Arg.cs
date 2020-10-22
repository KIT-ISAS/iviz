//#define DEBUG__

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Msgs;

namespace Iviz.XmlRpc
{
    public readonly struct Arg
    {
        readonly string content;

        public bool IsValid => content != null;

        Arg(bool f)
        {
            content = f ? "<value><boolean>1</boolean></value>\n" : "<value><boolean>0</boolean></value>\n";
        }

        Arg(double f)
        {
            content = $"<value><double>{f.ToString(BuiltIns.Culture)}</double></value>\n";
        }

        Arg(int f)
        {
            content = f == 0
                ? "<value><i4>0</i4></value>\n"
                : $"<value><i4>{f.ToString(BuiltIns.Culture)}</i4></value>\n";
        }

        Arg(Uri f) : this(ThrowIfNull(f).ToString())
        {
        }

        public Arg(IEnumerable<Uri> f) : this(ThrowIfNull(f).Select(x => new Arg(x)))
        {
        }

        Arg(in DateTime f)
        {
            content = $"<value><dateTime.iso8601>{f.ToString("yyyy-MM-ddTHH:mm:ssZ")}</dateTime.iso8601></value>\n";
        }

        public Arg(string f)
        {
            ThrowIfNull(f);
            content = $"<value>{f}</value>\n";
        }

        public Arg(IEnumerable<string> f) : this(ThrowIfNull(f).Select(x => new Arg(x)))
        {
        }

        public Arg(IEnumerable<string[]> f) : this(ThrowIfNull(f).Select(x => new Arg(x)))
        {
        }

        Arg((string, string) f) : this(new Arg[] {ThrowIfNull(f.Item1), ThrowIfNull(f.Item2)})
        {
        }

        public Arg(IEnumerable<(string, string)> f) : this(ThrowIfNull(f).Select(x => new Arg(x)))
        {
        }

        public Arg(IEnumerable<Arg> f)
        {
            ThrowIfNull(f);
            content = $"<value><array><data>{string.Join("", f)}</data></array></value>";
        }

        public Arg(IEnumerable<Arg[]> f) : this(ThrowIfNull(f).Select(x => new Arg(x)))
        {
        }

        Arg(byte[] f)
        {
            ThrowIfNull(f);
            content = $"<value><base64>{Convert.ToBase64String(f)}</base64></value>\n";
        }

        public Arg(in ArraySegment<byte> f)
        {
            ThrowIfNull(f.Array);
            content = $"<value><base64>{Convert.ToBase64String(f.Array, f.Offset, f.Count)}</base64></value>\n";
        }

        public Arg(IEnumerable<(string name, Arg value)> f)
        {
            ThrowIfNull(f);
            content = "<value><struct>" +
                      string.Join("",
                          f.Select(tuple => $"<member><name>{tuple.name}</name>{tuple.value.content}</member>")) +
                      "</struct></value>\n";
        }

        public Arg(IEnumerable<(string name, object value)> f)
        {
            ThrowIfNull(f);
            content = "<value><struct>" +
                      string.Join("",
                          f.Select(tuple =>
                              $"<member><name>{tuple.name}</name>{Create(tuple.value).content}</member>")) +
                      "</struct></value>\n";
        }

        static T ThrowIfNull<T>(T t)
        {
            return t ?? throw new NullReferenceException(nameof(t));
        }

        public override string ToString()
        {
            return content ?? throw new InvalidOperationException("Arg has no valid value");
        }

        public static implicit operator string(Arg arg)
        {
            return arg.ToString();
        }

        public static implicit operator Arg(bool f)
        {
            return new Arg(f);
        }

        public static implicit operator Arg(double f)
        {
            return new Arg(f);
        }

        public static implicit operator Arg(int f)
        {
            return new Arg(f);
        }

        public static implicit operator Arg(Uri f)
        {
            return new Arg(f);
        }

        public static implicit operator Arg(Uri[] f)
        {
            return new Arg(f);
        }

        public static implicit operator Arg(string f)
        {
            return new Arg(f);
        }

        public static implicit operator Arg(string[] f)
        {
            return new Arg(f);
        }

        public static implicit operator Arg(string[][] f)
        {
            return new Arg(f);
        }

        public static implicit operator Arg((string, string) f)
        {
            return new Arg(f);
        }

        public static implicit operator Arg((string, string)[] f)
        {
            return new Arg(f);
        }

        public static implicit operator Arg(Arg[] f)
        {
            return new Arg(f);
        }

        public static implicit operator Arg(Arg[][] f)
        {
            return new Arg(f);
        }

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
                _ => throw new InvalidOperationException("Type is not supported")
            };
        }
    }
}