using MarcusW.VncClient;

namespace VNC
{
    internal static class SupportedFormats
    {
        public static readonly PixelFormat RfbRgb565 = 
            new("RFB RGB565", 16, 16, false, true, false, 31, 63, 31, 0, 11, 5, 0, 0);
        
        public static readonly PixelFormat RfbRgb888 = 
            new("RFB RGB888", 24, 24, false, true, false, 255, 255, 255, 0, 0, 8, 16, 0);
    }
}