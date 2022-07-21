/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [DataContract]
    public sealed class MarkerArray : IDeserializableCommon<MarkerArray>, IMessageCommon
    {
        [DataMember (Name = "markers")] public Marker[] Markers;
    
        public MarkerArray()
        {
            Markers = System.Array.Empty<Marker>();
        }
        
        public MarkerArray(Marker[] Markers)
        {
            this.Markers = Markers;
        }
        
        public MarkerArray(ref ReadBuffer b)
        {
            b.DeserializeArray(out Markers);
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new Marker(ref b);
            }
        }
        
        public MarkerArray(ref ReadBuffer2 b)
        {
            b.DeserializeArray(out Markers);
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new Marker(ref b);
            }
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new MarkerArray(ref b);
        
        public MarkerArray RosDeserialize(ref ReadBuffer b) => new MarkerArray(ref b);
        
        public MarkerArray RosDeserialize(ref ReadBuffer2 b) => new MarkerArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Markers);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeArray(Markers);
        }
        
        public void RosValidate()
        {
            if (Markers is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Markers.Length; i++)
            {
                if (Markers[i] is null) BuiltIns.ThrowNullReference(nameof(Markers), i);
                Markers[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetArraySize(Markers);
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Markers);
        }
    
        public const string MessageType = "visualization_msgs/MarkerArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "d155b9ce5188fbaf89745847fd5882d7";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71X227bOBB9Xn3FAEHRZOHIuXS73QB+SGM3MZDb2m67RVEYtETbbGRRJam47tfvGVKS" +
                "4yZp92HTxIgpijNz5naGuRDmRpqPn2jhFzaKOv/zT3QxPD2iW2VLkalvwimdjxd2ZtsX3mK0RUMpae5c" +
                "cdRuL5fL2GgbazNrL9WNaptb9a3dVbbIxGq0KmQtRiJPfyw0Kp02SmS1hH12ePzsYO+1sCrB93AuoI2m" +
                "2tBCG0kqx3Lh4RE+pVX5jNxcWVpIa8VM0lK5ObHqKCpV7l7R8WBw9b6zVz2dvH3d6+xXD8Prs96g1zmo" +
                "33047192e4POYbWBx954OBr0rzsv7m6d94ejzh93NIadlxtqw96f1d71Vf9yNOy8qh5HvX9G43f93vvx" +
                "m+OT/uVp56/qxUVveDYe9IZXbwcnAFrDBobjy9PzSun+fuNct9u4dnHV7b/50Dx2e+e90dq58Hh8fg7v" +
                "ojMpUmRnHr4e+dmq33P0nVrI9tSIxUYOIusMZyC3jykJii4hZwuRSHKaUCS84KTpyWeZOGiM4xjJlCmW" +
                "lOj8c5knPsc+myplscRI4SQJKnP1pZSUMxYPbS4rRRE8PTzg87/99gM0ldV+l01Oy+xBo6w1b2D7AhS5" +
                "KsoMjsNjLuxUZtKFAqwREF6jWwIOh054HMkWcaeQnm6CFwHD41J7JNK0vdCpmq4Ao5Ju0T5tp7IwMgGC" +
                "dKdFBwEfuufOocP1ZpZVuzaaSb2QzqxCx19rK6ngP/eN+3eAfCfmm8LvsKXNIdlEZPI74aHf25Cm/RZ+" +
                "0bwCJZTKqSgzR9sls1C2In6BcJL9Ugojd1BtabByojNtBqevj5E3rL6z49/Sx714b3c/3vsUpaUJjJGp" +
                "qeRCfjiwZ3pJmd5Mp53rMkuRVetoIqdMQRPp8w/e4h5IPNAQ1DQmZCf4wkdvUQkTrTPyfTPOdHKDCn/Q" +
                "dn9asVigzcrsRAbR3SDaIhXLmAyiDRPchL5h0BvK2XCS2OjKd6t1soiirasc+EJrhcD7qrSFTNRUYXcu" +
                "LFlkkM/U7bSgbTmLK85q3aHBFqFRd+7VCzBgNhX8bZ/G4lZeLiYIDIrHZxyRKpESiU7FLuK0R1AkUScZ" +
                "c4Vv3kaiBnZ5NeodofCLuSDEOteOVtJ5qA+UFjwKphBECqK68czDll9dM5ArIuS9x85vUPv3gsjXfGyk" +
                "1aVJZKgavwXpsYQjaSoBkMmFh+VTzf8mCmFA8NR3YDphUu5EkQonvCtzNUPcdzNUWwYhsSjgo3/LubYx" +
                "BEdczvjMZC6NbxIfCCZyvViAw5mnqkK9Iw9JsLGgQhg0F9jW4Lw2qcr5uK9x1o6PRbZlDm7ud4+Yvq1M" +
                "SqcAaAUNPCz83QAk7wcgmBUC6LSPA233P0Vbo6Xexb6cSbNGgcIRjlHLr2BSy4CFPYKx34OXMYwgShLm" +
                "Ukvbfm+MR7tDsAYsstDJnLbhwvXKzUE5XIm3AkmbgPqgmPkCWp+z0POdO5pzrzoXua7VB41rG/9Fbd7o" +
                "ZZ9250hexmGw5QyRxMHC6FuFWqLJyitJMiVzjC01McKsIs+O3mS09cYTiqclnxp8C2t1onjA+CFZF2/g" +
                "N5U+VVnen09w8BhEyEkC/Oo+OA1TC1GaGsmMg8nd4nLj7bR6r/xZHt+4d9ayMUWexZoD0d8l91ru9a7P" +
                "/SoHAaVuIdSCEwoThbPV4IcvIvDaprvRNNPCvXxBX5vVqll9+zXw16GrfWgSFe4i63huguenL+u484CL" +
                "o594VK+Wv8a36nrzkGN0699tuhQzU/U9pfhhwJcDxyTYSEIwVbi1hTIcgVb9NQOz3lGqpR9T0LEQN1Ap" +
                "0d8sLYoCysC2fBHIQij9NQBjNJ7FLVrOZR5O+csKo/BErBIyasYX6voK0QgLqpxrkZseoL/5gsiYgzGU" +
                "H5QYHRK3E/OVZaVLWrJDWJiK/zVP4xqXpyendcvP+qDigVpv/nlDnTtMnp9m/WlSff8OEExidphmNWtW" +
                "k2Ylouhfq3/bBKIPAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
