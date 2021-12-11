using System.Globalization;
using System.Text;

namespace Iviz.Tools;

public static class Defaults
{
    public static readonly UTF8Encoding UTF8 = new(false);

    public static readonly CultureInfo Culture = CultureInfo.InvariantCulture;
}