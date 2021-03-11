/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/TimeReference")]
    public sealed class TimeReference : IDeserializable<TimeReference>, IMessage
    {
        // Measurement from an external time source not actively synchronized with the system clock.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; } // stamp is system time for which measurement was valid
        // frame_id is not used 
        [DataMember (Name = "time_ref")] public time TimeRef { get; set; } // corresponding time from this external source
        [DataMember (Name = "source")] public string Source { get; set; } // (optional) name of time source
    
        /// <summary> Constructor for empty message. </summary>
        public TimeReference()
        {
            Source = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public TimeReference(in StdMsgs.Header Header, time TimeRef, string Source)
        {
            this.Header = Header;
            this.TimeRef = TimeRef;
            this.Source = Source;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TimeReference(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            TimeRef = b.Deserialize<time>();
            Source = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TimeReference(ref b);
        }
        
        TimeReference IDeserializable<TimeReference>.RosDeserialize(ref Buffer b)
        {
            return new TimeReference(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(TimeRef);
            b.Serialize(Source);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Source is null) throw new System.NullReferenceException(nameof(Source));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Source);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/TimeReference";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "fded64a0265108ba86c3d38fb11c0c16";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1TwYrbMBC9C/IPAz5stpAU2lugt6XtHhYKu/cwkSaWqCy5o3Gy7td3JCfb7K2HCoOx" +
                "PfPezHvPHTwRlolpoCRw5DwAJqBXIU4YQcJAUPLEliBlAbQSThRnKHOynnMKv8nBOYgH8Vo5F6EBbMz2" +
                "59aY74SOGPxy09NBERxGCOVa2giOmeHsg/Uw3AxzxgInjMGZ2vrudDopDrQPrkLVwaaicxjT4KCh7pmO" +
                "tdJmZipjTi6k/sJX1xSvrW+LLjuaIlyrLhs3pnUeJWStuYeknJCPt6qYlfnyn8/KPD1/26lSbj+Uvnxc" +
                "VFyZDp4Fk0N2KpOgQ8GmnA+9J95EUmMWfVWK9lXmkcpWG1/qrnr1lIgxqn9NL8mqzjBMKVgUamu969fO" +
                "kABhRJZgp4is9ZlVyFreLKjoehX6NVFSyR4fdlqTCtnpkpSQLKurVdbHBzBTSPL5U20w3cs5b/SRek3H" +
                "G7kag1KHpddRjatzYtkpx4dlua1iqzqkLK7Aur3b62O5ByWxBDRmTdJaJ/8xi8+pJfOEHPAQqQJbVUBR" +
                "72rT3f0Nch17py6nfIVfEP9y/AtsRVlw604br57FFqqpVwG1cOR8Ck5LD3MDsTHUvMdwYOR5yXCjNN3X" +
                "qvES1eaI3rGUbIMasPx318hefwgN5B/2BVgR1AMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
