/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/Int64MultiArray")]
    public sealed class Int64MultiArray : IDeserializable<Int64MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout { get; set; } // specification of data layout
        [DataMember (Name = "data")] public long[] Data { get; set; } // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public Int64MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<long>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Int64MultiArray(MultiArrayLayout Layout, long[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Int64MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<long>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Int64MultiArray(ref b);
        }
        
        Int64MultiArray IDeserializable<Int64MultiArray>.RosDeserialize(ref Buffer b)
        {
            return new Int64MultiArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data, 0);
        }
        
        public void Dispose()
        {
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
                size += 8 * Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Int64MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "54865aa6c65be0448113a2afc6a49270";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR9N+Q/XJKXNkuzfJSyFvIQKOylhUEHZYRQVOs6VmJbQZKbdb9+R/Jn2tcx" +
                "YbB9P885utKIfmQsLFOm9YGEI5cyPZaZU2tjxPuDeNelo5ytFTsmyYkqlFO6oESbaERSx2XOhRPBhkdk" +
                "GeU+Xfh0O42iT8Uoq9/VGpE9cqwSFddFEpLCiToqUoW7ud5sm2is4G3XiEKnJi2KBtHqH69B9Pj0/Y6s" +
                "ky+53dmvHxkNIMRPyNbxhlBxJgxbErTjgo2KK++VVJDLgqfIOuACBY7COBWXyKoIuvcjT4num3iUMkza" +
                "SDYsKTE6J7RmQ7m2DvlOkyqK+v9M9rYEVER7KLZuFWtcdDT6yEDANioh+XIRULzoJLHc26qjkFIVO+KM" +
                "/bYDlPNYCtfpj/JxjHnRxpJNdZlJWj88r3890SvTySjnuABUAvbcnoOwzijJqCAK2UwFyAaeV55XLzZR" +
                "xvMcEZ5O+As12U8Ol7QKYDZ9Dl988kvVYjPfjtW5ZbEd72E5bKORpwAsACGMnNDyKk4FpM3o5nr2+/rb" +
                "jFTuD8NJuRREgA0n6A04Y51pQ3WwRZVTYA/aHRdh70IDdN7MttNMvKIu4A5TVrvUDTuXVX8Ymq8IHXvW" +
                "gBbW5Rhoxh7Nim4X85vZjOii0I7ryFpMUpb2JZQL5aB2wH5ZF5z3EZyUdOmw87QA0KhnPQOA9/x20bgX" +
                "/XK1DsPO1xZc9mxtuSDL5500nDAmCePtbyYvudGnCe3xAb3LvJiEaTn4/6rj9L9eAffNRA4izwVno5YA" +
                "p6X6gug79Yahb4e3OWK1IP4KrHfnQyBd+IOCm4BKXLv2sk2sVPOJ1dfn1EH0Fzlf0RfbBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
