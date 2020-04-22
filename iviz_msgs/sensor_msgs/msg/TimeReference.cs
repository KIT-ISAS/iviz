
namespace Iviz.Msgs.sensor_msgs
{
    public sealed class TimeReference : IMessage
    {
        // Measurement from an external time source not actively synchronized with the system clock.
        
        public std_msgs.Header header; // stamp is system time for which measurement was valid
        // frame_id is not used 
        
        public time time_ref; // corresponding time from this external source
        public string source; // (optional) name of time source
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/TimeReference";
    
        public IMessage Create() => new TimeReference();
    
        public int GetLength()
        {
            int size = 12;
            size += header.GetLength();
            size += source.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public TimeReference()
        {
            header = new std_msgs.Header();
            source = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out time_ref, ref ptr, end);
            BuiltIns.Deserialize(out source, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(time_ref, ref ptr, end);
            BuiltIns.Serialize(source, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "fded64a0265108ba86c3d38fb11c0c16";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACq1TwYrbMBC9C/IPAz5stpAU2lugt6XtHhYKu/cwkSaWqCy5o3Gy7td3JCfb7K2HCoOx" +
                "PfPezHvPHTwRlolpoCRw5DwAJqBXIU4YQcJAUPLEliBlAbQSThRnKHOynnMKv8nBOYgH8Vo5F6EBbMz2" +
                "59aY74SOGPxy09NBERxGCOVa2giOmeHsg/Uw3AxzxgInjMGZ2vrudDopDrQPrkLVwaaicxjT4KCh7pmO" +
                "tdJmZipjTi6k/sJX1xSvrW+LLjuaIlyrLhs3pnUeJWStuYeknJCPt6qYlfnyn8/KPD1/26lSbj+Uvnxc" +
                "VFyZDp4Fk0N2KpOgQ8GmnA+9J95EUmMWfVWK9lXmkcpWG1/qrnr1lIgxqn9NL8mqzjBMKVgUamu969fO" +
                "kABhRJZgp4is9ZlVyFreLKjoehX6NVFSyR4fdlqTCtnpkpSQLKurVdbHBzBTSPL5U20w3cs5b/SRek3H" +
                "G7kag1KHpddRjatzYtkpx4dlua1iqzqkLK7Aur3b62O5ByWxBDRmTdJaJ/8xi8+pJfOEHPAQqQJbVUBR" +
                "72rT3f0Nch17py6nfIVfEP9y/AtsRVlw604br57FFqqpVwG1cOR8Ck5LD3MDsTHUvMdwYOR5yXCjNN3X" +
                "qvES1eaI3rGUbIMasPx318hefwgN5B/2BVgR1AMAAA==";
                
    }
}
