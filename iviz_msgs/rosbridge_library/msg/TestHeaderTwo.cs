/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = "rosbridge_library/TestHeaderTwo")]
    public sealed class TestHeaderTwo : IDeserializable<TestHeaderTwo>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestHeaderTwo()
        {
            Header = new StdMsgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestHeaderTwo(StdMsgs.Header Header)
        {
            this.Header = Header;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestHeaderTwo(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestHeaderTwo(ref b);
        }
        
        TestHeaderTwo IDeserializable<TestHeaderTwo>.RosDeserialize(ref Buffer b)
        {
            return new TestHeaderTwo(ref b);
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
        [Preserve] public const string RosMessageType = "rosbridge_library/TestHeaderTwo";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d7be0bb39af8fb9129d5a76e6b63a290";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq2RS0vFQAyF94X+h4ALH3AVdHfBnfhYCILuL7kzsQ1MZ+okvdp/b9Lrc+fC0jJM55wv" +
                "Z5JbwkgV+mVpm8t/ftrm/vFmDaJxM0gnZ7cfdQ7gUTFHrBEGUoyoCM/FcnDXU10l2lEyFw4jRVhOdR5J" +
                "Tt351LOAvR1lqpjSDJOYSguEMgxT5oBKoDzQL4BbOQPCiFU5TAmrGUqNnF3/XHGghe+f0MtEORDcXa1N" +
                "lYXCpGyhZmOESiicOzs08cRZL87dYcan17KyPXXW068EoD2qJ6a3sZJ4WJS1lznZ3/HU8NYkskJR4Gj5" +
                "t7GtHIPVsRQ0ltDDkcV/mLUv2YgEO6yM20RODtYHwx666fD4J9qjryFjLp/8PfK7yF+4TvkA+7VWvQ0v" +
                "eQtk6qyPphxr2XE07XZeKCExZYXE24p1bhu37Ysa5NqbbTLzLbOxFUVKYJtEhFfWvm1EqxdY5rLh2DZt" +
                "8w4wIwkSrQIAAA==";
                
    }
}
