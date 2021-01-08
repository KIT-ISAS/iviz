/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/UInt8MultiArray")]
    public sealed class UInt8MultiArray : IDeserializable<UInt8MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout { get; set; } // specification of data layout
        [DataMember (Name = "data")] public byte[] Data { get; set; } // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public UInt8MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<byte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public UInt8MultiArray(MultiArrayLayout Layout, byte[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public UInt8MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new UInt8MultiArray(ref b);
        }
        
        UInt8MultiArray IDeserializable<UInt8MultiArray>.RosDeserialize(ref Buffer b)
        {
            return new UInt8MultiArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data, 0);
        }
        
        public void RosValidate()
        {
            if (Layout is null) throw new System.NullReferenceException(nameof(Layout));
            Layout.RosValidate();
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Layout.RosMessageLength;
                size += 1 * Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/UInt8MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "82373f1612381bb6ee473b5cd6f5d89c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWsbMRC9G/wfBu/FcW3XHyEkAR8MgV5SKKRQijFBWc16Ze+ujKSNm/76Pmk/vE56" +
                "LBVrdj2jmXnvaUYRfctYWKZM6wMJRy5l+lpmTq2NEW+P4k2XjnK2VuyYJCeqUE7pghJt+r2IpI7LnAsn" +
                "ghGPyDLKfbzw8Xba7/V7H/JRVr+rFZE9cqwSFddpEpLCiXpXv1eqwt1uts12rOBuV0ShWBPnS/Z7q3+8" +
                "QOPpyz1ZJ59zu7Of35PyanyHeGfykCvOhGFLgnZcsFFx5Z1IBc0sqIrsDB24IzoK41RcIqzi6N6OPCV6" +
                "aAKQyzBpI9mwpMTonFCbDeXaBghOkyqK2vBO/TYLtAQE6LZudWtcdDT6yADBthJ+uQhInnWSWO4c2VFI" +
                "qYodcca+AQDMeTyF6x4DKsQxmkcbSzbVZSZp/fhj/fOJXphORjnHBfASGOT2Eod1Rkn2KUQhmwYB50B3" +
                "4tl1NifKBLYR+d/5CIZqvB8frmgVEG26RD758Oeqyma+HalLy2I72sNy2CJh4AFAACKMHNNyEqcCImd0" +
                "cz37dX07I5X78Tgpl4IN8GGmXoE11pk2VG+GnhGdggYgfyYk7H1dA+U3s+00Ey9IDcyDlNUudYOOz6rf" +
                "DPlXhKpdcwAN83IESCMPaUV3i/nNbEY0LLTjemctKylL+xIShnzQPRC4ajLOuyBOSrp00HG1GFCqa77A" +
                "gPf8btH6F92MtSCDjrPNuewa24y1QB8P1nDC6C10vb+4vP5Gn8a0xwfEL/NiHNrn4P9XVaf/93J4aHq0" +
                "3/N0MDC1Dhih6gvq79Qr5qBt53bwalX8BVmf07udNPTDgzuCStzLFgfYRFbS+cjq629V/gD5wdmK/QUA" +
                "AA==";
                
    }
}
