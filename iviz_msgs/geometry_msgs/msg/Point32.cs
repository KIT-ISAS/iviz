
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class Point32 : IMessage
    {
        // This contains the position of a point in free space(with 32 bits of precision).
        // It is recommeded to use Point wherever possible instead of Point32.  
        // 
        // This recommendation is to promote interoperability.  
        //
        // This message is designed to take up less space when sending
        // lots of points at once, as in the case of a PointCloud.  
        
        public float x;
        public float y;
        public float z;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Point32";
    
        public IMessage Create() => new Point32();
    
        public int GetLength() => 12;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out x, ref ptr, end);
            BuiltIns.Deserialize(out y, ref ptr, end);
            BuiltIns.Deserialize(out z, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(x, ref ptr, end);
            BuiltIns.Serialize(y, ref ptr, end);
            BuiltIns.Serialize(z, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "cc153912f1453b708d221682bc23d9ac";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACj1QMW7DMAzcDfQPB2RpgSJD8oRO3Tr0A7JF20RlURDppunrSylpAA0UeXe84wGfKysm" +
                "yRY4K2wlFFE2lgyZEfzH2cAZcyWCljDR84VtxfmEkU0bqlSaWJ3ychwOeHe4wluybRQpwgS7Ej660mWl" +
                "St9U2xrlMZFrq1GITahDzqcj4Dr+urm7Uo6hu/KOC5Yqm1gjG1UpVMPIie3aqf/MjVTD4iBFJOUl38xY" +
                "+CLsBcnHt0TNVYb6Ds6Ls5PcgzU/imCQPNErgrZLtCNNwRP1A3XPb0n22HYPc5LgEfDzqK6P6vdp+ANL" +
                "+MezcQEAAA==";
                
    }
}
