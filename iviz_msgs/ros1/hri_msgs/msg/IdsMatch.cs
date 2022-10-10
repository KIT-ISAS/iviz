/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.HriMsgs
{
    [DataContract]
    public sealed class IdsMatch : IHasSerializer<IdsMatch>, IMessage
    {
        // This message encodes possible matches between transient IDs (face, body,
        // voice) and permanent IDs (person), with their corresponding confidence level.
        // Such messages should be published on the /humans/candidate_matches topic.
        //
        // confidence == 0.0 has the special meaning of: those two IDs are *not* associated.
        // This can be use for instance if one of the ID (eg, a face id) is not valid anymore
        // (face not tracked anymore)
        //
        // if only `id1` is set, and `id1_type` is *not* PERSON, then:
        //  - an 'anonymous' person is created, whose ID will match the face/body/voice ID
        //  - that person's `/anonymous` subtopic will publish the value 'true'
        //  - similar to regular persons, a confidence value of 0.0 means that the
        //  anonymous person does not exist anymore (typically because the corresponding
        //  face/body/voice is not seen/heard anymore)
        //
        // Note that setting only `id2` or setting only `id1` with `id1_type == PERSON`
        // are both invalid.
        //
        // ## Examples
        //
        // For instance, a face recognition module might publish the following message
        // to indicate that face 'a3eb4' has been recognised with 63% confidence as
        // person 'b31ad':
        //
        // {id1: 'a3eb4', id1_type: FACE, id2: 'b31ad', id2_type: PERSON, confidence:
        // 0.63, }
        //
        // Another example: a module that detect both faces and skeleton might publish a
        // 100% confidence match between the two:
        //
        // {id1: 'ff424', id1_type: FACE, id2: '850d1', id2_type: BODY, confidence:
        // 1.0, }
        //
        // Example of an 'anonymous' face: a face _detector_ (ie, not performing face
        // recognition) can publish the following message to inform the system that an
        // (so far anonymous) person has been detected:
        //
        // {id1: 'ff424', id1_type: FACE, confidence: 0.0,
        // }
        //
        public const sbyte UNSET = 0;
        public const sbyte PERSON = 1;
        public const sbyte FACE = 2;
        public const sbyte BODY = 3;
        public const sbyte VOICE = 4;
        [DataMember (Name = "id1")] public string Id1;
        [DataMember (Name = "id1_type")] public sbyte Id1Type;
        [DataMember (Name = "id2")] public string Id2;
        [DataMember (Name = "id2_type")] public sbyte Id2Type;
        [DataMember (Name = "confidence")] public float Confidence;
    
        public IdsMatch()
        {
            Id1 = "";
            Id2 = "";
        }
        
        public IdsMatch(ref ReadBuffer b)
        {
            Id1 = b.DeserializeString();
            b.Deserialize(out Id1Type);
            Id2 = b.DeserializeString();
            b.Deserialize(out Id2Type);
            b.Deserialize(out Confidence);
        }
        
        public IdsMatch(ref ReadBuffer2 b)
        {
            b.Align4();
            Id1 = b.DeserializeString();
            b.Deserialize(out Id1Type);
            b.Align4();
            Id2 = b.DeserializeString();
            b.Deserialize(out Id2Type);
            b.Align4();
            b.Deserialize(out Confidence);
        }
        
        public IdsMatch RosDeserialize(ref ReadBuffer b) => new IdsMatch(ref b);
        
        public IdsMatch RosDeserialize(ref ReadBuffer2 b) => new IdsMatch(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Id1);
            b.Serialize(Id1Type);
            b.Serialize(Id2);
            b.Serialize(Id2Type);
            b.Serialize(Confidence);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Id1);
            b.Serialize(Id1Type);
            b.Align4();
            b.Serialize(Id2);
            b.Serialize(Id2Type);
            b.Align4();
            b.Serialize(Confidence);
        }
        
        public void RosValidate()
        {
            if (Id1 is null) BuiltIns.ThrowNullReference(nameof(Id1));
            if (Id2 is null) BuiltIns.ThrowNullReference(nameof(Id2));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 14;
                size += WriteBuffer.GetStringSize(Id1);
                size += WriteBuffer.GetStringSize(Id2);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Id1);
            size += 1; // Id1Type
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Id2);
            size += 1; // Id2Type
            size = WriteBuffer2.Align4(size);
            size += 4; // Confidence
            return size;
        }
    
        public const string MessageType = "hri_msgs/IdsMatch";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "47ee5557c84afd004bec4ac7f5fa56f7";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE41VS28bNxC+61cMYBRrBar1coNAgA5p7QC+2EGdFsjJ4i5ntUS4pMDhWhGK/vfMkLuy" +
                "JCBFb+KS/OZ7zFBX8KUxBC0SqS0CusprJNh5IlNahFbFquEPJcY9ooMYlCODLsLDHcF1rSqcQOn1YTK6" +
                "gldvKhyDchp2GFrljud4Sd6NJ7A3sYHYoAlQ+RCQdt5p47a8crXRXB/B4ivaG8Z77qpmoEZAje+sZiaw" +
                "60prqEEN3gkYTJuOq9G04tJGq4gvA+/od6ZiLEY7qbBew+xmBo2idJ12WBlluZRywsXXK/7uCSHufRKg" +
                "AsI75+M7UESeD0fUwjCZx1WFVcfnax/AOIpKqpia+SGjpSIPd3CN2wkoENPA6DHwXcaEV2WNZtcOrQ/I" +
                "oMnVtMNuV9/wuDdOOhKsPcDG6PlGMAjjJJkuX17iYYfpc+b7+f7P56fHiVBwK74Nv/JRKJTzgtlRATkb" +
                "uVEFFGGcUhLPjPfG2twDSYMQm0ra0xQ1n8iIsVGxxykINtMj+gaoK1MGGapPLoGx7A6hiKHDIsOQaY1V" +
                "gUODgNtOfmZQEttO8stX2VhJUVKjTIFhBelYftCmPWar8buhONgJ1+yVqZRlM0uslAQoxM4aU/AuZfe5" +
                "EQ/EtEEVLgJ69BEzH04mpobq81psgBvk8iunmMbiGJ/0Z85tw3DSe6XnfeNSp+RuvrqC+++q3VmktP50" +
                "0nnHJgtY+a0z0bAHrdedDLTZNvEsh9pb6/dCqB81RuMEDMuv1KAkwRVqieVtkeamlNegxydu0aTg/fKX" +
                "05gUUxsiKMrlXOlilcj+w0pXA9wEBt0r+PTxj3tZL1bDhbTqd4defishHT27eb+cwL8J+CPn0mDgnJM1" +
                "Kzai151UaIxYxeymKKI0NvQNLUax6MwbxXjz2exMUR6F42PYpBfiTFNd3y5+runDbzM9P9P0+9Pd10tF" +
                "85vZIKjPWHr9YmyF/2oI+iUr8+EFrg3nL+3JxvNz1EqwcobBTtphnF6t/2yD3AQCkR/JA0Vss4/KyTNF" +
                "noHD27SNh6yP/ZFZof5fDp14IHMtfyjiwci4+AH+eny+/7Ke5UXug/U8r+T2epF/i5vrZf7999MDb9yO" +
                "RhSDyOJ6eWMo/LaxGDZyKqNRbb2Ky8UJp9HoB1rtiJkrBwAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<IdsMatch> CreateSerializer() => new Serializer();
        public Deserializer<IdsMatch> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<IdsMatch>
        {
            public override void RosSerialize(IdsMatch msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(IdsMatch msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(IdsMatch msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(IdsMatch msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(IdsMatch msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<IdsMatch>
        {
            public override void RosDeserialize(ref ReadBuffer b, out IdsMatch msg) => msg = new IdsMatch(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out IdsMatch msg) => msg = new IdsMatch(ref b);
        }
    }
}
