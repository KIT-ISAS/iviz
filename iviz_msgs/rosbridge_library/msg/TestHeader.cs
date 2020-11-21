/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = "rosbridge_library/TestHeader")]
    public sealed class TestHeader : IDeserializable<TestHeader>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestHeader()
        {
            Header = new StdMsgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestHeader(StdMsgs.Header Header)
        {
            this.Header = Header;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestHeader(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestHeader(ref b);
        }
        
        TestHeader IDeserializable<TestHeader>.RosDeserialize(ref Buffer b)
        {
            return new TestHeader(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosbridge_library/TestHeader";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d7be0bb39af8fb9129d5a76e6b63a290";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq2RS0vFQAyF94X+h4ALH3AVdHfBnfhYCILuL7kzsQ1MZ+okvdp/b9Lrc+fC0jJM55wv" +
                "ZxLRuBmkk7NbwkgV+mVpm8t/ftrm/vFmDfK7XNscwKNijlgjDKQYURGei+Xgrqe6SrSjZC4cRoqwnOo8" +
                "kpy686lnAXs7ylQxpRkmMZUWCGUYpswBlUB5oF8At3IGhBGrcpgSVjOUGjm7/rniQAvfP6GXiXIguLta" +
                "myoLhUnZQs3GCJVQOHd2aOKJs16cu8OMT69lZXvqrKdfCUB7VE9Mb2Ml8bAoay9zsr/jqeGtSWSFosDR" +
                "8m9jWzkGq2MpaCyhhyOL/zBrX7IRCXZYGbeJnBysD4Y9dNPh8U+0R19Dxlw++Xvkd5G/cJ3yAfZrrXob" +
                "XvIWyNRZH0051rLjaNrtvFBCYsoKibcV69w2btsXNci1N9tk5ltmYyuKlMA2iQivrH3biFYvsMxlw7Ft" +
                "2uYdI3o4BbYCAAA=";
                
    }
}
