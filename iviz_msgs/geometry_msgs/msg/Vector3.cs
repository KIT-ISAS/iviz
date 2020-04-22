
namespace Iviz.Msgs.geometry_msgs
{
    public struct Vector3 : IMessage
    {
        // This represents a vector in free space. 
        // It is only meant to represent a direction. Therefore, it does not
        // make sense to apply a translation to it (e.g., when applying a 
        // generic rigid transformation to a Vector3, tf2 will only apply the
        // rotation). If you want your data to be translatable too, use the
        // geometry_msgs/Point message instead.
        
        public double x;
        public double y;
        public double z;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Vector3";
    
        public IMessage Create() => new Vector3();
    
        public int GetLength() => 24;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeStruct(out this, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeStruct(this, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "4a842b65f413084dc2b10fb484ea7f17";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACkWQwUrFQAxF9wX/4cLbKJQRVPyHt3PxcCt5bTodbCclk2etX2/aSt1dhpyTmznh0qcC" +
                "5Um5cLYCwhc3JoqU0SkzykQNB1QnnA0+K3lYMDJlg8k/6WCb1NEkObiVlTtRrpEMrXBBFnPHSJ+u5Fx4" +
                "pWmaXEYwpVwGWtn12ZF7DjHUmHvO+1TK0QfdEDmzpgaaYmp30heNB0x43w54rmHdE+Y0DHvnfZn17BIV" +
                "24CHgHOHRW6Y14M8KFoybyS4esW/XnQd1r5S47YW3xSRZWTT5WMssTy+SXJ+5FIosv9dMaY2VFU3CNnr" +
                "C76PtBzp5676BVNGoPyAAQAA";
                
    }
}
