using System;
using System.Globalization;
using System.Text;

namespace Iviz.Tools;

public static class Defaults
{
    static UTF8Encoding? utf8;
    static Random? random;
    
    public static UTF8Encoding UTF8 => utf8 ??= new UTF8Encoding();
    public static CultureInfo Culture => CultureInfo.InvariantCulture;
    public static Random Random => random ??= new Random();
}