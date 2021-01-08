/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/QuaternionStamped")]
    public sealed class QuaternionStamped : IDeserializable<QuaternionStamped>, IMessage
    {
        // This represents an orientation with reference coordinate frame and timestamp.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "quaternion")] public Quaternion Quaternion { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public QuaternionStamped()
        {
            Header = new StdMsgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public QuaternionStamped(StdMsgs.Header Header, in Quaternion Quaternion)
        {
            this.Header = Header;
            this.Quaternion = Quaternion;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public QuaternionStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Quaternion = new Quaternion(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new QuaternionStamped(ref b);
        }
        
        QuaternionStamped IDeserializable<QuaternionStamped>.RosDeserialize(ref Buffer b)
        {
            return new QuaternionStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Quaternion.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 32;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/QuaternionStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e57f1e547e0e1fd13504588ffc8334e2";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVTTYvcMAy9B/IfBHPY3cJsoS09DPRW+nEotOzeB42tSQyOnZWVmU1/fSWHmRRKoYdt" +
                "SLAcS09PT/IGHvtQgGlkKpSkACbIHNRECTnBOUivx0diSo7A5cw+JBSCI+NA6u5BwkBFcBjv26ZtvhB6" +
                "Yujr0jY/JnXmZFhPV9P8Przw0zbfHj7voIjfD6UrrxcebbOBB1GWyB4GEvQoCMesBEPXE28jnShCpU8e" +
                "6qnMIxWtZbOIo29HiRhjnGEq6iVZhRiGKQVnSlzrvwBYaEiAMCJLcFNE/kO5im9foaepavv14069UiE3" +
                "SVBSs2I4JiwhdXqozlNI8vaNRWjg4zlvdU+din1lANKjGGN6to4aWSw7S/NqqfFe4VUk0kS+wG39t9dt" +
                "uQPNoyxozK6HW6X/fZZeuyY9wQk54CGSITvVQWFvLOjm7ndoo76DhClf8BfINcm/4KYV2Mra9tq8aBKU" +
                "qVMd1XPkfApefQ9zRXHR5hViODDy3DYWtiRVkE91TMUaWXujK5aSXdBO+DrebVOELUHtyz74/zidHWUd" +
                "Qp6XEV3vxnXY/n4TVbkjkxY2omqqu/U62TwP9fIdY0Z5/w6eV1MFuZg/V/Ns3r8ASpQdOf4DAAA=";
                
    }
}
