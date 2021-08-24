using System.Globalization;
using System.Text;

namespace Iviz.Tools
{
    public static class Defaults
    {
        public static UTF8Encoding UTF8 { get; } = new(false);

        public static CultureInfo Culture { get; } = CultureInfo.InvariantCulture;
    }
}