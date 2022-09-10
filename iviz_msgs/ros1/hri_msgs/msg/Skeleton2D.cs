/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.HriMsgs
{
    [DataContract]
    public sealed class Skeleton2D : IHasSerializer<Skeleton2D>, IMessage
    {
        // This message contains a list of skeletal keypoints 
        // (0, 0) is at top-left corner of image
        /// <summary> Header timestamp should be acquisition time of the original image </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // skeletal key Points naming
        // Follows OpenPose coco model convention
        // https://github.com/CMU-Perceptual-Computing-Lab/openpose/blob/master/doc/output.md#pose-output-format-coco
        public const byte NOSE = 0;
        public const byte NECK = 1;
        public const byte RIGHT_SHOULDER = 2;
        public const byte RIGHT_ELBOW = 3;
        public const byte RIGHT_WRIST = 4;
        public const byte LEFT_SHOULDER = 5;
        public const byte LEFT_ELBOW = 6;
        public const byte LEFT_WRIST = 7;
        public const byte RIGHT_HIP = 8;
        public const byte RIGHT_KNEE = 9;
        public const byte RIGHT_ANKLE = 10;
        public const byte LEFT_HIP = 11;
        public const byte LEFT_KNEE = 12;
        public const byte LEFT_ANKLE = 13;
        public const byte LEFT_EYE = 14;
        public const byte RIGHT_EYE = 15;
        public const byte LEFT_EAR = 16;
        public const byte RIGHT_EAR = 17;
        [DataMember (Name = "skeleton")] public NormalizedPointOfInterest2D[] Skeleton;
    
        public Skeleton2D()
        {
            Skeleton = EmptyArray<NormalizedPointOfInterest2D>.Value;
        }
        
        public Skeleton2D(in StdMsgs.Header Header, NormalizedPointOfInterest2D[] Skeleton)
        {
            this.Header = Header;
            this.Skeleton = Skeleton;
        }
        
        public Skeleton2D(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<NormalizedPointOfInterest2D>.Value
                    : new NormalizedPointOfInterest2D[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new NormalizedPointOfInterest2D(ref b);
                }
                Skeleton = array;
            }
        }
        
        public Skeleton2D(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<NormalizedPointOfInterest2D>.Value
                    : new NormalizedPointOfInterest2D[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new NormalizedPointOfInterest2D(ref b);
                }
                Skeleton = array;
            }
        }
        
        public Skeleton2D RosDeserialize(ref ReadBuffer b) => new Skeleton2D(ref b);
        
        public Skeleton2D RosDeserialize(ref ReadBuffer2 b) => new Skeleton2D(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Skeleton.Length);
            foreach (var t in Skeleton)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Skeleton.Length);
            foreach (var t in Skeleton)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (Skeleton is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Skeleton.Length; i++)
            {
                if (Skeleton[i] is null) BuiltIns.ThrowNullReference(nameof(Skeleton), i);
                Skeleton[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += Header.RosMessageLength;
                size += 12 * Skeleton.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Skeleton.Length
            size += 12 * Skeleton.Length;
            return size;
        }
    
        public const string MessageType = "hri_msgs/Skeleton2D";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "efedc2dc59671380a1d9b497f0740be4";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VVTW/bOBC961cQ8KHOorKspJ8BeugmTmPEjY04RVEEQUBJY4koRaokFVf99X2kbNe6" +
                "LPawKxiQ+DjzZjh8Mx6x+0pYVpO1vCSWa+W4UJZxJoV1TG+Y/U6SHJfsO3WNFspZFo3YePqSTU8YXLlj" +
                "TjexpI2Du1FkvJeoQRdF18QLAFX/2j0jtoOdQFzH64bZSreyYBkxnv9ohRVOaBX2PZmr8DKiFApp7JhH" +
                "g8TYqs9M8VqoEptXWkq9tWzZkFpp60+Wa1brgqQ/5DMpHwGGlXONPU+SUriqzSa5rpOLz1/iFZmcGtdy" +
                "GV/oumkdaOMFzxINwgaESSZ1ltTcOjJJofNEtw5mk7oY+e24X8YbbWruYh89ilrk+I7dLtcz9oFN98vZ" +
                "xQ2W6W55N/90ff+0vl5+WVzO7rBxOtiYLf5efgV6NkC/3s3X90Bf7dDF7GrA8foY31O8OQb3DG8HvNfz" +
                "FbB3A+zmdubTfz8AP97eLDyaTo9Je/c0PcZ27unpMXhwPxsk+i1gr4YF6MHhiT76Q6ZvhoY9+DaKolt/" +
                "CVL8oiLIZLmZK9wapHd6+fC40xHEgOfDf/xEn9efzpl1xVNtS5v0uofq1o6rgpsCjed4wR1nEAqrRFmR" +
                "QSs9Q6WhMahgYdd1DdkJHEO34lcSGo1L2bHWwshpqLquWyVy7uhPY+394SkUmrrhxom8ldzAXpsCHQXz" +
                "jeE1eXbfVfSjJZUTm1+e+06xlEP8SKgDQ26IW3QCNlko9tmpd0BHP9xpmz5Go/utjoFTOWhvV2FKIGv6" +
                "2aDsPmFuzxHsr/6UEwRBlQjhCsvGAXvC0p4wREMu1Oi8YmMcYdW5yk8GTIRnbgTPJHniHKUA6wvv9OLk" +
                "iFkFasWV3tP3jH9i/BtadeD1Z4orXJ70ZbBtiUrCsDH6WRQwzbpAkkuBCYMZmhluuigMshAyGl35YsMI" +
                "XuFq/Ai1VucCN1GwLcZQZJ3x7OFankTxf8myMqKX5T/0x15yhz8GfzoMuH5AYzZDU94nzPydFxtDriIP" +
                "6vSqU/3MDuXztTkIDyc3mPhyyzuM7kMS8EBjtCDKSGoUAup+mE5eppNHL9I8lMxntEHJ/WX2DTPOyG2J" +
                "FJtOAkM6OTmuLEiCDFfLebSRmnvt/jx8dYevPIp+A43u5MoVBwAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Skeleton2D> CreateSerializer() => new Serializer();
        public Deserializer<Skeleton2D> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Skeleton2D>
        {
            public override void RosSerialize(Skeleton2D msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Skeleton2D msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Skeleton2D msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Skeleton2D msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Skeleton2D msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Skeleton2D>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Skeleton2D msg) => msg = new Skeleton2D(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Skeleton2D msg) => msg = new Skeleton2D(ref b);
        }
    }
}
