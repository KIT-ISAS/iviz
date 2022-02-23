/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.PclMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PolygonMesh : IDeserializable<PolygonMesh>, IMessage
    {
        // Separate header for the polygonal surface
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Vertices of the mesh as a point cloud
        [DataMember (Name = "cloud")] public SensorMsgs.PointCloud2 Cloud;
        // List of polygons
        [DataMember (Name = "polygons")] public Vertices[] Polygons;
    
        /// Constructor for empty message.
        public PolygonMesh()
        {
            Cloud = new SensorMsgs.PointCloud2();
            Polygons = System.Array.Empty<Vertices>();
        }
        
        /// Explicit constructor.
        public PolygonMesh(in StdMsgs.Header Header, SensorMsgs.PointCloud2 Cloud, Vertices[] Polygons)
        {
            this.Header = Header;
            this.Cloud = Cloud;
            this.Polygons = Polygons;
        }
        
        /// Constructor with buffer.
        public PolygonMesh(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Cloud = new SensorMsgs.PointCloud2(ref b);
            Polygons = b.DeserializeArray<Vertices>();
            for (int i = 0; i < Polygons.Length; i++)
            {
                Polygons[i] = new Vertices(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new PolygonMesh(ref b);
        
        public PolygonMesh RosDeserialize(ref ReadBuffer b) => new PolygonMesh(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Cloud.RosSerialize(ref b);
            b.SerializeArray(Polygons);
        }
        
        public void RosValidate()
        {
            if (Cloud is null) throw new System.NullReferenceException(nameof(Cloud));
            Cloud.RosValidate();
            if (Polygons is null) throw new System.NullReferenceException(nameof(Polygons));
            for (int i = 0; i < Polygons.Length; i++)
            {
                if (Polygons[i] is null) throw new System.NullReferenceException($"{nameof(Polygons)}[{i}]");
                Polygons[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += Cloud.RosMessageLength;
                size += BuiltIns.GetArraySize(Polygons);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "pcl_msgs/PolygonMesh";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "45a5fc6ad2cde8489600a790acc9a38a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71WUW/bNhB+nn4FUT80LmIPSbosCGAMxYKsAbo0WLK9FEVAkSeLmES6JOVM/fX7jhRt" +
                "L8jDHtYZDmKd7j5+d/fdSTNxTxvpZSTRktTkReO8iC2JjevGtbOyE2HwjVRUvc8O2a+aiT/IR6MoCNek" +
                "iJ5CK2QQErHGRqE6N+gqkA3OP/ZhHb6/Y/vPbD6d7s7EBxMiI0znharAfvq8t1Wr//hT/Xr/y6UIUWdi" +
                "70tO91FaLb1GMlFqGWWqR2vWLflFR1tCOaLsN6RFuhvHDYUlAh9aEwS+a7LkZdeNYghwik4o1/eDNYpr" +
                "HA2KdBiPSGO5YpKTHjrp4e+8NpbdGy97YnR8A30ZyCoSN1eX8LGB1BANCI1AUJ5kMHaNm6IaUOSzUw6o" +
                "Zg9PboFLWqNxu8PRLRmZLP218RSYpwyXOONNTm4JbBSHcIoO4ijZHnEZ5gKHgAJtnGrFEZjfjbF1NrV/" +
                "K72RdUcMrFABoL7moNfzA2SboK20rsBnxP0Z/wbW7nA5p0WLnnWcfRjWKCAcN95tjYZrPSYQ1RmCIjtT" +
                "e+nHiqPykdXsmmsMJ0SljuC/DMEpgwZo8WRiW4XoGT1149Hob6bGlyeliAvNC3KNOXWd5iFTDsVQ0aBO" +
                "GJ/bhUZSNpg0smkAw7F4ag0a1csRICh3lCw2rU3MbsZC3b1MGGFQaXotWzrEsmyAF8djQVEtQYKAkke7" +
                "VCpE55N6wKeGZv0o6s7VCI5BdHJ0A3wpKG/qXTMmKujHbnO8agwhqVdCei/HZZVSpsM1kk9EIqIm4fxa" +
                "WvMViKcaculRlUVn/qQ57ogTXipHA9LARJOeL8XdHiYcxII0wlN0KMjQjR5Uoso00W8vkcAmtlN3wq5O" +
                "IQLe8YlJg65ZNB3WRMzsWWBILgdl8lJ9GUxIpT8WUGwW5rNh5wE+4pVzpkuxw3z5bPXizukVCPhBxcFT" +
                "qeJBuZbippmEz8VDp3YFOQYK84QROCeJyZPRyBBeHNKRXePqBdCyWTJAuUrBzOlqanSGUZhKS10oqRpf" +
                "BGHyYE96SbVh0Syr1KhrlgJ2f5ZEVdXOdQIfEx5rg+2qjbRiJm7CwczubvxUSGXej2jSBrF4yuySKk8n" +
                "sKjHSGEX4d1T8X8egVt7/xkHXIBhOrp8ZuIdmlFGL907xm77mvbWUcF+k0s3rw4xvpsAPv2Gmfg8ZYx0" +
                "NcRDe/wHPwAsdQU9l/izDqy2sjN6Usr/tplSk15eTNzaPPKbspucLTpCgn6cBIDww/eBgpI3Utbahbi5" +
                "fbjg9FfiZLL8PplW4nTvc3KeLGcHPmxaibd7H+4xLD8c+LBpJc4ny/WHj+/YtBI/HlrO38JyUZWngOUp" +
                "nVpyK/OYJ60WIbmmCRSzw8f8u/Gu5+eNn950uBR5fKeDkg74XYKDrspvsgMvoLwxAqHptdtSOUe5wcaJ" +
                "yHsItJd2FNRRnzbrNGSZ2TeSxUZ1WRPlle0fr3N5yHSyZ8rQ+7Z4Vn8Dl24DbnoKAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
