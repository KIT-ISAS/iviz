/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MapMetaData : IDeserializable<MapMetaData>, IMessage
    {
        // This hold basic information about the characterists of the OccupancyGrid
        // The time at which the map was loaded
        [DataMember (Name = "map_load_time")] public time MapLoadTime;
        // The map resolution [m/cell]
        [DataMember (Name = "resolution")] public float Resolution;
        // Map width [cells]
        [DataMember (Name = "width")] public uint Width;
        // Map height [cells]
        [DataMember (Name = "height")] public uint Height;
        // The origin of the map [m, m, rad].  This is the real-world pose of the
        // cell (0,0) in the map.
        [DataMember (Name = "origin")] public GeometryMsgs.Pose Origin;
    
        /// Constructor for empty message.
        public MapMetaData()
        {
        }
        
        /// Explicit constructor.
        public MapMetaData(time MapLoadTime, float Resolution, uint Width, uint Height, in GeometryMsgs.Pose Origin)
        {
            this.MapLoadTime = MapLoadTime;
            this.Resolution = Resolution;
            this.Width = Width;
            this.Height = Height;
            this.Origin = Origin;
        }
        
        /// Constructor with buffer.
        internal MapMetaData(ref Buffer b)
        {
            MapLoadTime = b.Deserialize<time>();
            Resolution = b.Deserialize<float>();
            Width = b.Deserialize<uint>();
            Height = b.Deserialize<uint>();
            b.Deserialize(out Origin);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MapMetaData(ref b);
        
        MapMetaData IDeserializable<MapMetaData>.RosDeserialize(ref Buffer b) => new MapMetaData(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(MapLoadTime);
            b.Serialize(Resolution);
            b.Serialize(Width);
            b.Serialize(Height);
            b.Serialize(ref Origin);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 76;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "nav_msgs/MapMetaData";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "10cfc8a2818024d3248802c00c95f11b";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1TTU/DMAy951dY2gWk0U2AOCBx4LTTxBDcpmnKUq+x1CYlSVXGr8dJP7bBgQtalUNs" +
                "P9vPz+kE3jV50LbMYSc9KSCzt66SgawBubNNgKARlJZOqoCOfPBg98n5olRTS6MOC0e5EBOuhRCoQpAB" +
                "Wk1KJ1gla2ilh9LKHHORAOzbRnsbrT4z4hx6Wzap+bqaKSzLjdgzMNzdnsQ4YRmLUh40rCPKb0RDJqKS" +
                "swdopEKHn4jO2ze1jgoyw0SRwrqaAh8n800GnTx8YtShLG9a61ir2nrsk7hQrA9X8+n8muUbCmWiQFth" +
                "cIdt5Qs/W6WU1E48/fMnlm+LR/jdj7k9M+2apUMTuqUy68Seme4dIvhaKpyCslV0532cugdg2HY05GYg" +
                "VpZFHAHitZH8KEyqe8SJCw3IVNIaeT/Kcm8y3aJG/jyLZCtSPhtXpDf1cA+f4+0w3r4uQ/8o3TDDuCjP" +
                "wp/qeU4+Wh9H3ePvmok/JhpurRDfRIxpGPMDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
