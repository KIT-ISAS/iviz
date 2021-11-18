/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/ARMarkerArray")]
    public sealed class ARMarkerArray : IDeserializable<ARMarkerArray>, IMessage
    {
        [DataMember (Name = "markers")] public ARMarker[] Markers;
    
        /// <summary> Constructor for empty message. </summary>
        public ARMarkerArray()
        {
            Markers = System.Array.Empty<ARMarker>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ARMarkerArray(ARMarker[] Markers)
        {
            this.Markers = Markers;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ARMarkerArray(ref Buffer b)
        {
            Markers = b.DeserializeArray<ARMarker>();
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new ARMarker(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ARMarkerArray(ref b);
        }
        
        ARMarkerArray IDeserializable<ARMarkerArray>.RosDeserialize(ref Buffer b)
        {
            return new ARMarkerArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Markers, 0);
        }
        
        public void RosValidate()
        {
            if (Markers is null) throw new System.NullReferenceException(nameof(Markers));
            for (int i = 0; i < Markers.Length; i++)
            {
                if (Markers[i] is null) throw new System.NullReferenceException($"{nameof(Markers)}[{i}]");
                Markers[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Markers);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/ARMarkerArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "38745a121d365c2cc5cc1b47928542b2";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTW/bRhA9ewH9hwF8iB3IapsEBWrAB8N2Ex+COI5bIDACYkWOyE3IXWZ3KZn+9X2z" +
                "lKgoMJocYgsExI95783Mm9nT67faf2F/+4madBPURJ384t9Evf3w+pjM0txnTSjDb6dr1Yma95Hp5uPV" +
                "RXZ6/c/ZOzqh37959/767N35BV7+odQb1gV7qtLfEBP7llWI3tiSclewKtk1HH0/qPzLeXT+5e2rT/jq" +
                "LWrb29un1txxTa0LJhpnaWViRfdJd1E7Hf98dfsX4nXDXmfGCnkw+Z4gvVuhSZ+dp/H9d4pXLvAGCwVW" +
                "au5cTZUOGd+tIcOHtda66Vkw9wy1rGmg9BCpgDLPtY5myVl02aCy93huhVgM+kPjJ2qfPkRtC+0LQna6" +
                "0FHTAs2oTFmxP6p5ib6GqJuWC0pfxZ8wA/CmMoFwlQwXdF331AUERQdjmqazJtdip2l4Bw+ksaSp1T6a" +
                "vKu1R7zzhbESvvBogbDjCvy1Y5szXZ4fI8YGzjvpFJSMzT3rIDNyeU6qg3cvXwhA7d+s3BEeucRcjeIU" +
                "Kx0lWb5rPQfJU4djaDwfipuBG91hqBSBDtK7DI/hkCCCFLh1eUUHyPyqjxVGLFZMS+2NntcsxDk6ANZn" +
                "Anp2+A2zpH1MVlu3oR8Ytxo/Q2tHXqnpqIJntVQfuhINRGDr3dIUCJ33iSSvDdtItZl77XslqEFS7f8t" +
                "PUYQUMkR/OsQXG5gQJF2Z7N/yY3MFI83kA8u92QzXZ7FLdSBDGmZPsrwLDyjmFbnPJM5uUzOOou5aFij" +
                "aIzgiASwMB5QnAszsLJnzDdPyUQqHAeyLoKj0V9AyWizoHXbggyz7rUNsp7ijBPIAc/K2ZRWFdshStqU" +
                "hjqtgcnJm9IUAxJCzQjWtK5uSnHxAm2u6yHnQQyegcS7mACHM7pcUO86WklBuPHr7XM05zGvNCXRuams" +
                "3pri+3MGu4C2hKBLDJQNEYs/U+NRdTfe9ePd/RO5LWegWH26dWvolluko3HX6qkcK/K6WH8fjnosAjlv" +
                "NlgMxFDyJkC97zDW3ibebdxTTXRKZpxn7H/UMCFt6FgCysFxmLLeqVj9r0lPVcG2fw+u5U5Xd/OXp6/b" +
                "7ss2/GjyxrsVyvsPd7KjA8cIAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
