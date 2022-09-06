/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class ChannelFloat32 : IDeserializable<ChannelFloat32>, IHasSerializer<ChannelFloat32>, IMessage
    {
        // This message is used by the PointCloud message to hold optional data
        // associated with each point in the cloud. The length of the values
        // array should be the same as the length of the points array in the
        // PointCloud, and each value should be associated with the corresponding
        // point.
        // Channel names in existing practice include:
        //   "u", "v" - row and column (respectively) in the left stereo image.
        //              This is opposite to usual conventions but remains for
        //              historical reasons. The newer PointCloud2 message has no
        //              such problem.
        //   "rgb" - For point clouds produced by color stereo cameras. uint8
        //           (R,G,B) values packed into the least significant 24 bits,
        //           in order.
        //   "intensity" - laser or pixel intensity.
        //   "distance"
        // The channel name should give semantics of the channel (e.g.
        // "intensity" instead of "value").
        [DataMember (Name = "name")] public string Name;
        // The values array should be 1-1 with the elements of the associated
        // PointCloud.
        [DataMember (Name = "values")] public float[] Values;
    
        public ChannelFloat32()
        {
            Name = "";
            Values = EmptyArray<float>.Value;
        }
        
        public ChannelFloat32(string Name, float[] Values)
        {
            this.Name = Name;
            this.Values = Values;
        }
        
        public ChannelFloat32(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<float>.Value
                    : new float[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 4);
                }
                Values = array;
            }
        }
        
        public ChannelFloat32(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out Name);
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<float>.Value
                    : new float[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 4);
                }
                Values = array;
            }
        }
        
        public ChannelFloat32 RosDeserialize(ref ReadBuffer b) => new ChannelFloat32(ref b);
        
        public ChannelFloat32 RosDeserialize(ref ReadBuffer2 b) => new ChannelFloat32(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.SerializeStructArray(Values);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Name);
            b.SerializeStructArray(Values);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Values is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += WriteBuffer.GetStringSize(Name);
                size += 4 * Values.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Name);
            size = WriteBuffer2.Align4(size);
            size += 4; // Values.Length
            size += 4 * Values.Length;
            return size;
        }
    
        public const string MessageType = "sensor_msgs/ChannelFloat32";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "3d40139cdd33dfedcb71ffeeeb42ae7f";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE11Ty47bMAy8+ysI7yUBsgY27aHosQu016LoreiBlhhbqCwaopRs/r6UYudl+CBA5Mxw" +
                "hnqB36MTmEgEBwI9ZiEL/RnSSPCTXUjvnrO9ViSGkb0FnpPjgB4sJmxeAEXYOEzafHJpBEIzwlz6wYUK" +
                "ZgpOp3wEnsKgNXyoF0f0maRgxIhnkJGzEvRULwUnUvB6fmyr4LI0XTgU4yZ5BxjsRUdluAN+FlvlcYwk" +
                "MwfrwqBAFb5r9PQ+YgjkIagUKUz04SRpFcwRTXJGfQvGZ0tftRqgze0O2mMLrxD5VFUY9nkKsCkMpC1H" +
                "8uftaoynQwJJFInBTWpyV2HuvpqR/jzPLC7VFLJkdd9wOFIoUQj0OUGkCZ2eDxyfQRQjcXRGuyKhaMcl" +
                "jEAnine+7a9Zj+p74GccySXZyL2n6aK0jUNfpv3OcYm8Zi2lymZz2Se1QK+XMY1aGVEFZK3+8sCw+bX7" +
                "sfu2XbYCZjT/FEDLeDELRd1yQ3AHnUW59p+hd0l2DyhqLUdLcRGo7RTUuXOR6VF04KLVfWis17ul1qpP" +
                "GAy1JftikLnLf12iQSMEUbPVeyPrSq6VG+qGgnbPq6kkQltK2zpbu+0aSbHsUUFe2Za5n9/C2+vbbVdJ" +
                "raey/AvvbZ8fXkDXHDxj+rT/83d9ZM1/XIL8vPEDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<ChannelFloat32> CreateSerializer() => new Serializer();
        public Deserializer<ChannelFloat32> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<ChannelFloat32>
        {
            public override void RosSerialize(ChannelFloat32 msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(ChannelFloat32 msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(ChannelFloat32 msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(ChannelFloat32 msg) => msg.Ros2MessageLength;
            public override void RosValidate(ChannelFloat32 msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<ChannelFloat32>
        {
            public override void RosDeserialize(ref ReadBuffer b, out ChannelFloat32 msg) => msg = new ChannelFloat32(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out ChannelFloat32 msg) => msg = new ChannelFloat32(ref b);
        }
    }
}
