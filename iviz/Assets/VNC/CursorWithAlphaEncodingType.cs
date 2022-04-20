using System.IO;
using MarcusW.VncClient;
using MarcusW.VncClient.Protocol.EncodingTypes;
using MarcusW.VncClient.Protocol.Implementation.EncodingTypes.Pseudo;

namespace Iviz.VncClient.Extensions
{
    /// <summary>
    /// A pseudo encoding type to detect the last rectangle in a framebuffer update message.
    /// </summary>
    public class CursorWithAlphaEncodingType : PseudoEncodingType
    {
        /// <inheritdoc />
        public override int Id => (int)WellKnownEncodingType.CursorWithAlpha;

        /// <inheritdoc />
        public override string Name => "Cursor With Alpha";

        /// <inheritdoc />
        public override bool GetsConfirmed => true;

        /// <inheritdoc />
        public override void ReadPseudoEncoding(Stream transportStream, Rectangle rectangle)
        {
        }
    }
}